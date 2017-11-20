using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Models.Configuration
{
    public class ProviderTokenAccess : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = User.Buscar(context.UserName, context.Password).FirstOrDefault();
            if (usuario == null || usuario.Id == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado um senha incorreta.");
                return;
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "username", usuario.UserName
                },
                {
                    "id", usuario.Id.ToString()
                }
            });

            var ticket = new AuthenticationTicket(identidadeUsuario, props);

            context.Validated(ticket);
        }
    }
}