using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IAppointments;
using Microsoft.Extensions.Logging;
using ModelDTO.Appointments;
using Models.Tests;
using System.Linq.Expressions;

namespace Services.AppointmentsService
{
    public class GetAllAppointmentsService : IGetAllAppointmentsService
    {
        private readonly IGetAllRepository<Appointment> _getAllRepository;
        private readonly ILogger<AppointmentDTO> _logger;
        private readonly IMapper _mapper;

        public GetAllAppointmentsService(IGetAllRepository<Appointment> getAllRepository,
            ILogger<AppointmentDTO> logger,
            IMapper mapper)
        {
            _getAllRepository = getAllRepository;
            this._logger = logger;
            _mapper = mapper;
        }



        public async Task<List<AppointmentDTO>> GetAllAsync()
        {
            _logger.LogInformation($"{this.GetType().Name} GetAllAsync");

            var lst = await _getAllRepository.GetAllAsync();

            var lstDTO = lst.Count > 0
                ? lst.Select(x => _mapper.Map<AppointmentDTO>(x)).ToList()
                : null;

            return lstDTO;
        }

        public async Task<IQueryable<AppointmentDTO>> GetAllAsync(Expression<Func<Appointment, bool>> predicate)
        {
            _logger.LogInformation($"{this.GetType().Name} GetAllAsync By Expression.");

            var lst = await _getAllRepository.GetAllAsync(predicate);

            var lstDTO = lst.Select(x => _mapper.Map<AppointmentDTO>(x)).AsQueryable();

            return lstDTO;
        }


    }
}
