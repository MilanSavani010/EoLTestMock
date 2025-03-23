import React from 'react';

interface BrickControlsProps {
  onInitialize: () => void;
  onStart: () => void;
  onStop: () => void;
}

const BrickControls: React.FC<BrickControlsProps> = ({ onInitialize, onStart, onStop }) => {
  return (
    <div className="brick-controls">
      <button onClick={onInitialize}>Initialize</button>
      <button onClick={onStart}>Start</button>
      <button onClick={onStop}>Stop</button>
    </div>
  );
};

export default BrickControls;