import React, { useState, useEffect } from 'react'
import { Modal, Icon, Input, Button, Header } from 'semantic-ui-react'
// import matthew from '../../images/matthew.png';
import styled from "styled-components";
import axios from 'axios';
import { selectUser } from '../../reducers/rootReducer'
import { useSelector } from "react-redux";
import logo from '../../assets/logo/logo.png'
import { IconContext } from 'react-icons';
import Alert from '@material-ui/lab/Alert';
import { Select } from './navbar/StyledComponents';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router';
import { logout } from '../../reducers/rootReducer';
import PuntoriNav from './puntorinav/Navbar';
import AdminNav from './adminnav/Navbar';
import Navbar from './navbar/Navbar';

export const BoxContainer = styled.div`
margin-left:21%;
width:50%;
height:40%;
margin-top:3%;
display:flex;
`;

export const Flex = styled.div`
display:flex;
flex-direction:row;
`;

export default function UserCard() {

    const dispatch = useDispatch();
    const history = useHistory();

    const [emri, setEmri] = useState("");
    const [email, setEmail] = useState("");
    const [mbiemri, setMbiemri] = useState("");
    const [qyteti, setQyteti] = useState(0);
    const [user, setUser] = useState([]);
    const [qytetet, setQytetet] = useState([]);

    
    
    const [deleteModal,setDeleteModal]=useState(false);

    const [Editmodal, setEditModal] = useState({
        modali: {
            currentID: 0,
            open: false,
            emri: '',
            mbiemri: '',
            email: '',
            qytetiId: 0
        }
    });
    
    const [alert,setAlert] = useState({
        validity:null,
        message:''
    })


    const useriFromRedux = useSelector(selectUser);
    var userIdd = useriFromRedux.userId;


    const config = {
        headers: {
            Authorization: 'Bearer ' + useriFromRedux.token
        }
    };

    const [password,setPassword]=useState("");
    const [current,setCurrent]=useState("");

    const EditPassword = async () =>{
        console.log(current + password);
        setDeleteModal(false);
        await axios.put("http://localhost:5000/api/User/password/"+userIdd+"?currentPassword="+current, {password:password} ,config)
        .then((response) => {
            setAlert({validity:true,message:response.data})
            dispatch(logout());
            history.push('/AccountBox');
        })
        .catch((error) => {
            setAlert({validity:false,message:'Diqka shkoi gabim!'})
        })
    }



    const EditoUserin = async () => {
        setEditModal({ open: false })
        await axios.put("http://localhost:5000/api/User/" + userIdd,
        {   
            emri: emri===""? Editmodal.emri:emri,
            mbiemri: mbiemri ===""? Editmodal.mbiemri:mbiemri,
            email: email ===""? Editmodal.email:email,
            qytetiId: qyteti ===0? Editmodal.qytetiId:qyteti,
        }
        ,config)

            .then((response) => {
                setAlert({validity:true,message:response.data + ", ri-kyquni për ti parë"})
            })
            .catch((error) => {
                setAlert({validity:false,message:'Diqka shkoi gabim!'})
            })
    }
    useEffect(() => {
        fetchData();
    }, [user])

    const fetchData = async () => {
        axios.get('http://localhost:5000/api/User/current/' + userIdd, config).then(response => {
            setUser(response.data);

        });

        axios.get('http://localhost:5000/api/Qyteti?shtetiId=1').then(response => {
            setQytetet(response.data);

        });

    }


    return (      
        <IconContext.Provider value={{ color: 'white', size: '2%' }}>
        {((useriFromRedux.roli === "Puntor") ? <PuntoriNav/> : (useriFromRedux.roli === "Admin")? <AdminNav/>:<Navbar/>)}
        <BoxContainer>
            <Flex>
                <img src={logo} className="picu" width="35%" />
                <div className="myInfo-box">
                    <h2 className="titulli">{'I kyqur si :  ' + useriFromRedux.emri}</h2>
                    <hr></hr>
                    <div className="data">
                        <h3>Te dhënat tuaja:</h3>
                        <p>{'Emri: ' + useriFromRedux.emri}</p>
                        <p>{'Email: ' + useriFromRedux.email}</p>
                        <p>{'Roli: ' + useriFromRedux.roli}</p>
                        <Flex>
                        <Button color = "red" onClick={() =>
                            setEditModal(
                                { currentID: user[0].userId, open: true, emri: user[0].emri, mbiemri: user[0].mbiemri, email: user[0].email, qytetiId: user[0].qytetiId })}>Përditëso të dhënat</Button>

                        <Button color = "red" onClick={() =>setDeleteModal(true) }>Ndrysho Fjalëkalimin</Button>
                       
                        </Flex>
                        {(alert.validity == null) ? null : (alert.validity == false) ? <Alert severity="error">{alert.message}</Alert> : <Alert severity="success">{alert.message}</Alert>}
                    </div>
                    
                </div>
            </Flex>
            <Modal
                closeIcon
                open={Editmodal.open}
                size='mini'
                onClose={() => setEditModal({ open: false })}
                onOpen={() => setEditModal({ open: true })}
            >
                <Header icon='edit' content='Përditëso të dhënat' />
                <Modal.Content>
                    <Input focus placeholder='Emri' name="emri" defaultValue={Editmodal.emri}
                        onChange={(e) => setEmri(e.target.value)} />
                    <Input focus placeholder='Mbiemri' name="mbiemri" defaultValue={Editmodal.mbiemri}
                        onChange={(e) => setMbiemri(e.target.value)} />
                    <Input focus placeholder='Email' name="email" defaultValue={Editmodal.email}
                        onChange={(e) => setEmail(e.target.value)} />

                    <Select onChange={(e) => setQyteti(e.target.value)} name="qytetiId" defaultValue={Editmodal.qytetiId}>
                        {qytetet.map((e, key) => {
                            return <option key={key} value={e.qytetiId}>{e.emri}</option>;
                        })}
                    </Select>


                </Modal.Content>
                <Modal.Actions>
                    <Button color='black' onClick={() => setEditModal({ open: false })}>
                        <Icon name='remove' />Pishmon
                    </Button>
                    <Button color='green' onClick={EditoUserin}>
                        <Icon name='checkmark' /> Përditëso
                    </Button>
                </Modal.Actions>
            </Modal>


            
            {/* Modali per password */}

            <Modal
                closeIcon
                open={deleteModal}
                size='mini'
                onClose={() => setDeleteModal(false )}
                onOpen={() => setDeleteModal(true)}
            >
                <Header icon='lock' content='Ndrysho fjalëkalimin' />
                <Modal.Content>
                    <Input focus placeholder='Fjalëkalimi vjetër' type="password" name="current"
                        onChange={(e) => setCurrent(e.target.value)} />
                    <Input focus placeholder='Fjalëkalimi i ri' type="password" name="password"
                        onChange={(e) => setPassword(e.target.value)} />
                </Modal.Content>
                <Modal.Actions>
                    <Button color='black' onClick={() => setDeleteModal(false)}> 
                        <Icon name='remove' />Pishmon
                    </Button>
                    <Button color='green' onClick={EditPassword}>
                        <Icon name='checkmark' /> Përditëso
                    </Button>
                </Modal.Actions>
            </Modal>
        </BoxContainer>
        </IconContext.Provider>


    );
}
