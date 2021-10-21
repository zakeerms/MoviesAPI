using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.APIBehavior
{
    //This Class for Overriding Application Error msg with API Behaviour
    public class BadRequestsBehavior
    {
        public static void Parse(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var respone = new List<string>();
                foreach (var key in context.ModelState.Keys)
                {
                    foreach (var error in context.ModelState[key].Errors)
                    {
                        respone.Add($"{key}:{error.ErrorMessage}");
                    }
                }
                return new BadRequestObjectResult(respone);
            };
        }
    }
}
