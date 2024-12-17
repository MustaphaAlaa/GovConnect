using AutoFixture;
using AutoMapper;
using Azure.Core;
using FluentAssertions;
using IRepository;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;
using Moq;
using Services.ApplicationServices.For;
using System.Linq.Expressions;
using Web.Mapper;


namespace GovConnect_Tests.ApplicationServices
{
    public class CreateServiceCategoryServiceTest
    {

        private readonly IFixture _fixture;
        private readonly Mock<IMapper> _mapper;

        private readonly ICreateServiceCategory _iCreateServiceCategory;
        private readonly ICreateRepository<ServiceCategory> _createRepository;
        private readonly Mock<ICreateRepository<ServiceCategory>> _MockcreateRepository;


        private readonly IGetRepository<ServiceCategory> _getRepository;
        private readonly Mock<IGetRepository<ServiceCategory>> _MockGetRepository;
        public CreateServiceCategoryServiceTest()
        {

            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _MockcreateRepository = new Mock<ICreateRepository<ServiceCategory>>();
            _MockGetRepository = new Mock<IGetRepository<ServiceCategory>>();

            _iCreateServiceCategory =
                new CreateServiceCategoryService(_MockcreateRepository.Object,
                                                _MockGetRepository.Object,
                                                _mapper.Object);
        }

        #region Validation Test


        [Fact]
        public async Task CreateAsync_CreateApplicationForRequestIsNull_ThrowArgumentNullException()
        {
            //Act
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(null);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropertyisNull_ThrowArgumentException()
        {
            //Arrange
            CreateServiceCategoryRequest createRequest = new() { Category = null };
            //Act
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropertyisEmpty_ThrowArgumentException()
        {
            //Arrange
            CreateServiceCategoryRequest createRequest = new() { Category = "" };
            //Act
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(createRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_ForPropIsAlreadyExist_ThrowArgumentException()
        {
            //Arrange
            CreateServiceCategoryRequest createRequest = new() { Category = "New" };

            ServiceCategory serviceCategory = new() { ServiceCategoryId = 1973, Category = "New" };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                                    .ReturnsAsync(serviceCategory);

            //Act          
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(createRequest);

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
            CreateServiceCategoryRequest creatRequest = _fixture.Create<CreateServiceCategoryRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                .ReturnsAsync(null as ServiceCategory);

            _mapper.Setup(temp =>
                          temp.Map<ServiceCategory>(It.IsAny<CreateServiceCategoryRequest>()))
                         .Returns(null as ServiceCategory);
            //Act
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        [Fact]
        public async Task CreateAsync_MappingFromCreateApplicationForToDTO_ThrwosAutoMapperMappingException()
        {
            //Arrange
            CreateServiceCategoryRequest creatRequest = _fixture.Create<CreateServiceCategoryRequest>();

            _MockGetRepository.Setup(temp => temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                .ReturnsAsync(null as ServiceCategory);

            _mapper.Setup(temp =>
                          temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>()))
                         .Returns(null as ServiceCategoryDTO);
            //Act
            Func<Task> action = async () => await _iCreateServiceCategory.CreateAsync(creatRequest);

            //Assert
            await action.Should().ThrowAsync<AutoMapperMappingException>();
        }


        #endregion


        [Fact]
        public async Task CreateApplication_RequestWithSpacingAndUpperCase_ReturnTrimmingAndLowwerCase()
        {
            //Arrange
            CreateServiceCategoryRequest createRequest = new() { Category = "  NewFor  " };

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                                    .ReturnsAsync(null as ServiceCategory);

            ServiceCategory serviceCategory = new ServiceCategory()
            {
                ServiceCategoryId = 1888,
                Category = createRequest.Category.Trim().ToLower()
            };

            _mapper.Setup(temp => temp.Map<ServiceCategory>(It.IsAny<CreateServiceCategoryRequest>())).Returns(serviceCategory);

            ServiceCategoryDTO serviceCategoryDto = new ServiceCategoryDTO()
            {
                ServiceCategoryId = serviceCategory.ServiceCategoryId,
                Category = serviceCategory.Category
            };
            _mapper.Setup(temp => temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>())).Returns(serviceCategoryDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceCategory>())).ReturnsAsync(serviceCategory);


            //Act
            var result = await _iCreateServiceCategory.CreateAsync(createRequest);

            //Assert
            result.Category.Should().Be("newfor");
        }



        [Fact]
        public async Task CreateApplication_ValidApplicationForRequest_ReturnApplicationForDTO()
        {
            //Arrange
            CreateServiceCategoryRequest createRequest = _fixture.Create<CreateServiceCategoryRequest>();

            _MockGetRepository.Setup(temp =>
                                     temp.GetAsync(It.IsAny<Expression<Func<ServiceCategory, bool>>>()))
                                    .ReturnsAsync(null as ServiceCategory);

            ServiceCategory serviceCategory = new() { ServiceCategoryId = 1973, Category = createRequest.Category };

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceCategory>())).ReturnsAsync(serviceCategory);

            _mapper.Setup(temp => temp.Map<ServiceCategory>(It.IsAny<CreateServiceCategoryRequest>())).Returns(serviceCategory);

            ServiceCategoryDTO serviceCategoryDto = new ServiceCategoryDTO()
            {
                ServiceCategoryId = serviceCategory.ServiceCategoryId,
                Category = serviceCategory.Category
            };
            _mapper.Setup(temp => temp.Map<ServiceCategoryDTO>(It.IsAny<ServiceCategory>())).Returns(serviceCategoryDto);

            _MockcreateRepository.Setup(temp => temp.CreateAsync(It.IsAny<ServiceCategory>())).ReturnsAsync(serviceCategory);



            ServiceCategoryDTO expected = new() { ServiceCategoryId = serviceCategory.ServiceCategoryId, Category = serviceCategory.Category };

            //Act          
            var result = await _iCreateServiceCategory.CreateAsync(createRequest);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
