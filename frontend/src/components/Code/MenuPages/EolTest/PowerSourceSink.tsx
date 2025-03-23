import React from 'react';
import ValuePanel from './ValuePanel';

interface PowerData {
  active: boolean;
  voltage: number;
  currentDischarge: number;
  currentCharge: number;
  powerDischarge: number;
  powerCharge: number;
}

interface PowerSourceSinkProps {
  powerData: PowerData;
}

const PowerSourceSink: React.FC<PowerSourceSinkProps> = ({ powerData }) => {
  return (
    <div className="power-source-sink">
      <h2>Power Source/Sink</h2>
      <div>
        <span>Active: </span>
        <span className={powerData.active ? 'active' : 'inactive'}>
          {powerData.active ? 'Power ON' : 'Power OFF'}
        </span>
      </div>
      <ValuePanel label="Voltage Measured/Set" value={powerData.voltage} unit='V' />
      <ValuePanel label="Current Discharge Measured/Set" value={powerData.currentDischarge} unit='A'/>
      <ValuePanel label="Current Charge Measured/Set" value={powerData.currentCharge} unit='A'/>
      <ValuePanel label="Power Discharge Measured/Set" value={powerData.powerDischarge}  unit='W' />
      <ValuePanel label="Power Charge Measured/Set" value={powerData.powerCharge} unit='W'/>
    </div>
  );
};

export default PowerSourceSink;