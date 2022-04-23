using MarsRover.Data.Context;
using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using MarsRover.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Service.Concreate
{
    public class RoversService : IRoversService
    {
        public async Task<ApiResult<bool>> GetActiveRecordRovers()
        {
            ApiResult<bool> result = null;
            try
            {
                using (var context = new MarsRoverContext())
                {
                    bool isExist = await context.Rovers.AsNoTracking().AnyAsync(x => x.IsActive == true);
                    if (isExist)
                    {
                        result = new ApiResult<bool>
                        {
                            data = isExist,
                            message = "Aktif çalışan Mars Rover aracı bulunmaktadır.",
                            rc = "RC00000"
                        };
                    }
                    else
                    {
                        result = new ApiResult<bool>
                        {
                            data = isExist,
                            message = "Aktif çalışan Mars Rover aracı bulunmamaktadır.",
                            rc = "RC00000"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result = new ApiResult<bool>
                {
                    data = false,
                    message = ex.Message,
                    rc = "RC00001"
                };
            }
            return result;
        }

        public async Task<ApiResult<bool>> InsertRovers(RoversRequestModel roversRequestModel)
        {
            try
            {
                ApiResult<bool> result = await GetActiveRecordRovers();
                if (!result.data)
                {
                    using (var context = new MarsRoverContext())
                    {
                        Rovers entityModel = new Rovers
                        {
                            CreateDate = DateTime.Now,
                            IsActive = true,
                            RoverDirective = roversRequestModel.RoverDirective,
                            Way = roversRequestModel.Way,
                            X = roversRequestModel.X,
                            Y = roversRequestModel.Y
                        };
                        await context.Rovers.AddAsync(entityModel);
                        context.SaveChanges();

                        return new ApiResult<bool>
                        {
                            data = true,
                            message = "Başarılı",
                            rc = "RC00000"
                        };
                    }
                }
                else
                {
                    return new ApiResult<bool>
                    {
                        data = false,
                        message = "Sıradaki Mars Rover aracının işlemini tamamlamasını bekleyiniz.",
                        rc = "RC00001"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    data = false,
                    message = ex.Message,
                    rc = "RC00001"
                };
            }
        }

        public async Task<ApiResult<bool>> UpdateRoversStatus()
        {
            try
            {
                using (var context = new MarsRoverContext())
                {
                    var entityModel = context.Rovers.FirstOrDefault(x => x.IsActive == true);
                    entityModel.IsActive = false;
                    await context.SaveChangesAsync();
                    return new ApiResult<bool>
                    {
                        data = true,
                        message = "Başarılı",
                        rc = "RC00000"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    data = true,
                    message = $"UpdateRoversStatus : {ex.Message}",
                    rc = "RC00001"
                };
            }
        }

        public RoversResponseModel GetRoverResult(RoversRequestModel roversRequestModel)
        {
            string roverDirectory = roversRequestModel.RoverDirective;
            RoversResponseModel roversResponseModel = new RoversResponseModel()
            {
                Y = roversRequestModel.Y,
                X = roversRequestModel.X,
                Way = roversRequestModel.Way,
                RoverDirective = roversRequestModel.RoverDirective,
            };
            for (int i = 0; i < roverDirectory.Length; i++)
                Process(roverDirectory[i].ToString(), ref roversResponseModel);

            return roversResponseModel;
        }

        private void Process(string roverDirectoryChar, ref RoversResponseModel roversResponseModel)
        {
            if (roverDirectoryChar == "L")
            {
                if (roversResponseModel.Way == "N")
                    roversResponseModel.Way = "W";
                else if (roversResponseModel.Way == "E")
                    roversResponseModel.Way = "N";
                else if (roversResponseModel.Way == "S")
                    roversResponseModel.Way = "E";
                else//(roversModel.WAY == "W")
                    roversResponseModel.Way = "S";
            }
            else if (roverDirectoryChar == "R")
            {
                if (roversResponseModel.Way == "N")
                    roversResponseModel.Way = "E";
                else if (roversResponseModel.Way == "E")
                    roversResponseModel.Way = "S";
                else if (roversResponseModel.Way == "S")
                    roversResponseModel.Way = "W";
                else//(WAY == "W")
                    roversResponseModel.Way = "N";
            }
            else if (roverDirectoryChar == "M")
            {
                if (roversResponseModel.Way == "N")
                    roversResponseModel.Y = roversResponseModel.Y + 1;
                else if (roversResponseModel.Way == "E")
                    roversResponseModel.X = roversResponseModel.X + 1;
                else if (roversResponseModel.Way == "S")
                    roversResponseModel.Y = roversResponseModel.Y - 1;
                else//(roversModel.WAY == "W")
                    roversResponseModel.X = roversResponseModel.X - 1;
            }
        }
    }
}
