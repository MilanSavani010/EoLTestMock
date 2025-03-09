import React from 'react';
import '../../styles/SidebarMenuItems/NotFound.css'

const NotFound: React.FC = () => {
  return (
    <div className="not-found">
      <h1>404 - Not Found</h1>
      <p>The page you're looking for doesn't exist</p>
    </div>
  );
};

export default NotFound;