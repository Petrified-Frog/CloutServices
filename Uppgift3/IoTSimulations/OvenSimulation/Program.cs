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
            //Guid deviceId = Guid.Parse("ae1cf220-92ba-4d31-957e-dd5ef2ba9617");
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

                var data = JsonConvert.SerializeObject(new {temperature = temp, status = status });
                var message = new Message(Encoding.UTF8.GetBytes(data));
                message.Properties["deviceType"] = "function app";
                message.Properties["DeviceGuid"] = "ae1cf220-92ba-4d31-957e-dd5ef2ba9617";
                await deviceClient.SendEventAsync(message);
                Console.Write("Message sent: " + data+"\n");

                await Task.Delay(10 * 1000);
            }
        }
    }
}
