import React from "react";

interface CommandBarProps {
    onCreateProfile: () => void;
}

const CommandBar: React.FC<CommandBarProps> = ({ onCreateProfile }) => {
    return (
      <div className="flex justify-between items-center bg-gray-100 p-4 rounded shadow-md mb-6">
        <h1 className="text-2xl font-bold">Profile Management</h1>
        <div className="flex gap-4">
          <button
            onClick={onCreateProfile}
            className="bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600"
          >
            Create Profile
          </button>
        
          
        </div>
      </div>
    );
  };
  
  export default CommandBar;