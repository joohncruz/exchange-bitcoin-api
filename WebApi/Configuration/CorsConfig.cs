using Microsoft.Owin.Cors;
using Owin;
using System.Threading.Tasks;
using System.Web.Cors;

namespace WebApi.Configuration
{
    public static class CorsConfig
    {
        public static void ConfigureCors(this IAppBuilder app)
        {
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true
            };
            policy.Origins.Add("http://localhost:50410");
            policy.Methods.Add("GET");
            policy.Methods.Add("POST");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            };

            app.UseCors(corsOptions);
        }
    }
}