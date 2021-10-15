import React from "react";
import { Element } from "react-scroll";
import styled from "styled-components";
import { SectionTitle } from "../../components/sectionTitle";

import AboutImgUrl from "../../assets/illustrations/rocket_launch_.png";

const MoreAboutContainer = styled(Element)`
  min-height: 500px;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 0 1em;
`;

const AboutContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  max-width: 1000px;

  @media screen and (max-width: 480px) {
    max-width: 100%;
    flex-direction: column-reverse;
  }
`;

const AboutText = styled.p`
  font-size: 21px;
  color: #2f2f2f;
  font-weight: normal;
  line-height: 1.4;
`;

const AboutImg = styled.img`
  width: 600px;
  height: 500px;
  margin-left: 2em;

  @media screen and (max-width: 480px) {
    width: 370px;
    height: 290px;
    margin-left: 0;
  }
`;

export function MoreAboutSection(props) {
  return (
    <MoreAboutContainer>
      <SectionTitle>Më shumë rreth nesh</SectionTitle>
      <AboutContainer>
        <AboutText>
          Auto Parts Management System është një aplikacion i cili bën organizimin e auto pjesëve në invertarë dhe sektorë. {<br />}
          {<br />} Qëllimi i këtij aplikacioni është lehtësimi në gjetjen dhe identifikimin e auto pjesës
          në mënyrë sa më të shpejtë dhe efikase.{" "}
          {<br />}
          {<br />} Aplikacioni përmban me vete edhe shumë funksione të tjera të cilat ndihmojnë qdo individ apo kompani.
        </AboutText>
        <AboutImg src={AboutImgUrl} />
      </AboutContainer>
    </MoreAboutContainer>
  );
}
