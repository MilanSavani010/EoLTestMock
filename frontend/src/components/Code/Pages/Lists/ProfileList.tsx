import React, { useEffect, useState } from "react";
import { Profile } from "../../Models/interfaces";
import { fetchProfiles } from "../../services/profile/api";
import List from "../../UtilComponents/List";

const ProfileList: React.FC = () => {

  const users = [
    { id: 1, name: "John Doe", email: "john.doe@example.com" },
    { id: 2, name: "Jane Smith", email: "jane.smith@example.com" },
    { id: 3, name: "Alice Johnson", email: "alice.johnson@example.com" },
  ];
  
  const [profiles,setProfiles] = useState<Profile[]>([]);
  const [error, setError] = useState<string | null>(null);
  
  const loadProfiles = async () => {
    try {
        const data: Profile[] | null = await fetchProfiles();
        setProfiles(data);
    } catch (err) {
        setError((err as Error).message);
    }
  };

  useEffect(() => {
    loadProfiles();
}, []);


return (
  <div>
  <h1 style={{ textAlign: "center", marginTop: "20px" }}>Self-Managed List</h1>
    <List items={profiles} getKey={(profile)=>profile.id} renderContent={(profile) => (
          <div>
          <strong>{profile.id}</strong>
          <br />
          <span>{profile.name}</span>
        </div>
        )} />
</div>
)
  
};

export default ProfileList;
