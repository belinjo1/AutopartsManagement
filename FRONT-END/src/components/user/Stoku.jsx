import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import axios from 'axios'
import { Header, Icon, Modal, Input, Button } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import { BoxContainer, Flexirimi, MainDiv, TableHead, TableText, RowText } from './navbar/StyledComponents';
import { AddButton } from '../button/add'
import { UpdateButton } from '../button/update'
import { DeleteButton } from '../button/delButton'
import { IconContext } from 'react-icons';
import {selectUser} from '../../reducers/rootReducer'
import { useSelector } from "react-redux";
import { SearchBar } from './navbar/SearchBar';
import Alert from '@material-ui/lab/Alert';
import { GoFileDirectory } from 'react-icons/go';
import Navbar from './navbar/Navbar';
import PuntoriNav from './puntorinav/Navbar';


export function StokuTable() {

    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const [formState, setFormState] = useState({
        formValues: {
            number:'',
            sasia:0,
            qmimi:0
        }
        });

    const [stoku, setStoku] = useState([]);
    

    const [alert,setAlert] = useState({
        validity:null,
        message:''
    })

    const [AddModal, setAddModal] = useState({
        modal: {
            open: false
        }
    });

    const [SearchField, setSearchField] = useState('');

    useEffect(() => {
        axios.get('http://localhost:5000/api/Produkti/stoku', config).then(response => {
            setStoku(response.data);
        });

    }, [stoku])


    const handleChange = ({ target }) => {
        const { formValues } = formState;
        formValues[target.name] = target.value;
        setFormState({ formValues });

        };

    const shtoStok = async () => {

        const { formValues } = formState;
        const { number } = formValues;
        console.log(formValues)
        console.log(formValues.number)
        setAddModal({ open: false })

        axios.post("http://localhost:5000/api/Produkti/stoku?productNo="+formValues.number, formValues, config)
            .then((response) => {
                console.log(response.data)
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error.response.data);
                setAlert({validity:false,message:error.response.data})
            })

    }

    return (
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
             {(useri.roli == "Puntor") ? <PuntoriNav/> : <Navbar/>}
            <BoxContainer>
                <MainDiv>
                    <Flexirimi>
                        <AddButton onClick={() => setAddModal({ open: true })}>
                            <Icon name='add' />
                            Shto stok
                        </AddButton>
                        {(alert.validity == null) ? null : (alert.validity == false) ? <Alert severity="error">{alert.message}</Alert> : <Alert severity="success">{alert.message}</Alert>}
                        <div style={{ display: 'block', padding: 10, marginBottom: 1 }}>
                            <SearchBar
                                placeholder="Enter Name"
                                handleChange={e => setSearchField(e.target.value)} />
                        </div>
                    </Flexirimi>
                </MainDiv>

                <Table className="" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell align="left"><TableText></TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Numri Serik</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Emri</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Sasia</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Qmimi</TableText></TableCell>
                            <TableCell align="center"><TableText>Data</TableText></TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {stoku.map((row, key) => (
                                <TableRow key={row.faturaId}>
                                    <TableCell align="left"><GoFileDirectory color="#fc4747" size="30"/></TableCell>
                                    <TableCell align="center"><RowText>{row.produkti.number}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.produkti.emri}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.sasia}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.qmimi} €</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.createdAt}</RowText></TableCell>
                                
                                </TableRow>
                            ))}
                    </TableBody>
                </Table>

                <Modal
                    closeIcon
                    open={AddModal.open}
                    size='mini'
                    onClose={() => setAddModal({ open: false })}
                    onOpen={() => setAddModal({ open: true })}
                >
                    <Header icon='add' content='Shto sektor' />
                    <Modal.Content>
                        <Input focus placeholder='Numri serik' name="number"
                            onChange={handleChange} />

                        <Input focus placeholder='Sasia' name="sasia"
                            onChange={handleChange} />

                        <Input focus placeholder='Qmimi €' name="qmimi"
                            onChange={handleChange} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setAddModal({ open: false })}>
                            <Icon name='remove' /> Pishmon
                        </Button>
                        <Button color='green' onClick={shtoStok}>
                            <Icon name='checkmark' />Shto
                        </Button>
                    </Modal.Actions>
                </Modal>
            </BoxContainer>
        </IconContext.Provider>
    );
}
 