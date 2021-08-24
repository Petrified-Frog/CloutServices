using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSimulator
{
    class Program
    {
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Net21IoTHub.azure-devices.net;DeviceId=PrinterSimulator;SharedAccessKey=GdAvC3k6MQbXl2iO6qD8kW7gCMhw/nyYu9VY5sncrXc=", TransportType.Mqtt);

        static async Task Main(string[] args)
        {
            //Guid deviceId = Guid.Parse("6502c8ed-bd22-45ca-b3cc-37d5e64cf574");
            bool jammed = false;
            string status = "Ok";
            var rand = new Random();
            while (true)
            {
                if (rand.Next(100) < 11)
                {
                    status = "Jammed";
                    jammed = true;
                }

                var data = JsonConvert.SerializeObject(new {status = status });
                Console.Write("Message sent: " + data + "\n");
                var message = new Message(Encoding.UTF8.GetBytes(data));
                message.Properties["DeviceGuid"] = "6502c8ed-bd22-45ca-b3cc-37d5e64cf574";
                await deviceClient.SendEventAsync(message);

                await Task.Delay(10 * 1000);
            }
        }
    }
}
