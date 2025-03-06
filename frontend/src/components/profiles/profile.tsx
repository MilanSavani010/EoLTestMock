import React, { useEffect, useState } from "react";
import { fetchProfiles, deleteProfile } from "../services/api";
import ProfileForm from "./profileForm";
import { Profile } from "../types";
import CommandBar from "../commandbar";

const ProfileComponent: React.FC = () => {
    const [profiles, setProfiles] = useState<Profile[] | null>([]);
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
        <div className="p-4">
            <CommandBar
                onCreateProfile={() => alert("Create Profile action")}
            />

            {error && <p className="text-red-500">{error}</p>}

            <ProfileForm onProfileCreated={loadProfiles} />

            <ul className="mt-6 border rounded shadow-md divide-y divide-gray-200 bg-white">
                {profiles?.map((profile) => (
                    <li
                        key={profile.id}
                        onClick={() => handleSelect(profile.id)}
                        className={`p-4 flex justify-between items-center cursor-pointer transition-all ${selectedProfile === profile.id
                                ? "bg-blue-100 border-l-4 border-blue-500 shadow-inner"
                                : "hover:bg-gray-50"
                            }`}
                    >
                        <div>
                            <h2 className="text-xl font-semibold text-gray-800">{profile.name}</h2>
                            <p className="text-sm text-gray-600">ID: {profile.id}</p>
                        </div>
                        <button
                            onClick={(e) => {
                                e.stopPropagation(); // Prevent list item selection
                                handleDelete(profile.id);
                            }}
                            className="bg-red-500 text-white py-1 px-3 rounded hover:bg-red-600 focus:outline-none focus:ring focus:ring-red-300"
                        >
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProfileComponent