using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.API.AuthorizationFilter
{
    public class AuthorizationAPIFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string controllerName = context.RouteData.Values["Controller"].ToString();
            string actionName = context.RouteData.Values["Action"].ToString();

            
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                // context.Result = new HttpUnauthorizedResult();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(new APIResultObject() { error=true, errorCode=401, returnId = -1, errorMessage = "Authorization Token invalid" });
                return;
            }
            else
            {
                string returnAuthKey = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                string apiKey = "2020Sssnp#!";
              
                if ( returnAuthKey.Length <=7 || returnAuthKey.Substring(7) != apiKey.HashSHA512())
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new APIResultObject() { error = true, errorCode = 401, returnId = -1, errorMessage = "Unauthorize Access" });
                    return;
                }

                // Authorized


            }

        }
    }

}
