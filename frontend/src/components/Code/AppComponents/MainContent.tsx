import React  from "react";
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
        </div>
      </main>
    );
  };
  
  export default MainContent;