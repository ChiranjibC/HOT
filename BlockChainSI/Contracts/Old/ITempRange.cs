using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface ITempRange
    {
        IEnumerable<TempRangeViewModel> GetTempRanges();
        TempRangeViewModel GetTempRange(Guid guid);
    }
}
