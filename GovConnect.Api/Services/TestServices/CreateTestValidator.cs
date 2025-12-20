using AutoMapper;
using DataConfigurations.TVFs.ITVFs;
using IServices.IValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TestServices
{
    public class CreateTestValidator : ICreateTestValidator
    {

        private readonly ITVF_GetTestResultForABookingId _tVF_GetTestResultForABookingId;
        private readonly ILogger<Test> _logger;
        private readonly IMapper _mapper;
        public CreateTestValidator(ITVF_GetTestResultForABookingId tVF_GetTestResultForABookingId, ILogger<Test> logger, IMapper mapper)
        {
            _tVF_GetTestResultForABookingId = tVF_GetTestResultForABookingId;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> IsValid(CreateTestRequest request)
        {
            TestDTO? test = await _tVF_GetTestResultForABookingId.GetTestResultForABookingId(request.BookingId);
            return test == null;
        }
    }
}
