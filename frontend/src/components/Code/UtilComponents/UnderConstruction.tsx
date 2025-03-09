import React from 'react';
import '../../styles/UtilComponents/UnderConstruction.css';

interface UnderConstructionProps {
  message?: string; // Optional custom message
}

const UnderConstruction: React.FC<UnderConstructionProps> = ({ message = 'Page Under Construction' }) => {
  return (
    <div className="under-construction">
      <h1 className="under-construction-title">🚧</h1>
      <p className="under-construction-message">{message}</p>
      <p className="under-construction-note">Check back later!</p>
    </div>
  );
};

export default UnderConstruction;