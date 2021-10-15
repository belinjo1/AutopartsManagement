import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import axios from 'axios'
import { Header, Icon, Modal, Input, Button } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import { BoxContainer, Flexirimi, MainDiv, TableHead, TableText, RowText,Select } from '../navbar/StyledComponents';
import { AddButton } from '../../button/add'
import { UpdateButton } from '../../button/update'
import { DeleteButton } from '../../button/delButton'
import { IconContext } from 'react-icons';
import { SearchBar } from '../navbar/SearchBar';
import Alert from '@material-ui/lab/Alert';
import { FaUser } from 'react-icons/fa';
import {selectUser} from '../../../reducers/rootReducer';
import { useSelector } from "react-redux";
import Navbar from '../navbar/Navbar';
import AdminNav from '../adminnav/Navbar';


export function AdminTable() {

    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const [formState, setFormState] = useState({
        formValues: {
            emri: '',
            mbiemri: '',
            email: '',
            password: '',
            qytetiId: 0,
        }
    });

    const [shtetet, setShtetet] = useState([]);
    const [qytetet, setQytetet] = useState([]);
    const [sh, setSh] = useState();


    const [puntori, setPuntori] = useState([]);


    const [modali, setModal] = useState({
        modal: {
            currentID: '',
            open: false,
        }
    });

    const [alert, setAlert] = useState({
        validity: null,
        message: ''
    })

    const [Editmodal, setEditModal] = useState({
        modali: {
            currentID: '',
            open: false,
            emri: '',
            mbiemri: '',
            email: '',
            password: '',
            qytetiId: 0,
        }
    });

    const [AddModal, setAddModal] = useState({
        modal: {
            emri: '',
            open: false
        }
    });
    const [SearchField, setSearchField] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        axios.get('http://localhost:5000/api/Admin/users', config).then(response => {
            setPuntori(response.data);
        });

    }, [puntori])

    const fshijPuntorin = async () => {

        var id = Editmodal.currentID;
        setModal({ open: false })
        axios.delete("http://localhost:5000/api/Admin/"+ modali.currentID, config)
            .then((response) => {
                setAlert({ validity: true, message: response.data })
            })
            .catch((error) => {
                setAlert({ validity: false, message:error.data})
            })
    }

    const handleChange = ({ target }) => {
        const { formValues } = formState;
        formValues[target.name] = target.value;
        setFormState({ formValues });
    };


    const UpdatePuntori = async () => {

        var id = Editmodal.currentID;
        console.log(id);
        setEditModal({ open: false })
        axios.put("http://localhost:5000/api/Admin/" + id, {
            password: password === "" ? Editmodal.password : password,
        }, config)
            .then((response) => {
                console.log(response.data.message)
                setAlert({ validity: true, message: response.data })
            })
            .catch((error) => {
                console.log(error);
                setAlert({ validity: false, message:error.response.data})
            })

    }

    return (
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
            <AdminNav/>
            <BoxContainer>
                <MainDiv>
                    <Flexirimi>
                        
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
                            <TableCell fontSize="large" align="center"><TableText>Emri</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Mbiemri</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Email</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Kompania</TableText></TableCell>
                    
                            <TableCell align="right"><TableText>Menaxho</TableText></TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {puntori.filter(rreshti => rreshti.emri.toLowerCase()
                            .includes(SearchField.toLowerCase())).map((row, key) => (
                                <TableRow key={row.id}>
                                    <TableCell align="left"><FaUser color="#fc4747" size="30" /></TableCell>
                                    <TableCell align="center"><RowText>{row.emri}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.mbiemri}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.email}</RowText></TableCell>
                                    <TableCell align="center"><RowText>{row.kompania}</RowText></TableCell>
                                    <TableCell align="right">
                                        <UpdateButton
                                            onClick={() =>
                                                setEditModal(
                                                    { currentID: row.id, open: true, emri: row.emri, mbiemri: row.mbiemri, email: row.email, qytetiId: row.qytetiId })}>
                                            <Icon name='lock' />
                                            Fjalëkalimi
                                        </UpdateButton>
                                        <DeleteButton
                                            onClick={() => setModal({ currentID: row.id, open: true })}>
                                            <Icon name='delete' />
                                            Fshij
                                        </DeleteButton>
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
                    <Header icon='update' content='Konfirmo fshirjen e produktit' />
                    <Modal.Content>
                        <p>
                            Dëshironi të fshini Puntorin?
                        </p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setModal({ open: false })}>
                            <Icon name='remove' /> Jo
                        </Button>
                        <Button color='green' onClick={fshijPuntorin}>
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
                    <Header icon='archive' content='Ndërro fjalëkalimin' />
                    <Modal.Content>
                        <Input focus type="password" placeholder='Fjalëkalimi' name="password" defaultValue={Editmodal.password}
                            onChange={(e) => setPassword(e.target.value)} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setEditModal({ open: false })}>
                            <Icon name='remove' />Pishmon
                        </Button>
                        <Button color='green' onClick={UpdatePuntori}>
                            <Icon name='checkmark' /> Përditëso
                        </Button>
                    </Modal.Actions>
                </Modal>
                
            </BoxContainer>
        </IconContext.Provider>
    );
}