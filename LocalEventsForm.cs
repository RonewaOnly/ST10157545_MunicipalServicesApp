using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Displays upcoming local events and announcements, lets the user search
    /// them by category, date and keyword, and surfaces a small
    /// recommendation panel driven by what the user has searched for.
    /// See Models/EventRepository.cs for the data structures behind this
    /// screen (priority queue, sorted dictionary, hash table, sets, stack).
    /// </summary>
    public partial class LocalEventsForm : Form
    {
        private const string AllCategoriesOption = "All Categories";
        public LocalEventsForm()
        {
            InitializeComponent();
            PopulateCategoryFilter();
            dtpDate.Value = DateTime.Today;
            ShowUpcomingEvents();
            RefreshRecommended();
            RefreshRecentlyViewed();
        }
        private void PopulateCategoryFilter()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(AllCategoriesOption);
            foreach (string category in EventRepository.Categories)
            {
                cmbCategory.Items.Add(category);
            }
            cmbCategory.SelectedIndex = 0;
        }
        private void ShowUpcomingEvents()
        {
            List<Event> upcoming = EventRepository.GetUpcomingEvents();
            PopulateResults(upcoming);
            lblResultsCount.Text = string.Format("Showing all {0} upcoming events, soonest first.", upcoming.Count);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem as string;
            DateTime? selectedDate = chkFilterByDate.Checked ? (DateTime?)dtpDate.Value : null;
            string keyword = txtKeyword.Text;
            List<Event> results = EventRepository.Search(selectedCategory, selectedDate, keyword);
            PopulateResults(results);
            lblResultsCount.Text = results.Count == 0
                ? "No events match those filters - try widening your search."
                : string.Format("Found {0} matching event(s).", results.Count);
            RefreshRecommended();
        }
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
            chkFilterByDate.Checked = false;
            dtpDate.Value = DateTime.Today;
            txtKeyword.Clear();
            ShowUpcomingEvents();
        }
        private void PopulateResults(List<Event> events)
        {
            lvEvents.Items.Clear();
            foreach (Event ev in events)
            {
                ListViewItem item = new ListViewItem(ev.Date.ToString("ddd, dd MMM yyyy"));
                item.SubItems.Add(ev.Title);
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Location);
                item.Tag = ev;
                lvEvents.Items.Add(item);
            }
        }
        private void lvEvents_DoubleClick(object sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count == 0)
            {
                return;
            }
            Event selected = (Event)lvEvents.SelectedItems[0].Tag;
            MessageBox.Show(
                selected.Description + "\n\nLocation: " + selected.Location,
                selected.Title + " - " + selected.Date.ToString("ddd, dd MMM yyyy"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            EventRepository.MarkViewed(selected);
            RefreshRecentlyViewed();
            RefreshRecommended();
        }
        private void RefreshRecommended()
        {
            lstRecommended.Items.Clear();
            List<Event> recommendations = EventRepository.GetRecommendations(5);
            if (recommendations.Count == 0)
            {
                lstRecommended.Items.Add("Search by category to get personalised recommendations.");
                return;
            }
            foreach (Event ev in recommendations)
            {
                lstRecommended.Items.Add(ev.ToString());
            }
        }
        private void RefreshRecentlyViewed()
        {
            lstRecentlyViewed.Items.Clear();
            List<Event> viewed = EventRepository.GetRecentlyViewed(5);
            if (viewed.Count == 0)
            {
                lstRecentlyViewed.Items.Add("Double-click an event to view its details.");
                return;
            }
            foreach (Event ev in viewed)
            {
                lstRecentlyViewed.Items.Add(ev.ToString());
            }
        }
        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}