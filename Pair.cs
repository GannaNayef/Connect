namespace N_puzzle
{
    public struct Pair
    {
        public int F, S, nodeIdx;
        public Pair(int first = 0, int second = 0, int idx = 0) //O(1)
        {
            F = first;
            S = second;
            nodeIdx = idx;
        }
    }
}