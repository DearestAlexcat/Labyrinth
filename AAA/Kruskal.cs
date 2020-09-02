using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{

    class MComparer : IComparer<Kruskal>
    {
        public int Compare(Kruskal obj1, Kruskal obj2)
        {
            return (obj1.s == obj2.s) ? 0 : ((obj1.s < obj2.s) ? -1 : 1);
        }
    }

    class Kruskal
    {
        public int v1, v2, s;
        static public int[] Edge;

        public Kruskal()
        {  }

        public Kruskal(int v1, int v2, int s)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.s = s;
        }

        public static void FormLieder(int number_vertices)
        {
            int n = number_vertices;
            Edge = new int[n];
            for (int i = 0; i < n; i++)
                Edge[i] = i;
        }

        static int check(int v)
        {
            if (Edge[v] == v)
                return v;
            return Edge[v] = check(Edge[v]);
        }

        static void Swap(ref int x, ref int y)
        {
            Random rand = new Random();
            if (rand.Next() % 2 == 0)
            {
                int z;
                z = x;
                x = y;
                y = z;
            }
        }

        static public bool union(int x, int y)
        {
            x = check(x);
            y = check(y);
            if (x == y)
                return false;
            Swap(ref x, ref y);
            Edge[x] = y;
            return true;
        }
         
    }
}


  
