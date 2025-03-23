import React from 'react';

interface BrickData {
  loadActive: boolean;
  chargeActive: boolean;
  balancingActive: boolean;
}

interface BrickInfoProps {
  brickData: BrickData;
}

const BrickInfo: React.FC<BrickInfoProps> = ({ brickData }) => {
  return (
    <div className="brick-info">
      <div>
        <span>BI_LOAD_ACTIVE: </span>
        <span className={brickData.loadActive ? 'active' : 'inactive'}>
          {brickData.loadActive ? 'Active' : 'Inactive'}
        </span>
      </div>
      <div>
        <span>BI_CHARGE_ACTIVE: </span>
        <span className={brickData.chargeActive ? 'active' : 'inactive'}>
          {brickData.chargeActive ? 'Active' : 'Inactive'}
        </span>
      </div>
      <div>
        <span>BI_BALANCING_ACTIVE: </span>
        <span className={brickData.balancingActive ? 'active' : 'inactive'}>
          {brickData.balancingActive ? 'Active' : 'Inactive'}
        </span>
      </div>
    </div>
  );
};

export default BrickInfo;