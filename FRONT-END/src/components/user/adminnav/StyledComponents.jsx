import styled from "styled-components";

export const BoxContainer = styled.div`
margin-top:10px !important;
width: 80%;
display: flex;
flex-direction: column;
align-items: center;
margin-left:18%;

`;

export const ButtonContainer = styled.div`
width: 50px;
display: flex;
flex-direction: column;
align-items: center;
margin-left:0%;
`;

export const Flexirimi = styled.div`
display:flex;
width:100%;
flex-direction:row;
justify-content:space-between;
align-items:center;
`;

export const Centirimi = styled.div`
width:60%;
margin-left:20%;
display:flex;
flex-direction:row;
align-items:center;
`;
export const NavText = styled.h4`
  font-size: 20px;
  font-weight: 500;
  line-height: 1.4;
  color: #fff;
  margin: 0;
  text-align: center;
`;

export const MainDiv = styled.div`
  margin:30px;
  display:flex;
  padding:0;
  width:100%;
  justify-content:space-between;
  `;

export const TableHead = styled.thead`

  border: 1px solid black;
  background-color:#181818;
  color:white;
  `;


export const TableText = styled.p`
  font-size: 21px;
  color: #white;
  font-weight: normal;
  line-height: 1.4;
  margin-right:60px;
`;

export const RowText = styled.p`
  font-size: 21px;
  color: #black;
  font-weight: normal;
  line-height: 1.4;
  margin-right:60px;
`;

export const PicCentiration = styled.p`
  display:flex;
  flex-direction:column;
  align-items:center;
  justify-content:center;
`;

export const Select = styled.select`
  width: 53%;
  height: 35px;
  outline: none;
  background: white;
  border: 1px solid #85b7d9;
  border-radius:5px;
  padding: 0px 10px;
  color:#a1a1a1;
  transition: all 200ms ease-in-out;
  `;