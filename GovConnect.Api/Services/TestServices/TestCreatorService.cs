using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.ITests.ITest;
using IServices.IValidators;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using Models.Tests.Enums;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.TestServices
{
    public class TestCreatorService : ITestCreationService
    {


        private readonly ICreateRepository<Test> _createRepository;
        private readonly ICreateTestValidator _createValidator;
        private readonly ILogger<TestCreatorService> _logger;
        private readonly IMapper _mapper;

        public TestCreatorService(ICreateRepository<Test> createRepository,
            ICreateTestValidator createValidator,
            ILogger<TestCreatorService> logger,
            IMapper mapper)
        {
            _createRepository = createRepository;
            _createValidator = createValidator;
            _logger = logger;
            _mapper = mapper;
        }




        public event Func<object?, TestDTO, Task> TestCreated;
        public event Func<object?, TestDTO, Task> OnFinalTestPassed;


        public async Task<TestDTO> CreateAsync(CreateTestRequest entity)
        {

            _logger.LogInformation($"{this.GetType().Name} -- CreateAsync ");

            bool isValid = await _createValidator.IsValid(entity);

            if (!isValid)
            {
                _logger.LogError($"{this.GetType().Name} -- CreateAsync -- invalid reqeust.");
                throw new AlreadyExistException("the booking id is used before");
            }
            Test testReq = _mapper.Map<Test>(entity);

            try
            {
                _logger.LogInformation($"{this.GetType().Name} -- CreateAsync -- creating request is Valid.");

                var test = await _createRepository.CreateAsync(testReq);

                //TestDTO testDTO = _mapper.Map<TestDTO>(test);

                var testDTO = new TestDTO
                {
                    TestId = test.TestId,
                    BookingId = test.BookingId,
                    TestResult = test.TestResult,
                    Notes = test.Notes,
                };

                TestCreated?.Invoke(this, testDTO);

                _logger.LogInformation($"{this.GetType().Name} -- CreateAsync -- test is created.");

                if(testDTO.TestTypeId == (int)EnTestTypes.Practical_Street && testDTO.TestResult  )
                {
                    _logger.LogInformation($"{this.GetType().Name} -- CreateAsync -- final test is created and it is practical street and pass.");
                    OnFinalTestPassed?.Invoke(this, testDTO);
                     
                }


                return testDTO;
            }

            catch (System.Exception ex)
            {
                _logger.LogInformation($"{this.GetType().Name} -- CreateAsync -- exception is throwed.");

                throw new System.Exception(ex.Message, ex);
            }
        }
    }
}
