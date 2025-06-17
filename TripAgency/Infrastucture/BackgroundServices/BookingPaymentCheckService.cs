using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.IReositosy;
using Domain.Entities.ApplicationEntities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundServices
{
    public class BookingPaymentCheckService : BackgroundService
    {
        private readonly ILogger<BookingPaymentCheckService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BookingPaymentCheckService(
            ILogger<BookingPaymentCheckService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Booking payment check service started at: {time}", DateTimeOffset.Now);
                    
                    await CheckAndCancelIncompleteBookingsAsync();
                    
                    _logger.LogInformation("Booking payment check service completed at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking booking payments");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task CheckAndCancelIncompleteBookingsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            
            var bookingRepository = scope.ServiceProvider.GetRequiredService<IAppRepository<Booking>>();
            var carRepository = scope.ServiceProvider.GetRequiredService<IAppRepository<Car>>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<Application.IUnitOfWork.IAppUnitOfWork>();

            try
            {
                // Get all active bookings that haven't started yet using FindWithComplexIncludes
                var currentTime = DateTime.UtcNow;
                var activeBookingsQuery = bookingRepository.FindWithComplexIncludes(
                    predicate: b => b.Status == BookingStatusEnum.Pending || 
                                   b.Status == BookingStatusEnum.Confirmed ||
                                   b.Status == BookingStatusEnum.InProgress,
                    includeExpression: q => q.Include(b => b.Payments)
                                             .Include(b => b.CarBooking)
                                             .ThenInclude(cb => cb!.Car));

                var activeBookings = await activeBookingsQuery.ToListAsync();

                int cancelledCount = 0;
                int carsMadeAvailable = 0;

                foreach (var booking in activeBookings)
                {
                    var timeUntilStart = booking.StartDateTime - currentTime;
                    var tenMinutes = TimeSpan.FromMinutes(10);
                    
                    if (timeUntilStart <= tenMinutes)
                    {
                        // Check if booking has any payments
                        if (!booking.Payments.Any())
                        {
                            _logger.LogWarning("Booking {BookingId} has no payments associated", booking.Id);
                            continue;
                        }

                        var totalAmountDue = booking.Payments.Sum(p => p.AmountDue);
                        var totalAmountPaid = booking.Payments.Sum(p => p.AmountPaid);
                        
                        if (totalAmountDue != totalAmountPaid)
                        {
                            booking.Status = BookingStatusEnum.Cancelled;
                            await bookingRepository.UpdateAsync(booking);
                            cancelledCount++;

                            _logger.LogInformation("Cancelled booking {BookingId} due to incomplete payment. AmountDue: {AmountDue}, AmountPaid: {AmountPaid}, TimeUntilStart: {TimeUntilStart}", 
                                booking.Id, totalAmountDue, totalAmountPaid, timeUntilStart);
                            
                            if (booking.BookingType == BookingTypes.CarBooking && booking.CarBooking?.Car != null)
                            {
                                var car = booking.CarBooking.Car;
                                car.CarStatus = CarStatusEnum.Available;
                                await carRepository.UpdateAsync(car);
                                carsMadeAvailable++;

                                _logger.LogInformation("Made car {CarId} available again after cancelling booking {BookingId}", 
                                    car.Id, booking.Id);
                            }
                        }
                    }
                }

                // Save all changes
                await unitOfWork.SaveChangesAsync();

                if (cancelledCount > 0 || carsMadeAvailable > 0)
                {
                    _logger.LogInformation("Payment check completed: {CancelledCount} bookings cancelled, {CarsMadeAvailable} cars made available", 
                        cancelledCount, carsMadeAvailable);
                }
                else
                {
                    _logger.LogInformation("Payment check completed: No action needed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking and cancelling incomplete bookings");
                throw;
            }
        }
    }
} 