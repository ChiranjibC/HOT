using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using BlockChain.Common;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace BCDeviceMoc_Console
{
    class Program
    {
        static DeviceClient _deviceClient;
        static string iotHubEndPointUriKey = "iotHubEndPointUri"; // "BCHOT.azure-devices.net";
        static string deviceNameKey = "iotDeviceName"; //"myFirstDevice";
        static string deviceKeyConf = "iotDeviceKey"; // "whWBRFCoMXHMxT7VUICt6USs8OwvbLEP0ETBpWOerr4=";
        static string _deviceName = string.Empty;
        static string _deviceKey = string.Empty;
        //static RegistryManager registryManager;
        //static string connectionString = "HostName=BCHOT.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cfQpRzah51/5afF5JqwHwGuKO1uQXg3Ftl2U4ax6XNU=";

        static void Main(string[] args)
        {
            string iotHubEndPointUri = ConfigurationManager.AppSettings[iotHubEndPointUriKey] as string;
            _deviceName = ConfigurationManager.AppSettings[deviceNameKey] as string;
            _deviceKey = ConfigurationManager.AppSettings[deviceKeyConf] as string;

            Console.WriteLine("Simulated device\n");
            _deviceClient = DeviceClient.Create(iotHubEndPointUri, 
                                               new DeviceAuthenticationWithRegistrySymmetricKey(_deviceName, _deviceKey), 
                                               Microsoft.Azure.Devices.Client.TransportType.Http1);

            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            Random rand = new Random();
            int cnt = 0, maxCnt = 10;
            //while (true)
            while (cnt < maxCnt)
            {
                cnt++;
                Console.WriteLine("Starting to send Mock temp at: {0} Try Count: {1}", DateTime.Now, cnt);
                Console.WriteLine("_____________________________________________________________________");
                foreach (var batchItem in GetBCActiveBatchList())
                {

                    int currentTemperature = (rand.Next(-10, 10) * rand.Next(0, 4));

                    var telemetryDataPoint = new TempTelemetryJson()
                    {
                        AzureDeviceName = _deviceName,
                        AzureDeviceKey = _deviceKey,
                        batchId = batchItem,
                        temperature = currentTemperature
                    };
                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    await _deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                }
                Task.Delay(15 * 1000).Wait(); //repeat in 2 minute, if in while (true) loop
                
            }
        }
        
        private static IList<string> GetBCActiveBatchList()
        {
            try
            {
                var sendrequesturl = ConfigurationManager.AppSettings["BlockChainWebUrl"] as string;
                Console.WriteLine("Starting to get Active Batchlist, Url: {0}", sendrequesturl);

                var request = (HttpWebRequest)WebRequest.Create(sendrequesturl);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Response for Active Batchlist, Data: $${0}$$", responseString);
                var batchList = JsonConvert.DeserializeObject<IList<string>>(responseString);                
                return batchList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while getting Active Batchlist: {0}", ex);
                return new List<string>();
            }
        }

        //private static async Task AddDeviceAsync()
        //{
        //    registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        //    string deviceId = "myFirstDevice";
        //    Device device;
        //    try
        //    {
        //        device = await registryManager.AddDeviceAsync(new Device(deviceId));
        //    }
        //    catch (DeviceAlreadyExistsException)
        //    {
        //        device = await registryManager.GetDeviceAsync(deviceId);
        //    }
        //    Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        //}
    }
}
