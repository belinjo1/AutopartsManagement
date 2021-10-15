import React from "react";
import styled from "styled-components";
import { theme } from "../../theme";

const ButtonWrapper = styled.button`
  padding: ${({ small }) => (small ? "5px 8px" : "7px 15px")};
  border-radius: 5px;
  background-color: grey;
  color: #fff;
  font-weight: bold;
  font-size: ${({ small }) => (small ? "12px" : "16px")};
  outline: none;
  width:140px;
  border: 2px solid transparent;
  transition: all 220ms ease-in-out;
  cursor: pointer;
  

  &:hover {
    background-color: transparent;
    border: 2px solid grey;
    color: grey;
  }
`;

export function UpdateButton(props) {
  return <ButtonWrapper {...props}>{props.children}</ButtonWrapper>;
}
