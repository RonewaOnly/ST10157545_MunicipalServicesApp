using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public enum RbColor { Red, Black }
    /// <summary>
    /// A standard left-leaning Red-Black Tree (CLRS insert + fix-up scheme)
    /// keyed by a composite long: (priority * 10,000,000) + ticketNumber. This
    /// keeps every request grouped by urgency (Critical..Low) while staying
    /// uniquely ordered within a priority band by submission order, and - the
    /// reason a Red-Black tree specifically was chosen over the plain BST
    /// above - guarantees O(log n) insertion and retrieval even under the
    /// worst case of many same-priority tickets arriving back-to-back, which
    /// is exactly the access pattern the admin "Requests by Priority" panel
    /// produces.
    /// </summary>
    public class RbNode<TValue>
    {
        public long Key;
        public TValue Value;
        public RbColor Color;
        public RbNode<TValue> Left;
        public RbNode<TValue> Right;
        public RbNode<TValue> Parent;
        public RbNode(long key, TValue value)
        {
            Key = key;
            Value = value;
            Color = RbColor.Red;
        }
    }
    public class RedBlackTree<TValue>
    {
        private RbNode<TValue> _root;
        private void RotateLeft(RbNode<TValue> x)
        {
            RbNode<TValue> y = x.Right;
            x.Right = y.Left;
            if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) _root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }
        private void RotateRight(RbNode<TValue> x)
        {
            RbNode<TValue> y = x.Left;
            x.Left = y.Right;
            if (y.Right != null) y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) _root = y;
            else if (x == x.Parent.Right) x.Parent.Right = y;
            else x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }
        public void Insert(long key, TValue value)
        {
            RbNode<TValue> newNode = new RbNode<TValue>(key, value);
            RbNode<TValue> parent = null;
            RbNode<TValue> current = _root;
            while (current != null)
            {
                parent = current;
                current = key < current.Key ? current.Left : current.Right;
            }
            newNode.Parent = parent;
            if (parent == null) _root = newNode;
            else if (key < parent.Key) parent.Left = newNode;
            else parent.Right = newNode;
            FixInsert(newNode);
        }
        private void FixInsert(RbNode<TValue> z)
        {
            while (z.Parent != null && z.Parent.Color == RbColor.Red)
            {
                RbNode<TValue> grandparent = z.Parent.Parent;
                if (grandparent == null) break;
                if (z.Parent == grandparent.Left)
                {
                    RbNode<TValue> uncle = grandparent.Right;
                    if (uncle != null && uncle.Color == RbColor.Red)
                    {
                        z.Parent.Color = RbColor.Black;
                        uncle.Color = RbColor.Black;
                        grandparent.Color = RbColor.Red;
                        z = grandparent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            RotateLeft(z);
                        }
                        z.Parent.Color = RbColor.Black;
                        grandparent.Color = RbColor.Red;
                        RotateRight(grandparent);
                    }
                }
                else
                {
                    RbNode<TValue> uncle = grandparent.Left;
                    if (uncle != null && uncle.Color == RbColor.Red)
                    {
                        z.Parent.Color = RbColor.Black;
                        uncle.Color = RbColor.Black;
                        grandparent.Color = RbColor.Red;
                        z = grandparent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RotateRight(z);
                        }
                        z.Parent.Color = RbColor.Black;
                        grandparent.Color = RbColor.Red;
                        RotateLeft(grandparent);
                    }
                }
            }
            _root.Color = RbColor.Black;
        }
        /// <summary>In-order traversal, descending key (most critical / most recent-in-band first).</summary>
        public List<TValue> InOrderDescending()
        {
            List<TValue> result = new List<TValue>();
            ReverseInOrderVisit(_root, result);
            return result;
        }
        private void ReverseInOrderVisit(RbNode<TValue> node, List<TValue> result)
        {
            if (node == null) return;
            ReverseInOrderVisit(node.Right, result);
            result.Add(node.Value);
            ReverseInOrderVisit(node.Left, result);
        }
    }
}
