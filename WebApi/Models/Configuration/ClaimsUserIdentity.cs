using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApi.Models.Configuration
{
    public class ClaimsUserIdentity
    {
        public User user { get; set; }
        public ClaimsIdentity identity { get; set; }
    }
}