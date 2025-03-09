import React from 'react';
import '../styles/UtilComponents/Header.css';

interface HeaderProps {
  title: string;
  buttonText: string;
  onClick: () => void;
}

const Header: React.FC<HeaderProps> = ({ title, buttonText, onClick }) => {
  return (
    <header className="header">
      <h1 className="header-title">{title}</h1>
      <button className="header-button" onClick={onClick}>
        {buttonText}
      </button>
    </header>
  );
};

export default Header;