import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import axios from 'axios'
import { Header, Icon, Modal, Button } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import {  Flexirimi, MainDiv, TableHead, TableText, RowText } from './navbar/StyledComponents';
import { DeleteButton } from '../button/delButton';
import { IconContext } from 'react-icons';
import { SearchBar } from './navbar/SearchBar';
import Alert from '@material-ui/lab/Alert';
import { RiQuestionnaireFill } from 'react-icons/ri';
import { selectUser } from '../../reducers/rootReducer'
import { useSelector } from "react-redux";
import Navbar from './navbar/Navbar';
import AdminNav from './adminnav/Navbar';
import styled from "styled-components";

export const BoxContainer = styled.div`
margin-left:20%;
width:80%;
height:40%;
margin-top:3%;
`;

export const Flex = styled.div`
display:flex;
flex-direction:row;
`;


export function ContactTable() {




    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const [alert, setAlert] = useState({
        validity: null,
        message: ''
    })

    const [forma, setForma] = useState([]);

    const [modali, setModal] = useState({
        modal: {
            currentID: 0,
            open: false,
        }
    });

    const removeContactUs = async () => {

        setModal({ open: false })
        axios.delete("http://localhost:5000/api/Contact/" + modali.currentID, config)
            .then((response) => {
                setAlert({ validity: true, message: response.data })
            })
            .catch((error) => {
                setAlert({ validity: false, message: 'Diqka shkoi gabim!' })
            })

    }


    const [SearchField, setSearchField] = useState('');

    useEffect(() => {
        axios.get('http://localhost:5000/api/Contact', config).then(response => {
            setForma(response.data);
        });

    }, [forma])


    return (

        <IconContext.Provider value={{ color: 'white', size: '2%' }}>

            {(useri.roli === "Admin") ? <AdminNav /> : <Navbar />}
            <BoxContainer>
                <MainDiv>
                    <Flex>
                        {(alert.validity == null) ? null : (alert.validity === false) ? <Alert severity="error">{alert.message}</Alert> : <Alert severity="success">{alert.message}</Alert>}
                        <div style={{ display: 'block', padding: 10, marginBottom: 1 }}>
                            <SearchBar
                                placeholder="Enter Name"
                                handleChange={e => setSearchField(e.target.value)} 
                            />
                        </div>
                    </Flex>
                </MainDiv>

                <Table aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell  align="left"><TableText></TableText></TableCell>
                            <TableCell  size="small"  align="left"><TableText>Emri</TableText></TableCell>
                            <TableCell  align="left"><TableText>Mbiemri</TableText></TableCell>
                            <TableCell  align="left"><TableText>Mesazhi</TableText></TableCell>
                            <TableCell  align="left"><TableText>Email</TableText></TableCell>
                            <TableCell  align="left"><TableText>Data</TableText></TableCell>
                            <TableCell  align="left"><TableText>Menaxho</TableText></TableCell>

                            <TableCell align="left"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                    {forma.filter(rreshti => rreshti.emri.toLowerCase()
                            .includes(SearchField.toLowerCase())).map((row, key) => 
                            <TableRow key={row.contactId}>
                                <TableCell align="left"><RiQuestionnaireFill color="#fc4747" size="30" /></TableCell>
                                <TableCell  size="small" padding="none" align="left"><RowText>{row.emri}</RowText></TableCell>
                                <TableCell  size="small" padding="none" align="left"><RowText>{row.mbiemri}</RowText></TableCell>
                                <TableCell  size="small" padding="none" align="left"><RowText>{row.pyetja}</RowText></TableCell>
                                <TableCell  size="small" padding="none" align="left"><RowText>{row.email}</RowText></TableCell>
                                <TableCell  size="small" padding="none" align="left"><RowText>{row.createdAt}</RowText></TableCell>
                                <TableCell  size="small" padding="none" align="left">
                                    <DeleteButton
                                        onClick={() => setModal({ currentID: row.contactId, open: true })}>
                                        <Icon name='delete' />
                                        Fshij
                                    </DeleteButton>
                                </TableCell>
                            </TableRow>

                        )}
                    </TableBody>
                </Table>


                <Modal
                    closeIcon
                    open={modali.open}
                    size='mini'
                    onClose={() => setModal({ open: false })}
                    onOpen={() => setModal({ open: true })}
                >
                    <Header icon='delete' content='Konfirmo fshirjen?' />
                    <Modal.Content>
                        <p>
                            Dëshironi të fshini mesazhin?
                        </p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setModal({ open: false })}>
                            <Icon name='remove' /> Pishmon?
                        </Button>
                        <Button color='green' onClick={removeContactUs}>
                            <Icon name='checkmark' /> Po
                        </Button>
                    </Modal.Actions>
                </Modal>

            </BoxContainer>
        </IconContext.Provider>
 
    );
}
