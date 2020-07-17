using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catel.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ImageManager.API.Controllers.Core
{
    public class ApiContext
    {
        public class UserContext
        {
            private readonly Func<Controller> controller;

            internal UserContext(Func<Controller> controller)
            {
                this.controller = controller;
            }
            private ClaimsPrincipal Principal { get { return controller().User; } }
            public int UserId
            {
                get
                {
                    var claim = Principal.FindFirstValue(ClaimTypes.Sid);
                    return string.IsNullOrEmpty(claim) ? 0 : Int32.Parse(claim);
                }
            }
            public string Email { get { return Principal.FindFirstValue(ClaimTypes.Email); } }
            public bool IsAuthenticated { get { return Principal.Identity.IsAuthenticated; } }
        }
        private Controller Controller { get { return controller(); } }
        private readonly Func<Controller> controller;
        private readonly IConfigurationService config;

        internal ApiContext(Func<Controller> controller, IConfigurationService config)
        {
            this.controller = controller;
            this.config = config;
            this.CurrentUser = new UserContext(controller);
        }

        public UserContext CurrentUser { get; private set; }

        private string GetHeaderValue(string key, string defaultValue)
        {
            var headers = Controller.Request.Headers;

            if (Controller.Request.Headers.ContainsKey(key))
            {
                var header = Controller.Request.Headers[key];
                if (header.Count > 0)
                    return header[0].ToLower();

            }
            return defaultValue.ToLower();
        }

    }
}
