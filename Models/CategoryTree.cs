using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    /**
     * <summary>
            A plain binary tree (Technical Requirement: "Basic Trees, Binary
            Trees") that models how the municipality groups its service
               categories into departments. Unlike the BST/AVL/Red-Black trees below,
            this tree is not ordered by a search key - it is a fixed hand-built
            hierarchy, which is exactly what a "basic tree" is used for: grouping
            related items for display rather than fast lookup.
                    All Categories
                    /            \
           Infrastructure     Community Services
            /         \          /         \
         Water     Roads &   Waste      Public Safety &
       & Elec.    Stormwater Mgmt         Recreation
        </summary>
     * **/

    public class CategoryTreeNode
    {
        public string Name { get; private set; }
        public CategoryTreeNode Left { get; set; }
        public CategoryTreeNode Right { get; set; }

        public CategoryTreeNode(string name)
        {
            Name = name;
        }
    }
    public static class CategoryTree
    {
        public static CategoryTreeNode Root { get; private set; }

        //<summary>Leaf category names, in the order citizens pick them from in Report Issues</summary>
        public static readonly string[] LeafCategories = 
        { 
            "Water & Sanitation",
            "Electricity",
            "Roads & Stormwater",
            "Waste Management",
            "Public Safety",
            "Parks & Recreation"
        
        };

        static CategoryTree()
        {
            CategoryTreeNode water = new CategoryTreeNode("Water & Sanitation");
            CategoryTreeNode electricity = new CategoryTreeNode("Electricity");
            CategoryTreeNode roads = new CategoryTreeNode("Roads & Stormwater");
            CategoryTreeNode waste = new CategoryTreeNode("Waste Management");
            CategoryTreeNode safety = new CategoryTreeNode("Public Safety");
            CategoryTreeNode parks = new CategoryTreeNode("Parks & Recreation");
            CategoryTreeNode infrastructure = new CategoryTreeNode("Infrastructure") { Left = water, Right = electricity };
            CategoryTreeNode infrastructure2 = new CategoryTreeNode("Roads & Waste") { Left = roads, Right = waste };
            CategoryTreeNode community = new CategoryTreeNode("Community Services") { Left = safety, Right = parks };
            CategoryTreeNode infrastructureBranch = new CategoryTreeNode("Infrastructure & Utilities") { Left = infrastructure, Right = infrastructure2 };
            Root = new CategoryTreeNode("All Categories") { Left = infrastructureBranch, Right = community };
        }

        //<summary> In-order trvera;" visits leaf categories left to right.</summary>
        public static List<string> InOrderLeaves()
        {
            List<string> result = new List<string>();
            InOrderVisit(Root, result);
            return result;
        }

        private static void InOrderVisit(CategoryTreeNode node, List<string> result)
        {
            if (node == null) return;
            InOrderVisit(node.Left, result);
            if (node.Left == null && node.Right == null)
            {
                result.Add(node.Name);
            }
            InOrderVisit(node.Right, result);
        }
        /// <summary>Pre-order traversal: department name first, then its children - used to print the department hierarchy.</summary>
        public static List<string> PreOrderHierarchy()
        {
            List<string> result = new List<string>();
            PreOrderVisit(Root, 0, result);
            return result;
        }
        private static void PreOrderVisit(CategoryTreeNode node, int depth, List<string> result)
        {
            if (node == null) return;
            result.Add(new string(' ', depth * 3) + node.Name);
            PreOrderVisit(node.Left, depth + 1, result);
            PreOrderVisit(node.Right, depth + 1, result);
        }



    }
}
