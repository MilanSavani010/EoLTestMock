import React, { useState, useEffect } from 'react';
import BrickControls from './BrickControls';
import BrickInfo from './BrickInfo';
import ValuePanel from './ValuePanel';
import PowerSourceSink from './PowerSourceSink';
import { Start,Stop ,Init} from './api';
import './EolTest.css';
import DeltaCellVChart from './DeltaVoltageGraph';


// Define types for Brick and Power data
interface BrickData {
  loadActive: boolean;
  chargeActive: boolean; // Now a percentage (0-100)
  balancingActive: boolean;
  voltage: number;
  maxCellV: number;
  minCellV: number;
  current: number;
  dcli: number;
  dclo: number;
  temperature: number; // Temperature in °C, will be used for progressive bar
  deltaCellV: number;
  cells: number[];
}

interface PowerData {
  active: boolean;
  voltage: number;
  currentDischarge: number;
  currentCharge: number;
  powerDischarge: number;
  powerCharge: number;
}

interface BmsData {
    brickData: BrickData;
    powerData: PowerData;
  }

const EolTest: React.FC = () => {
  const [brickId, setBrickId] = useState<string>('');
  const [brickData, setBrickData] = useState<BrickData>({
    loadActive: false,
    chargeActive: false, // Start at 0%
    balancingActive: false,
    voltage: 0,
    maxCellV: 0,
    minCellV: 0,
    current: 0,
    dcli: 0,
    dclo: 0,
    temperature: 0, // Start at 0°C
    deltaCellV: 0,
    cells: Array(14).fill(0),
  });
  const [powerData, setPowerData] = useState<PowerData>({
    active: false,
    voltage: 0,
    currentDischarge: 0,
    currentCharge: 0,
    powerDischarge: 0,
    powerCharge: 0,
  });
  const [isRunning,setIsRunning]= useState<boolean>(true);
 
   

  // Initialize SSE connection
  useEffect(() => {
    const es = new EventSource('http://localhost:5227/stream'); // Adjust URL/port as needed

    es.onopen = () => {
      console.log('SSE connected');
    };

    es.onmessage = (event) => {
      try {
        if (event.data === 'null') {
          // Handle null data
          return;
        }

        const data: BmsData = JSON.parse(event.data);
        if (isRunning) {
          // Defensive checks to ensure data.brickData and data.powerData exist
          if (data.brickData && data.powerData) {
            setBrickData(data.brickData);
            setPowerData(data.powerData);
          } else {
            console.error('Invalid SSE data:', data);
          }
        }
      } catch (err) {
        console.error('Error parsing SSE data:', err);
      }
    };

    es.onerror = () => {
      console.error('SSE connection error');
      es.close();
    };

    es.addEventListener('close', () => {
      console.log('SSE connection closed by server');
      es.close();
    });


    return () => {
      es.close();
    };
  }, [isRunning]);
  


  const handleInitialize = () => {
    setBrickData({
      ...brickData,
      loadActive: true,
      chargeActive: true,
      balancingActive: true,
      voltage: 0,
      maxCellV: 0,
      minCellV: 0,
      current: 0,
      dcli: 0,
      dclo: 0,
      temperature: 0,
      deltaCellV: 0,
      cells: Array(14).fill(0),
    });
    setPowerData({
      active: true,
      voltage: 0,
      currentDischarge: 0,
      currentCharge: 0,
      powerDischarge: 0,
      powerCharge: 0,
    });
    Init();
  };

  const handleStart = () => {
    setBrickData({
      ...brickData,
      loadActive: true,
      chargeActive: true,
      balancingActive: true,
    });
    setPowerData({ ...powerData, active: true });
    Start();
    setIsRunning(true);
  };

  const handleStop = () => {
    setBrickData({
      ...brickData,
      chargeActive:false,
      loadActive: false,
      balancingActive: false,
    });
    setPowerData({ ...powerData, active: false });
    Stop();
    setIsRunning(false);
    };

  return (
    <div className="EoLTest">
      <h1>Battery Management System GUI</h1>
      <div className="brick-id">
        <label>Brick ID: </label>
        <input
          type="text"
          value={brickId}
          onChange={(e) => setBrickId(e.target.value)}
          placeholder="Enter Brick ID"
        />
      </div>
      <BrickControls
        onInitialize={handleInitialize}
        onStart={handleStart}
        onStop={handleStop}
      />
      <BrickInfo brickData={brickData} />
      <div className="metrics-grid">
        <ValuePanel label="BI_VOLTAGE" value={brickData.voltage} unit='V' />
        <ValuePanel label="BI_MAX_CELL_V" value={brickData.maxCellV} unit='mV' />
        <ValuePanel label="BI_MIN_CELL_V" value={brickData.minCellV}  unit='mV'/>
        <ValuePanel label="BI_CURRENT" value={brickData.current}  unit='A'/>
        <ValuePanel label="BI_DCLI" value={brickData.dcli}  unit='A'/>
        <ValuePanel label="BI_DCLO" value={brickData.dclo} unit='A'/>
        <ValuePanel
          label="BI_TEMPERATURE"
          value={brickData.temperature}
          unit='°C'
          progress={brickData.temperature}
          maxProgress={60} // Assuming max temperature is 60°C
        />
        <ValuePanel label="BI_DELTA_CELL_V" value={brickData.deltaCellV}  unit='mV' />
      </div>
      <DeltaCellVChart deltaCellV={brickData.deltaCellV} />
      <div className="cell-voltages">
        {brickData.cells.map((cell, index) => (
          <ValuePanel
            key={index}
            label={`BI_CELL_${index + 1}`}
            value={cell} 
            unit='mV'
          />
        ))}
      </div>
      <PowerSourceSink powerData={powerData} />
    </div>
  );
};

export default EolTest;