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
import Alert from '@material-ui/lab/Alert';
import { DeleteButton } from '../button/delButton'
import { IconContext } from 'react-icons';
import { SearchBar } from './navbar/SearchBar';
import Navbar from './navbar/Navbar';
import { selectUser } from '../../reducers/rootReducer'
import { useSelector } from "react-redux";
import PuntoriNav from './puntorinav/Navbar';
import { MdComment } from 'react-icons/md'

export function KomentiTable() {

    const user = useSelector(selectUser);

    var userId = user.userId;

    const config = {
        headers: {
            Authorization: 'Bearer ' + user.token
        }
    };

    const [komenti, setKomenti] = useState([]);

    const [modali, setModal] = useState({
        modal: {
            currentID: 0,
            open: false,
        }
    });

    const [Editmodal, setEditModal] = useState({
        modali: {
            currentID: 0,
            open: false,
            titulli: '',
            body: ''
        }
    });
    const [AddModal, setAddModal] = useState({
        modal: {
            titulli: '',
            body: '',
            open: false,
        }
    });



    const [alert, setAlert] = useState({
        validity: null,
        message: ''
    })


    const [SearchField, setSearchField] = useState('');
    const [titulli, setTitulli] = useState('');
    const [body, setBody] = useState('');

    useEffect(() => {
        axios.get('http://localhost:5000/api/Komenti', config).then(response => {
            setKomenti(response.data)
        });

    }, [komenti])

    const fshijKomentin = async () => {

        setModal({ open: false })

        axios.delete("http://localhost:5000/api/Komenti/" + modali.currentID, config)
            .then((response) => {
                setAlert({ validity: true, message: response.data })
            })
            .catch((error) => {
                setAlert({ validity: false, message: 'Diqka shkoi gabim!' })
            })
    }



    const ShtoKoment = async () => {
        setAddModal({ open: false })
        axios.post("http://localhost:5000/api/Komenti",
            { titulli: titulli, body: body, emriKomentuesit: user.emri, userId: userId }, config)
            .then((response) => {
                setAlert({ validity: true, message: response.data })
            })
            .catch((error) => {
                setAlert({ validity: false, message: 'Diqka shkoi gabim!' })
            })

    }



    return (
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
            {(user.roli == "Puntor") ? <PuntoriNav /> : <Navbar />}
            <BoxContainer>
                <MainDiv>
                    <Flexirimi>
                        <AddButton onClick={() => setAddModal({ open: true })}>
                            <Icon name='add' />
                            Shto Koment
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
                            <TableCell fontSize="large" align="center"><TableText></TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Titulli</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Komenti</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Komentuesi</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Menaxho</TableText></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {komenti.filter(rreshti => rreshti.titulli.toLowerCase()
                            .includes(SearchField.toLowerCase())).map((row, key) => (
                                <TableRow key={row.komentiId}>
                                    <TableCell align="left"><MdComment color="#fc4747" size="30"/></TableCell>
                                    <TableCell align="right"><RowText>{row.titulli}</RowText></TableCell>
                                    <TableCell align="right"><RowText>{row.body}</RowText></TableCell>
                                    <TableCell align="right"><RowText>{row.emriKomentuesit}</RowText></TableCell>
                                    <TableCell align="right">
                                        <DeleteButton variant="contained" color="secondary"
                                            onClick={() => setModal({ currentID: row.komentiId, open: true })}>
                                            <Icon name='delete' />
                                            Fshij
                                        </DeleteButton>
                                    </TableCell>

                                </TableRow>
                            ))}
                    </TableBody>
                </Table>

                {/* Modali per Delete */}

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
                            Dëshironi të fshini komentin?
                        </p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setModal({ open: false })}>
                            <Icon name='remove' />  Pishmon?
                        </Button>
                        <Button color='green' onClick={fshijKomentin}>
                            <Icon name='checkmark' /> Po
                        </Button>
                    </Modal.Actions>
                </Modal>


                {/* Modali per Add */}
                <Modal
                    closeIcon
                    size='mini'
                    open={AddModal.open}
                    onClose={() => setAddModal({ open: false })}
                    onOpen={() => setAddModal({ open: true })}
                >
                    <Header icon='add' content='Shto koment' />
                    <Modal.Content>
                        <Input focus placeholder='Titulli Komentit'
                            onChange={(e) => setTitulli(e.target.value)} />
                        <Input focus placeholder='Komenti'
                            onChange={(e) => setBody(e.target.value)} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setAddModal({ open: false })}>
                            <Icon name='remove' /> Pishmon
                        </Button>
                        <Button color='green' onClick={ShtoKoment}>
                            <Icon name='checkmark' /> Shto
                        </Button>
                    </Modal.Actions>
                </Modal>
            </BoxContainer>
        </IconContext.Provider>
    );
}