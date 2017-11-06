using Nancy;
using Nancy.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    public class ApplicationBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("data/publish_informations", @"data/publish_informations"));
            base.ConfigureConventions(nancyConventions);
            StaticConfiguration.EnableRequestTracing = true;
        }
    }
}
