using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using System.Threading.Tasks;

namespace MarsRover.UI.Service.Abstract
{
    public interface IRoverService
    {
        Task<ApiResult> InsertRovers(RoversRequestModel roversRequestModel);
    }
}
