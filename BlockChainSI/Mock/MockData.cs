using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Mock
{
    public class MockData
    {
        Random mathRand = new Random(11);
        protected int GetRandInt()
        {
            return mathRand.Next(25);
        }

        protected int GetRandInt(int maxval)
        {
            return mathRand.Next(maxval);
        }

        protected int GetRandInt(int minval, int maxval)
        {
            return mathRand.Next(minval, maxval);
        }

        protected string GetRand()
        {
            return mathRand.Next(99).ToString();
        }
    }
}
