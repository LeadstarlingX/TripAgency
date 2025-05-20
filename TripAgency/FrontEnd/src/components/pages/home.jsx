import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './home.css';
import api from '../../api'; // Adjust this if needed

const Home = () => {
    const navigate = useNavigate();
    const [cars, setCars] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    const handleLogout = () => {
        localStorage.removeItem('token');
        window.dispatchEvent(new Event('storage'));
        navigate('/login', { replace: true });
    };

    useEffect(() => {
        const fetchCars = async () => {
            try {
                const response = await api.get('/Car/GetAllCars');
                setCars(response.data);
            } catch (err) {
                console.log('Failed to fetch cars:' + err);
                setError('Failed to load cars.');
            } finally {
                setLoading(false);
            }
        };

        fetchCars();
    }, []);

    const handleBook = (carId) => {
        navigate(`/book/${carId}`);
    };

    return (
        <div className="home-container">
            <div className="header">
                <h1>Available Cars</h1>
                <button onClick={handleLogout} className="logout-btn">
                    Logout
                </button>
            </div>

            {loading ? (
                <p>Loading cars...</p>
            ) : error ? (
                <p className="error">{error}</p>
            ) : (
                <div className="car-grid">
                    {cars.map((car) => (
                        <div key={car.id} className="car-card">
                            <img src={car.image} alt={car.model} className="car-image" />
                            <h3>{car.model}</h3>
                            <p><strong>Capacity:</strong> {car.capacity}</p>
                            <p><strong>Color:</strong> {car.color}</p>
                            <p><strong>Status:</strong> {car.status}</p>
                            <p><strong>Price/Hour:</strong> ${car.pricePerHour}</p>
                            <p><strong>Price/Day:</strong> ${car.pricePerDay}</p>
                            <p><strong>MBW:</strong> {car.mbw}</p>
                            <button onClick={() => handleBook(car.id)} className="book-btn">
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
