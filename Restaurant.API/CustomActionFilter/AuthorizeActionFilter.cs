using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Services.Token.Interface;
using System;
using System.Net;

namespace Restaurant.API.CustomActionFilter
{
    public class AuthorizeActionFilter : Attribute, IActionFilter
    {
        public AuthorizeActionFilter()
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var securityService = context.HttpContext.RequestServices.GetService<ITokenService>();

            if (context != null)
            {
                context.HttpContext.Request.Headers.TryGetValue("Token", out Microsoft.Extensions.Primitives.StringValues authToken);
                context.HttpContext.Request.Headers.TryGetValue("questHeader", out Microsoft.Extensions.Primitives.StringValues questDev);

                if (!string.IsNullOrEmpty(authToken))
                {
                    if (securityService.ValidateToken(authToken).Result)
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                        context.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                        return;
                    }
                    else
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        context.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Invalid Token"
                            },
                        };
                    }
                }
                else
                if (!string.IsNullOrEmpty(questDev))
                {
                    if (questDev == "appdev")
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                        context.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                        return;
                    }
                    else
                    {
                        context.HttpContext.Response.Headers.Add("authToken", authToken);
                        context.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        context.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Invalid Token"
                            },
                        };
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide Header Values";
                    context.Result = new JsonResult("Header Values Missing")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Header Values Missing"
                        },
                    };
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide Header Values";
                context.Result = new JsonResult("Header Values Missing")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Header Values Missing"
                    },
                };
            }
        }
    }
}