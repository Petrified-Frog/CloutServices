using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIot
{
    class Program
    {
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Net21IoTHub.azure-devices.net;DeviceId=WeatherDevice;SharedAccessKey=9x+a477hQi7appbqpoKXuf1zIZ5vJJifdAEW6FDuJlI=", TransportType.Mqtt);

        static async Task Main(string[] args)
        {
            var rand = new Random();
            var temp = rand.Next(-20, 20);
            double hum = rand.NextDouble();
            while (true)
            {
                temp = rand.Next(-20, 20);
                hum = Math.Round( rand.NextDouble(),2);
                var data = JsonConvert.SerializeObject(new { temperature = temp, humidity = hum });
                var message = new Message(Encoding.UTF8.GetBytes(data));            
                await deviceClient.SendEventAsync(message);
                Console.Write("Message sent: " + data + "\n");

                await Task.Delay(10 * 1000);
            }
        }
    }
}
