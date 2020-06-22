using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization.Configuration;

namespace N_puzzle
{
    public class Trie
    {
        private Node root;
        public class Node 
        {
            public Node[] nxt;
            public int val;
            
            public Node(int v = -1) //O(1)
            {
                nxt = new Node[30];
                val = v;
            }
        }

        public Trie() //O(1)
        {
            root = new Node();
        }

        private void add(ref Node cur, int ind, ref List<int> vector, ref int v) //O(N)
        {
            if (cur.nxt[vector[ind]] == null)
            {
                cur.nxt[vector[ind]] = new Node();
            }
            if (ind == vector.Count - 1)
            {
                cur.nxt[vector[ind]].val = v;
                return;
            }
            add(ref cur.nxt[vector[ind]], ind+1, ref vector, ref v);
        }
        public void Insert(List<int> vector, int v) //O(N)
        {
            add(ref root, 0, ref vector, ref v);
        }

        public int Find(List<int> vector) //O(N)
        {
            Node temp = root;
            for (int i = 0; i < vector.Count; i++)
            {
                if (temp.nxt[vector[i]] == null) return -1;
                temp = temp.nxt[vector[i]];
            }
            return temp.val;
        }
    }
}