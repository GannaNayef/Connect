using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;

namespace N_puzzle
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int sz = 0;
            List<int> initialState = new List<int>();
            handlingIO IO = new handlingIO();
            IO.readFile(ref sz, ref initialState); //O(N)
            solve s = new solve(sz, initialState);
            if (s.isSolvable(initialState))
            {
                Console.WriteLine("Solvable");
                Console.WriteLine();
                Console.WriteLine(s.AStarUsingManhattan()); //O(E Log V)
            }
            else
            {
                Console.WriteLine("Unsolvable");
            }
        }
    }
}