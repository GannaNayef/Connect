using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace N_puzzle
{
    public class solve
    {
        // DURL
        private static int[]stateX = {1, -1, 0, 0};
        private static int[]stateY = {0, 0, 1, -1};
        private int N;
        private List<int> initialState;
        public solve(int n, List<int> state) //O(N)
        {
            N = n;
            initialState = new List<int>(state.ToList());
        }

        public bool isSolvable(List<int> iState)
        {
            int cnt = 0, pos = -1;
            for (int i = 0; i < iState.Count - 1; i++)
            {
                if (iState[i] == 0)
                {
                    continue;
                }

                for (int j = i + 1; j < iState.Count; j++)
                {
                    if (iState[j] != 0 && iState[j] < iState[i])
                    {
                        cnt++;
                    }

                    if (iState[j] == 0)
                    {
                        pos = j;
                    }
                }
            }
            if (N % 2 == 0)
            {
                if ((pos / N) % 2 == 0)
                {
                    return cnt % 2 != 0;
                }
                else
                {
                    return cnt % 2 == 0;
                }
            }
            else
            {
                return cnt % 2 == 0;
            }
        }
        private bool Valid(int x, int y) //O(1)
        {
            return x >= 0 && x < N && y >= 0 && y < N;
        }

        private void DisplayState(List<int> currState)
        {
            int cnt = 0;
            for (int i = 0; i < currState.Count; i++)
            {
                Console.Write(currState[i] + " ");
                cnt++;
                if (cnt == N)
                {
                    Console.WriteLine();
                    cnt = 0;
                }
            }
            Console.WriteLine();
        }
        private int Hamming(List<int> iState) //O(N)
        {
            int cnt = 0;
            for (int i = 0; i < iState.Count - 1; i++)
            {
                if (i + 1 != iState[i])
                {
                    cnt++;
                }
            }
            return cnt;
        }
        
        public int AStarUsingHamming() //O(E Log V)
        {
            Trie visited = new Trie();
            List<List<int>> all = new List<List<int>>();
            Priorty_Queue Q = new Priorty_Queue();
            all.Add(initialState);
            visited.Insert(initialState, 0);
            Q.Insert(new Pair(0, Hamming(initialState), all.Count - 1));
            while (Q.Size() > 0)
            {
                int curState = Q.Top().nodeIdx;
                int stps = Q.Top().F;
                Q.Pop();
                if (Hamming(all[curState]) == 0) return stps;
                int x = 0, y = 0;
                for (int i = 0; i < all[curState].Count; i++) //O(N)
                {
                    if (all[curState][i] == 0)
                    {
                        x = i / N;
                        y = i % N;
                        break;
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    int newX = x + stateX[k];
                    int newY = y + stateY[k];
                    if (Valid(newX, newY))
                    {
                        all.Add(new List<int>(all[curState].ToList()));
                        all[all.Count - 1][x * N + y] = all[curState][newX * N + newY];
                        all[all.Count - 1][newX * N + newY] = all[curState][x * N + y];
                        
                        int cst = visited.Find(all[all.Count - 1]); //O(N)
                        if (cst == -1 || cst > stps + 1)
                        {
                            Q.Insert(new Pair(stps + 1, Hamming(all[all.Count - 1]), all.Count - 1)); //O(Log V)
                            visited.Insert(all[all.Count - 1], stps + 1); //O(N)
                        }
                        else all.RemoveAt(all.Count - 1); //O(1)
                    }
                }
            }
            return -1;
        }
        
        private int Manhattan(List<int> iState)
        {
            int total = 0;
            for (int i = 0; i < iState.Count; i++)
            {
                total += (iState[i] != 0) ? Math.Abs((i / N) - ((iState[i] - 1) / N)) 
                                            + Math.Abs((i % N - ((iState[i] - 1) % N))) : 0;
                //Console.WriteLine(iState[i] + " " + i / N + " " + (iState[i] - 1) / N + " " + i % N + " " + (iState[i] - 1) % N);
            }
            return total;
        }
        public int AStarUsingManhattan() //O(E Log V)
        {
            Trie visited = new Trie();
            List<List<int>> all = new List<List<int>>();
            Priorty_Queue Q = new Priorty_Queue();
            all.Add(initialState);
            visited.Insert(initialState, 0);
            Q.Insert(new Pair(0, Manhattan(initialState), all.Count - 1));
            int cnt = 0;
            
            while (Q.Size() > 0)
            {
                cnt++;
                
                if (cnt == 50)
                {
                    break;
                }
                
                int curState = Q.Top().nodeIdx;
                int stps = Q.Top().F;
                Q.Pop();
                if (Manhattan(all[curState]) == 0) return stps;
                int x = 0, y = 0;
                
                for (int i = 0; i < all[curState].Count; i++) //O(N)
                {
                    if (all[curState][i] == 0)
                    {
                        x = i / N;
                        y = i % N;
                        Console.WriteLine(x + " " + y);
                        break;
                    }
                }
                
                for (int k = 0; k < 4; k++)
                {
                    int newX = x + stateX[k];
                    int newY = y + stateY[k];
                    
                    if (Valid(newX, newY))
                    {
                        all.Add(new List<int>(all[curState].ToList()));
                        
                        all[all.Count - 1][x * N + y] = all[curState][newX * N + newY];
                        all[all.Count - 1][newX * N + newY] = all[curState][x * N + y];
                        
                        int cst = visited.Find(all[all.Count - 1]); //O(N)
                    
                        if (cst == -1 || cst > stps + 1)
                        {
                            Q.Insert(new Pair(stps + 1, Manhattan(all[all.Count - 1]), all.Count - 1)); //O(Log V)
                            visited.Insert(all[all.Count - 1], stps + 1); //O(N)
                            DisplayState(all[all.Count - 1]);
                            //Console.WriteLine(stps + 1);
                        }
                        
                        else all.RemoveAt(all.Count - 1); //O(1)
                    }
                }
            }
            return -1;
        }
    }
}