import React, { useEffect, useState } from 'react';
import { deleteProfile, fetchProfiles } from '../services/profile/api';
import { Profile } from "../Models/interfaces";
import ProfileList from '../Pages/Lists/ProfileList';

const ProfileComponent: React.FC = () => {
  const [profiles, setProfiles] = useState<Profile[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [selectedProfile, setSelectedProfile] = useState<string | null>(null);


  const loadProfiles = async () => {
      try {
          const data: Profile[] | null = await fetchProfiles();
          setProfiles(data);
      } catch (err) {
          setError((err as Error).message);
      }
  };


  const handleDelete = async (id: string) => {
      try {
          await deleteProfile(id);
          setProfiles(profiles.filter((profile) => profile.id !== id));
      } catch (err) {
          setError("Failed to delete profile.");
      }
  };

  const handleSelect = (id: string) => {
      setSelectedProfile(id === selectedProfile ? null : id); // Toggle selection
  };
  useEffect(() => {
      loadProfiles();
  }, []);

  if (error) {
      return <p className="text-red-500">Error : {error}</p>
  }

  return (
      <div className="profile-tab-container">
        
          <ProfileList/>
      </div>
  );
};

export default ProfileComponent;