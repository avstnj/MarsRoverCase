using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using MarsRover.UI.ApiHelpers;
using MarsRover.UI.Service.Abstract;
using System.Threading.Tasks;

namespace MarsRover.UI.Service.Concreate
{
    public class RoverService : IRoverService
    {
        public async Task<ApiResult> InsertRovers(RoversRequestModel roversRequestModel)
        {
            string URL = "https://localhost:44307/api/" + "Rovers/InsertRovers";
            var datalist = await ApiProcess.PostMetod<RoversRequestModel, ApiResult>(URL, roversRequestModel);
            return datalist;
        }
    }
}
