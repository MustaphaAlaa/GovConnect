using AutoFixture;
using AutoMapper;
using Azure.Core;
using FluentAssertions;
using IRepository;
using IServices.Application.Type;
using ModelDTO.Application.Type;
using Models.ApplicationModels;
using Moq;
using Services.Application.Type;
using System.Linq.Expressions;
using Web.Mapper;


namespace DVLD_Tests.Applications
{
    public class CreateApplicationTypeServiceTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        private readonly ICreateApplicationType _createApplicationType;
        private readonly ICreateRepository<ApplicationType> _createRepository;
        private readonly Mock<ICreateRepository<ApplicationType>> _MockcreateRepository;

        private readonly IGetApplicationType _getApplicationType;
        private readonly IGetRepository<ApplicationType> _getRepository;
        private readonly Mock<IGetRepository<ApplicationType>> _MockGetRepository;
        public CreateApplicationTypeServiceTest()
        {

            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _MockcreateRepository = new Mock<ICreateRepository<ApplicationType>>();
            _MockGetRepository = new Mock<IGetRepository<ApplicationType>>();

            _createApplicationType =
                new CreateApplicationTypeService(_MockcreateRepository.Object,
                                                _MockGetRepository.Object,
                                                _mapper.Object);
        }

        #region Validation Test


        [Fact]
        public async Task CreateAsync_CreateApplicationTypeRequestIsNull_ThrowArgumentNullException()
        {
            //Act
            Func<Task> action = async () => await _createApplicationType.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAsync_TypePropertyisNull_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationTypeRequest createRequest = new() { Type = null };
            //Act
            Func<Task> action = async () => await _createApplicationType.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_TypePropertyisEmpty_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationTypeRequest createRequest = new() { Type = "" };
            //Act
            Func<Task> action = async () => await _createApplicationType.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_TypeisAlreadyExist_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationTypeRequest createRequest = new() { Type = "New" };

            ApplicationType applicationType = new() { Id = 1973, Type = "New" };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                                    .ReturnsAsync(applicationType);

            //Act          
            Func<Task> action = async () => await _createApplicationType.CreateAsync(createRequest);

            //Assert
            await action.Should()
                        .ThrowAsync<InvalidOperationException>();
        }
        #endregion



        #region Mapping

        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationRequestToApplicationType_ThrowsAutoMapperMappingException()
        {
            //Arrange
            CreateApplicationTypeRequest creatRequest = _fixture.Create<CreateApplicationTypeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                .ReturnsAsync(null as ApplicationType);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationType>(It.IsAny<CreateApplicationTypeRequest>()))
                         .Returns(null as ApplicationType);
            //Act
            Func<Task> action = async () => await _createApplicationType.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationTypeToDTO_ThrwosAutoMapperMappingException()
        {
            //Arrange
            CreateApplicationTypeRequest creatRequest = _fixture.Create<CreateApplicationTypeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                .ReturnsAsync(null as ApplicationType);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>()))
                         .Returns(null as ApplicationTypeDTO);
            //Act
            Func<Task> action = async () => await _createApplicationType.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        #endregion


        [Fact]
        public async Task CreateApplication_RequestWithSpacingAndUpperCase_ReturnTrimmingAndLowwerCase()
        {
            //Arrange
            CreateApplicationTypeRequest createRequest = new() { Type = "  NewType  " };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                                    .ReturnsAsync(null as ApplicationType);

            ApplicationType applicationType = new ApplicationType()
            {
                Id = 1888,
                Type = createRequest.Type.Trim().ToLower()
            };

            _mapper.Setup(temp => temp.Map<ApplicationType>(It.IsAny<CreateApplicationTypeRequest>())).Returns(applicationType);

            ApplicationTypeDTO applicationTypeDTO = new ApplicationTypeDTO()
            {
                Id = applicationType.Id,
                Type = applicationType.Type
            };
            _mapper.Setup(temp => temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>())).Returns(applicationTypeDTO);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationType>())).ReturnsAsync(applicationType);


            //Act
            var result = await _createApplicationType.CreateAsync(createRequest);

            //Assert
            result.Type.Should().Be("newtype");
        }



        [Fact]
        public async Task CreateApplication_ValidApplicationTypeRequest_ReturnApplicationTypeDTO()
        {
            //Arrange
            CreateApplicationTypeRequest createRequest = _fixture.Create<CreateApplicationTypeRequest>();

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationType, bool>>>()))
                                    .ReturnsAsync(null as ApplicationType);

            ApplicationType applicationType = new() { Id = 1973, Type = createRequest.Type };

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationType>())).ReturnsAsync(applicationType);

            _mapper.Setup(temp => temp.Map<ApplicationType>(It.IsAny<CreateApplicationTypeRequest>())).Returns(applicationType);

            ApplicationTypeDTO applicationTypeDTO = new ApplicationTypeDTO()
            {
                Id = applicationType.Id,
                Type = applicationType.Type
            };
            _mapper.Setup(temp => temp.Map<ApplicationTypeDTO>(It.IsAny<ApplicationType>())).Returns(applicationTypeDTO);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationType>())).ReturnsAsync(applicationType);



            ApplicationTypeDTO expected = new() { Id = applicationType.Id, Type = applicationType.Type };

            //Act          
            var result = await _createApplicationType.CreateAsync(createRequest);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
