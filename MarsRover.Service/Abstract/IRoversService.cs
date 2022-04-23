using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Service.Abstract
{
    public interface IRoversService
    {
        Task<ApiResult<bool>> InsertRovers(RoversRequestModel roversRequestModel);
        Task<ApiResult<bool>> GetActiveRecordRovers();
        Task<ApiResult<bool>> UpdateRoversStatus();
        RoversResponseModel GetRoverResult(RoversRequestModel roversRequestModel);
    }
}
