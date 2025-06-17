import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api';
import './addCar.css'; // Optional styling file

const AddCar = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        Model: '',
        Capacity: 0,
        Color: '',
        CategoryId: 0,
        CarStatus: 0,
        Pph: 0,
        Ppd: 0,
        Mbw: 0
    });
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name === 'Model' || name === 'Color'
                ? value
                : parseInt(value)
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        try {
            const formDataToSend = new FormData();

            // Append the image file
            formDataToSend.append('ImageFile', selectedFile);

            // Append all form data with exact DTO property names
            Object.keys(formData).forEach(key => {
                formDataToSend.append(key, formData[key].toString());
            });

            const response = await api.post('/Car/CreateCar', formDataToSend, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            if (response.data.Success) {
                setSuccess('Car added successfully!');
                setTimeout(() => navigate('/'), 2000); // Redirect to home after 2 seconds
            } else {
                setError(response.data.Message || 'Failed to add car');
            }
        } catch (err) {
            console.error('Error:', err);
            setError(err.response?.data?.Message || err.message || 'Failed to add car');
        } finally {
            setIsUploading(false);
        }
    };

    return (
        <div className="add-car-container">
            <h2>Add New Car</h2>
            {error && <p className="error">{error}</p>}
            {success && <p className="success">{success}</p>}

            <form onSubmit={handleSubmit} className="two-column-form" encType="multipart/form-data">
                <div className="form-column">
                    <div className="form-group">
                        <label>Model:</label>
                        <input
                            type="text"
                            name="Model"
                            value={formData.Model}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Capacity:</label>
                        <input
                            type="number"
                            name="Capacity"
                            value={formData.Capacity}
                            onChange={handleChange}
                            min="1"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Color:</label>
                        <select
                            name="Color"
                            value={formData.Color}
                            onChange={handleChange}
                            required
                            className="color-select"
                        >
                            <option value="">Select a color</option>
                            {standardColors.map(color => (
                                <option key={color} value={color}>
                                    {color}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Category:</label>
                        {loadingCategories ? (
                            <p>Loading categories...</p>
                        ) : (
                            <select
                                name="CategoryId"
                                value={formData.CategoryId}
                                onChange={handleChange}
                                required
                            >
                                <option value="">Select a category</option>
                                {categories.map(category => (
                                    <option key={category.Id} value={category.Id}>
                                        {category.Title}
                                    </option>
                                ))}
                            </select>
                        )}
                    </div>
                </div>

                <div className="form-column">
                    <div className="form-group">
                        <label>Status:</label>
                        <select
                            name="CarStatus"
                            value={formData.CarStatus}
                            onChange={handleChange}
                            required
                        >
                            <option value={0}>Available</option>
                            <option value={1}>Unavailable</option>
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Price per Hour ($):</label>
                        <input
                            type="number"
                            name="Pph"
                            value={formData.Pph}
                            onChange={handleChange}
                            min="0"
                            step="0.01"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Price per Day ($):</label>
                        <input
                            type="number"
                            name="Ppd"
                            value={formData.Ppd}
                            onChange={handleChange}
                            min="0"
                            step="0.01"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Mileage (MBW):</label>
                        <input
                            type="number"
                            name="Mbw"
                            value={formData.Mbw}
                            onChange={handleChange}
                            min="0"
                            required
                        />
                    </div>
                </div>

                <div className="form-group image-upload full-width">
                    <label>Car Image (required):</label>
                    <input
                        type="file"
                        name="ImageFile"
                        accept="image/*"
                        onChange={handleFileChange}
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