using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    /**
     * Represents an AVL tree data structure.
     * An AVL tree is a self-balancing binary search tree where the difference in heights between the left and right subtrees of any node is at most one.
     * A self-balancing AVL tree keyed by a long (DateTime.Ticks of when the request was submitted).
     * ServiceRequestRepository uses this - rather than the plain BST - whenever the admin dashboard needs a guaranteed
     * O(log n) chronological listing even after thousands of requests have been logged in a single run, because an AVL tree's rotations keep
     * the tree height balanced instead of degrading to a linked list the way an
     * unbalanced BST would if tickets happened to arrive in date order.
     */

    public class AvlNode<TValue>
    {
        public long Key;

        public TValue Value;

        public AvlNode<TValue> Left;

        public AvlNode<TValue> Right;
        public int Height = 1;

        public AvlNode(long key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    public class AVLTree<TValue>
    {
        private AvlNode<TValue> _root;

        private static int Height(AvlNode<TValue> node)
        {
            return node == null ? 0 : node.Height;
        }

        private static int BalanceFactor(AvlNode<TValue> node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private static void RecalculateHeight(AvlNode<TValue> node)
        {
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private static AvlNode<TValue> RotateRight(AvlNode<TValue> y)
        {
            AvlNode<TValue> x = y.Right;
            AvlNode<TValue> t2 = y.Left;

            y.Left = x;
            x.Right = t2;

            RecalculateHeight(y);
            RecalculateHeight(x);
            return x;

        }

        private static AvlNode<TValue> RotateLeft(AvlNode<TValue> x)
        {
            AvlNode<TValue> y = x.Right;
            AvlNode<TValue> t2 = y.Left;

            y.Left = x;
            x.Left = t2;

            RecalculateHeight(x);
            RecalculateHeight(y);
            return y;
        }

        public void Insert(long key, TValue value)
        {
            _root = InsertRecursive(_root, key, value);
        }

        private AvlNode<TValue> InsertRecursive(AvlNode<TValue> node, long key, TValue value)
        {
            if(node == null)
            {
                return new AvlNode<TValue>(key, value);
            }

            if(key < node.Key)
            {
                node.Left = InsertRecursive(node.Left, key, value);
            }else if(key > node.Key){
                node.Right = InsertRecursive(node.Right, key, value);

            }
            else
            {
                //Duplicate submission timestamps (same tick) are vanishingly
                //unlikely in practice, but nudge the key so no request is lost
                node.Right = InsertRecursive(node.Right, key + 1, value);
            }

            RecalculateHeight(node);
            int balance = BalanceFactor(node);

            //Left-Left
            if(balance > 1 && key < node.Left.Key)
            {
                return RotateRight(node);
            }

            //Right-Right
            if(balance < -1 && key > node.Right.Key)
            {
                return RotateLeft(node);
            }

            //Left-Right
            if(balance > 1 && key > node.Left.Key)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            //Right-Left
            if(balance < -1 && key < node.Right.Key)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        /**
         <summary>In-order traversal yields values sorted chronologically, oldest first.</summary>
         */
        public List<TValue> InOrder()
        {
            List<TValue> result = new List<TValue>();
            InOrderVisit(_root, result);
            return result;
        }

        /// <summary>In-order traversal, newest first - the ordering the admin dashboard actually shows.</summary>
        public List<TValue> InOrderDescending()
        {
            List<TValue> result = new List<TValue>();
            ReverseInOrderVisit(_root, result);
            return result;
        }
        private void InOrderVisit(AvlNode<TValue> node, List<TValue> result)
        {
            if (node == null) return;
            InOrderVisit(node.Left, result);
            result.Add(node.Value);
            InOrderVisit(node.Right, result);
        }
        private void ReverseInOrderVisit(AvlNode<TValue> node, List<TValue> result)
        {
            if (node == null) return;
            ReverseInOrderVisit(node.Right, result);
            result.Add(node.Value);
            ReverseInOrderVisit(node.Left, result);
        }
        public int CurrentHeight
        {
            get { return Height(_root); }
        }
    }
}
