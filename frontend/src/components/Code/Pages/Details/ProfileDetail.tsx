import React from "react";
import TabLayout from "../../UtilComponents/TabLayout";

const ProfileDetail: React.FC = () => {
  // Define the tabs and their content
  const tabData = [
    {
      label: "General",
      content: <div>🏠 Welcome to the Home tab!</div>,
    },
    {
      label: "Matrices",
      content: <div>👤 Here's your Profile information.</div>,
    },
    {
      label: "Sequences",
      content: <div>⚙️ Adjust your Settings here.</div>,
    },
    {
      label: "Steps",
      content: <div>🔢 Manage your Matrix data.</div>,
    },
  ];

  return (
    <div className="container mx-auto mt-10">
      <h1 className="text-3xl font-bold text-center mb-6"></h1>
      {/* Use the Tabs component */}
      <TabLayout tabs={tabData} layout="vertical"/>
    </div>
  );
};

export default ProfileDetail;
