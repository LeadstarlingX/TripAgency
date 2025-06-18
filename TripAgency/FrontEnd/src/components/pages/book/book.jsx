import React, { useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import api from '../../../api';
import './book.css'; // Import the CSS file

const Book = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const params = useParams();

    // Get carId from URL param or navigation state
    const token = localStorage.getItem('token');
    const carId = params.carId || location.state?.carId;
    const customerId = jwtDecode(token).nameid;

    // Form state
    const [pickupLocation, setPickupLocation] = useState('');
    const [dropoffLocation, setDropoffLocation] = useState('');
    const [startDateTime, setStartDateTime] = useState('');
    const [endDateTime, setEndDateTime] = useState('');
    const [numOfPassengers, setNumOfPassengers] = useState(1);
    const [withDriver, setWithDriver] = useState(false);

    // Validation errors
    const [errors, setErrors] = useState({});

    // Validate form inputs
    const validate = () => {
        const errs = {};

        if (!pickupLocation.trim()) {
            errs.pickupLocation = 'Pickup location is required';
        }
        if (!dropoffLocation.trim()) {
            errs.dropoffLocation = 'Dropoff location is required';
        }
        if (!startDateTime) {
            errs.startDateTime = 'Start date/time is required';
        } else if (new Date(startDateTime) < new Date()) {
            errs.startDateTime = 'Start date/time cannot be in the past';
        }
        if (!endDateTime) {
            errs.endDateTime = 'End date/time is required';
        } else if (new Date(endDateTime) <= new Date(startDateTime)) {
            errs.endDateTime = 'End date/time must be after start date/time';
        }
        if (!numOfPassengers || numOfPassengers < 1) {
            errs.numOfPassengers = 'Number of passengers must be at least 1';
        }

        setErrors(errs);
        return Object.keys(errs).length === 0;
    };

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validate()) return;

        if (!carId) {
            alert('Car ID is missing. Cannot proceed with booking.');
            return;
        }
        if (!customerId) {
            alert('User not authenticated. Please login again.');
            navigate('/login');
            return;
        }

        const bookingData = {
            customerId: parseInt(customerId, 10) || 0,
            carId: parseInt(carId, 10),
            pickupLocation: pickupLocation.trim(),
            dropoffLocation: dropoffLocation.trim(),
            startDateTime: new Date(startDateTime).toISOString(),
            endDateTime: new Date(endDateTime).toISOString(),
            numOfPassengers: parseInt(numOfPassengers, 10),
            withDriver,
        };

        try {
            await api.post('/CarBooking/CreateCarBooking', bookingData);
            alert('Booking successful!');
            navigate('/home');
        } catch (error) {
            console.error('Booking failed:', error);
            alert('Booking failed. Please try again.');
        }
    };

    return (
        <div className="booking-container">
            <form onSubmit={handleSubmit} noValidate>
                <div className="form-group">
                    <label>Pickup Location</label>
                    <input
                        type="text"
                        value={pickupLocation}
                        onChange={(e) => setPickupLocation(e.target.value)}
                        className={errors.pickupLocation ? 'error' : ''}
                    />
                    {errors.pickupLocation && <small className="error-text">{errors.pickupLocation}</small>}
                </div>

                <div className="form-group">
                    <label>Dropoff Location</label>
                    <input
                        type="text"
                        value={dropoffLocation}
                        onChange={(e) => setDropoffLocation(e.target.value)}
                        className={errors.dropoffLocation ? 'error' : ''}
                    />
                    {errors.dropoffLocation && <small className="error-text">{errors.dropoffLocation}</small>}
                </div>

                <div className="form-group">
                    <label>Start Date & Time</label>
                    <input
                        type="datetime-local"
                        value={startDateTime}
                        onChange={(e) => setStartDateTime(e.target.value)}
                        className={errors.startDateTime ? 'error' : ''}
                    />
                    {errors.startDateTime && <small className="error-text">{errors.startDateTime}</small>}
                </div>

                <div className="form-group">
                    <label>End Date & Time</label>
                    <input
                        type="datetime-local"
                        value={endDateTime}
                        onChange={(e) => setEndDateTime(e.target.value)}
                        className={errors.endDateTime ? 'error' : ''}
                    />
                    {errors.endDateTime && <small className="error-text">{errors.endDateTime}</small>}
                </div>

                <div className="form-group">
                    <label>Number of Passengers</label>
                    <input
                        type="number"
                        min="1"
                        value={numOfPassengers}
                        onChange={(e) => setNumOfPassengers(e.target.value)}
                        className={errors.numOfPassengers ? 'error' : ''}
                    />
                    {errors.numOfPassengers && <small className="error-text">{errors.numOfPassengers}</small>}
                </div>

                <div className="form-group checkbox-group">
                    <label>
                        <input
                            type="checkbox"
                            checked={withDriver}
                            onChange={(e) => setWithDriver(e.target.checked)}
                        />
                        With Driver
                    </label>
                </div>

                <button type="submit" className="book-submit-btn">
                    Submit Booking
                </button>
            </form>
        </div>
    );
};

export default Book;