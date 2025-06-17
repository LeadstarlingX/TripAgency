import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api';
import './addCar.css'; // Optional styling file

const AddCar = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        model: '',
        capacity: 0,
        color: '',
        image: '',
        categoryId: 0,
        carStatus: 0,
        pph: 0,
        ppd: 0,
        mbw: 0
    });
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name === 'model' || name === 'color' || name === 'image'
                ? value
                : parseInt(value)
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        try {
            const response = await api.post('/Car/CreateCar', formData);
            if (response.data.Success) {
                setSuccess('Car added successfully!');
                setTimeout(() => navigate('/'), 2000); // Redirect to home after 2 seconds
            } else {
                setError(response.data.Message || 'Failed to add car');
            }
        } catch (err) {
            console.error('Error adding car:', err);
            setError('Failed to connect to server');
        }
    };

    return (
        <div className="add-car-container">
            <h2>Add New Car</h2>
            {error && <p className="error">{error}</p>}
            {success && <p className="success">{success}</p>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Model:</label>
                    <input
                        type="text"
                        name="model"
                        value={formData.model}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Capacity:</label>
                    <input
                        type="number"
                        name="capacity"
                        value={formData.capacity}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Color:</label>
                    <input
                        type="text"
                        name="color"
                        value={formData.color}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Image URL:</label>
                    <input
                        type="text"
                        name="image"
                        value={formData.image}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Category ID:</label>
                    <input
                        type="number"
                        name="categoryId"
                        value={formData.categoryId}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Status (0-1):</label>
                    <input
                        type="number"
                        name="carStatus"
                        value={formData.carStatus}
                        onChange={handleChange}
                        min="0"
                        max="1"
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Price per Hour:</label>
                    <input
                        type="number"
                        name="pph"
                        value={formData.pph}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Price per Day:</label>
                    <input
                        type="number"
                        name="ppd"
                        value={formData.ppd}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Mileage (MBW):</label>
                    <input
                        type="number"
                        name="mbw"
                        value={formData.mbw}
                        onChange={handleChange}
                        required
                    />
                </div>

                <button type="submit" className="submit-btn">Add Car</button>
                <button
                    type="button"
                    className="cancel-btn"
                    onClick={() => navigate('/')}
                >
                    Cancel
                </button>
            </form>
        </div>
    );
};

export default AddCar;