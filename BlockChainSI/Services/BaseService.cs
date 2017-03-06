using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Services
{
    public class BaseService
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
        
    }
}
