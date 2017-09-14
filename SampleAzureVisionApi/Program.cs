using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.IO;

namespace SampleAzureVisionApi
{
    class Program
    {
        static void Main(string[] args)
		{
			MakeRequest();
			Console.WriteLine("Hit ENTER to exit...");
			Console.ReadLine();
        }

		static async void MakeRequest()
		{
			var client = new HttpClient();
            var queryString = new StringBuilder();

			// Request headers
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "832bf635ebdf4589812fa41ef2f6dda2");

			// Request parameters
            queryString.Append("language=en");
            queryString.Append("&detectOrientation=true");
			var uri = "https://eastus2.api.cognitive.microsoft.com/vision/v1.0/ocr?" + queryString;

			HttpResponseMessage response;

			// Request body
			byte[] imgArray = File.ReadAllBytes("img/BL.jpg");
			//string imgContent = Convert.ToBase64String(imgArray);

			//byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(imgArray))
			{
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
				response = await client.PostAsync(uri, content);
			}

            var result = response.ToString();
			if (result == null) return;

            Console.WriteLine(response.ToString());
            Console.WriteLine(await response.Content.ReadAsStringAsync());
		}
    }
}
