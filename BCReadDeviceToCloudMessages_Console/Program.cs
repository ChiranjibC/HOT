using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.Net;
using System.Configuration;
using System.IO;
using BlockChain.Common;
using Newtonsoft.Json;

namespace BCReadDeviceToCloudMessages_Console
{
    class Program
    {
        static string _connectionStringKey = "iotConnectionString"; //"HostName=BCHOT.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cfQpRzah51/5afF5JqwHwGuKO1uQXg3Ftl2U4ax6XNU=";
        static string _connectionString = string.Empty;
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        static void Main(string[] args)
        {
            _connectionString = ConfigurationManager.AppSettings[_connectionStringKey] as string;
            Console.WriteLine("Reading Messages from Cloud sent by Devices......");
            Console.WriteLine("Receive messages. Ctrl-C to exit.\n");
           eventHubClient = EventHubClient.CreateFromConnectionString(_connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            int cnt = 0, maxCnt = 10;
            //while (true)
            while (cnt < maxCnt)
            {
                cnt++;

                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null)
                {
                    Console.WriteLine("No Messages to be processed at: {0} Try Count:{1}", DateTime.Now, cnt);
                    Task.Delay(120 * 1000).Wait(); //repeat in 2 minute, if in while (true) loop
                    continue;
                }
                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                var responseData = await ProcessTelemetryData(data);
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
            }
        }

        private static async Task<string> ProcessTelemetryData(string data)
        {
            try
            {
                var sendrequesturl = ConfigurationManager.AppSettings["BlockChainWebUrl"] as string;
                var tempTelemetryJson = JsonConvert.DeserializeObject<TempTelemetryJson>(data);
                var formattedUrl = string.Format(sendrequesturl, tempTelemetryJson.batchId, tempTelemetryJson.temperature);

                var request = (HttpWebRequest)WebRequest.Create(formattedUrl);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }           
        }
        
    }
}
