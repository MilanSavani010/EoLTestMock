import React, { useEffect, useState } from 'react';
import '../../styles/MenuPages/Matrix.css'
import { deleteMatrix, fetchMatrices } from '../services/matrix/api';
import { Matrix } from "../Models/interfaces";

const MatrixComponent: React.FC = () => {
  const [matrices, setMatrices] = useState<Matrix[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [selectedMatrix, setSelectedMatrix] = useState<string | null>(null);


  const loadMatrices = async () => {
      try {
          const data: Matrix[] | null = await fetchMatrices();
          setMatrices(data);
      } catch (err) {
          setError((err as Error).message);
      }
  };


  const handleDelete = async (id: string) => {
      try {
          await deleteMatrix(id);
          setMatrices(matrices.filter((matrix) => matrix.id !== id));
      } catch (err) {
          setError("Failed to delete matrix.");
      }
  };

  const handleSelect = (id: string) => {
    setSelectedMatrix(id === selectedMatrix ? null : id); // Toggle selection
  };
  useEffect(() => {
      loadMatrices();
  }, []);

  if (error) {
      return <p className="text-red-500">Error : {error}</p>
  }

  return (
      <div className="matrix-tab-container">
        
          
      </div>
  );
};

export default MatrixComponent;