import {Profile} from "../../Models/interfaces";

const API_URL = "http://localhost:5227";


// Fetch all profiles
export const fetchProfiles = async (): Promise<Profile[]> => {
  const response = await fetch(`${API_URL}/profiles`);
  return await response.json();
};

// Fetch a profile by ID
export const fetchProfileById = async (id: string): Promise<Profile> => {
  const response = await fetch(`${API_URL}/profiles/${id}`);
  if (!response.ok) {
    throw new Error(`Failed to fetch profile: ${response.statusText}`);
  }
  return await response.json();
};

// Create a new profile
export const createProfile = async (profile: Profile): Promise<void> => {
  const response = await fetch(`${API_URL}/profiles`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(profile),
  });
  if (!response.ok) {
    throw new Error(`Failed to create profile: ${response.statusText}`);
  }
};

// Update an existing profile
export const updateProfile = async (id: string, profile: Profile): Promise<void> => {
  const response = await fetch(`${API_URL}/profiles/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(profile),
  });
  if (!response.ok) {
    throw new Error(`Failed to update profile: ${response.statusText}`);
  }
};

// Delete a profile
export const deleteProfile = async (id: string): Promise<void> => {
  const response = await fetch(`${API_URL}/profiles/${id}`, { method: "DELETE" });
  if (!response.ok) {
    throw new Error(`Failed to delete profile: ${response.statusText}`);
  }
};
