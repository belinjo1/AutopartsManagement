import styled from "styled-components";

export const BoxContainer = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: px;
`;

export const Select = styled.select`
  width: 100%;
  height: 35px;
  outline: none;
  background: white;
  color: rgba(200, 200, 200, 1);
  border: 1px solid rgba(200, 200, 200, 0.3);
  padding: 0px 10px;
  font-size: 12px;
  transition: all 200ms ease-in-out;
  border-bottom: 1.4px solid transparent;
  
  &:not(:last-of-type) {
    border-bottom: 1.5px solid rgba(200, 200, 200, 0.4);
  }

  &:focus {
    outline: none;
    border-bottom: 2px solid #fc4747;
  }


       option {
        color:gray;
         background: white;
         font-size: 12px;
         display: flex;
         white-space: pre;
         min-height: 20px;
         padding: 0px 2px 1px;
       }
`;

export const ErrMessage = styled.p`
  color: red;
  font-weight: 500;
  font-size: 10px;
  z-index: 10;
  margin: 0;
  margin-top: 5px;
`;

export const Success = styled.a`
  text-decoration:none;
  color: green;
  font-weight: 500;
  font-size: 11px;
  z-index: 10;
  margin: 0;
  margin-top: 5px;
`;

export const FormContainer = styled.form`
  width: 100%;
  display: flex;
  flex-direction: column;
  box-shadow: 0px 0px 2.5px rgba(15, 15, 15, 0.19);
`;

export const MutedLink = styled.a`
  font-size: 11px;
  color: rgba(200, 200, 200, 0.8);
  font-weight: 500;
  text-decoration: none;
`;

export const BoldLink = styled.a`
  font-size: 11px;
  color: #fc4747;
  font-weight: 500;
  text-decoration: none;
  margin: 0 4px;
`;

export const Input = styled.input`
  width: 100%;
  height: 35px;
  outline: none;
  border: 1px solid rgba(200, 200, 200, 0.3);
  padding: 0px 10px;
  border-bottom: 1.4px solid transparent;
  transition: all 200ms ease-in-out;
  font-size: 12px;

  &::placeholder {
    color: rgba(200, 200, 200, 1);
  }

  &:not(:last-of-type) {
    border-bottom: 1.5px solid rgba(200, 200, 200, 0.4);
  }

  &:focus {
    outline: none;
    border-bottom: 2px solid #fc4747;
  }
`;

export const SubmitButton = styled.button`
  width: 100%;
  padding: 11px 40%;
  color: #fff;
  font-size: 15px;
  font-weight: 600;
  border: none;
  border-radius: 100px 100px 100px 100px;
  cursor: pointer;
  transition: all, 240ms ease-in-out;
  background-color: #fc5296;
  background: rgb(252,71,71);
  background: linear-gradient(90deg, rgba(252,71,71,1) 0%, rgba(172,3,3,1) 100%);
  );

  &:hover {
    filter: brightness(1.03);
  };
`;
