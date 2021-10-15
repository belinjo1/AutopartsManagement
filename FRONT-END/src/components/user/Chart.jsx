import React, { useState, useEffect } from "react";
import { Line } from "react-chartjs-2";
import axios from "axios";
import FeaturedInfo from "./FeaturedInfo";
import { BoxContainer, Flexirimi, MainDiv, TableHead, TableText, RowText } from './navbar/StyledComponents';
import { IconContext } from 'react-icons';
import PuntoriNav from './puntorinav/Navbar';
import Navbar from './navbar/Navbar';
import {selectUser} from '../../reducers/rootReducer'
import { useSelector } from "react-redux";

export const Dankmemes = () => {
    const [chartData, setChartData] = useState({});

    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const chart = () => {
        let empSal = [];
        let empAge = [];
        axios
            .get("http://dummy.restapiexample.com/api/v1/employees")
            .then(res => {
                console.log(res);
                for (const dataObj of res.data.data) {
                    empSal.push(parseInt(dataObj.employee_salary));
                    empAge.push(parseInt(dataObj.employee_age));
                }
                setChartData({
                    labels: empAge,
                    datasets: [
                        {
                            label: "level of thiccness",
                            data: empSal,
                            backgroundColor: ["rgba(75, 192, 192, 0.6)"],
                            borderWidth: 4
                        }
                    ]
                });
            })
            .catch(err => {
                console.log(err);
            });
        console.log(empSal, empAge);
    };

    useEffect(() => {
        chart();
    }, []);
    return (
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
        {(useri.roli == "Puntor") ? <PuntoriNav/> : <Navbar/>}
        <BoxContainer>
        <div className="chart">
            <div style={{ paddingTop: 20 }}>
                <FeaturedInfo />
            </div>
            <h1 style={{ marginLeft: 330 }}>Qmimi Hyrës / Dalës</h1>
            <div style={{ width: 600, marginLeft: 150 }}>
                <Line
                    data={chartData}
                    options={{
                        responsive: true,
                        title: { text: "THICCNESS SCALE", display: true },
                        scales: {
                            yAxes: [
                                {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 10,
                                        beginAtZero: true
                                    },
                                    gridLines: {
                                        display: false
                                    }
                                }
                            ],
                            xAxes: [
                                {
                                    gridLines: {
                                        display: false
                                    }
                                }
                            ]
                        }
                    }}
                />
            </div>
        </div>
        </BoxContainer>
        </IconContext.Provider>
    );
};

export default Dankmemes;