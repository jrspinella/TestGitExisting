using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TFSTFServiceHook.Startup))]

namespace TFSTFServiceHook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureLog4Net();
        }
    }
}
