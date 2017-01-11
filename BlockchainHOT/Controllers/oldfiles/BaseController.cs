using Nethereum.Web.Sample.Services;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class _BaseController : Controller
    {
        public BatchService GetBatchService()
        {
            var web3 = new Web3("http://echaind23.centralus.cloudapp.azure.com:8545");
            //var contractAddres = web3.GetAddressFromPrivateKey("4c45ba3387d00578b24c9b9be24b55678b165465934742b4440c930517452f3d");
            var service = new BatchService(web3, "0x297cf20061d5434212fdf7f768e9bda34550baf7");
            //var service = new BatchService(web3, contractAddres);
            return service;
        }

        public OwnerService GetOwnerService()
        {
            var web3 = new Web3("http://echaind23.centralus.cloudapp.azure.com:8545");
            //var contractAddres = web3.GetAddressFromPrivateKey("4c45ba3387d00578b24c9b9be24b55678b165465934742b4440c930517452f3d");
            var service = new OwnerService(web3, "0x64524f9652a362747c360a2eb4112be473628669");
            //var service = new BatchService(web3, contractAddres);
            return service;
        }

        public DaoService GetDaoService()
        {
            var web3 = new Web3("https://eth2.augur.net");
            var service = new DaoService(web3, "0xbb9bc244d798123fde783fcc1c72d3bb8c189413");
            return service;
        }
    }
}