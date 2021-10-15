import React from 'react';
import './userindex.css';
import styled from "styled-components";
import Navbar from './navbar/Navbar';
import {BrowserRouter,Switch,Route} from 'react-router-dom';

import UserCard from './Useri';

function UserApp() {
    return (
     <div>
        
        <UserCard/>
        </div>
    );
}

export default UserApp;