import { useState, useContext, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { AuthContext } from '../../../AuthContext';
import api from '../../../api';
import './login.css';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const { isAuthenticated, setIsAuthenticated } = useContext(AuthContext);
    const navigate = useNavigate();

    // Monitor authentication changes
    useEffect(() => {
        console.log('isAuthenticated changed to:', isAuthenticated);
        if (isAuthenticated) {
            console.log('Redirecting to /home...');
            navigate('/home', { replace: true });
        }
    }, [isAuthenticated, navigate]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        setError('');

        try {
            console.log('Attempting login...'); // Debug
            const response = await api.post('/Auth/Login', { email, password });

            if (!response.data?.Data?.Token?.Token) {
                throw new Error('Invalid response structure');
            }

            console.log('Login successful, storing token'); // Debug
            localStorage.setItem('token', response.data.Data.Token.Token);
            setIsAuthenticated(true);
            
        } catch (err) {
            console.error('Login error:', err); // Debug
            setError(err.response?.data?.Message || 'Login failed. Please try again.');
            localStorage.removeItem('token');
            setIsAuthenticated(false);
        } finally {
            setIsLoading(false);
        }
    };

   

    return (
        <div className="auth-container">
            <div className="auth-card">
                <h2>Sign In</h2>
                <form onSubmit={handleSubmit} className="auth-form">
                    <div className="form-group">
                        <label htmlFor="email">Email Address</label>
                        <input
                            id="email"
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            disabled={isLoading}
                            placeholder="Enter your email"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            disabled={isLoading}
                            placeholder="Enter your password"
                            minLength={6}
                        />
                    </div>

                    {error && (
                        <div className="alert alert-error">
                            {error}
                        </div>
                    )}

                    <button
                        type="submit"
                        className="btn-primary"
                        disabled={isLoading}
                    >
                        {isLoading ? (
                            <>
                                <span className="spinner"></span>
                                Signing In...
                            </>
                        ) : 'Sign In'}
                    </button>

                    <div className="auth-links">
                        <Link to="/forgot-password">Forgot password?</Link>
                        <span className="divider">|</span>
                        <Link to="/register">Create account</Link>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;