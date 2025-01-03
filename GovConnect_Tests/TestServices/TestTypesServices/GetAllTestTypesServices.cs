using AutoMapper;
using FluentAssertions;
using IRepository;
using IServices.ITests;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTOs;
using Models.Tests;
using Moq;
using Services.TestServices;
using System.Linq.Expressions;

namespace GovConnect_Tests.TestServices.TestTypesServices
{
    public class GetAllTestTypesServiceTEST
    {
        private readonly IGetAllTestTypesService _getAllTestTypes;
        private readonly Mock<IGetAllRepository<TestType>> _getAllRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<TestType>> _logger;
        public GetAllTestTypesServiceTEST()
        {
            _getAllRepository = new Mock<IGetAllRepository<TestType>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<TestType>>();
            _getAllTestTypes = new GetAllTestTypesService(_getAllRepository.Object, _logger.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllTestTypes_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(new List<TestType>() { });

            //Act
            var result = await _getAllTestTypes.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllTestType_DbHasData_ReturnTestTypeList()
        {
            //Arrange
            List<TestType> testTypeList = new()
            {
                new  TestType() {  TestTypeId = 1, TestTypeDescription = "bla bla 1", TestTypeTitle = "kiki 1", TestTypeFees = 150},
                new  TestType() {  TestTypeId = 2, TestTypeDescription = "bla bla 2", TestTypeTitle = "kiki 2", TestTypeFees = 120},
                new  TestType() {  TestTypeId = 3, TestTypeDescription = "bla bla 3", TestTypeTitle = "kiki 3", TestTypeFees = 130},
                new  TestType() {  TestTypeId = 4, TestTypeDescription = "bla bla 4", TestTypeTitle = "kiki 4", TestTypeFees = 140},
            };

            List<TestTypeDTO> testTypeDTOs = testTypeList
                .Select(ttd => new TestTypeDTO
                {
                    TestTypeId = ttd.TestTypeId,
                    TestTypeTitle = ttd.TestTypeTitle,
                    TestTypeDescription = ttd.TestTypeDescription,
                    TestTypeFees = ttd.TestTypeFees
                }).ToList();

            _getAllRepository.Setup(temp => temp.GetAllAsync()).ReturnsAsync(testTypeList);


            _mapper.Setup(temp => temp.Map<TestTypeDTO>(It.IsAny<TestType>()))
                .Returns((TestType source) => new TestTypeDTO
                {
                    TestTypeId = source.TestTypeId,
                    TestTypeTitle = source.TestTypeTitle,
                    TestTypeDescription = source.TestTypeDescription,
                    TestTypeFees = source.TestTypeFees
                });

            //Act
            var result = await _getAllTestTypes.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(testTypeDTOs);
        }

        [Fact]
        public async Task GetAllApplicationsFeesExpr_EmptyDb_ReturnEmptyList()
        {
            //Arrange
            IQueryable<TestType> emptyQueryable = new List<TestType>() { }.AsQueryable();

            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<TestType, bool>>>()))
                .ReturnsAsync(emptyQueryable);

            //Act
            var result = await _getAllTestTypes.GetAllAsync(It.IsAny<Expression<Func<TestType, bool>>>());

            //Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllApplicationsFeesExpr_DbHasData_ReturnApplicationFeesQueryable()
        {
            //Arrange
            IQueryable<TestType> testTypeQueryable = new List<TestType>()
            {
                new  TestType() {  TestTypeId = 1, TestTypeDescription = "bla bla 1", TestTypeTitle = "kiki 1", TestTypeFees = 150},
                new  TestType() {  TestTypeId = 2, TestTypeDescription = "bla bla 2", TestTypeTitle = "kiki 2", TestTypeFees = 120},
                new  TestType() {  TestTypeId = 3, TestTypeDescription = "bla bla 3", TestTypeTitle = "kiki 3", TestTypeFees = 130},
                new  TestType() {  TestTypeId = 4, TestTypeDescription = "bla bla 4", TestTypeTitle = "kiki 4", TestTypeFees = 140},
            }.AsQueryable();


            IQueryable<TestTypeDTO> expectedTestTypeQueryable = testTypeQueryable
               .Select(ttd => new TestTypeDTO()
               {
                   TestTypeId = ttd.TestTypeId,
                   TestTypeTitle = ttd.TestTypeTitle,
                   TestTypeDescription = ttd.TestTypeDescription,
                   TestTypeFees = ttd.TestTypeFees
               }).AsQueryable();

            _mapper.Setup(temp => temp.Map<TestTypeDTO>(It.IsAny<TestType>()))
                .Returns((TestType source) => new TestTypeDTO
                {
                    TestTypeId = source.TestTypeId,
                    TestTypeTitle = source.TestTypeTitle,
                    TestTypeDescription = source.TestTypeDescription,
                    TestTypeFees = source.TestTypeFees

                });


            _getAllRepository.Setup(temp => temp.GetAllAsync(It.IsAny<Expression<Func<TestType, bool>>>()))
               .ReturnsAsync(testTypeQueryable);

            //Act
            var result = await _getAllTestTypes.GetAllAsync(It.IsAny<Expression<Func<TestType, bool>>>());

            //Assert
            result.Should().BeEquivalentTo(expectedTestTypeQueryable);
        }


    }
}
