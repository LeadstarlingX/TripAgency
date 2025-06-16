using Application.Common;
using Application.DTOs.Trip;
using Application.IApplicationServices.Trip;
using Application.IReositosy;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.Trip
{
    public class TripService : ITripService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Trip> _tripRepository;
        private readonly IMapper _mapper;

        public TripService(IAppRepository<Domain.Entities.ApplicationEntities.Trip> tripRepository, IMapper mapper)
        {
            _tripRepository = tripRepository;
            _mapper = mapper;
        }

        public async Task<TripDto> GetTripByIdAsync(BaseDto<int> dto)
        {
            var trip = (await _tripRepository.FindAsync(t => t.Id == dto.Id)).FirstOrDefault();
            if (trip == null)
                throw new KeyNotFoundException($"Trip with ID {dto.Id} not found.");
            return _mapper.Map<TripDto>(trip);
        }

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = (await _tripRepository.GetAllAsync()).ToList();
            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }

        public async Task<TripDto> CreateTripAsync(CreateTripDto dto)
        {
            var tripEntity = _mapper.Map<Domain.Entities.ApplicationEntities.Trip>(dto);
            var newTrip = await _tripRepository.InsertAsync(tripEntity);
            return _mapper.Map<TripDto>(newTrip);
        }

        public async Task<TripDto> UpdateTripAsync(UpdateTripDto dto)
        {
            var existingTrip = (await _tripRepository.FindAsync(t => t.Id == dto.Id)).FirstOrDefault();
            if (existingTrip == null)
                throw new KeyNotFoundException($"Trip with ID {dto.Id} not found.");

            _mapper.Map(dto, existingTrip);
            await _tripRepository.UpdateAsync(existingTrip);
            return _mapper.Map<TripDto>(existingTrip);
        }

        public async Task DeleteTripAsync(BaseDto<int> dto)
        {
            var tripToDelete = (await _tripRepository.FindAsync(t => t.Id == dto.Id)).FirstOrDefault();
            if (tripToDelete == null)
                throw new KeyNotFoundException($"Trip with ID {dto.Id} not found.");
            await _tripRepository.RemoveAsync(tripToDelete);
        }
        public async Task<IEnumerable<TripDto>> GetTripsByFilterAsync(TripFilter? filter)
        {
            var query = _tripRepository.GetAll();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    query = query.Where(t => t.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(filter.Description))
                {
                    query = query.Where(t => t.Description.Contains(filter.Description, StringComparison.OrdinalIgnoreCase));
                }

                if (filter.IsAvailable.HasValue)
                {
                    query = query.Where(t => t.IsAvailable == filter.IsAvailable.Value);
                }

                if (filter.IsPrivate.HasValue)
                {
                    query = query.Where(t => t.IsPrivate == filter.IsPrivate.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.Slug))
                {
                    query = query.Where(t => t.Slug.Contains(filter.Slug, StringComparison.OrdinalIgnoreCase));
                }
            }

            var trips = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }
    }
}
