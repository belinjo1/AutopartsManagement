import React, { useState, useEffect } from "react";
import axios from 'axios';
import { useHistory } from 'react-router-dom';

export const Home = () =>{

  let history = useHistory();
  
  const token = window.localStorage.getItem('token');

  const config = {
    headers: {
      Authorization: 'Bearer ' + token}
  };

  useEffect (() => {

  if(token != null){
  axios.get('http://localhost:5000/api/User/current/'+ window.localStorage.getItem('userId'),config)
  .then((response)=>{
    console.log(response.data[0].emri);
  })
  }
  else{
    return {}
  }
  },[])

    const logout = async () => {
    window.localStorage.removeItem('token');
    window.localStorage.removeItem('userId');
    window.localStorage.removeItem('emri');
    window.localStorage.removeItem('roli');
    history.push('/AccountBox');
  }

return(

      <div>
        <h3>Emri : { window.localStorage.getItem('emri')} </h3>
        <h3>ID :   { window.localStorage.getItem('userId')} </h3>
        <h3>Roli : { window.localStorage.getItem('roli')} </h3>
        <button onClick={logout}>LOGOUT</button>
      </div>

);
};
export default Home;