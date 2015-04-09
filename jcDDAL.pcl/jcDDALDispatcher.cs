using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcDDAL.pcl {
    public class jcDDALDispatcher {
        private readonly PlatformTypes _platformType;

        public jcDDALDispatcher(PlatformTypes argPlatformType) {
            _platformType = argPlatformType;
        }

        private HttpClient httpClient {
            get { 
                var tmpClient = new HttpClient();

                tmpClient.DefaultRequestHeaders.Add(Common.Constants.HEADER_NAME, _platformType.ToString());

                return tmpClient;
            }
        }

        public async Task<T> Get<T>(string url) {
            var result = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}