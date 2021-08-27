using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EngineSim
{
    class Program
    {
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Net21IoTHub.azure-devices.net;DeviceId=EngineSim;SharedAccessKey=mqugpyZjx+jCvLTHU8/1l6QSvfogq+YPC6bj1AKED74=", TransportType.Mqtt);
        static async Task Main(string[] args)
        {
            int temp = 78;
            string status = "Ok";
            var rand = new Random();
            while (true)
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
                message.Properties["devicename"] = "0a634438-8936-4604-a861-da8f69dd9a25";
                await deviceClient.SendEventAsync(message);
                Console.Write("Message sent: " + data + "\n");

                await Task.Delay(10 * 1000);
            }
        }
    }
}
