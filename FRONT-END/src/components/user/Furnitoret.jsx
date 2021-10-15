import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import axios from 'axios'
import { Header, Icon, Modal, Input, Button } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import { BoxContainer, Flexirimi, MainDiv, TableHead, TableText, RowText } from './navbar/StyledComponents';
import ReactCountryFlag from "react-country-flag"
import { AddButton } from '../button/add'
import { UpdateButton } from '../button/update'
import { DeleteButton } from '../button/delButton'
import { IconContext } from 'react-icons';
import { SearchBar } from './navbar/SearchBar';
import { RiUserLocationFill } from 'react-icons/ri';
import Alert from '@material-ui/lab/Alert';
import Navbar from './navbar/Navbar';
import PuntoriNav from './puntorinav/Navbar';
import {selectUser} from '../../reducers/rootReducer'
import { useSelector } from "react-redux";

export function FurnitoriTable() {
    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };

    const [SearchField, setSearchField] = useState('');
    const [furnitorii, setfurnitoret] = useState([]);
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
            emri: '',
            mbiemri: '',
            lokacioni: '',
            telefoni: ''
        }
    });
    const [AddModal, setAddModal] = useState({
        modal: {
            open: false,
            emri: '',
            mbiemri: '',
            lokacioni: '',
            telefoni: ''
        }
    });

    const [formState, setFormState] = useState({
        formValues: {

            emri: '',
            mbiemri: '',
            lokacioni: '',
            telefoni: ''
        }
    });
    const [emrii ,setEmri] = useState("");
    const [mbiemrii, setMbiemri] = useState("");
    const [lokacionii, setLokacioni] = useState("");
    const [telefonii, setTelefoni] = useState("");



    const [alert,setAlert] = useState({
        validity:null,
        message:''
    })



    useEffect(() => {
        axios.get('http://localhost:5000/api/Furnitori/user', config).then(response => {
            setfurnitoret(response.data);
            
        }).catch(error=>console.log(error.response.data));

    }, [furnitorii])

    const removeFurnitor = async () => {

        setModal({ open: false })
        axios.delete("http://localhost:5000/api/Furnitori/" + modali.currentID, config)
            .then((response) => {
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    const handleChange = ({ target }) => {
        const { formValues } = formState;
        formValues[target.name] = target.value;
        setFormState({ formValues });
    };



    const updateFurnitor = async () => {
    
        var id=Editmodal.currentID;
        setEditModal({ open: false })

        axios.put("http://localhost:5000/api/Furnitori/" + id,
            {
                emri: emrii===""? Editmodal.emri:emrii,
                mbiemri: mbiemrii ===""? Editmodal.mbiemri:mbiemrii,
                lokacioni: lokacionii ===""? Editmodal.lokacioni:lokacionii,
                telefoni: telefonii ===""? Editmodal.telefoni:telefonii,
                userId: useri.userId
            }, config)
            .then((response) => {
                setEmri("");
                setMbiemri("");
                setLokacioni("");
                setTelefoni("");
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error.data);
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    const ShtoFurnitor = async () => {
        const { formValues } = formState;
        console.log(formValues);
        setAddModal({ open: false })
        axios.post("http://localhost:5000/api/Furnitori", formValues, config)
            .then((response) => {
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                console.log(error.data);
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
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
                            Shto furnitor
                        </AddButton>
                        {(alert.validity == null) ? null : (alert.validity == false) ? <Alert severity="error">{alert.message}</Alert> : <Alert severity="success">{alert.message}</Alert>}
                        <div style={{ display: 'block', padding: 10, marginBottom: 1 }}>
                            <SearchBar
                                placeholder="Enter Name"
                                handleChange={e => setSearchField(e.target.value)} />
                        </div>
                    </Flexirimi>
                </MainDiv>

                <Table className="tabelaa" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell fontSize="large" align="center"><TableText></TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Emri i Furnitorit</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Mbiemri i Furnitorit</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Lokacioni</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Telefoni</TableText></TableCell>
                            <TableCell fontSize="large" align="center"><TableText>Menaxho</TableText></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {furnitorii
                            .filter(rreshti => rreshti.emri.toLowerCase()
                                .includes(SearchField.toLowerCase())).map((row, key) => (
                                    <TableRow key={row.furntoriId}>
                                        <TableCell align="left"><RiUserLocationFill color="#fc4747" size="30"/></TableCell>
                                        <TableCell align="center"><RowText>{row.emri}</RowText></TableCell>
                                        <TableCell align="center"><RowText>{row.mbiemri}</RowText></TableCell>
                                        <TableCell align="center"><RowText>{row.lokacioni}</RowText></TableCell>
                                        <TableCell align="center"><RowText>{row.telefoni}</RowText></TableCell>
                                        <TableCell align="center">
                                        <Flexirimi>
                                            <UpdateButton
                                                onClick={() =>
                                                    setEditModal(
                                                        {
                                                            currentID: row.furnitoriId,
                                                            open: true,
                                                            emri: row.emri,
                                                            mbiemri: row.mbiemri,
                                                            lokacioni: row.lokacioni,
                                                            telefoni: row.telefoni
                                                        })}>
                                                <Icon name='history' />
                                                Përditëso
                                            </UpdateButton>
                                            <DeleteButton
                                                onClick={() => setModal({ currentID: row.furnitoriId, open: true })}>
                                                <Icon name='delete' />
                                                Fshij
                                            </DeleteButton>
                                            </Flexirimi>
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
                            Dëshironi të fshini furnitorin?
                        </p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setModal({ open: false })}>
                            <Icon name='remove' /> Pishmon?
                        </Button>
                        <Button color='green' onClick={removeFurnitor}>
                            <Icon name='checkmark' /> Po
                        </Button>
                    </Modal.Actions>
                </Modal>

                {/* Modali per Edit */}

                <Modal
                    closeIcon
                    open={Editmodal.open}
                    size='mini'
                    onClose={() => setEditModal({ open: false })}
                    onOpen={() => setEditModal({ open: true })}
                >
                    <Header icon='edit' content='Edito të dhënat' />
                    <Modal.Content>
                        <Input focus placeholder='Emri' name='emri' defaultValue={Editmodal.emri}
                            onChange={(e) => setEmri(e.target.value)} />
                        <Input focus placeholder='Mbiemri' name='mbiemri' defaultValue={Editmodal.mbiemri}
                            onChange={(e) => setMbiemri(e.target.value)} />
                        <Input focus placeholder='Lokacioni' name='lokacioni' defaultValue={Editmodal.lokacioni}
                            onChange={(e) => setLokacioni(e.target.value)} />
                        <Input focus placeholder='Telefoni' name='telefoni' defaultValue={Editmodal.telefoni}
                            onChange={(e) => setTelefoni(e.target.value)} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setEditModal({ open: false })}>
                            <Icon name='remove' /> Pishmon
                        </Button>
                        <Button color='green' onClick={updateFurnitor}>
                            <Icon name='checkmark' /> Përditëso
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
                    <Header icon='add' content='Shto furnitorë' />
                    <Modal.Content>
                        <Input focus placeholder='Emri' name='emri'
                            onChange={handleChange} />
                        <Input focus placeholder='Mbiemri' name='mbiemri'
                            onChange={handleChange} />
                        <Input focus placeholder='Lokacioni' name='lokacioni'
                            onChange={handleChange} />
                        <Input focus placeholder='Telefoni' name='telefoni' 
                            onChange={handleChange} />
                    </Modal.Content>
                    <Modal.Actions>
                        <Button color='black' onClick={() => setAddModal({ open: false })}>
                            <Icon name='remove' /> Pishmon
                        </Button>
                        <Button color='green' onClick={ShtoFurnitor}>
                            <Icon name='checkmark' /> Shto
                        </Button>
                    </Modal.Actions>
                </Modal>
            </BoxContainer>
        </IconContext.Provider>
    );
}