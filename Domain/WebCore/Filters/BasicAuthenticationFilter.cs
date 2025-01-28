using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthService.Basic
{
    public class BasicAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        private string[] Users { get; }

        public BasicAuthenticationFilter(string[] users)
        {
            Users = users;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

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

            string decodedAuthHeader = Encoding.UTF8.GetString(Convert.FromBase64String(authValue.Parameter ?? string.Empty));
            string[] credentials = decodedAuthHeader.Split(':');

            if (credentials.Length == 2)
            {
                string userName = credentials[0];
                string password = credentials[1];

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
