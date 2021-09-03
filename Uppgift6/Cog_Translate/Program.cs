using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet

class Program
{
    private static readonly string subscriptionKey = "db406f76366647428aec08fb15660f6f";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    // Add your location, also known as region. The default is global.
    // This is required if using a Cognitive Services resource.
    private static readonly string location = "global";

    static async Task Main(string[] args)
    {
        // Input and output languages are defined as parameters.
        //string route = "/translate?api-version=3.0&from=en&to=de&to=it";
        // Output languages are defined as parameters, input language detected.
        //string route = "/translate?api-version=3.0&to=de&to=it";
        // Just detect language
        //string route = "/detect?api-version=3.0";
        // Input and output languages are defined as parameters.
        //string route = "/translate?api-version=3.0&to=th&toScript=latn";
        // Include sentence length details.
        //string route = "/translate?api-version=3.0&to=es&includeSentenceLength=true";
        // Only include sentence length details.
        //string route = "/breaksentence?api-version=3.0";
        // See many translation options
        //string route = "/dictionary/lookup?api-version=3.0&from=en&to=es";
        //string textToTranslate = "shark";        
        //object[] body = new object[] { new { Text = textToTranslate } };
        // See examples of terms in context
        string route = "/dictionary/examples?api-version=3.0&from=en&to=es";
        object[] body = new object[] { new { Text = "Shark", Translation = "tiburón" } };
        var requestBody = JsonConvert.SerializeObject(body);

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);

            // Send the request and get response.
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}