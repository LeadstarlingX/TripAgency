import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../../api';
import './payments.css';

const Payments = () => {
    const navigate = useNavigate();
    const [payments, setPayments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [filter, setFilter] = useState('all'); // 'all', 'pending', 'complete', 'refund'

    // Status mapping
    const statusMap = {
        1: 'Pending',
        2: 'Complete',
        3: 'Refund',
    };

    const fetchPayments = async (statusFilter) => {
        try {
            setLoading(true);
            let endpoint = '/Payment/GetAllPayments';

            // Add status filter to the endpoint if not 'all'
            if (statusFilter !== 'all') {
                let statusValue;
                switch (statusFilter) {
                    case 'pending':
                        statusValue = 1;
                        break;
                    case 'complete':
                        statusValue = 2;
                        break;
                    case 'refund':
                        statusValue = 3;
                        break;
                    default:
                        statusValue = null;
                }

                if (statusValue !== null) {
                    endpoint += `?Status=${statusValue}`;
                }
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

    const handleProcessPayment = (paymentId) => {
        navigate('/payment', { state: { paymentId } });
    };

    return (
        <div className="payments-container">
            <div className="payments-header">
                <h1>Payments</h1>
                <div className="filter-options">
                    <button
                        className={filter === 'all' ? 'active' : ''}
                        onClick={() => setFilter('all')}
                    >
                        All Payments
                    </button>
                    <button
                        className={filter === 'pending' ? 'active' : ''}
                        onClick={() => setFilter('pending')}
                    >
                        Pending
                    </button>
                    <button
                        className={filter === 'complete' ? 'active' : ''}
                        onClick={() => setFilter('complete')}
                    >
                        Complete
                    </button>
                    <button
                        className={filter === 'refund' ? 'active' : ''}
                        onClick={() => setFilter('refund')}
                    >
                        Refund
                    </button>
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
                        <div key={payment.id} className="payment-card">
                            <div className="payment-info">
                                <h3>Payment #{payment.id}</h3>
                                <p>Amount: ${payment.amount}</p>
                                <p>Status: {statusMap[payment.status] || payment.status}</p>
                                <p>Date: {new Date(payment.date).toLocaleDateString()}</p>
                                <p>Method: {payment.method}</p>
                            </div>
                            {payment.status !== 2 && ( // Show button if not complete
                                <button
                                    className="process-btn"
                                    onClick={() => handleProcessPayment(payment.id)}
                                >
                                    {payment.status === 1 ? 'Process Payment' : 'Process Refund'}
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