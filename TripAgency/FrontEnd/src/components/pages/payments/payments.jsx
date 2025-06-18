import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../../api';
import './payments.css';

const Payments = () => {
    const navigate = useNavigate();
    const [payments, setPayments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [filter, setFilter] = useState('all');

    const statusMap = {
        0: 'Pending',
        1: 'Complete',
        2: 'Refund',
    };

    const fetchPayments = async (statusFilter) => {
        try {
            setLoading(true);
            let endpoint = '/Payment/GetPaymentsByFilter';

            if (statusFilter !== 'all') {
                const statusValue = {
                    pending: 0,
                    complete: 1,
                    refund: 2
                }[statusFilter];

                if (statusValue) endpoint += `?Status=${statusValue}`;
            }

            const response = await api.get(endpoint);
            setPayments(response.data.Data || []);
        } catch (err) {
            console.error('Failed to fetch payments:', err);
            setError('Failed to load payments');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPayments(filter);
    }, [filter]);

    const handlePaymentAction = (payment) => {
        if (payment.Status === 0) { // Pending payment
            navigate('/payment', { state: { paymentId: payment.Id } });
        } else if (payment.Status === 2) { // Refund
            navigate('/refund', { state: { paymentId: payment.Id } });
        }
    };

    return (
        <div className="payments-container">
            <div className="payments-header">
                <h1>Payments</h1>
                <div className="filter-options">
                    {['all', 'pending', 'complete', 'refund'].map((filterType) => (
                        <button
                            key={filterType}
                            className={filter === filterType ? 'active' : ''}
                            onClick={() => setFilter(filterType)}
                        >
                            {filterType.charAt(0).toUpperCase() + filterType.slice(1)} {filterType !== 'all' && 'Payments'}
                        </button>
                    ))}
                </div>
            </div>

            {loading ? (
                <p>Loading payments...</p>
            ) : error ? (
                <p className="error">{error}</p>
            ) : payments.length === 0 ? (
                <p>No payments found.</p>
            ) : (
                <div className="payments-list">
                    {payments.map(payment => (
                        <div key={payment.Id} className="payment-card">
                            <div className="payment-info">
                                <h3>Payment #{payment.Id}</h3>
                                <p>AmountPaid: ${payment.AmountPaid}</p>
                                <p>Status: {statusMap[payment.Status] || payment.Status}</p>
                                <p>Date: {new Date(payment.Date).toLocaleDateString()}</p>
                                <p>Notes: {payment.Notes}</p>
                            </div>
                            {payment.Status !== 1 && (
                                <button
                                    className={`action-btn ${payment.Status === 2 ? 'refund-btn' : 'payment-btn'}`}
                                    onClick={() => handlePaymentAction(payment)}
                                >
                                    {payment.Status === 1 ? 'Process Payment' : 'Process Refund'}
                                </button>
                            )}
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Payments;