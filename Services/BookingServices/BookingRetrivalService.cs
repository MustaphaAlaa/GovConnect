using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IValidators.BookingValidators;
using Microsoft.Extensions.Logging;
using ModelDTO.BookingDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookingServices
{


    public class BookingRetrivalService : IBookingRetrieveService
    {
        private readonly IGetRepository<Booking> _getRepository;
        private readonly ILogger<BookingRetrivalService> _logger;
        private readonly IMapper _mapper;

        public BookingRetrivalService(IGetRepository<Booking> getRepository, ILogger<BookingRetrivalService> logger, IMapper mapper)
        {
            _getRepository = getRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BookingDTO> GetByAsync(Expression<Func<Booking, bool>> predicate)
        {
            _logger.LogInformation($"{this.GetType().Name} -- GetByAsync Expression");

            var booking = await _getRepository.GetAsync(predicate);
            var bookingDTO = _mapper.Map<BookingDTO>(booking);
            return bookingDTO;

        }
    }
}
