using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Mock
{
    public class MockData
    {
        private static Random mathRand = new Random(11);
        protected static int GetRandInt()
        {
            return mathRand.Next(25);
        }

        protected static int GetRandInt(int maxval)
        {
            return mathRand.Next(maxval);
        }

        protected static int GetRandInt(int minval, int maxval)
        {
            return mathRand.Next(minval, maxval);
        }

        protected static string GetRand()
        {
            return mathRand.Next(99).ToString();
        }
    }
}
