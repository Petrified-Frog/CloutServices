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
        static async Task Main(string[] args)
        {
            Guid deviceId = Guid.NewGuid();
            int temp = 60;
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

                var data = JsonConvert.SerializeObject(new { id = deviceId, temperature = temp, status = status });
                var message = new Message(Encoding.UTF8.GetBytes(data));
                await deviceClient.SendEventAsync(message);
                Console.Write("Message sent: " + data+"\n");

                await Task.Delay(10 * 1000);
            }
        }
    }
}
