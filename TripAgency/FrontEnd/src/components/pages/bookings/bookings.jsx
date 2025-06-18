import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../../api';
import './bookings.css';

const Bookings = () => {
    const navigate = useNavigate();
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [filters, setFilters] = useState({
        myBookings: false,
        confirmedOnly: false
    });

    // Status enum mapping
    const statusEnum = {
        1: 'Pending',
        2: 'Confirmed',
        3: 'In Progress',
        4: 'Completed',
        5: 'Cancelled',
        6: 'No Show'
    };

    // Booking type mapping
    const bookingTypeMap = {
        'Car Rental': 'Car Rental',
        'CarBooking': 'Car Booking',
        'Event Booking': 'Event Booking'
    };

    useEffect(() => {
        const fetchBookings = async () => {
            try {
                setLoading(true);
                let employeeId = null;
                if (filters.myBookings) {
                    const token = localStorage.getItem('token');
                    if (token) {
                        const payload = JSON.parse(atob(token.split('.')[1]));
                        employeeId = payload.employeeId || payload.userId;
                    }
                }

                const status = filters.confirmedOnly ? 2 : null;

                const response = await api.get('/Booking/GetAllBookingsByFilter', {
                    params: {
                        EmployeeId: employeeId,
                        Status: status
                    }
                });

                setBookings(response.data.Data || []);
            } catch (err) {
                console.error('Failed to fetch bookings:', err);
                setError('Failed to load bookings');
            } finally {
                setLoading(false);
            }
        };

        fetchBookings();
    }, [filters]);

    const toggleFilter = (filterName) => {
        setFilters(prev => ({
            ...prev,
            [filterName]: !prev[filterName]
        }));
    };

    const handlePayment = (bookingId) => {
        navigate(`/payment`, { state: { bookingId: bookingId } });
    };

    // Helper function to determine if payment button should be shown
    const shouldShowPaymentButton = (booking) => {
        return booking.BookingType.includes('Car') &&
            (booking.Status === 2 || booking.Status === 3);
    };

    return (
        <div className="bookings-container">
            <div className="bookings-header">
                <h1>Bookings</h1>
                <div className="filter-options">
                    <button
                        className={!filters.myBookings && !filters.confirmedOnly ? 'active' : ''}
                        onClick={() => setFilters({ myBookings: false, confirmedOnly: false })}
                    >
                        All Bookings
                    </button>
                    <button
                        className={filters.myBookings ? 'active' : ''}
                        onClick={() => toggleFilter('myBookings')}
                    >
                        My Bookings
                    </button>
                    <button
                        className={filters.confirmedOnly ? 'active' : ''}
                        onClick={() => toggleFilter('confirmedOnly')}
                    >
                        Confirmed Only
                    </button>
                </div>
            </div>

            {loading ? (
                <p>Loading bookings...</p>
            ) : error ? (
                <p className="error">{error}</p>
            ) : (
                <div className="bookings-list">
                    {bookings.map(booking => (
                        <div key={booking.Id} className="booking-card">
                            <div className="booking-info">
                                <h3>Booking #{booking.Id}</h3>
                                <p>Type: {bookingTypeMap[booking.BookingType] || booking.BookingType}</p>
                                <p>Customer: {booking.CustomerName}</p>
                                <p>Dates: {new Date(booking.StartDateTime).toLocaleDateString()} - {new Date(booking.EndDateTime).toLocaleDateString()}</p>
                                <p>Status: {statusEnum[booking.Status] || booking.Status}</p>
                                <p>Passengers: {booking.NumOfPassengers}</p>
                                {booking.EmployeeId > 0 && <p>Assigned Employee: {booking.EmployeeId}</p>}
                            </div>
                            {shouldShowPaymentButton(booking) && (
                                <button
                                    className="payment-btn"
                                    onClick={() => handlePayment(booking.Id)}
                                >
                                    Make Payment
                                </button>
                            )}
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Bookings;