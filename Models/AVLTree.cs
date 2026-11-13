using System;
using System.Collections.Generic;

namespace ST10157545_MunicipalServicesApp.Models
{
    /// <summary>
    /// A self-balancing AVL tree keyed by a long (DateTime.Ticks of when the
    /// request was submitted). ServiceRequestRepository uses this - rather
    /// than the plain BST - whenever the admin dashboard needs a guaranteed
    /// O(log n) chronological listing even after thousands of requests have
    /// been logged in a single run, because an AVL tree's rotations keep the
    /// tree height balanced instead of degrading to a linked list the way an
    /// unbalanced BST would if tickets happened to arrive in date order.
    /// </summary>
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
            AvlNode<TValue> x = y.Left;
            AvlNode<TValue> t2 = x.Right;

            x.Right = y;
            y.Left = t2;

            RecalculateHeight(y);
            RecalculateHeight(x);
            return x;
        }

        private static AvlNode<TValue> RotateLeft(AvlNode<TValue> x)
        {
            AvlNode<TValue> y = x.Right;
            AvlNode<TValue> t2 = y.Left;

            y.Left = x;
            x.Right = t2;

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
            if (node == null)
            {
                return new AvlNode<TValue>(key, value);
            }

            // Keys equal to an existing key (two requests stamped with the same
            // tick, which a coarse system clock can produce) go to the right, so
            // the key itself is never altered and in-order traversal still
            // returns them in the order they were inserted.
            if (key < node.Key)
            {
                node.Left = InsertRecursive(node.Left, key, value);
            }
            else
            {
                node.Right = InsertRecursive(node.Right, key, value);
            }

            return Rebalance(node);
        }

        /// <summary>
        /// Restores the AVL property at <paramref name="node"/>. The rotation is
        /// chosen from the balance factor of the heavy child rather than by
        /// comparing keys, which stays correct when keys are duplicated.
        /// </summary>
        private static AvlNode<TValue> Rebalance(AvlNode<TValue> node)
        {
            RecalculateHeight(node);
            int balance = BalanceFactor(node);

            if (balance > 1)
            {
                // Left-Right case: left child leans right, so rotate it left first.
                if (BalanceFactor(node.Left) < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                return RotateRight(node);
            }

            if (balance < -1)
            {
                // Right-Left case: right child leans left, so rotate it right first.
                if (BalanceFactor(node.Right) > 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                return RotateLeft(node);
            }

            return node;
        }

        /// <summary>In-order traversal yields values sorted chronologically, oldest first.</summary>
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
