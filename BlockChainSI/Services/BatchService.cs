using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;
using BlockChainHot.Repository;
using AutoMapper;
using System.Web.Mvc;
using BlockChainSI.SIServices;
using BlockChainSI.Dao;

namespace BlockChainSI.Services
{
    public class BatchService : BaseService, IBatchService
    {
        private BlockChainHotDBContext dbContext;
        public BatchService()
        {
            dbContext = new BlockChainHotDBContext();
        }

        public IEnumerable<BatchViewModel> GetBatchItems(int pageSize, int pageNo)
        {
            return GetBatchItemsWithDetails();
        }

        private List<BatchViewModel> GetBatchItemsWithDetails()
        {
            var batchlist = (from batch in dbContext.Batch
                             join batchHistoryList in dbContext.BatchOwnershipHistory
                                 on batch.BatchCode equals batchHistoryList.BatchCode into batchHistTemp
                             from batchHistIfAny in batchHistTemp.DefaultIfEmpty()
                             join currentOwner in dbContext.OwnerManager
                                on batchHistIfAny.OwnerCode equals currentOwner.OwnerCode into currentOwnerTemp
                             from currentOwnerIfAny in currentOwnerTemp.DefaultIfEmpty()
                             join deviceOwner in dbContext.OwnerManager
                                on batch.TempLoggerCode equals deviceOwner.OwnerCode
                             join producer in dbContext.OwnerManager
                                on batch.ProducerCode equals producer.OwnerCode
                             where batchHistIfAny.EndTime == null
                             orderby batch.ExpiryStatus, batch.Description
                             select new BatchViewModel()
                            {
                                 CurrentOwner = new OwnerViewModel()
                                 {
                                     OwnerDesc = (currentOwnerIfAny != null ? currentOwnerIfAny.OwnerDesc : "NA"),
                                     OwnerCode = (currentOwnerIfAny != null ? currentOwnerIfAny.OwnerCode : "NA"),
                                 },
                                 CurrentOwnerCode = currentOwnerIfAny.OwnerCode,
                                 BatchCode = batch.BatchCode,
                                 Description = batch.Description,                                 
                                 TempLoggerCode = batch.TempLoggerCode,
                                TempLogger = new OwnerViewModel()
                                {
                                    OwnerDesc = deviceOwner.OwnerDesc,
                                    OwnerCode = deviceOwner.OwnerCode
                                },
                                ProducerCode = batch.ProducerCode,
                                Producer = new OwnerViewModel()
                                {
                                    OwnerDesc = producer.OwnerDesc,
                                    OwnerCode = producer.OwnerCode
                                },
                                IsExpired = (batch.ExpiryStatus ? "Yes" : "No"),
                             });
            var sql = batchlist.ToString();
            return batchlist.ToList();
        }

        public BatchViewModel GetDetails(string id)
        {
            var batch = dbContext.Batch.Where(x => x.BatchCode == id).FirstOrDefault();
            var batchModel = Mapper.Map<BatchViewModel>(batch);
            if (batchModel == null)
            {
                batchModel = new BatchViewModel();
            }
            var ownerMangerlist = GetOwnersList();
            var ownersList = ownerMangerlist.Where(x => x.IsTempLogger == false).ToList();
            var tempLoggerList = ownerMangerlist.Where(x => x.IsTempLogger == true).ToList();
            batchModel.OwnerList = new SelectList(ownersList, "OwnerCode", "OwnerDesc");
            batchModel.TempLoggerList  = new SelectList(tempLoggerList, "OwnerCode", "OwnerDesc");
            return batchModel;
        }
        
        public List<OwnerViewModel> GetOwnersList()
        {
            var ownerListDB = dbContext.OwnerManager.ToList();
            var ownerListModel = Mapper.Map<IList<OwnerManager>, List<OwnerViewModel>>(ownerListDB);
            return ownerListModel;
        }

        public BatchViewModel UpdateBatch(BatchViewModel batch)
        {
            try
            {                
                var batchdb = Mapper.Map<Batch>(batch);
                if (batchdb.Id == 0)
                {                    
                    batchdb.BatchCode = Guid.NewGuid().ToString();
                    dbContext.Batch.Add(batchdb);                    
                }
                else
                {
                    var updateBatch = dbContext.Batch.FirstOrDefault(x => x.Id == batchdb.Id);
                    updateBatch.Description = batchdb.Description;
                    updateBatch.ProducerCode = batchdb.ProducerCode;
                    updateBatch.TempLoggerCode = batchdb.TempLoggerCode;

                    dbContext.Entry(updateBatch);
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return batch;
        }
                
        private List<OwnerManager> GetOwnersListDB()
        {
            return dbContext.OwnerManager.ToList();
        }

        public bool RefreshDBFromBlockChain()
        {
            try
            {
                var batchList = dbContext.Batch.Where(x => x.ExpiryStatus == false).ToList();
                foreach (var batch in batchList)
                {
                    var batchService = new BatchSIService();
                    var batchDAO = batchService.GetBatchInfoSync(batch.Description);
                    //var batchDAOTask = batchService.GetBatchInfo(batch.Description);
                    //batchDAOTask.Wait();
                    //var batchDAO = batchDAOTask.Result;
                    if (batchDAO._expiryStatus)
                    {
                        batch.ExpiryStatus = true;
                        dbContext.Entry(batch);
                    }
                }
                dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }            
        }
    }
}
