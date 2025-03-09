import React, { Children } from "react";
import { Outlet } from "react-router-dom";
import '../../styles/AppComponents/MainContent.css'

interface MainContentProps
{
    sidebarOpen : boolean;
    children:any;
}

const MainContent: React.FC<MainContentProps> = ({ sidebarOpen,children }) => {
    return (
      <main className={`main-content ${sidebarOpen ? 'shifted' : ''}`}>
        <div className="content-container">
            {children}
          <Outlet />
        </div>
      </main>
    );
  };
  
  export default MainContent;