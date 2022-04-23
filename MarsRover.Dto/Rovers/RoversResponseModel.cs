using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Dto.Rovers
{
    public class RoversResponseModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Way { get; set; }
        public string RoverDirective { get; set; }
    }
}
