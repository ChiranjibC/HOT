using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockMapper : MockData, IMapper
    {

        public IEnumerable<MapperViewModel> GetMapList(int pageSize, int pageNo)
        {
            return GetMapList(pageSize);
        }

        public MapperViewModel UpdateMapItem(MapperViewModel mapper)
        {
            mapper.MapId = Guid.NewGuid();
            return mapper;
        }

        public MapperViewModel GetDetails(Guid id)
        {
            return GetMapperItem(id);
        }

        private IEnumerable<MapperViewModel> GetMapList(int count)
        {
            var batchLists = new List<MapperViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetMapperItem(Guid.NewGuid()));
            }
            return batchLists;
        }

        private MapperViewModel GetMapperItem(Guid guid)
        {
            var batch = new MapperViewModel()
            {
                MapId = guid,
                DeviceNo = "DeviceNo_" + GetRand(),
                BatchNumber = "BatchNumber_" + GetRand(),
                StartTime = DateTime.Now.AddDays(-1 * GetRandInt()),
            };
            return batch;
        }

    }
}
