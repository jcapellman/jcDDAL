using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

using jcDDAL.pcl;

namespace jcDDAL.lib {
    public class jcDDALDispatcher : HttpControllerDispatcher {
        public jcDDALDispatcher(HttpConfiguration configuration) : base(configuration) { }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var platformTypes = request.Headers.GetValues(pcl.Common.Constants.HEADER_NAME);

            var platformType = PlatformTypes.WEB;

            var enumerable = platformTypes as IList<string> ?? platformTypes.ToList();

            if (enumerable.Any()) {
                platformType = (PlatformTypes) Enum.Parse(typeof (PlatformTypes), enumerable.FirstOrDefault());
            } else {
                var userAgent = request.Headers.UserAgent.ToString();

                if (userAgent.Contains("iPhone")) {
                    platformType = PlatformTypes.IPHONE;
                }
            }

            var routeData = request.GetRouteData();

            var controller = routeData.Values["controller"].ToString();
            routeData.Values["controller"] = controller + platformType;

            return base.SendAsync(request, cancellationToken);
        }
    }
}