import React, { useState, useEffect } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import axios from 'axios'
import { Header, Icon, Modal, Input, Button } from 'semantic-ui-react'
import 'semantic-ui-css/semantic.min.css'
import { BoxContainer, Flexirimi, MainDiv, TableHead , TableText , RowText } from './navbar/StyledComponents';
import Alert from '@material-ui/lab/Alert';
import { AddButton } from '../button/add'
import { UpdateButton } from '../button/update'
import { DeleteButton } from '../button/delButton'
import { IconContext } from 'react-icons';
import { SearchBar }  from './navbar/SearchBar';
import { GoListUnordered } from 'react-icons/go'
import Navbar from './navbar/Navbar';
import PuntoriNav from './puntorinav/Navbar';
import { useSelector } from 'react-redux';
import { selectUser } from '../../reducers/rootReducer';

export function PorositeTable() {
    const useri = useSelector(selectUser);

    const config = {
        headers: {
            Authorization: 'Bearer ' + useri.token
        }
    };
    const [data, setData] = useState('')

    const [formState, setFormState] = useState({
        formValues: {

            titulli: '',
            sasia: 0,
            klienti: '',
            telefoni: '',
            userId:useri.userId
        }
    });

    const [alert,setAlert] = useState({
        validity:null,
        message:''
    })

    const[porosia,setPorosia] = useState([]);
    const [modali, setModal] = useState({
        modal:{
            currentID:0,
            open:false,
        }
    });

    const [Editmodal, setEditModal] = useState({
        modali:{
            currentID:0,
            open:false,
            titulli: '',
            sasia: 0,
            klienti: '',
            telefoni: ''
        }
    });
    const [AddModal, setAddModal] = useState({
        modal:{
            open:false,
            titulli: '',
            sasia: 0,
            klienti: '',
            telefoni: ''
        }
    });
    const[SearchField,setSearchField]=useState('');
    const [titulli ,setTitulli] = useState("");
    const [sasia, setSasia] = useState(0);
    const [klienti, setKlienti] = useState("");
    const [telefoni, setTelefoni] = useState("");




    useEffect(() => {
        axios.get('http://localhost:5000/api/Porosia/user',config).then(response => {
            setPorosia(response.data);
        });

    }, [porosia])

    const fshijPorosine = async () => {

        setModal({open:false})
        axios.delete("http://localhost:5000/api/Porosia/" + modali.currentID,config)
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
    
    const UpdatePorosia = async () => {

        setEditModal({open:false})
        axios.put("http://localhost:5000/api/Porosia/" + Editmodal.currentID, 
        {titulli: titulli===""? Editmodal.titulli:titulli,
        sasia: sasia ===""? Editmodal.sasia:sasia,
        klienti: klienti ===""? Editmodal.klienti:klienti,
        telefoni: telefoni ===""? Editmodal.telefoni:telefoni,
        userId:useri.userId}, config)
            .then((response) => {

                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }

    const ShtoPorosi = async () => {
        const { formValues } = formState;
        console.log(formValues);
        setAddModal({open:false})
        axios.post("http://localhost:5000/api/Porosia", formValues, config)
            .then((response) => {
                setAlert({validity:true,message:response.data})
            })
            .catch((error) => {
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })

    }


    return (
        <IconContext.Provider value={{ color: 'white' , size: '2%'}}>
        {(useri.roli == "Puntor") ? <PuntoriNav/> : <Navbar/>}
        <BoxContainer>
        <MainDiv>
            <Flexirimi>
                <AddButton onClick={() => setAddModal({open:true})}>
                <Icon name='add'/>
                Shto Porosi
                </AddButton>
                {(alert.validity == null) ? null : (alert.validity == false) ? <Alert severity="error">{alert.message}</Alert> : <Alert severity="success">{alert.message}</Alert>}
                <div style={{display:'block', padding:10, marginBottom:1}}>
                <SearchBar 
                        placeholder="Enter Name"
                        handleChange={e=>setSearchField(e.target.value)} />
                </div>
            </Flexirimi>
        </MainDiv>
        
            <Table className="" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell fontSize="large" align="center"><TableText></TableText></TableCell>
                        <TableCell fontSize="large" align="center"><TableText>Titulli</TableText></TableCell>
                        <TableCell fontSize="large" align="center"><TableText>Sasia</TableText></TableCell>
                        <TableCell fontSize="large" align="center"><TableText>Klienti</TableText></TableCell>
                        <TableCell fontSize="large" align="center"><TableText>Telefoni</TableText></TableCell>
                        <TableCell fontSize="large" align="right"><TableText>Menaxho</TableText></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                {porosia.filter(rreshti=>rreshti.titulli.toLowerCase()
                .includes(SearchField.toLowerCase())).map((row,key)=>(
                    <TableRow key={row.porosiaId}>
                        <TableCell align="center"><GoListUnordered color="#fc4747" size="30"/></TableCell>
                        <TableCell align="center"><RowText>{row.titulli}</RowText></TableCell>
                        <TableCell align="center"><RowText>{row.sasia}</RowText></TableCell>
                        <TableCell align="center"><RowText>{row.klienti}</RowText></TableCell>
                        <TableCell align="center"><RowText>{row.telefoni}</RowText></TableCell>
                        <TableCell align="right"> 
                        <UpdateButton
                        onClick={() => 
                        setEditModal(
                            {currentID:row.porosiaId,open:true,titulli:row.titulli,sasia:row.sasia,klienti:row.klienti,
                            telefoni:row.telefoni})}>
                            <Icon name='history'/>
                            Përditëso
                        </UpdateButton>
                            <DeleteButton 
                            onClick={() => setModal({currentID:row.porosiaId,open:true})}>
                            <Icon name='delete'/>
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
                size='mini'
                open={modali.open}
                onClose={() => setModal({open:false})}
                onOpen={() => setModal({open:true})}
            >
                <Header icon='delete' content='Konfirmo fshirjen?' />
                <Modal.Content>
                    <p>
                    Dëshironi të fshini porosine?
                    </p>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='black' onClick={() => setModal({open:false})}>
                        <Icon name='remove' /> No
                    </Button>                   
                    <Button color='green' onClick={fshijPorosine}>
                        <Icon name='checkmark' /> Yes
                    </Button>
                </Modal.Actions>
            </Modal>

            {/* Modali per Edit */}
            
            <Modal
                closeIcon
                size='mini'
                open={Editmodal.open}
                onClose={() => setEditModal({open:false})}
                onOpen={() => setEditModal({open:true})}
            >
                <Header icon='edit' content='Edito porositë' />
                <Modal.Content>
                <Input focus placeholder='Search...' defaultValue={Editmodal.titulli} 
                  onChange={(e) => setTitulli(e.target.value)} />
                <Input focus placeholder='Search...' defaultValue={Editmodal.sasia} 
                  onChange={(e) => setSasia(e.target.value)} />
                   <Input focus placeholder='Search...' defaultValue={Editmodal.klienti} 
                  onChange={(e) => setKlienti(e.target.value)} />
                   <Input focus placeholder='Search...' defaultValue={Editmodal.telefoni} 
                  onChange={(e) => setTelefoni(e.target.value)} />
                </Modal.Content>
                <Modal.Actions>
                    <Button color='black' onClick={() => setEditModal({open:false})}>
                        <Icon name='remove' /> Pishmon
                    </Button>
                    <Button color='green' onClick={UpdatePorosia}>
                        <Icon name='checkmark' /> Përditeso
                    </Button>
                </Modal.Actions>
            </Modal>

               {/* Modali per Add */}
            <Modal
                closeIcon
                open={AddModal.open}
                size='mini'
                onClose={() => setAddModal({open:false})}
                onOpen={() => setAddModal({open:true})}
            >
                <Header icon='add' content='Shto porosi' />
                <Modal.Content>
                <Input focus placeholder='Titulli' name="titulli"
                onChange={handleChange} />
                <Input focus placeholder='Sasia' name="sasia"
                onChange={handleChange} />
                <Input focus placeholder='Klienti' name="klienti"
                onChange={handleChange} />
                <Input focus placeholder='Telefoni' name="telefoni"
                onChange={handleChange} />
                </Modal.Content>
                <Modal.Actions>
                    <Button color='black' onClick={() => setAddModal({open:false})}>
                        <Icon name='remove' /> Pishmon
                    </Button>
                    <Button color='green' onClick={ShtoPorosi}>
                        <Icon name='checkmark' /> Shto
                    </Button>
                </Modal.Actions>
            </Modal>
        </BoxContainer>
        </IconContext.Provider>
    );
}