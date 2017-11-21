using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using WebApi.Configuration;
using WebApi.Models.Configuration;

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigCors(app);

            ActivateToken(app);

            app.UseWebApi(config);
        }


        public void ActivateToken(IAppBuilder app)
        {
            var optionConfigToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new ProviderTokenAccess()
            };

            app.UseOAuthAuthorizationServer(optionConfigToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        private void ConfigCors(IAppBuilder app)
        {
            var police = new CorsPolicy();
            police.AllowAnyHeader = true;
            police.AllowAnyOrigin = true;     
            police.Methods.Add("GET");
            police.Methods.Add("POST");
            police.Methods.Add("DELETE");

            var options = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(police)
                }
            };

            app.UseCors(options);
        }
    }
}
