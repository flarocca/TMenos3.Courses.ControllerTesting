using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TMenos3.Courses.ControllerTesting.TestingUtils
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeToAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var serializer = new JsonSerializer();

            using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            using var sr = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(sr);

            return serializer.Deserialize<T>(jsonTextReader);
        }
    }
}
