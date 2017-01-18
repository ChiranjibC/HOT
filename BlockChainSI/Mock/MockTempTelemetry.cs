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
using BlockChainSI.Services;
using BlockChainSI.Dao;
using Nethereum.ABI.Encoders;
using System.Data;

namespace BlockChainSI.Mock
{
    public class MockTempTelemetry : MockData, ITempTelemetry
    {
        private BlockChainHotDBContext dbContext;
        public MockTempTelemetry()
        {
            dbContext = new BlockChainHotDBContext();
        }

        public TemparatureTelemetryViewModel Get()
        {
            var tempTeleModel = new TemparatureTelemetryViewModel();
            tempTeleModel.BatchList = GetValidBatchList();
            return tempTeleModel;
        }

        public TemparatureTelemetryListViewModel GetTelemetryHistory(int pageSize, int pageNo, string batchId)
        {
            var tempTelemetryListModel = new TemparatureTelemetryListViewModel();
            var telemetryList = (from tempTele in dbContext.TemparatureTelemetry
                                    join batch in dbContext.Batch
                                    on tempTele.BatchCode equals batch.BatchCode
                                    orderby tempTele.LogTime descending
                                    select new TemparatureTelemetryViewModel()
                                    {
                                        BatchCode = tempTele.BatchCode,
                                        BatchDescription = batch.Description,
                                        Id = tempTele.Id,
                                        LogTime = tempTele.LogTime,
                                        Temperature = tempTele.Temperature,
                                    }
                                    ).AsQueryable();                                 
            if (!string.IsNullOrEmpty(batchId))
            {
                telemetryList = telemetryList.Where(x => x.BatchCode == batchId);
            }
            tempTelemetryListModel.TempTelemetryList = telemetryList.ToList();
            tempTelemetryListModel.BatchList = GetValidBatchList();
            return tempTelemetryListModel;
        }

        private SelectList GetValidBatchList()
        {
            var batchList = (from batch in dbContext.Batch
                             where batch.ExpiryStatus == false
                             orderby batch.Description
                             select batch).ToList();
            return new SelectList(batchList, "BatchCode", "Description");
        }

        public string BulkUpdate(DataSet tempTelemetryBulkData, string batchCode)
        {
            var status = string.Empty;
            for (int i = 0; i < tempTelemetryBulkData.Tables[0].Rows.Count; i++)
            {
                var dr = tempTelemetryBulkData.Tables[0].Rows[i];
                var tempTelemetry = new TemparatureTelemetryViewModel()
                {
                    BatchCode = batchCode,
                    Temperature = Convert.ToInt32(dr[1]),
                    LogTime = Convert.ToDateTime(dr[2]), //ideally current server time is being used in system as logtime
                };
                status += Update(tempTelemetry);
            }
            return status;
        }

        public string Update(TemparatureTelemetryViewModel tempTelemetry)
        {
            var status = string.Empty;
            try
            {
                var tempTelemetrydb = Mapper.Map<TemparatureTelemetry>(tempTelemetry);
                if (tempTelemetrydb.Id == 0)
                {
                    //Using the input record to create a new entry, and updating existing record endtime
                    tempTelemetrydb.LogTime = DateTime.UtcNow; //can be set from UI later
                    dbContext.TemparatureTelemetry.Add(tempTelemetrydb);
                    var batch = dbContext.Batch.Where(x => x.BatchCode == tempTelemetrydb.BatchCode).FirstOrDefault();
                    status = InitiateTempTelemetry(batch, tempTelemetrydb);
                }
                else
                {
                    throw new ArgumentException("Editing TemparatureTelemetry data is not allowed.");
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

        private string InitiateTempTelemetry(Batch batch, TemparatureTelemetry tempTelemetrydb)
        {
            var status = string.Empty;
            var tempTelemetryService = new TemperatureTelemetryService();

            var tempTelemetryDao = GetTempTelemetryDao(batch, tempTelemetrydb);
            try
            {
                status = tempTelemetryService.InputTemparatureTelemetry(tempTelemetryDao);
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        private TempTelemetryInput GetTempTelemetryDao(Batch batch, TemparatureTelemetry tempTelemetrydb)
        {
            var tempTelemetryInput = new TempTelemetryInput();
            //Temperature logger address from where temperature details are sent
            tempTelemetryInput.SenderAddress = batch.TempLoggerCode;

            ITypeEncoder byte32Encoder = new Bytes32TypeEncoder();
            var batchCode = byte32Encoder.Encode(batch.Description);
            tempTelemetryInput._batchId = batchCode;
            tempTelemetryInput._temp = (short)tempTelemetrydb.Temperature;
            return tempTelemetryInput;
        }
    }
}
