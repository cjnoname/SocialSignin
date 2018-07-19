using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Amazon.Lambda.AspNetCoreServer;

namespace PremierAppSign
{
    /// <summary>
    /// This class extends from APIGatewayProxyFunction which contains the method FunctionHandlerAsync which is the 
    /// actual Lambda function entry point. The Lambda handler field should be set to
    /// 
    /// PremierAppSign::PremierAppSign.LambdaEntryPoint::FunctionHandlerAsync
    /// </summary>
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {

        private Dictionary<string, ResponseContentEncoding> _responseContentEncodingForContentType = new Dictionary<string, ResponseContentEncoding>
        {
            // The complete list of registered MIME content-types can be found at:
            //    http://www.iana.org/assignments/media-types/media-types.xhtml

            // Here we just include a few commonly used content types found in
            // Web API responses and allow users to add more as needed below

            ["text/plain"] = ResponseContentEncoding.Default,
            ["text/xml"] = ResponseContentEncoding.Default,
            ["application/xml"] = ResponseContentEncoding.Default,
            ["application/json"] = ResponseContentEncoding.Default,
            ["text/html"] = ResponseContentEncoding.Default,
            ["text/css"] = ResponseContentEncoding.Default,
            ["text/javascript"] = ResponseContentEncoding.Default,
            ["text/csv"] = ResponseContentEncoding.Default,
            ["application/x-font-ttf"] = ResponseContentEncoding.Default,

            ["application/octet-stream"] = ResponseContentEncoding.Base64,
            ["image/png"] = ResponseContentEncoding.Base64,
            ["image/gif"] = ResponseContentEncoding.Base64,
            ["image/jpeg"] = ResponseContentEncoding.Base64,
            ["image/x-icon"] = ResponseContentEncoding.Base64,
            ["image/svg+xml"] = ResponseContentEncoding.Base64,
            ["application/zip"] = ResponseContentEncoding.Base64,
            ["application/pdf"] = ResponseContentEncoding.Base64,
        };

        /// <summary>
        /// The builder has configuration, logging and Amazon API Gateway already configured. The startup class
        /// needs to be configured in this method using the UseStartup<>() method.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Init(IWebHostBuilder builder)
        {
            foreach (var type in _responseContentEncodingForContentType)
            {
                RegisterResponseContentEncodingForContentType(type.Key, type.Value);
            }

            builder
                .UseStartup<Startup>();
        }
    }
}
