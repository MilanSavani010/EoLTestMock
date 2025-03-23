import React from 'react';

interface ValuePanelProps {
    label: string;
    value: number;
    unit?: string;
    progress?: number; // Optional progress value for the bar
    maxProgress?: number; // Optional max value for the progress bar
}

const ValuePanel: React.FC<ValuePanelProps> = ({ label, value, unit, progress, maxProgress }) => {
    const showProgressBar = progress !== undefined && maxProgress !== undefined;
    const progressPercentage = showProgressBar ? (progress / maxProgress) * 100 : 0;

    return (
        <div className="value-panel">
            <div className="label">{label}</div>
            {showProgressBar ? (
                <div className="progress-container">
                    <div
                        className="progress-bar"
                        style={{ width: `${progressPercentage}%` }}
                    ></div>
                    <div className="progress-value">{value.toFixed(2)}</div>
                </div>
            ) : (
                <>
                    <div className="value">{value.toFixed(2)}</div>
                    <div className="unit">{unit}</div>
                </>

            )}
        </div>
    );
};

export default ValuePanel;