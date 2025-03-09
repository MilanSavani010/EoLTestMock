import React from "react";
import { NavLink } from "react-router-dom";
import '../../styles/AppComponents/SideBar.css'

interface MenuItem
{
    id: number;
    label: string;
    path: string;
    icon?: string;
}

interface SidebarProps
{
    isOpen:boolean;
    onToggle:()=>void;
    menuItems: MenuItem[];
}

const Sidebar: React.FC<SidebarProps> = ({ isOpen, onToggle, menuItems }) => {
  return (
    <div className={`sidebar ${isOpen ? 'open' : 'closed'}`}>
      <button className="toggle-btn" onClick={onToggle}>
        {isOpen ? '✕' : '☰'}
      </button>
      <nav className="menu">
        <ul>
          {menuItems.map((item) => (
            <li key={item.id}>
              <NavLink
                to={item.path}
                className={({ isActive }) => (isActive ? 'active' : '')}
                onClick={onToggle} // Close dropdown on item click
              >
                {item.icon && <span className="menu-icon">{item.icon}</span>}
                <span className="menu-text">{item.label}</span>
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
    </div>
  );
};
  
  export default Sidebar;