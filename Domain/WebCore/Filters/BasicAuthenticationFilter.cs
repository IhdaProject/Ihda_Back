using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCore.Filters
{
    public class BasicAuthenticationFilter(string[] users) : Attribute, IAuthorizationFilter
    {
        private string[] Users { get; } = users;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (!AuthenticationHeaderValue.TryParse(authHeader, out var authValue) ||
                !"Basic".Equals(authValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var decodedAuthHeader = Encoding.UTF8.GetString(Convert.FromBase64String(authValue.Parameter ?? string.Empty));
            var credentials = decodedAuthHeader.Split(':');

            if (credentials.Length == 2)
            {
                var userName = credentials[0];
                var password = credentials[1];

                if (!IsUserValid(userName, password))
                {
                    context.Result = new UnauthorizedResult();
                }
                return;
            }
            context.Result = new UnauthorizedResult();
        }

        private bool IsUserValid(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;
                
            return Users.Contains($"{userName} : {password}");
        }
    }
}
