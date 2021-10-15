import React from "react";
import styled from "styled-components";
import { Button } from "../button";
import { Logo } from "../logo";
import { Marginer } from "../marginer";
import { Link } from 'react-router-dom';

const NavbarContainer = styled.div`
  width: 100%;
  height: 50px;
  padding: 0 2em;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

const BrandContainer = styled.div``;

const AccessibilityContainer = styled.div`
  display: flex;
  flex-direction: row;
`;

const LoginButton = styled(Button)`
  background-color: transparent;
  border: none;

  &:hover {
    background-color: transparent;
    border: none;
    color: rgb(212, 212, 212);
  }
`;

export function Navbar(props) {
  return (
    <NavbarContainer>
      <BrandContainer>
      <Link to="/"><Logo inline /></Link>
      </BrandContainer>
      <AccessibilityContainer>
        <Link  to="/ContactUs"><Button small>Na kontaktoni</Button></Link>
        <Marginer direction="horizontal" margin="8px" />
        <Link  to="/AccountBox"><LoginButton small>Kyquni</LoginButton></Link>
      </AccessibilityContainer>
    </NavbarContainer>
  );
}
