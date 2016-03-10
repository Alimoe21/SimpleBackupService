using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleService
{
    public class ServiceController : ApiController
    {
        public string GetString ( int id )
        {
            return $"Hello from the simpleService: {id}";
        }
    }
}
