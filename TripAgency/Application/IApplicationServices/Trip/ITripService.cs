using Application.Common;
using Application.DTOs.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Trip
{
    public interface ITripService
    {
        Task<TripDto> GetTripByIdAsync(BaseDto<int> dto);
        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<IEnumerable<TripDto>> GetTripsByFilterAsync(TripFilter? filter);
        Task<TripDto> CreateTripAsync(CreateTripDto dto);
        Task<TripDto> UpdateTripAsync(UpdateTripDto dto);
        Task DeleteTripAsync(BaseDto<int> dto);
    }
}
