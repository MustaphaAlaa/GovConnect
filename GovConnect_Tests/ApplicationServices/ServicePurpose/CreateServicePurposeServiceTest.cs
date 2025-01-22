using AutoFixture;
using AutoMapper;
using Azure.Core;
using FluentAssertions;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.IPurpose;
using Web.Mapper;
using IRepository.IGenericRepositories;


namespace GovConnect_Tests.ApplicationServices
{
    public class CreateServicePurposeServiceTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        private readonly ICreateServicePurpose _createServicePurpose;
        private readonly ICreateRepository<ServicePurpose> _createRepository;
        private readonly Mock<ICreateRepository<ServicePurpose>> _MockcreateRepository;

        private readonly IGetServicePurpose _getServicePurpose;
        private readonly IGetRepository<ServicePurpose> _getRepository;
        private readonly Mock<IGetRepository<ServicePurpose>> _MockGetRepository;
        public CreateServicePurposeServiceTest()
        {

            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _MockcreateRepository = new Mock<ICreateRepository<ServicePurpose>>();
            _MockGetRepository = new Mock<IGetRepository<ServicePurpose>>();

            _createServicePurpose =
                new CreateServicePurposeService(_MockcreateRepository.Object,
                                                _MockGetRepository.Object,
                                                _mapper.Object);
        }

        #region Validation Test


        [Fact]
        public async Task CreateAsync_CreateServicePurposeRequestIsNull_ThrowArgumentNullException()
        {
            //Act
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAsync_PurposePropertyIsNull_ThrowArgumentException()
        {
            //Arrange
            CreateServicePurposeRequest createRequest = new() { Purpose = null };
            //Act
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_PurposePropertyIsEmpty_ThrowArgumentException()
        {
            //Arrange
            CreateServicePurposeRequest createRequest = new() { Purpose = "" };
            //Act
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_PurposeIsAlreadyExist_ThrowArgumentException()
        {
            //Arrange
            CreateServicePurposeRequest createRequest = new() { Purpose = "New" };

            ServicePurpose applicationPurpose = new() { ServicePurposeId = 73, Purpose = "New" };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                                    .ReturnsAsync(applicationPurpose);

            //Act          
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(createRequest);

            //Assert
            await action.Should()
                        .ThrowAsync<InvalidOperationException>();
        }
        #endregion



        #region Mapping

        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationRequestToServicePurpose_ThrowsAutoMapperMappingException()
        {
            //Arrange
            CreateServicePurposeRequest creatRequest = _fixture.Create<CreateServicePurposeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                .ReturnsAsync(null as ServicePurpose);

            _mapper.Setup(temp =>
                          temp.Map<ServicePurpose>(It.IsAny<CreateServicePurposeRequest>()))
                         .Returns(null as ServicePurpose);
            //Act
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        [Fact]
        public async Task CreateAsync_MappingFromCreateServicePurposeToDTO_ThrwosAutoMapperMappingException()
        {
            //Arrange
            CreateServicePurposeRequest creatRequest = _fixture.Create<CreateServicePurposeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                .ReturnsAsync(null as ServicePurpose);

            _mapper.Setup(temp =>
                          temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>()))
                         .Returns(null as ServicePurposeDTO);
            //Act
            Func<Task> action = async () => await _createServicePurpose.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        #endregion


        [Fact]
        public async Task CreateApplication_RequestWithSpacingAndUpperCase_ReturnTrimmingAndLowwerCase()
        {
            //Arrange
            CreateServicePurposeRequest createRequest = new() { Purpose = "  NewPurpose  " };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                                    .ReturnsAsync(null as ServicePurpose);

            ServicePurpose applicationPurpose = new ServicePurpose()
            {
                ServicePurposeId = 13,
                Purpose = createRequest.Purpose.Trim().ToLower()
            };

            _mapper.Setup(temp => temp.Map<ServicePurpose>(It.IsAny<CreateServicePurposeRequest>()))
                .Returns(applicationPurpose);

            ServicePurposeDTO applicationPurposeDto = new ServicePurposeDTO()
            {
                ServicePurposeId = applicationPurpose.ServicePurposeId,
                Purpose = applicationPurpose.Purpose
            };
            _mapper.Setup(temp => temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>())).Returns(applicationPurposeDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServicePurpose>())).ReturnsAsync(applicationPurpose);


            //Act
            var result = await _createServicePurpose.CreateAsync(createRequest);

            //Assert
            result.Purpose.Should().Be("newpurpose");
        }



        [Fact]
        public async Task CreateApplication_ValidServicePurposeRequest_ReturnServicePurposeDTO()
        {
            //Arrange
            CreateServicePurposeRequest createRequest = _fixture.Create<CreateServicePurposeRequest>();

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServicePurpose, bool>>>()))
                                    .ReturnsAsync(null as ServicePurpose);

            ServicePurpose applicationPurpose = new() { ServicePurposeId = 73, Purpose = createRequest.Purpose };

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServicePurpose>())).ReturnsAsync(applicationPurpose);

            _mapper.Setup(temp => temp.Map<ServicePurpose>(It.IsAny<CreateServicePurposeRequest>())).Returns(applicationPurpose);

            ServicePurposeDTO applicationPurposeDto = new ServicePurposeDTO()
            {
                ServicePurposeId = applicationPurpose.ServicePurposeId,
                Purpose = applicationPurpose.Purpose
            };
            _mapper.Setup(temp => temp.Map<ServicePurposeDTO>(It.IsAny<ServicePurpose>())).Returns(applicationPurposeDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServicePurpose>())).ReturnsAsync(applicationPurpose);



            ServicePurposeDTO expected = new() { ServicePurposeId = applicationPurpose.ServicePurposeId, Purpose = applicationPurpose.Purpose };

            //Act          
            var result = await _createServicePurpose.CreateAsync(createRequest);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
