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
using BlockChainSI.Dao;
using BlockChainSI.SIServices;
using Nethereum.ABI.Encoders;

namespace BlockChainSI.Services
{
    public class OwnershipService : BaseService, IOwnershipService
    {
        private BlockChainHotDBContext dbContext;
        public OwnershipService()
        {
            dbContext = new BlockChainHotDBContext();
        }
        public BatchOwnershipHistoryViewModel GetDetails(string batchCode)
        {
            BatchOwnershipHistoryViewModel ownerShipDetails = null;
            if (!string.IsNullOrEmpty(batchCode))
            {
                ownerShipDetails = (from ownership in dbContext.BatchOwnershipHistory
                                    join batch in dbContext.Batch 
                                        on ownership.BatchCode equals batch.BatchCode
                                    join owner in dbContext.OwnerManager
                                        on ownership.OwnerCode equals owner.OwnerCode
                                    where ownership.EndTime == null && ownership.BatchCode == batchCode
                                    select new BatchOwnershipHistoryViewModel()
                                    {
                                        Batch = new BatchViewModel()
                                        {
                                            BatchCode = batch.BatchCode,
                                            Description = batch.Description,
                                        },
                                        BatchCode = ownership.BatchCode,
                                        Id = ownership.Id,
                                        EndTime = ownership.EndTime,
                                        Owner = new OwnerViewModel()
                                        {
                                            OwnerCode = owner.OwnerCode,
                                            OwnerDesc = owner.OwnerDesc,
                                        },
                                        OwnerCode = ownership.OwnerCode,
                                        StartTime = ownership.StartTime,
                                    }).FirstOrDefault();
            }
            if (ownerShipDetails == null)
            {
                ownerShipDetails = new BatchOwnershipHistoryViewModel();
            }
            ownerShipDetails.BatchList = GetBatchList();
            ownerShipDetails.OwnerList = GetOwnersList();
            return ownerShipDetails;
        }

        private SelectList GetBatchList()
        {
            var batchList = from batch in dbContext.Batch
                            join ownership in dbContext.BatchOwnershipHistory
                                on batch.BatchCode equals ownership.BatchCode into ownershipTemp
                            from ownershipIfAny in ownershipTemp.DefaultIfEmpty()
                            where ownershipIfAny.BatchCode == null
                            select batch;
            return new SelectList(batchList.ToList(), "BatchCode", "Description");
        }

        private SelectList GetOwnersList()
        {
            var ownersList = dbContext.OwnerManager.Where(x => x.IsTempLogger == false).ToList();
            return new SelectList(ownersList, "OwnerCode", "OwnerDesc");
        }


        public IList<BatchOwnershipHistoryViewModel> GetOwnerShipDetails(int pageSize, int pageNo)
        {
            var ownershiplist = (from ownership in dbContext.BatchOwnershipHistory
                                 join batch in dbContext.Batch on ownership.BatchCode equals batch.BatchCode
                                 join owner in dbContext.OwnerManager
                                     on ownership.OwnerCode equals owner.OwnerCode
                                 where ownership.EndTime == null
                                 select new BatchOwnershipHistoryViewModel()
                                 {
                                     Batch = new BatchViewModel()
                                     {
                                         BatchCode = batch.BatchCode,
                                         Description = batch.Description,
                                     },
                                     BatchCode = ownership.BatchCode,
                                     Id = ownership.Id,
                                     EndTime = ownership.EndTime,
                                     Owner = new OwnerViewModel()
                                     {
                                         OwnerCode = owner.OwnerCode,
                                         OwnerDesc = owner.OwnerDesc,
                                     },
                                     OwnerCode = ownership.OwnerCode,
                                     StartTime = ownership.StartTime,
                                 }).ToList();
            return ownershiplist;
        }

        public IList<BatchOwnershipHistoryViewModel> GetOwnerShipDetails(int pageSize, int pageNo, string batchCode)
        {
            var ownerShipDetails = dbContext.BatchOwnershipHistory.Where(x => x.EndTime == null && x.BatchCode == batchCode).ToList();
            return Mapper.Map<IList<BatchOwnershipHistory>, IList<BatchOwnershipHistoryViewModel>>(ownerShipDetails);
        }

        private BatchOwnershipHistory GetOwnershipItem(string batchCode)
        {
            return dbContext.BatchOwnershipHistory.Where(x => x.EndTime == null && x.BatchCode == batchCode).FirstOrDefault();
        }

        public string Update(BatchOwnershipHistoryViewModel ownerShipItem)
        {
            var status = string.Empty;
            try
            {
                var ownerShipItemdb = Mapper.Map<BatchOwnershipHistory>(ownerShipItem);
                var batch = dbContext.Batch.Where(x => x.BatchCode == ownerShipItemdb.BatchCode).FirstOrDefault();
                if (ownerShipItemdb.Id == 0)
                {
                    //Using the input record to create a new entry, and updating existing record endtime
                    ownerShipItemdb.StartTime = DateTime.UtcNow; //can be set from UI later
                    dbContext.BatchOwnershipHistory.Add(ownerShipItemdb);
                    //Make a call to create entry in Block Chain
                    var stabilityRanges = dbContext.StabilityRanges.Where(x => x.BatchCode == ownerShipItemdb.BatchCode).ToList();
                    status = InitiateBatchTracking(batch, ownerShipItemdb, stabilityRanges);
                }
                else
                {
                    //Using the input record to create a new entry, and updating existing record endtime
                    ownerShipItemdb.Id = 0;
                    ownerShipItemdb.StartTime = DateTime.UtcNow; //can be set from UI later
                    dbContext.BatchOwnershipHistory.Add(ownerShipItemdb);
                    var existingRecord = GetOwnershipItem(ownerShipItem.BatchCode);
                    if (existingRecord != null)
                    {
                        existingRecord.EndTime = ownerShipItemdb.StartTime;
                        dbContext.Entry(existingRecord);
                    }
                    //Make a call to change the associateDevice
                    status = AcknowledgeReceive(batch, ownerShipItemdb);
                }
                if (string.IsNullOrEmpty(status))
                {
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        private string AcknowledgeReceive(Batch batch, BatchOwnershipHistory batchOwnership)
        {
            var batchService = new BatchSIService();
            var batchDao = GetBatchDao(batch);
            batchDao.SenderAddress = batchOwnership.OwnerCode;
            var status = string.Empty;
            try
            {
                status = batchService.AcknowledgeReceive(batchDao);
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        private string InitiateBatchTracking(Batch batch, BatchOwnershipHistory batchOwnership, List<StabilityRange> stabilityRanges)
        {
            var batchService = new BatchSIService();
            var batchDao = GetBatchDao(batch, batchOwnership, stabilityRanges);            
            var status = string.Empty;
            try
            {
                status = batchService.InitiateBatchTracking(batchDao);
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        private BatchInput GetBatchDao(Batch batch, BatchOwnershipHistory batchOwnership, List<StabilityRange> stabilityRanges)
        {
            var newBatchItem = GetBatchDao(batch);
            newBatchItem.SenderAddress = batchOwnership.OwnerCode;

            var stabilityRangeCount = stabilityRanges.Count();
            newBatchItem._lstMinTemp = new short[stabilityRangeCount];
            newBatchItem._lstMaxTemp = new short[stabilityRangeCount];
            newBatchItem._lstExpireTickCount = new short[stabilityRangeCount];
            for (int i = 0; i < stabilityRangeCount; i++)
            {
                newBatchItem._lstMinTemp[i] = (short)stabilityRanges[i].MinTemp;
                newBatchItem._lstMaxTemp[i] = (short)stabilityRanges[i].MaxTemp;
                newBatchItem._lstExpireTickCount[i] = (short)stabilityRanges[i].ExpireTickCount;
            }
            return newBatchItem;
        }
        private BatchInput GetBatchDao(Batch batch)
        {
            var newBatchItem = new BatchInput();
            ITypeEncoder byte32Encoder = new Bytes32TypeEncoder();
            var batchCode = byte32Encoder.Encode(batch.Description);
            newBatchItem._batchId = batchCode;
            newBatchItem._deviceId = batch.TempLoggerCode;
            return newBatchItem;
        }        
    }
}
