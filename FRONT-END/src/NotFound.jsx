import React from 'react'
import notAuthorized from '../src/assets/pictures/notAuthorized.png'
import logo from '../src/assets/logo/logo.png'
import { PicCentiration } from './components/user/navbar/StyledComponents'


export const NotFound = () => {
    return (
        <div>
            <PicCentiration>
                <img src={notAuthorized} />
                <img src={logo} width="300px" />
            </PicCentiration>
        </div>
    )
}
