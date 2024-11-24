using AutoFixture;
using AutoMapper;
using Azure.Core;
using FluentAssertions;
using IRepository;
using IServices.Application.For;
using ModelDTO.Application.For;
using Models.ApplicationModels;
using Moq;
using Services.Application.For;
using System.Linq.Expressions;
using Web.Mapper;


namespace DVLD_Tests.Applications
{
    public class CreateApplicationForServiceTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        private readonly ICreateApplicationFor _createApplicationFor;
        private readonly ICreateRepository<ApplicationFor> _createRepository;
        private readonly Mock<ICreateRepository<ApplicationFor>> _MockcreateRepository;


        private readonly IGetRepository<ApplicationFor> _getRepository;
        private readonly Mock<IGetRepository<ApplicationFor>> _MockGetRepository;
        public CreateApplicationForServiceTest()
        {

            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _MockcreateRepository = new Mock<ICreateRepository<ApplicationFor>>();
            _MockGetRepository = new Mock<IGetRepository<ApplicationFor>>();

            _createApplicationFor =
                new CreateApplicationForService(_MockcreateRepository.Object,
                                                _MockGetRepository.Object,
                                                _mapper.Object);
        }

        #region Validation Test


        [Fact]
        public async Task CreateAsync_CreateApplicationForRequestIsNull_ThrowArgumentNullException()
        {
            //Act
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropertyisNull_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationForRequest createRequest = new() { For = null };
            //Act
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropertyisEmpty_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationForRequest createRequest = new() { For = "" };
            //Act
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropIsAlreadyExist_ThrowArgumentException()
        {
            //Arrange
            CreateApplicationForRequest createRequest = new() { For = "New" };

            ApplicationFor applicationFor = new() { Id = 1973, For = "New" };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                                    .ReturnsAsync(applicationFor);

            //Act          
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(createRequest);

            //Assert
            await action.Should()
                        .ThrowAsync<InvalidOperationException>();
        }
        #endregion



        #region Mapping

        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationRequestToApplicationFor_ThrowsAutoMapperMappingException()
        {
            //Arrange
            CreateApplicationForRequest creatRequest = _fixture.Create<CreateApplicationForRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                .ReturnsAsync(null as ApplicationFor);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationFor>(It.IsAny<CreateApplicationForRequest>()))
                         .Returns(null as ApplicationFor);
            //Act
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationForToDTO_ThrwosAutoMapperMappingException()
        {
            //Arrange
            CreateApplicationForRequest creatRequest = _fixture.Create<CreateApplicationForRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                .ReturnsAsync(null as ApplicationFor);

            _mapper.Setup(temp =>
                          temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>()))
                         .Returns(null as ApplicationForDTO);
            //Act
            Func<Task> action = async () => await _createApplicationFor.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        #endregion


        [Fact]
        public async Task CreateApplication_RequestWithSpacingAndUpperCase_ReturnTrimmingAndLowwerCase()
        {
            //Arrange
            CreateApplicationForRequest createRequest = new() { For = "  NewFor  " };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                                    .ReturnsAsync(null as ApplicationFor);

            ApplicationFor applicationFor = new ApplicationFor()
            {
                Id = 1888,
                For = createRequest.For.Trim().ToLower()
            };

            _mapper.Setup(temp => temp.Map<ApplicationFor>(It.IsAny<CreateApplicationForRequest>())).Returns(applicationFor);

            ApplicationForDTO applicationForDTO = new ApplicationForDTO()
            {
                Id = applicationFor.Id,
                For = applicationFor.For
            };
            _mapper.Setup(temp => temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>())).Returns(applicationForDTO);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFor>())).ReturnsAsync(applicationFor);


            //Act
            var result = await _createApplicationFor.CreateAsync(createRequest);

            //Assert
            result.For.Should().Be("newfor");
        }



        [Fact]
        public async Task CreateApplication_ValidApplicationForRequest_ReturnApplicationForDTO()
        {
            //Arrange
            CreateApplicationForRequest createRequest = _fixture.Create<CreateApplicationForRequest>();

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ApplicationFor, bool>>>()))
                                    .ReturnsAsync(null as ApplicationFor);

            ApplicationFor applicationFor = new() { Id = 1973, For = createRequest.For };

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFor>())).ReturnsAsync(applicationFor);

            _mapper.Setup(temp => temp.Map<ApplicationFor>(It.IsAny<CreateApplicationForRequest>())).Returns(applicationFor);

            ApplicationForDTO applicationForDTO = new ApplicationForDTO()
            {
                Id = applicationFor.Id,
                For = applicationFor.For
            };
            _mapper.Setup(temp => temp.Map<ApplicationForDTO>(It.IsAny<ApplicationFor>())).Returns(applicationForDTO);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ApplicationFor>())).ReturnsAsync(applicationFor);



            ApplicationForDTO expected = new() { Id = applicationFor.Id, For = applicationFor.For };

            //Act          
            var result = await _createApplicationFor.CreateAsync(createRequest);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
