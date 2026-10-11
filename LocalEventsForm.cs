using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        private const string AllCategoriesOption = "All Categories";
        public LocalEventsForm()
        {
            InitializeComponent();
            PopulateCategoryFilter();
            dtpDate.Value = DateTime.Today;
            ShowUpcomingEvents();
            RefreshRecommanded();
            RefreshRecentlyViewed();
        }
        private void PopulateCategoryFilter()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(AllCategoriesOption);

            foreach(string category in EventRepository.Categories)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void ShowUpcomingEvents()
        {
            List<Event> upcoming = EventRepository.GetUpcomingEvents();
            PopluateResults(upcoming);
            lblResultsCount.Text = string.Format("Showing all {0} upcoming events, soonest first.", upcoming.Count);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem as string;
            DateTime? selectedDate = chkFilterByDate.Checked ? (DateTime?)dtpDate.Value : null;
            string keyword = txtKeyword.Text;

            List<Event> results = EventRepository.Search(selectedCategory, selectedDate, keyword);
            PopluateResults(results);

            lblResultsCount.Text = results.Count == 0 ? "No events match those filters -try windening your search" : string.Format("Found {0} matching event(s).", results.Count());

            RefreshRecommanded();
         }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
            chkFilterByDate.Checked = false;
            dtpDate.Value = DateTime.Today;
            txtKeyword.Clear();
            ShowUpcomingEvents();
        }

        private void PopluateResults(List<Event> events)
        {
            lvEvents.Items.Clear();

            foreach(Event ev in events)
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
            if(lvEvents.SelectedItems.Count == 0)
            {
                return;
            }

            Event selected = (Event)lvEvents.SelectedItems[0].Tag;

            MessageBox.Show(
                selected.Description + "\n\nLocation: "+ selected.Location,
                selected.Title + " - "+ selected.Date.ToString("ddd, dd MMM yyyy"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );

            EventRepository.MarkViewed(selected);
            RefreshRecentlyViewed();
            RefreshRecommanded();
        }

        private void RefreshRecommanded()
        {
            lstRecommended.Items.Clear();
            List<Event> recommendations = EventRepository.GetRecommendations(5);

            if(recommendations.Count == 0)
            {
                lstRecommended.Items.Add("Search by Category to get personalised recommendations");
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
            if(viewed.Count == 0)
            {
                lstRecentlyViewed.Items.Add("Double-click an event to view its details.");
                return;
            }

            foreach(Event ev in viewed)
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
