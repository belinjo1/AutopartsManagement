import React from "react";
import styled from "styled-components";
import { theme } from "../../theme";



const ButtonWrapper = styled.button`
  padding: ${({ small }) => (small ? "8px 8px" : "8px 20px")};
  border-radius: 5px;
  background-color: #181818;
  color: #fff;
  font-weight: bold;
  font-size: ${({ small }) => (small ? "12px" : "16px")};
  outline: none;
  border: 2px solid transparent;
  transition: all 220ms ease-in-out;
  cursor: pointer;
  display:flex;
  width:170px;
  height:40px;
  justify-content:space-between;

  &:hover {
    background-color: transparent;
    border: 2px solid black;
    color: black;
  }
`;

export function AddButton(props) {
  return <ButtonWrapper {...props}>{props.children}</ButtonWrapper>;
}
