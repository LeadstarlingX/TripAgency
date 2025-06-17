import { createContext, useState, useEffect } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(
        !!localStorage.getItem('token')
    );

    useEffect(() => {
        const handleStorageChange = () => {
            setIsAuthenticated(!!localStorage.getItem('token'));
        };
        window.addEventListener('storage', handleStorageChange);
        return () => window.removeEventListener('storage', handleStorageChange);
    }, []);

    return (
        <AuthContext.Provider value={{
            isAuthenticated,
            setIsAuthenticated,
        }}>
            {children}
        </AuthContext.Provider>
    );
};
