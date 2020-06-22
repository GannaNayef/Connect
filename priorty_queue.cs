
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.Hosting;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace N_puzzle
{
    public class Priorty_Queue
    {
        private int sz;
        private List<Pair> heap;

        public Priorty_Queue() //O(1)
        {
            sz = 0;
            heap = new List<Pair>();
        }
        private void PushUp(int idx) //O(Log N)
        {
            if (idx == 0) return;
            if (heap[idx / 2].F + heap[idx / 2].S > heap[idx].F + heap[idx].S)
            {
                Pair temp = heap[idx];
                heap[idx] = heap[idx / 2];
                heap[idx / 2] = temp;
                PushUp(idx / 2);
            }
            else return;
        }
        
        public void Insert(Pair val) //O(Log N)
        {
            sz++;
            heap.Add(val);
            PushUp(heap.Count - 1);
        }

        private  void PushDown(int idx) //O(Log N)
        {
            if (idx * 2 + 1 >= heap.Count)
            {
                heap[idx] = new Pair(1000, 1000, 0);
            }
            else if (idx * 2 + 2 >= heap.Count)
            {
                Pair temp = heap[idx];
                heap[idx] = heap[idx * 2 + 1];
                heap[idx * 2 + 1] = temp;
                PushDown(idx * 2 + 1);
            }
            else
            {
                if (heap[idx * 2 + 1].F + heap[idx * 2 + 1].S <= heap[idx * 2 + 2].F + heap[idx * 2 + 2].S)
                {
                    Pair temp = heap[idx];
                    heap[idx] = heap[idx * 2 + 1];
                    heap[idx * 2 + 1] = temp;
                    PushDown(idx * 2 + 1);
                }
                else
                {
                    Pair temp = heap[idx];
                    heap[idx] = heap[idx * 2 + 2];
                    heap[idx * 2 + 2] = temp;
                    PushDown(idx * 2 + 2);
                }
            }
        }
        
        public void Pop() //O(Log N)
        {
            if (sz > 0)
            {
                sz--;
                PushDown(0);
            }
        }

        public Pair Top() //O(1)
        {
            return heap[0];
        }

        public int Size() //O(1)
        {
            return sz;
        }
    }
}