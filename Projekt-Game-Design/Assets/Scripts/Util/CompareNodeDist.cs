using System.Collections;
using System.Collections.Generic;

namespace GDP01.Util
{
    public class CompareNodeDist : IComparer<PathNode>
    {
        public int Compare(PathNode n1, PathNode n2)
        {

            if (n1.dist > n2.dist)
                return 1;
            if (n1.dist < n2.dist)
                return -1;
            return 0;
        }
    }
}