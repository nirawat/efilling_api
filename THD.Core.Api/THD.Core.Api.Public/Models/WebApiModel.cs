using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace THD.Core.Api.Public.Models
{
    public interface IWebApiModel
    {
        string BaseURL { get; set; }
        RoutePath RoutePath { get; set; }
    }
    public class WebApiModel : IWebApiModel
    {
        public string BaseURL { get; set; }
        public RoutePath RoutePath { get; set; }
    }

    public class RoutePath
    {
        public string DocReceiveData { get; set; }
    }

}
