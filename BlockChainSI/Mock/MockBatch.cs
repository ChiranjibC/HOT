using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockBatch : MockData, IBatch
    {
        private static List<BatchViewModel> _batchList = null;
        public static List<BatchViewModel> batchList
        {
            get
            {
                if (_batchList == null)
                {
                    _batchList = GetBatchItems(15).ToList();
                }
                return _batchList;
            }
        }

        public IEnumerable<BatchViewModel> GetBatchItems(int pageSize, int pageNo)
        {
            return batchList;
        }

        public BatchViewModel UpdateBatch(BatchViewModel batch)
        {
            batch.BatchId = Guid.NewGuid();
            return batch;
        }
        
        public BatchViewModel GetDetails(Guid id)
        {
            return batchList.Where(x => x.BatchId == id).FirstOrDefault();
        }

        private static List<BatchViewModel> GetBatchItems(int count)
        {
            var batchLists = new List<BatchViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetBatch());
            }
            return batchLists;
        }

        private static BatchViewModel GetBatch()
        {
            var batch = new BatchViewModel()
            {
                BatchDesc = "Batch details_" + GetRand(),
                BatchId = Guid.NewGuid(),
                BatchNumber = "BatchNumber_" + GetRand(),
                ProductId = MockProduct.productList[GetRandInt(0, MockProduct.productList.Count -1)].ProductId,
                Quantity = GetRand(),

            };
            return batch;
        }
    }
}
