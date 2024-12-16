using AutoFixture;
using AutoMapper;
using Azure.Core;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.Purpose;
using System.Linq.Expressions;
using IServices.IApplicationServices.Purpose;
using Web.Mapper;


namespace GovConnect_Tests.ApplicationServices
{
    public class ICreateApplicationPurposeServiceTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        private readonly ICreateApplicationPurpose _createApplicationPurpose;
        private readonly ICreateRepository<ApplicationPurpose> _createRepository;
        private readonly Mock<ICreateRepository<ApplicationPurpose>> _MockcreateRepository;

        private readonly IGetApplicationPurpose _getApplicationPurpose;
        private readonly IGetRepository<ApplicationPurpose> _getRepository;
        private readonly Mock<IGetRepository<ApplicationPurpose>> _MockGetRepository;
        public ICreateApplicationPurposeServiceTest()
        {

            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _MockcreateRepository = new Mock<ICreateRepository<ApplicationPurpose>>();
            _MockGetRepository = new Mock<IGetRepository<ApplicationPurpose>>();

            _createApplicationPurpose =
                new ICreateApplicationPurposeService(_MockcreateRepository.Object,
                                                _MockGetRepository.Object,
                                                _mapper.Object);
        }

        #region Validation Test


        [Fact]
        public async Task CreateAsync_CreateApplicationTypeRequestIsNull_ThrowArgumentNullException()
        {
            //Act
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAsync_TypePropertyisNull_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationPurposeRequest createRequest = new() { Purpose = null };
            //Act
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_TypePropertyisEmpty_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationPurposeRequest createRequest = new() { Purpose = "" };
            //Act
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_TypeisAlreadyExist_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationPurposeRequest createRequest = new() { Purpose = "New" };

            ApplicationPurpose applicationPurpose = new() { ApplicationPurposeId = 73, Purpose = "New" };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                                    .ReturnsAsync(applicationPurpose);

            //Act          
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(createRequest);

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
            CreateApplicationPurposeRequest creatRequest = _fixture.Create<CreateApplicationPurposeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                .ReturnsAsync(null as ApplicationPurpose);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationPurpose>(It.IsAny<CreateApplicationPurposeRequest>()))
                         .Returns(null as ApplicationPurpose);
            //Act
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationTypeToDTO_ThrwosAutoMapperMappingException()
        {
            //Arrange
            CreateApplicationPurposeRequest creatRequest = _fixture.Create<CreateApplicationPurposeRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                .ReturnsAsync(null as ApplicationPurpose);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>()))
                         .Returns(null as ApplicationPurposeDTO);
            //Act
            Func<Task> action = async () => await _createApplicationPurpose.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        #endregion


        [Fact]
        public async Task CreateApplication_RequestWithSpacingAndUpperCase_ReturnTrimmingAndLowwerCase()
        {
            //Arrange
            CreateApplicationPurposeRequest createRequest = new() { Purpose = "  NewType  " };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                                    .ReturnsAsync(null as ApplicationPurpose);

            ApplicationPurpose applicationPurpose = new ApplicationPurpose()
            {
                ApplicationPurposeId = 13,
                Purpose = createRequest.Purpose.Trim().ToLower()
            };

            _mapper.Setup(temp => temp.Map<ApplicationPurpose>(It.IsAny<CreateApplicationPurposeRequest>())).Returns(applicationPurpose);

            ApplicationPurposeDTO applicationPurposeDto = new ApplicationPurposeDTO()
            {
                ApplicationPurposeId = applicationPurpose.ApplicationPurposeId,
                Purpose = applicationPurpose.Purpose
            };
            _mapper.Setup(temp => temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>())).Returns(applicationPurposeDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationPurpose>())).ReturnsAsync(applicationPurpose);


            //Act
            var result = await _createApplicationPurpose.CreateAsync(createRequest);

            //Assert
            result.Purpose.Should().Be("newtype");
        }



        [Fact]
        public async Task CreateApplication_ValidApplicationTypeRequest_ReturnApplicationTypeDTO()
        {
            //Arrange
            CreateApplicationPurposeRequest createRequest = _fixture.Create<CreateApplicationPurposeRequest>();

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationPurpose, bool>>>()))
                                    .ReturnsAsync(null as ApplicationPurpose);

            ApplicationPurpose applicationPurpose = new() { ApplicationPurposeId = 73, Purpose = createRequest.Purpose };

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationPurpose>())).ReturnsAsync(applicationPurpose);

            _mapper.Setup(temp => temp.Map<ApplicationPurpose>(It.IsAny<CreateApplicationPurposeRequest>())).Returns(applicationPurpose);

            ApplicationPurposeDTO applicationPurposeDto = new ApplicationPurposeDTO()
            {
                ApplicationPurposeId = applicationPurpose.ApplicationPurposeId,
                Purpose = applicationPurpose.Purpose
            };
            _mapper.Setup(temp => temp.Map<ApplicationPurposeDTO>(It.IsAny<ApplicationPurpose>())).Returns(applicationPurposeDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationPurpose>())).ReturnsAsync(applicationPurpose);



            ApplicationPurposeDTO expected = new() { ApplicationPurposeId = applicationPurpose.ApplicationPurposeId, Purpose = applicationPurpose.Purpose };

            //Act          
            var result = await _createApplicationPurpose.CreateAsync(createRequest);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
