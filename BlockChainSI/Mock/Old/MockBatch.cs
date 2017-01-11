using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;
using BlockChainHot.Repository;
using AutoMapper;

namespace BlockChainSI.Mock
{
    public class MockBatch : MockData, IBatch
    {
        private static List<BatchViewModel> _batchList = null;       

        public MockBatch()
        {
            
        }
        public static List<BatchViewModel> batchList
        {
            get
            {
                if (_batchList == null)
                {
                    _batchList = GetBatchItems(20).ToList();
                }
                return _batchList;
            }
        }

        public IEnumerable<BatchViewModel> GetBatchItems(int pageSize, int pageNo)
        {
            return batchList;
        }        

        public List<OwnerViewModel> GetOwnersList()
        {
            var owners = new List<OwnerViewModel>();
            var owner1 = new OwnerViewModel()
            {
                OwnerCode = Guid.NewGuid().ToString(),
                OwnerDesc = "NNTeam1"
            };
            owners.Add(owner1);
            var owner2 = new OwnerViewModel()
            {
                OwnerCode = Guid.NewGuid().ToString(),
                OwnerDesc = "FexTeam1"
            };
            owners.Add(owner2);
            var owner3 = new OwnerViewModel()
            {
                OwnerCode = Guid.NewGuid().ToString(),
                OwnerDesc = "RetailerTeam1"
            };
            owners.Add(owner3);

            var owner4 = new OwnerViewModel()
            {
                OwnerCode = Guid.NewGuid().ToString(),
                OwnerDesc = "Device4"
            };
            owners.Add(owner4);
            var owner5 = new OwnerViewModel()
            {
                OwnerCode = Guid.NewGuid().ToString(),
                OwnerDesc = "Device5"
            };
            owners.Add(owner5);

            return owners;
        }

        public BatchViewModel UpdateBatch(BatchViewModel batch)
        {
            if (string.IsNullOrEmpty(batch.BatchCode))
            {
                batch.BatchCode = Guid.NewGuid().ToString();
            }
            return batch;
        }
        
        public BatchViewModel GetDetails(string id)
        {
            return batchList.Where(x => x.BatchCode == id).FirstOrDefault();
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
                Description = "Batch details_" + GetRand(),
                BatchCode = Guid.NewGuid().ToString(),
                //BatchNumber = "BatchNumber_" + GetRand(),
                //Product = MockProduct.productList[GetRandInt(0, MockProduct.productList.Count -1)],
                //Quantity = GetRand(),
                CurrentOwner = new OwnerViewModel() { OwnerCode = Guid.NewGuid().ToString(), OwnerDesc = "Current Owner1" },
                TempLogger = new OwnerViewModel() { OwnerCode = Guid.NewGuid().ToString(), OwnerDesc = "Device1" },
                Producer = new OwnerViewModel() { OwnerCode = Guid.NewGuid().ToString(), OwnerDesc = "Producer1" },
                IsExpired = false.ToString(),
            };
            return batch;
        }

        public bool RefreshDBFromBlockChain()
        {
            throw new NotImplementedException();
        }
    }
}
