import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Button from '@material-ui/core/Button';
import axios from 'axios'
import { Header, Icon, Modal, Input } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import SearchBar from '../SearchBar';
import { BoxContainer, Flexirimi,Select,Centirimi} from '../StyledComponents';


export default function Produkti() {
    const [markat, setMarkat] = useState([]);
    const [data, setData] = useState([]);
    const [SearchField, setSearchField] = useState('');
    const [produktet, setProduktet] = useState([]);
    const [sektoret, setSektoret] = useState([]);


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
            emri: "",
            number: "",
            sektoriId: "",
            markaId: ""
        }
    });
    const [AddModal, setAddModal] = useState({
        modal: {
            emri: '',
            number: "",
            sektoriId: 0,
            markaId: 0,
            open: false,
        }
    });


    
    useEffect(() => {
        axios.get("http://localhost:5000/api/Produkti/", { withCredentials: false })
        .then((response) => {
            console.log(response.data.message)
        });
        axios.get("http://localhost:5000/api/Sektori/user", { withCredentials: false })
        .then((response) => {
            console.log(response.data.message)
        });
        axios.get("http://localhost:5000/api/Marka", { withCredentials: false })
        .then((response) => {
            setMarkat(response.data);
        });
    

    }, [])


    const fshijProdukt = async () => {

        setModal({ open: false })
        axios.delete("http://localhost:5000/api/Produkti/" + modali.currentID, { withCredentials: true })
            .then((response) => {
                console.log(response.data.message)
            })
            .catch((error) => {
                console.log(error.data);
            })

    }

    function handleChange(e) {
        setData(e.target.value);
        console.log(data);
    }

    const updateProdukt = async () => {
        console.log(Editmodal.currentID);
        console.log(data);
        setEditModal({ open: false })
        axios.put("http://localhost:5000/api/Produkti/" + Editmodal.currentID,
            {
                emri: Editmodal.emri,
                number: Editmodal.number,
                sektoriId: Editmodal.sektoriId,
                markaId: Editmodal.markaId
            }, { withCredentials: false })
            .then((response) => {
                console.log(response.data.message)
            })
            .catch((error) => {
                console.log(error.data);
            })

    }

    const ShtoProdukt = async () => {

        setAddModal({ open: false })
        axios.post("http://localhost:5000/api/Produkti",
            {
                emri: data.emri,
                number: data.number,
                sektoriId: data.sektoriId,
                markaId: data.markaId
            }, { withCredentials: true })
            .then((response) => {
                console.log(response.data.message)
            })
            .catch((error) => {
                console.log(error.data);
            })

    }


    //Tabela

    return (
        <BoxContainer>
            <div style={{ margin: 30, padding: 0, width: 500 }}>
                <Centirimi>
                    <div style={{ display: 'block', padding: 10, marginBottom: 1 }}>
                        <SearchBar placeholder="Enter Name" handleChange={e => setSearchField(e.target.value)} />
                    </div>
                    <div style={{ display: 'block', padding: 10 }}>
                        <Button variant="contained" color="secondary"
                            onClick={() => setAddModal({ open: true })}>
                            Shto nje Produkt
                        </Button>
                    </div>
                </Centirimi>
            </div>
            <Table className="tabelaa" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Produkti ID</TableCell>
                        <TableCell align="right">Emri </TableCell>
                        <TableCell align="right">Nr. Serik</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {produktet
                        .filter(rreshti => rreshti.emri.toLowerCase()
                            .includes(SearchField.toLowerCase())).map((row, key) => (
                                <TableRow key={row.produktiId}>
                                    <TableCell align="right">{row.produktiId}</TableCell>
                                    <TableCell align="right">{row.emri}</TableCell>
                                    <TableCell align="right">{row.number}</TableCell>
                                    <TableCell align="right">{row.sektoriId}</TableCell>
                                    <TableCell align="right">{row.markaId}</TableCell>
                                    <TableCell align="right"> <Button variant="contained" color="primary"
                                        onClick={() =>
                                            setEditModal(
                                                {
                                                    currentID: row.produktiId,
                                                    open: true,
                                                    emri: row.emri,
                                                    number: row.number,
                                                    sektoriId: row.sektoriId,
                                                    markaId: row.markaId
                                                })}>
                                        Perditeso
                                    </Button>
                                        <Button variant="contained" color="secondary"
                                            onClick={() => setModal({ currentID: row.produktiId, open: true })}>
                                            Fshij
                                        </Button>
                                    </TableCell>

                                </TableRow>
                            ))}
                </TableBody>
            </Table>

            {/* Modali per Delete */}

            <Modal
                closeIcon
                open={modali.open}
                onClose={() => setModal({ open: false })}
                onOpen={() => setModal({ open: true })}
            >
                <Header icon='archive' content='Konfirmo Fshierjen!' />
                <Modal.Content>
                    <p>
                        Deshironi te fshini Produktin?
                    </p>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='red' onClick={() => setModal({ open: false })}>
                        <Icon name='remove' /> Pishmon?
                    </Button>
                    <Button color='green' onClick={fshijProdukt}>
                        <Icon name='checkmark' /> Yes
                    </Button>
                </Modal.Actions>
            </Modal>

            {/* Modali per Edit */}

            <Modal
                closeIcon
                open={Editmodal.open}
                onClose={() => setEditModal({ open: false })}
                onOpen={() => setEditModal({ open: true })}
            >
                <Header icon='archive' content='Edito Te Dhenat!' />
                <Modal.Content>
                    <Input focus placeholder='Search...' defaultValue={Editmodal.emri}
                        onChange={handleChange} />
                    <Input focus placeholder='Search...' defaultValue={Editmodal.number}
                        onChange={handleChange} />
                    {/* <Select focus placeholder='Search...' defaultValue={Editmodal.sektoriId} 
                onChange={handleChange} />
                <Select focus placeholder='Search...' defaultValue={Editmodal.markaId} 
                onChange={handleChange} /> */}
                </Modal.Content>
                <Modal.Actions>
                    <Button color='red' onClick={() => setEditModal({ open: false })}>
                        <Icon name='remove' /> Pishmon
                    </Button>
                    <Button color='green' onClick={updateProdukt}>
                        <Icon name='checkmark' /> Perditeso
                    </Button>
                </Modal.Actions>
            </Modal>

            {/* Modali per Add */}
            <Modal
                closeIcon
                open={AddModal.open}
                onClose={() => setAddModal({ open: false })}
                onOpen={() => setAddModal({ open: true })}
                className="modali"
            >
                <Header icon='wrench' content='Shto Produkt' />
                <Modal.Content>
                        
                        <Flexirimi>
                        <Input style={{margin:"2%"}} focus placeholder='Emri Produktit...'
                            onChange={handleChange} />
                        <Input  focus placeholder='Nr Serik...'
                            onChange={handleChange} />

                        <Select  name="Sektori"  >  
                        <option>Sektori</option> 
                        {sektoret.map((e, key) => {  
                        return <option key={key} value={e.sektoriId}>{e.emri}</option>;  
                        })}  
                        </Select>

                        <Select  name="Marka"  >  
                        <option>Marka</option> 
                        {markat.map((e, key) => {  
                        return <option key={e.markaId} value={e.markaId}>{e.emri}</option>;  
                        })}  
                        </Select>

  
                        </Flexirimi>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='red' onClick={() => setAddModal({ open: false })}>
                        <Icon name='remove' /> Pishmon
                    </Button>
                    <Button color='green' onClick={ShtoProdukt}>
                        <Icon name='checkmark' /> Shto
                    </Button>
                </Modal.Actions>
            </Modal>
        </BoxContainer>
    );
}