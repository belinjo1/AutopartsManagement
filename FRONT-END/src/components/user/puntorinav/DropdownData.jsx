import React from 'react';
import * as RiIcons from 'react-icons/ri';
import * as IoIcons from 'react-icons/io';


export const DropdownData = [
  {
    title: 'Cilësimet',
    path: '/settings',
    icon: <IoIcons.IoMdSettings className="iconn" />,
    cName: 'nav-text'
  },
  {
    title: 'Çkyçu',
    path: '/Logout',
    icon: <RiIcons.RiLogoutBoxRFill className="iconn"/>,
    cName: 'nav-text'
  }
];