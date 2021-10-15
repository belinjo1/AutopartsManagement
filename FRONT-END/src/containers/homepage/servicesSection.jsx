import React from "react";
import { Element } from "react-scroll";
import styled from "styled-components";
import { Marginer } from "../../components/marginer";
import { OurSerivce } from "../../components/ourService";
import { SectionTitle } from "../../components/sectionTitle";

import Service1Img from "../../assets/illustrations/web_development_.png";
import Service2Img from "../../assets/illustrations/mobile_phone.png";
import Service3Img from "../../assets/illustrations/bug_fixed.png";

const ServicesContainer = styled(Element)`
  width: 100%;
  min-height: 1100px;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px 0;
`;

export function ServicesSection(props) {
  return (
    <ServicesContainer name="servicesSection">
      <SectionTitle>Qka ofron ky aplikacion?</SectionTitle>
      <Marginer direction="vertical" margin="3em" />
      <OurSerivce
        title="Menaxhim i lehtë i autopjesëve"
        description="Me anë të produkteve dhe sektorëve ne ju ofrojmë menaxhim 
        dhe kërkim të lehtë të auto-pjesëve."
        imgUrl={Service1Img}
      />
      <OurSerivce
        title="Aplikacion për të gjithë"
        description="Aplikacioni ynë ka përdorim të lehtë dhe se mund të përdoret
        nga kompani dhe individë të ndryshëm."
        imgUrl={Service2Img}
        isReversed
      />
      <OurSerivce
        title="Asistencë 24/7 nga ekipi ynë"
        description="Për qdo parregullsi apo në ndonjë vështirësi ekipa jonë është e 
        gatshme tju asistoj në qdo kohë."
        imgUrl={Service3Img}
      />
    </ServicesContainer>
  );
}
