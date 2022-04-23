using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using MarsRover.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarsRover.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoversController : ControllerBase
    {
        private readonly IRoversService _roversService;
        public RoversController(IRoversService roversService)
        {
            _roversService = roversService;
        }

        [HttpPost]
        [Route("InsertRovers")]
        public async Task<ApiResult> InsertRovers([FromBody] RoversRequestModel roversRequestModel)
        {
            ApiResult resultResponse = null;
            ApiResult<bool> resut = null;

            resut = await _roversService.InsertRovers(roversRequestModel);
            if (resut.data)
            {
                RoversResponseModel roverResult = _roversService.GetRoverResult(roversRequestModel);
                resut = await _roversService.UpdateRoversStatus();
                if (resut.data == true)
                {
                    return resultResponse = new ApiResult
                    {
                        data = roverResult,
                        message = "Başarılı",
                        rc = "RC00000"
                    };
                }
                else
                {
                    return resultResponse = new ApiResult
                    {
                        data = resut.data,
                        message = resut.message,
                        rc = resut.rc
                    };
                }
            }
            else
            {
                return resultResponse = new ApiResult
                {
                    data = resut.data,
                    message = resut.message,
                    rc = resut.rc
                };
            }
        }

    }
}

