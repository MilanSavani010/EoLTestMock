import React, { useState } from "react";
import { createProfile } from "../services/api";
import { Profile } from "../types"

interface ProfileFormProps {
    onProfileCreated: () => void;  //Callback to refresh the profile list
}

const ProfileForm: React.FC<ProfileFormProps> = ({ onProfileCreated }) => {
    const [name, setName] = useState("");
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();


        if (!name.trim()) {
            setError("Profile name is required.");
            return;
        }

        try {
            const newProfile: Profile =
            {
                id: "",
                name,
                matrices: [],
                sequences: [],
            }

            await createProfile(newProfile);
            setName("");
            setError(null);
            onProfileCreated();
        } catch (err) {
            setError("Failed to create the profile. Please try again.");
        }
    };


    return(
        <form onSubmit={handleSubmit} className="p-4 border rounded shadow-md bg-white">
      <h2 className="text-lg font-bold mb-4">Create New Profile</h2>
      {error && <p className="text-red-500">{error}</p>}
      <div className="mb-4">
      
        <input
          id="name"
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="mt-1 p-2 block w-full border rounded"
          placeholder="Enter profile name"
        />
      </div>
      <button
        type="submit"
        className="bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600"
      >
        Create Profile
      </button>
    </form>
    );

};

export default ProfileForm;