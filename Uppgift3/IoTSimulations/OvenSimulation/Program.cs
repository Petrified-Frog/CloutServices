using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OvenSimulation
{
    class Program
    {        
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Net21IoTHub.azure-devices.net;DeviceId=OvenSimulation;SharedAccessKey=4tF/rKsV30GNbTOU5Hp7NDiyFYUVbx8KMbp2H2bi5bA=", TransportType.Mqtt);
        public static bool sending = true;
        static async Task Main(string[] args)
        {
            await deviceClient.SetMethodHandlerAsync("start", Start, null);
            await deviceClient.SetMethodHandlerAsync("stop", Stop, null);
           
            int temp = 60;
            string status = "Ok";
            var rand = new Random();
            while (true)
            {
                while (sending)
                {
                    if (rand.Next(0, 2) == 0)
                        temp++;
                    else
                        temp--;

                    if (temp > 80)
                        status = "Overheated";
                    else
                        status = "Ok";

                    var data = JsonConvert.SerializeObject(new { temperature = temp, status = status });
                    var message = new Message(Encoding.UTF8.GetBytes(data));
                    message.Properties["devicename"] = "ae1cf220-92ba-4d31-957e-dd5ef2ba9617";
                    await deviceClient.SendEventAsync(message);
                    Console.Write("Message sent: " + data + "\n");

                    await Task.Delay(10 * 1000);
                }
                await Task.Delay(500);
            }
        }

        private static Task<MethodResponse> Start(MethodRequest methodRequest, Object userContext)
        {
            sending = true;
            return Task.FromResult(new MethodResponse(new byte[0], 200));
        }
        private static Task<MethodResponse> Stop(MethodRequest methodRequest, Object userContext)
        {
            sending = false;
            return Task.FromResult(new MethodResponse(new byte[0], 200));
        }
    }
}
