import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { useContext } from 'react';
import Login from './components/auth/login/login';
import Register from './components/auth/register/register';
import Home from './components/pages/home/home';
import Book from './components/pages/book/book';
import AddCar from './components/pages/addcar/addcar';
import Bookings from './components/pages/bookings/bookings';
import Payments from './components/pages/payments/payments';
//import Payment from './components/pages/payment/payment';
//import Refund from './components/pages/refund/refund';
import './App.css';
import { AuthProvider, AuthContext } from './AuthContext.jsx';

function AppRoutes() {
    const { isAuthenticated } = useContext(AuthContext);

    return (
        <Routes>
            <Route path="/login"
                element={!isAuthenticated ? <Login /> : <Navigate to="/home" replace />}
            />
            <Route path="/home"
                element={isAuthenticated ? <Home /> : <Navigate to="/login" replace />}
            />
            <Route path="/"
                element={<Navigate to={isAuthenticated ? "/home" : "/login"} replace />}
            />
            <Route path="/register"
                element={!isAuthenticated ? <Register /> : <Navigate to="/register" replace />}
            />
            <Route path="/book"
                element={isAuthenticated ? <Book /> : <Navigate to="/login" replace />}
            />
            <Route path="/add-car"
                element={isAuthenticated ? <AddCar /> : <Navigate to="/login" replace />}
            />
            <Route path="/bookings"
                element={isAuthenticated ? <Bookings /> : <Navigate to="/login" replace />}
            />
            <Route path="/payments"
                element={isAuthenticated ? <Payments /> : <Navigate to="/login" replace />}
            />
            {/*<Route path="/payment"*/}
            {/*    element={isAuthenticated ? <Payment /> : <Navigate to="/login" replace />}*/}
            {/*/>*/}
            {/*<Route path="/refund"*/}
            {/*    element={isAuthenticated ? <Refund /> : <Navigate to="/login" replace />}*/}
            {/*/>*/}
        </Routes>
    );
}

function App() {
    return (
        <AuthProvider>
            <BrowserRouter>
                <AppRoutes />
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
