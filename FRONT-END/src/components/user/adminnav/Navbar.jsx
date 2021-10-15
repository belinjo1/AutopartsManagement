import React, { useState } from 'react';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as RiIcons from 'react-icons/ri';
import { Link } from 'react-router-dom';
import { SidebarData } from './SidebarData';
import './Navbar.css';
import { IconContext } from 'react-icons';
import { DropdownData } from './DropdownData';
import { useSelector } from 'react-redux';
import { selectUser } from '../../../reducers/rootReducer';
import { NavText } from './StyledComponents';

function AdminNav(props) {

  const [sidebar, setSidebar] = useState(false);
  const showSidebar = () => setSidebar(!sidebar);

  const[open, setOpen] = useState(false);

  const useri = useSelector(selectUser);
  
  const showDropdown =() => setOpen(!open);


  return (
    <>
    
      <IconContext.Provider value={{ color: 'FC4747' }}>
        <div className='navbar'>
            <Link to='#' className='menu-bars'>
              <FaIcons.FaBars onClick={showSidebar} />
            </Link>
  
            <Link to='#' className='menu-user' onClick={showDropdown}>
                  <NavText>{useri.emri}</NavText>
                  <RiIcons.RiUserFill />
            </Link>
        </div>

        <nav className={open ? 'dropdown active' : 'dropdown'}>
          <ul className='nav-menu-items' onClick={showDropdown}>
            <li className='dropdown-toggle'>
              <Link to='#' className='menu-user'>
                <AiIcons.AiOutlineClose />
              </Link>
            </li>
            {DropdownData.map((item, index) => {
              return (
                <li key={index} className={item.cName}>
                  <Link to={item.path}>
                    {item.icon}
                    <span>{item.title}</span>
                  </Link>
                </li>
              );
            })}
          </ul>
        </nav>

        <nav className={sidebar ? 'nav-menu active' : 'nav-menu'}>
          <ul className='nav-menu-items' onClick={showSidebar}>
            <li className='dropdown-toggle'>
              <Link to='#' className='menu-bars'>
                <AiIcons.AiOutlineClose />
              </Link>
            </li>
            {SidebarData.map((item, index) => {
              return (
                <li key={index} className={item.cName}>
                  <Link to={item.path}>
                    {item.icon}
                    <span>{item.title}</span>
                  </Link>
                </li>
              );
            })}
          </ul>
        </nav>
      </IconContext.Provider>
    </>
  );
}

export default AdminNav;