using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LiftSimulator
{
    class Program
    {
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Net21IoTHub.azure-devices.net;DeviceId=LiftSimulator;SharedAccessKey=NRDYZi+UyuHWgPpWdGpeHR5VjytkG1XwDTT43Ei+ot0=", TransportType.Mqtt);
        
        
        static async Task Main(string[] args)
        {
            Guid deviceId = Guid.NewGuid();
            int lifting = 0;
            string status = "Ok";
            var rand = new Random();
            while (true)
            {
                if (rand.Next(100) > 70)
                {
                    lifting = rand.Next(100, 800);
                    if (lifting > 500)
                        status = "Overloaded";
                    else
                        status = "Ok";
                }
                else
                    lifting = 0;

                var data = JsonConvert.SerializeObject(new {id = deviceId, lifting = lifting, status = status });
                Console.Write("Message sent: " + data + "\n");
                var message = new Message(Encoding.UTF8.GetBytes(data));
                await deviceClient.SendEventAsync(message);

                await Task.Delay(10 * 1000);
            }
        }
    }
}
