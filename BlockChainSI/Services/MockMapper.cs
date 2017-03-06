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
        private static IList<BatchViewModel> _batchList = MockBatch.batchList;
        private static IList<TempLoggerViewModel> _deviceList = MockTempLogger.deviceList;

        private static IList<MapperViewModel> _mapperList = null;

        public static List<MapperViewModel> mapperList
        {
            get
            {
                if (_mapperList == null)
                {
                    _mapperList = GetMapList(20).ToList();
                }
                return _mapperList.ToList();
            }
        }
        public IEnumerable<MapperViewModel> GetMapList(int pageSize, int pageNo)
        {
            return mapperList;
        }

        public MapperViewModel UpdateMapItem(MapperViewModel mapper)
        {
            mapper.MapId = Guid.NewGuid();
            return mapper;
        }

        public MapperViewModel GetDetails(Guid id)
        {
            return _mapperList.Where(x => x.MapId == id).FirstOrDefault();
        }

        private static IEnumerable<MapperViewModel> GetMapList(int count)
        {
            var batchLists = new List<MapperViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetMapperItem(Guid.NewGuid()));
            }
            return batchLists;
        }

        private static MapperViewModel GetMapperItem(Guid guid)
        {
            var batch = new MapperViewModel()
            {
                MapId = guid,
                TempLogger = _deviceList[GetRandInt(0, _deviceList.Count -1)],
                Batch = _batchList[GetRandInt(0, _batchList.Count - 1)],
                StartTime = DateTime.Now.AddDays(-1 * GetRandInt()),                
            };
            batch.EndTime = batch.StartTime.AddDays(3);
            return batch;
        }

    }
}
