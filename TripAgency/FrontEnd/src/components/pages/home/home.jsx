import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './home.css';
import api from '../../../api';

const Home = () => {
    const navigate = useNavigate();
    const [cars, setCars] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [firstName, setFirstName] = useState('User');
    const [loadedImages, setLoadedImages] = useState({});

    const handleLogout = () => {
        localStorage.removeItem('token');
        window.dispatchEvent(new Event('storage'));
        navigate('/login', { replace: true });
    };

    const handleImageLoad = (carId) => {
        setLoadedImages(prev => ({ ...prev, [carId]: true }));
    };

    const handleImageError = (carId) => {
        setLoadedImages(prev => ({ ...prev, [carId]: false }));
    };

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            try {
                const payload = JSON.parse(atob(token.split('.')[1]));
                setFirstName(payload.unique_name || 'User');
            } catch {
                setFirstName('User');
            }
        }

        const fetchCars = async () => {
            try {
                const response = await api.get('/Car/FilterCars?Status=0'); // Only get available cars
                setCars(response.data.Data || []);
            } catch (err) {
                console.log('Failed to fetch cars:', err);
                setError('Failed to load cars.');
            } finally {
                setLoading(false);
            }
        };

        fetchCars();
    }, []);

    const handleBook = (carId) => {
        navigate('/book', { state: { carId } });
    };

    const getImageUrl = (imageName) => {
        return `https://localhost:7070/images/cars/${imageName}`;
    };

    return (
        <div className="home-container">
            <div className="header">
                <h1>Available Cars</h1>
                <div className="user-info">
                    <span className="welcome">Welcome, {firstName}</span>
                    <button
                        onClick={() => navigate('/bookings')}
                        className="action-btn bookings-btn"
                    >
                        Bookings
                    </button>
                    <button
                        onClick={() => navigate('/payments')}
                        className="action-btn payments-btn"
                    >
                        Payments
                    </button>
                    <button
                        onClick={() => navigate('/add-car')}
                        className="action-btn add-car-btn"
                    >
                        Add Car
                    </button>
                    <button
                        onClick={handleLogout}
                        className="action-btn logout-btn"
                    >
                        Logout
                    </button>
                </div>
            </div>

            {loading ? (
                <p>Loading cars...</p>
            ) : error ? (
                <p className="error">{error}</p>
            ) : cars.length === 0 ? (
                <p>No available cars at the moment.</p>
            ) : (
                <div className="car-grid">
                    {cars.map((car) => (
                        <div key={car.Id} className="car-card">
                            <div className="image-container">
                                {loadedImages[car.Id] !== false && (
                                    <img
                                        src={getImageUrl(car.Image)}
                                        alt={car.Model}
                                        className="car-image"
                                        onLoad={() => handleImageLoad(car.Id)}
                                        onError={() => handleImageError(car.Id)}
                                        style={{ display: loadedImages[car.Id] ? 'block' : 'none' }}
                                    />
                                )}
                            </div>
                            <div className="car-info">
                                <div className="car-info-line"><strong>Model:</strong> {car.Model}</div>
                                <div className="car-info-line"><strong>Capacity:</strong> {car.Capacity}</div>
                                <div className="car-info-line"><strong>Color:</strong> {car.Color}</div>
                                <div className="car-info-line"><strong>Price/Hour:</strong> ${car.Pph}</div>
                                <div className="car-info-line"><strong>Price/Day:</strong> ${car.Ppd}</div>
                                <div className="car-info-line"><strong>MBW:</strong> {car.Mbw}</div>
                            </div>
                            <button onClick={() => handleBook(car.Id)} className="book-btn">
                                Book Now
                            </button>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Home;