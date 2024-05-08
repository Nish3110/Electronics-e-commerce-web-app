using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Product_Management_System.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class Authentication : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            string userJson = actionContext.HttpContext.Session.GetString("User");
            var user = new User();
            if (userJson != null)
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                if (user != null && (user.User_Type == 0 || user.User_Type == 1))
                {
                    actionContext.HttpContext.Response.Headers.Add("UserAuthStatus", "Authorized");
                    actionContext.HttpContext.Response.Headers.Add("UserstoreAccessiblity", "Authorized");
                    return;
                }
                else
                {
                    actionContext.HttpContext.Session.Clear();
                    actionContext.Result = new RedirectToActionResult("SignIn", "User", null);
                }
            }
            else
            {
                actionContext.HttpContext.Session.Clear();
                actionContext.Result = new RedirectToActionResult("SignIn", "User", null);
            }
        }
    }
}
