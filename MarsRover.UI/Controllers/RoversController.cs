using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using MarsRover.UI.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MarsRover.UI.Controllers
{
    public class RoversController : Controller
    {
        private readonly IRoverService _roverService;
        public RoversController(IRoverService roverService)
        {
            _roverService = roverService;
        }
        public IActionResult Index()
        {
            return View(false);
        }
        [HttpPost]
        public IActionResult Index(RoversRequestModel roversRequestModel)
        {
            ViewBag.Result = "";
            if (roversRequestModel.X >= 0 && roversRequestModel.Y >= 0 && roversRequestModel.Way != null && roversRequestModel.RoverDirective != null)
            {
                ApiResult result = _roverService.InsertRovers(roversRequestModel).Result;
                RoversResponseModel roversResponseModel = null;
                if (result.rc == "RC00000")
                {
                    roversResponseModel = JsonConvert.DeserializeObject<RoversResponseModel>(result.data.ToString());
                    ViewBag.Result = "X : " + roversResponseModel.X + " Y : " + roversResponseModel.Y + " Yön : " + roversResponseModel.Way;
                }
                else
                    ViewBag.Result = roversResponseModel;

                return View(true);
            }
            else
            {
                ViewBag.Result = "Bilgiler Eksiksiz girilmemelidir...";
                return View(true);
            }
        }
    }
}
