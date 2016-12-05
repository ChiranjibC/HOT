using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IMapper
    {
        IEnumerable<MapperViewModel> GetMapList(int pageSize, int pageNo);
        MapperViewModel UpdateMapItem(MapperViewModel batch);
    }
}
