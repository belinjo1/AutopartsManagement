import React,{useState,useEffect} from 'react'
import "./featuredInfo.css";
import axios from 'axios';
import { Icon, Image, Statistic } from 'semantic-ui-react';
import { useSelector } from 'react-redux';
import { selectUser } from '../../reducers/rootReducer';
import styled from "styled-components";


export default function FeaturedInfo() {
  const useri = useSelector(selectUser);

  const config = {
      headers: {
          Authorization: 'Bearer ' + useri.token
      }
  };
  
  const[count,setCount]=useState(0);
  const[countProd,setCountProd]=useState(0);
  const[countPor,setCountPor]=useState(0);
  const[countKom,setCountKom]=useState(0);
  const[countSec,setCountSec]=useState(0);

  
  useEffect(() => {
  fetchData();

}, [count,countProd,countPor,countKom])

const fetchData = async() =>{
  axios.get('http://localhost:5000/api/Furnitori/user',config).then(response => {
    setCount(response.data.length);
});
axios.get('http://localhost:5000/api/Produkti', config).then(response => {
  setCountProd(response.data.length);
});
axios.get('http://localhost:5000/api/Porosia/user',config).then(response => {
  setCountPor(response.data.length);
});
axios.get('http://localhost:5000/api/Komenti',config).then(response => {
  setCountKom(response.data.length);
});
axios.get('http://localhost:5000/api/Sektori',config).then(response => {
  setCountSec(response.data.length);
});
}


  return (

  
    <div className="featured">
      <div className="featuredItem">
    <Statistic>
    <Statistic.Value>
        {/* <Image src={furnitori} inline circular /> */}
        <Icon name='chart bar' size='tiny' /> {count}
      </Statistic.Value>
      <Statistic.Label>Furnitorë</Statistic.Label>
    </Statistic>
      </div>
      
      <div className="featuredItem">
      <Statistic>
      <Statistic.Value>
        <Icon name='chart bar' size='tiny' />{countProd}
      </Statistic.Value>
      <Statistic.Label>Produkte</Statistic.Label>
    </Statistic>
        </div>

        <div className="featuredItem">
      <Statistic>
      <Statistic.Value>
        <Icon name='chart bar' size='tiny' />{countPor}
      </Statistic.Value>
      <Statistic.Label>Porosi</Statistic.Label>
    </Statistic>
        </div>

        <div className="featuredItem">
      <Statistic>
      <Statistic.Value>
        <Icon name='chart bar' size='tiny' />{countKom}
      </Statistic.Value>
      <Statistic.Label>Komentet</Statistic.Label>
    </Statistic>
        </div>

         <div className="featuredItem">
      <Statistic>
      <Statistic.Value>
        <Icon name='chart bar' size='tiny' />{countSec}
      </Statistic.Value>
      <Statistic.Label>Sektorët</Statistic.Label>
    </Statistic>
        </div> 

        </div>
  );
}
