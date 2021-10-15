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
import { SearchBar } from './navbar/SearchBar';
import Alert from '@material-ui/lab/Alert';
import { GoFileDirectory } from 'react-icons/go';
import Navbar from './navbar/Navbar';
import PuntoriNav from './puntorinav/Navbar';
import { useSelector } from 'react-redux';
import { selectUser } from '../../reducers/rootReducer';

export function SektoriTable() {
    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const [data, setData] = useState('')
    const [sektori, setSektori] = useState([]);
    const [modali, setModal] = useState({
        modal: {
            currentID: 0,
            open: false,
        }
    });

    const [alert,setAlert] = useState({
        validity:null,
        message:''
    })

    const [Editmodal, setEditModal] = useState({
        modali: {
            currentID: 0,
            open: false,
            emri: ''
        }
    });
    const [AddModal, setAddModal] = useState({
        modal: {
            emri: '',
            open: false
        }
    });
    const [SearchField, setSearchField] = useState('');


    useEffect(() => {
        axios.get('http://localhost:5000/api/Sektori/', config).then(response => {
            setSektori(response.data);

        });

    }, [sektori])

    const fshijSektorin = async () => {

        setModal({ open: false })
        axios.delete("http://localhost:5000/api/Sektori/" + modali.currentID, config)
            .then((response) => {
                console.log(response.data)
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error);
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    function handleChange(e) {
        setData(e.target.value);
        console.log(data);
    }

    const UpdateSektori = async () => {
        setEditModal({ open: false })
        axios.put("http://localhost:5000/api/Sektori/" + Editmodal.currentID,
            { sektoriID: Editmodal.currentID, emri: data }, config)
            .then((response) => {
                console.log(response.data.message)
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error.data);
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    const ShtoSektor = async () => {

        setAddModal({ open: false })

        axios.post("http://localhost:5000/api/Sektori/", { emri: data }, config)
            .then((response) => {
                console.log(response.data)
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error);
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    return (
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
            {(useri.roli == "Puntor") ? <PuntoriNav/> : <Navbar/>}
            <BoxContainer>
                <MainDiv>
                    <Flexirimi>
                        {(useri.roli == "Puntor") ? null : <AddButton onClick={() => setAddModal({ open: true })}>
                            <Icon name='add' />
                            Shto sektorë
                        </AddButton>}
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
                            <TableCell fontSize="large" align="center"><TableText>Emri Sektorit</TableText></TableCell>
                            <TableCell align="right"><TableText>Menaxho</TableText></TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {sektori.filter(rreshti => rreshti.emri.toLowerCase()
                            .includes(SearchField.toLowerCase())).map((row, key) => (
                                <TableRow key={row.sektoriId}>
                                    <TableCell align="left"><GoFileDirectory color="#fc4747" size="30"/></TableCell>
                                    <TableCell align="center"><RowText>{row.emri}</RowText></TableCell>
                                    <TableCell align="right">
                                    {(useri.roli == "Puntor") ? null : <UpdateButton
                                    onClick={() =>setEditModal({ currentID: row.sektoriId, open: true, emri: row.emri })}>
                                            <Icon name='history' />
                                            Përditëso
                                        </UpdateButton>}
                                        {(useri.roli == "Puntor") ? null : <DeleteButton
                                            onClick={() => setModal({ currentID: row.sektoriId, open: true })}>
                                            <Icon name='delete' />
                                            Fshij
                                        </DeleteButton>}
                                    </TableCell>
                                </TableRow>
                            ))}
                    </TableBody>
                </Table>

                <Modal
                    closeIcon
                    open={modali.open}
                    size='mini'
                    onClose={() => setModal({ open: false })}
                    onOpen={() => setModal({ open: true })}
                >
                    <Header icon='archive' content='Konfirmo fshirjen e sektorit' />
                    <Modal.Content>
                        <p>
                            Dëshironi të fshini Sektorin?
                        </p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setModal({ open: false })}>
                            <Icon name='remove' /> Jo
                        </Button>
                        <Button color='green' onClick={fshijSektorin}>
                            <Icon name='checkmark' /> Po
                        </Button>
                    </Modal.Actions>
                </Modal>
                <Modal
                    closeIcon
                    open={Editmodal.open}
                    size='mini'
                    onClose={() => setEditModal({ open: false })}
                    onOpen={() => setEditModal({ open: true })}
                >
                    <Header icon='archive' content='Edito të dhënat' />
                    <Modal.Content>
                        <Input focus placeholder='Search...' defaultValue={Editmodal.emri}
                            onChange={handleChange} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setEditModal({ open: false })}>
                            <Icon name='remove' />Pishmon
                        </Button>
                        <Button color='green' onClick={UpdateSektori}>
                            <Icon name='checkmark' /> Përditëso
                        </Button>
                    </Modal.Actions>
                </Modal>
                <Modal
                    closeIcon
                    open={AddModal.open}
                    size='mini'
                    onClose={() => setAddModal({ open: false })}
                    onOpen={() => setAddModal({ open: true })}
                >
                    <Header icon='add' content='Shto sektor' />
                    <Modal.Content>
                        <Input focus placeholder='Emri Sektorit'
                            onChange={handleChange} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setAddModal({ open: false })}>
                            <Icon name='remove' /> Pishmon
                        </Button>
                        <Button color='green' onClick={ShtoSektor}>
                            <Icon name='checkmark' />Shto
                        </Button>
                    </Modal.Actions>
                </Modal>
            </BoxContainer>
        </IconContext.Provider>
    );
}
