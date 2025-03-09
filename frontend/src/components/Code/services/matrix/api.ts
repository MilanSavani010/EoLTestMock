import { Matrix } from "../../Models/interfaces";

const API_URL = "http://localhost:5227";

// Fetch all matrices
export const fetchMatrices = async (): Promise<Matrix[]> => {
  const response = await fetch(`${API_URL}/matrices`);
  return await response.json();
};

// Fetch a matrix by ID
export const fetchMatrixById = async (id: string): Promise<Matrix> => {
  const response = await fetch(`${API_URL}/matrices/${id}`);
  if (!response.ok) {
    throw new Error(`Failed to fetch matrix: ${response.statusText}`);
  }
  return await response.json();
};

// Create a new matrix
export const createMatrix = async (matrix: Matrix): Promise<void> => {
  const response = await fetch(`${API_URL}/matrices`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(matrix),
  });
  if (!response.ok) {
    throw new Error(`Failed to create matrix: ${response.statusText}`);
  }
};

// Update an existing matrix
export const updateMatrix = async (id: string, matrix: Matrix): Promise<void> => {
  const response = await fetch(`${API_URL}/matrices/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(matrix),
  });
  if (!response.ok) {
    throw new Error(`Failed to update matrix: ${response.statusText}`);
  }
};

// Delete a matrix
export const deleteMatrix = async (id: string): Promise<void> => {
  const response = await fetch(`${API_URL}/matrices/${id}`, { method: "DELETE" });
  if (!response.ok) {
    throw new Error(`Failed to delete matrix: ${response.statusText}`);
  }
};