using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;

namespace N_puzzle
{
    public class handlingIO
    {
        public void readFile(ref int sz, ref List<int> initialState)
        {
            String text = File.ReadAllText(@"/home/ganna/Desktop/N-puzzle/[MS2 Tests] N Puzzle/Complete Test/Solvable puzzles/Manhattan Only/15 Puzzle 1.txt") + " ";
            List<int> inputStream = new List<int>();
            int num = 0;
            Boolean f = false;
            for (int i = 0; i < text.Length; i++) //O(N)
            {
                if (text[i] >= '0' && text[i] <= '9')
                {
                    f = true;
                    num *= 10;
                    num += text[i] - '0';
                }
                else if (f)
                {
                    inputStream.Add(num);
                    num = 0;
                    f = false;
                }
            }
            List<int> state = new List<int>();
            for (int i = 1; i < inputStream.Count; i++) //O(N)
            {
                state.Add(inputStream[i]);
            }
            sz = inputStream[0];
            initialState = state;
            return;
        }
    }
}