import axios from 'axios'
import React, {useContext, useState} from "react";
import { useEffect } from 'react';
import {
  BoldLink,
  BoxContainer,
  FormContainer,
  Input,
  MutedLink,
  SubmitButton,
  ErrMessage,
  Select,
  Success
} from "./common";
import { Marginer } from "../marginer";
import { AccountContext } from "./accountContext";


export function SignupForm(){

    const[errorState,setError] = useState({
      errValues:{
        emailExist : '',
        succes:''
      }
    })

    const[shtetet,setShtetet] = useState([]);
    const[qytetet,setQytetet] = useState([]);
    const[sh,setSh]= useState();

    const [formState, setFormState] = useState({
      formValues: {
        Email: '',
        Password: '',
        Emri: '',
        Mbiemri:'',
        Kompania: '',
        QytetiId:0,
        Roles:['User']
      },
      formErrors: {
        Email: '',
        Password: '',
        Emri: '',
        Mbiemri:'',
        Kompania: '',
      },
      formValidity: {
        Emri: false,
        Mbiemri: false,
        Kompania: false,
        Email: false,
        Password: false,
      }
    });

    const handleValidation = target => {

      const { name, value } = target;
      const fieldValidationErrors = formState.formErrors;
      const validity = formState.formValidity;

      const isEmail = name === "Email";
      const isPassword = name === "Password";
      const emailTest = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
      const PasswordReg = /^^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%@&*-]).{8,}$/;
  
      validity[name] = value.length > 0 ;
      fieldValidationErrors[name] = validity[name]
        ? ""
        : `${name} duhet të ketë së paku 3 shkronja `;
      if (validity[name]) {
        if (isEmail) {
          validity[name] = emailTest.test(value);
          fieldValidationErrors[name] = validity[name]
            ? ""
            : `${name} duhet të jetë valid`;
        }
        if (isPassword) {
          validity[name] = PasswordReg.test(value);
          fieldValidationErrors[name] = validity[name]
            ? ""
            : `Fjalëkalimi duhet të përmbajë së paku 1 numër,shkronjë,karakter special`;
        }
      }

  setFormState({
        ...formState,
        formErrors: fieldValidationErrors,
        formValidity: validity
      });
    };

    const handleChange = ({ target }) => {
      const { formValues } = formState;
      formValues[target.name] = target.value;
      setFormState({ formValues });
      handleValidation(target);
    
    };

    const handleSubmit = event => {
      event.preventDefault();
      const { formValues, formValidity } = formState;
      const { errValues } = errorState;

      if (Object.values(formValidity).every(Boolean)){
        
        axios.post("http://localhost:5000/api/Account/register",formValues)
        .then(errorState.errValues.succes='Jeni regjistruar me sukses! Ju lutem kycuni!')
        .catch((error)=> {
        
            if(error.response){
                errorState.errValues.emailExist = error.response.data.message;
                setError({errValues});            
                console.log(errorState.errValues.emailExist);
            }
        })
        console.log(formValues)
          
        
      } else {
        console.log(formValidity)
        for (let key in formValues) {
          let target = {
            name: key,
            value: formValues[key]
          };
          handleValidation(target);
        }
      }
    };

    useEffect(() => {
      axios.get('http://localhost:5000/api/Shteti').then(response => {
          setShtetet(response.data);
      });
    })

    const ChangeteState = (e) => {
      setSh({ sh: e.target.value });
      axios.get('http://localhost:5000/api/Qyteti?shtetiId=' + e.target.value).then(response => {
          setQytetet(response.data);
      });
  }
    
    const { switchToSignin } = useContext(AccountContext);

  return (
    <BoxContainer>
      <Success onClick={switchToSignin}>{errorState.errValues.succes}</Success>
      <FormContainer onSubmit={handleSubmit}>
        <ErrMessage>{errorState.errValues.sukses}</ErrMessage>
        <ErrMessage>{formState.formErrors.Emri}</ErrMessage>
        <Input  type="text" placeholder="Emri" name="Emri" onChange={handleChange}  value={formState.formValues.Emri} />
        <ErrMessage>{formState.formErrors.Mbiemri}</ErrMessage>
        <Input  type="text" placeholder="Mbiemri" name="Mbiemri" onChange={handleChange} value={formState.formValues.Mbiemri} />
        <ErrMessage>{formState.formErrors.Kompania}</ErrMessage>
        <Input  type="text" placeholder="Kompania" name="Kompania" onChange={handleChange} value={formState.formValues.Kompania} /> 
        <Select  onChange={ChangeteState} name="emriQytetit"  >  
            <option>Shteti</option> 
          {shtetet.map((e, key) => {  
            return <option key={key} value={e.shtetiId}>{e.emri}</option>;  
            })}  
        </Select>
        <Select name="QytetiId" onChange={handleChange}>  
            <option>Qyteti</option> 
            {qytetet.map((e, key) => {  
            return <option  key={key} value={e.qytetiId}>{e.emri}</option>;  
            })}  
        </Select>  
        <ErrMessage>{formState.formErrors.Email}
                    {errorState.errValues.emailExist}
        </ErrMessage>
        <Input  type="email" placeholder="Emaili" name="Email" onChange={handleChange} value={formState.formValues.Email}/>
        <ErrMessage>{formState.formErrors.Password}</ErrMessage>
        <Input  type="password" placeholder="Fjalëkalimi" name="Password" onChange={handleChange} value={formState.formValues.Password}/>
      </FormContainer>
      <Marginer direction="vertical" margin={10} />
      <SubmitButton type="submit"  onClick={handleSubmit}>Krijo</SubmitButton>
      <Marginer direction="vertical" margin="1em" />
      <MutedLink href="#">
        Jeni regjistruar më parë?
          <BoldLink href="#" onClick={switchToSignin}>Kyquni
          </BoldLink>
      </MutedLink>
    </BoxContainer>
  );
}

