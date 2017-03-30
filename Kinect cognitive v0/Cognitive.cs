using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace Kinect_cognitive_v0
{
    public class Cognitive
    {
        public MainWindow main;
        public async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "645b7c34e24641c8b1f8f4e6202e1908");

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = ImageToBinary("C:\\Users\\Bingcheng\\Pictures\\kinectTest.png"); //Encoding.UTF8.GetBytes(); //http://i.imgur.com/MGqHQ42.jpg

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                Trace.WriteLine(content);
                Trace.WriteLine(uri);
                response = await client.PostAsync(uri, content);
                Trace.WriteLine(response);
                var test = await response.Content.ReadAsStringAsync();
                Trace.WriteLine(test);
                main.textbox1.Text = test;
            }

        }
        static byte[] ImageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }
    }
}