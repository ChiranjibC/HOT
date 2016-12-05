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
        public IEnumerable<BatchViewModel> GetBatchItems(int pageSize, int pageNo)
        {
            return GetBatchItems(pageSize);
        }

        public BatchViewModel UpdateBatch(BatchViewModel batch)
        {
            batch.BatchId = Guid.NewGuid();
            return batch;
        }

        private IEnumerable<BatchViewModel> GetBatchItems(int count)
        {
            var batchLists = new List<BatchViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetBatch());
            }
            return batchLists;
        }

        private BatchViewModel GetBatch()
        {
            var batch = new BatchViewModel()
            {
                BatchDesc = "Batch details_" + GetRand(),
                BatchId = Guid.NewGuid(),
                BatchNumber = "BatchNumber_" + GetRand(),
                Product = "DrugName_" + GetRand(),
                Quantity = GetRand(),

            };
            return batch;
        }
    }
}
