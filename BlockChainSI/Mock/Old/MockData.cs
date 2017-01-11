using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Mock
{
    public class MockData
    {
        private const string GENERIC_EXCEPTION_MSG = "Error calling {0} Block chain. Please try again in sometime or contact Administrator if error persists.";
        public T Sync<T>(Task<T> call)
        {
            return Task.Run(() => call).Result;
        }

        public void ThrowException(string method, string message = "") {
            var errorMsg = message;
            if (string.IsNullOrEmpty(errorMsg))
            {
                errorMsg = string.Format(GENERIC_EXCEPTION_MSG, method);
            }
            throw new OperationCanceledException(errorMsg);
        }

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
