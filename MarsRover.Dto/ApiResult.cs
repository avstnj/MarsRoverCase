using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Dto
{
    public class ApiResult<T>
    {
        public T data { get; set; }
        public string message { get; set; }
        public string rc { get; set; }
    }
    public class ApiResult
    {
        public object data { get; set; }
        public string message { get; set; }
        public string rc { get; set; }
    }
}
