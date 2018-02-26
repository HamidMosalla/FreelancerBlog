using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace FreelancerBlog.Experimental.Convention
{
    public class UserAgentAttribute : Attribute, IActionConstraint
    {
        private string substring;
        public UserAgentAttribute(string sub)
        {
            substring = sub.ToLower();
        }
        public int Order { get; set; } = 0;
        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext
                .Request.Headers["User-Agent"]
                .Any(h => h.ToLower().Contains(substring));
        }
    }
}