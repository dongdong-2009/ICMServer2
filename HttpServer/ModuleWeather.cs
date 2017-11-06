using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class ModuleWeather : NancyModule
    {
        public ModuleWeather() 
            : base("/doorbell")
        {
            Get["/weather"] = parameters =>
            {
                string weatherXmlFilePath = Path.GetWeatherXmlFilePath();
                string responseText = "";
                try
                {
                    responseText = System.IO.File.ReadAllText(weatherXmlFilePath);
                }
                catch (Exception) { }
                return responseText;
            };
        }
    }
}
