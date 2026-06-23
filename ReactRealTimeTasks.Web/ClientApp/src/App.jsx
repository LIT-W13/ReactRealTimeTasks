import React from 'react';
import { Route, Routes } from 'react-router';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Signup from './Pages/Signup';
import Login from './Pages/Login';
import { AuthProvider } from './AuthContext';
import PrivateRoute from './PrivateRoute';
import Logout from './Pages/Logout';


const App = () => {
    return (
        <AuthProvider>
            <Layout>
                <Routes>
                    <Route path='/' element={
                        <PrivateRoute>
                            <Home />
                        </PrivateRoute>
                    } />
                    <Route path='/signup' element={<Signup />} />
                    <Route path='/login' element={<Login />} />
                    <Route path='/logout' element={
                        <PrivateRoute>
                            <Logout />
                        </PrivateRoute>
                    } />
                </Routes>
            </Layout>
        </AuthProvider>
    );
}

export default App;