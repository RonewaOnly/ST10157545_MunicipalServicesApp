using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    /**
     * <summary>
     * A classic , unbalance Binary Search Tree keyed by integer ticket number,
     * Used by ServiceRequestRepository to give citizens an O(log n) 
     * average-case lookup of "my ticket #1042" instead of a linear scan of
     * every issue ever reported, while still being simple to read and reason
     * about. Ticket numbers are assigned sequentially and never
     * change, so the tree never re-keyed after an Insert.
     * </summary>
     * **/

    public class BstNode<TValue>
    {
        public int Key;
        public TValue Value;
        public BstNode<TValue> Left;

        public BstNode<TValue> Right;

        public BstNode(int key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    public class BinarySearchTree<TValue>
    {
        private BstNode<TValue> _root;

        public void Insert(int key, TValue value)
        {
            _root = InsertRecursive(_root, key, value);
        }

        private BstNode<TValue> InsertRecursive(BstNode<TValue> node, int key, TValue value)
        {
            if (node == null)
            {
                return new BstNode<TValue>(key, value);
            }
            if (key < node.Key)
            {
                node.Left = InsertRecursive(node.Left, key, value);
            }
            else if (key > node.Key)
            {
                node.Right = InsertRecursive(node.Right, key, value);
            }
            else
            {
                node.Value = value;
            }
            return node;
        }

        public bool TryFind(int key, out TValue value)
        {
            BstNode<TValue> current = _root;
            while (current != null)
            {
                if (key == current.Key)
                {
                    value = current.Value;
                    return true;
                }
                current = key < current.Key ? current.Left : current.Right;
            }
            value = default(TValue);
            return false;
        }

        /**
         * <summary>In-order traversal yields values sorted by ticket number ascending</summary>
         * **/

        public List<TValue> InOrder()
        {
            List<TValue> result = new List<TValue>();
            InOrderVisit(_root, result);
            return result;
        }

        private void InOrderVisit(BstNode<TValue> node, List<TValue> result)
        {
            if (node == null) return;
            InOrderVisit(node.Left, result);
            result.Add(node.Value);
            InOrderVisit(node.Right, result);
        }
    }
}
