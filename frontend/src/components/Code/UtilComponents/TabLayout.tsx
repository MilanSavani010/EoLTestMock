import React, { useState, ReactNode } from "react";
import "../../styles/UtilComponents/TabLayout.css";

interface Tab {
  label: string; // Tab name
  content: ReactNode; // Tab content
}

interface TabLayoutProps {
  tabs: Tab[]; // Array of tabs
  defaultActiveTabIndex?: number; // Default active tab (optional)
  layout?: "horizontal" | "vertical"; // Layout style
}

const TabLayout: React.FC<TabLayoutProps> = ({ tabs, defaultActiveTabIndex = 0, layout = "horizontal" }) => {
  const [activeTabIndex, setActiveTabIndex] = useState(defaultActiveTabIndex);

  return (
    <div className={`tab-layout ${layout}`}>
      {/* Tab Headers */}
      <div className={`tab-header ${layout}`}>
        {tabs.map((tab, index) => (
          <button
            key={index}
            className={`tab-button ${index === activeTabIndex ? "active" : ""}`}
            onClick={() => setActiveTabIndex(index)}
          >
            {tab.label}
          </button>
        ))}
      </div>

      {/* Active Tab Indicator (For horizontal only) */}
      {layout === "horizontal" && (
        <div
          className="tab-indicator"
          style={{
            width: `${100 / tabs.length}%`,
            transform: `translateX(${activeTabIndex * 100}%)`,
          }}
        />
      )}

      {/* Tab Content */}
      <div className="tab-content">{tabs[activeTabIndex].content}</div>
    </div>
  );
};

export default TabLayout;
