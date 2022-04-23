using MarsRover.Dto;
using MarsRover.Dto.Rovers;
using MarsRover.Service.Abstract;
using MarsRover.Service.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.UnitTest
{
    public class TypeSafeData : TheoryData<int, int, string, string, int, int, string>
    {
        public TypeSafeData()
        {
            Add(5, 5, "E", "MMMMLMLM", 8, 6, "W");
            Add(5, 4, "W", "MMRMRMM", 5, 5, "E");
            Add(3, 2, "E", "MMLMML", 5, 4, "W");
            Add(0, 0, "E", "MMMLMMR", 3, 2, "E");
        }
    }
    public class MarsRoversUnitTest : IClassFixture<RoversService>
    {
        private readonly RoversService _roversService;
        public MarsRoversUnitTest(RoversService roversService)
        {
            _roversService = roversService;
        }
        [Theory]
        [InlineData(false)]
        public async void Test1(bool expected)
        {
            ApiResult<bool> result = await _roversService.GetActiveRecordRovers();
            Assert.Equal(expected, result.data);
        }
        //[ClassData(typeof(RoversRequestModel))]
        [Theory]
        [ClassData(typeof(TypeSafeData))]
        public void Test2(int x, int y, string way, string roverDirective, int expectedX, int expectedY, string expectedWay)
        {
            RoversRequestModel roversRequestModel = new RoversRequestModel
            {
                RoverDirective = roverDirective,
                Way = way,
                X = x,
                Y = y
            };

            RoversResponseModel roversResponseModel = _roversService.GetRoverResult(roversRequestModel);
            Assert.Equal(expectedX, roversResponseModel.X);
            Assert.Equal(expectedY, roversResponseModel.Y);
            Assert.Equal(expectedWay, roversResponseModel.Way);
        }
    }
}
