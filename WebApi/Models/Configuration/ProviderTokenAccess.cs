using Microsoft.Owin.Security.OAuth;
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
            var usuario = BaseUsuarios.Users().FirstOrDefault(x => x.UserName == context.UserName && x.Password == context.Password);
            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado um senha incorreta.");
                return;
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }
    }
}