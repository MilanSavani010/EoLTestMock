import React, { useEffect, useState } from "react";
import { Profile } from "../../Models/interfaces";
import { fetchProfiles } from "../../services/profile/api";
import List from "../../UtilComponents/List";

const ProfileList: React.FC = () => {

  const [profiles,setProfiles] = useState<Profile[]>([]);
  
  const loadProfiles = async () => {
    try {
        const data: Profile[] | null = await fetchProfiles();
        setProfiles(data);
    } catch (err) {
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
