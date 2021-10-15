import React, { useContext, useState } from "react";
import { Element, scroller } from "react-scroll";
import styled from "styled-components";
import BackgroundImg from "../../assets/pictures/company_team.jpg";
import { Logo } from "../../components/logo";
import { Marginer } from "../../components/marginer";
import { Navbar } from "../../components/navbar";
import axios from 'axios';
import { Link } from "react-router-dom";
import { Form, Input, TextArea, Select, Button } from "semantic-ui-react";

export const ErrMessage = styled.p`
  color: white;
  font-weight: 500;
  font-size: 15px;
  border-radius:5px;
  background-color:#fc4747;
  z-index: 10;
  margin: 0;
  margin-top: 5px;
`;

export const SuccessMessage = styled.p`
  color: white;
  font-weight: 500;
  font-size: 15px;
  border-radius:5px;
  background-color:green;
  z-index: 10;
  margin: 0;
  margin-top: 5px;
`;

const TopContainer = styled.div`
  width: 100%;
  height: 100vh;
  padding: 0;
  background-image: url(${BackgroundImg});
  position: relative;
`;

const BackgroundFilter = styled.div`
  width: 100%;
  height: 100%;
  background-color: rgba(55, 55, 55, 0.89);
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const PageContainer = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
`;

export function ContactUs(props) {
  const [formState, setFormState] = useState({
    formValues: {
      Emri: "",
      Mbiemri: "",
      Pyetja: "",
      Email: "",
    },
    formErrors: {
      Emri: "",
      Mbiemri: "",
      Pyetja: "",
      Email: "",
    },
    formValidity: {
      Emri: false,
      Mbiemri: false,
      Pyetja: false,
      Email: false,
    },
  });

  const[errorState,setError] = useState({
    errValues:{
      success:''
    }
  })

  const handleValidation = (target) => {
    const { id, value } = target;
    const fieldValidationErrors = formState.formErrors;
    const validity = formState.formValidity;

    const isEmail = id === "Email";
    const emailTest = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;

    validity[id] = value.length > 2;
    fieldValidationErrors[id] = validity[id]
      ? ""
      : `${id} duhet të ketë së paku 3 shkronja `;
    if (validity[id]) {
      if (isEmail) {
        validity[id] = emailTest.test(value);
        fieldValidationErrors[id] = validity[id]
          ? ""
          : `${id} duhet të jetë valid`;
      }
    }

    setFormState({
      ...formState,
      formErrors: fieldValidationErrors,
      formValidity: validity,
    });
  };

  const handleChange = ({ target }) => {
    const { formValues } = formState;
    formValues[target.id] = target.value;
    setFormState({ formValues });
    handleValidation(target);
    setscMessage('');
  };

  const[scmesage,setscMessage]=useState("");

  const handleSubmit = event => {
      event.preventDefault();
      const { formValues, formValidity } = formState;
      const { errValues } = errorState;
      if (Object.values(formValidity).every(Boolean)){

        axios.post("http://localhost:5000/api/Contact",formValues)
        .then(
          setscMessage('Sukses. Ne ju kontaktojmë në email!')
        )
        .catch((error)=> {
          if(error.response){
            errorState.errValues.emailExist = error.response.data.message;
            setError({errValues});
          }         
        });
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

  return (
    <PageContainer>
      <TopContainer>
        <BackgroundFilter>
          <Navbar />
          <Marginer direction="vertical" margin="8em" />
          <Link to="/"><Logo /></Link>
          <br></br>
          <br></br>
          <SuccessMessage>{scmesage}</SuccessMessage>
          <ErrMessage>{formState.formErrors.Emri}</ErrMessage>
          <ErrMessage>{formState.formErrors.Mbiemri}</ErrMessage>
          <ErrMessage>{formState.formErrors.Pyetja}</ErrMessage>
          <ErrMessage>{formState.formErrors.Email}</ErrMessage>
          <br></br>
          <br></br>

          <Element name="contactUs">
            <Form className="contact-us-form" inverted onSubmit={handleSubmit}>
              <Form.Group widths="equal">
                <Form.Field
                  id="Emri"
                  control={Input}
                  label="Emri"
                  onChange={handleChange}
                  placeholder="Filan"
                />

                <Form.Field
                  id="Mbiemri"
                  control={Input}
                  label="Mbiemri"
                  onChange={handleChange}
                  placeholder="Fisteku"
                />
              </Form.Group>
              <Form.Field
                id="Pyetja"
                control={TextArea}
                label="Pyetja"
                onChange={handleChange}
                placeholder="Pyetja"
              />

              <Form.Field
                id="Email"
                control={Input}
                label="Email"
                onChange={handleChange}
                placeholder="filan@fisteku.com"
              />

              <Form.Field
                id="form-button-control-public"
                control={Button}
                content="Konfirmo"
                placeholder="Button"
                onClick={handleSubmit}
              />
            </Form>
          </Element>
        </BackgroundFilter>
      </TopContainer>
    </PageContainer>
  );
}
