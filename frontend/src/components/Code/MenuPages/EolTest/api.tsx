
const API_URL = "http://localhost:5227";

// Create a new profile
export const Start = async (): Promise<void> => {
  const response = await fetch(`${API_URL}/start`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
  });
  if (!response.ok) {
  }
};

export const Stop = async (): Promise<void> => {
    const response = await fetch(`${API_URL}/stop`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
    });
    if (!response.ok) {
    }
  };
  