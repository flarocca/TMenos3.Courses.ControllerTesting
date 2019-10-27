using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace TMenos3.Courses.ControllerTesting.TestingUtils
{
    public static class HttpContentFactory
    {
        public static StringContent CreateStringContent<T>(T content) =>
            new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
    }
}
