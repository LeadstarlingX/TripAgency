import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api';
import './addCar.css';

const AddCar = () => {
    const navigate = useNavigate();
    const standardColors = [
        'Black', 'White', 'Silver', 'Gray', 'Red',
        'Blue', 'Green', 'Yellow', 'Orange', 'Brown',
        'Beige', 'Gold', 'Navy', 'Maroon', 'Purple'
    ];

    const [formData, setFormData] = useState({
        model: '',
        capacity: 0,
        color: '',
        categoryId: 0,
        carStatus: 0,
        pph: 0,
        ppd: 0,
        mbw: 0
    });
    const [categories, setCategories] = useState([]);
    const [selectedFile, setSelectedFile] = useState(null);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const [isUploading, setIsUploading] = useState(false);
    const [loadingCategories, setLoadingCategories] = useState(true);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await api.get('/Category/GetAllCategories');
                setCategories(response.data.Data || []);
            } catch (err) {
                console.error('Failed to fetch categories:', err);
                setError('Failed to load categories');
            } finally {
                setLoadingCategories(false);
            }
        };

        fetchCategories();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name === 'model' || name === 'color'
                ? value
                : parseInt(value)
        }));
    };

    const handleFileChange = (e) => {
        setSelectedFile(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        if (!selectedFile) {
            setError('Please upload a car image');
            return;
        }

        setIsUploading(true);

        try {
            const formDataToSend = new FormData();

            // Append the image file
            formDataToSend.append('image', selectedFile);

            // Append all other form data as JSON string
            formDataToSend.append('carData', JSON.stringify(formData));

            const response = await api.post('/Car/CreateCar', formDataToSend, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            if (response.data.Success) {
                setSuccess('Car added successfully!');
                setTimeout(() => navigate('/'), 2000);
            } else {
                setError(response.data.Message || 'Failed to add car');
            }
        } catch (err) {
            console.error('Error:', err);
            setError(err.message || 'Failed to add car');
        } finally {
            setIsUploading(false);
        }
    };

    return (
        <div className="add-car-container">
            <h2>Add New Car</h2>
            {error && <p className="error">{error}</p>}
            {success && <p className="success">{success}</p>}

            <form onSubmit={handleSubmit} className="two-column-form">
                <div className="form-column">
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
                            min="1"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Color:</label>
                        <select
                            name="color"
                            value={formData.color}
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
                                name="categoryId"
                                value={formData.categoryId}
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
                            name="carStatus"
                            value={formData.carStatus}
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
                            name="pph"
                            value={formData.pph}
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
                            name="ppd"
                            value={formData.ppd}
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
                            name="mbw"
                            value={formData.mbw}
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
                        accept="image/*"
                        onChange={handleFileChange}
                        required
                    />
                    <p className="file-hint">
                        {selectedFile
                            ? `Selected: ${selectedFile.name}`
                            : 'No file selected'}
                    </p>
                </div>

                <div className="form-actions full-width">
                    <button
                        type="submit"
                        className="submit-btn"
                        disabled={isUploading}
                    >
                        {isUploading ? 'Uploading...' : 'Add Car'}
                    </button>
                    <button
                        type="button"
                        className="cancel-btn"
                        onClick={() => navigate('/')}
                    >
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
};

export default AddCar;