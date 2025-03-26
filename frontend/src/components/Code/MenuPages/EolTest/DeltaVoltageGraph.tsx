import React, { useEffect, useState } from "react";
import { Line } from "react-chartjs-2";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from 'chart.js'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
);

interface DeltaCellVChartProps {
    deltaCellV: number;
}

const DeltaCellVChart: React.FC<DeltaCellVChartProps> = ({ deltaCellV }) => {
    const [chartData, setChartData] = useState<{
        labels: string[];
        datasets: {
            label: string;
            data: number[];
            borderColor: string;
            backgroundColor: string;
            fill: boolean;
        }[];}>({
        labels: [],
        datasets: [
            {
                label: 'Delta Cell Voltage (mV)',
                data: [],
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                fill: false,
            },
        ],
    });

    useEffect(() => {
        // Update chart data with the new deltaCellV value
        setChartData((prevData) => {
          const newLabels = [...prevData.labels, new Date().toLocaleTimeString()];
          const newData = [...prevData.datasets[0].data, deltaCellV];
    
          // Keep only the last 20 data points to prevent the chart from growing indefinitely
          if (newLabels.length > 20) {
            newLabels.shift();
            newData.shift();
          }
    
          return {
            labels: newLabels,
            datasets: [
              {
                ...prevData.datasets[0],
                data: newData,
              },
            ],
          };
        });
      }, [deltaCellV]);

      const options = {
        responsive: true,
        borderJoinStyle:'round',
        cubicInterpolationMode:'monotone',
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'top' as const,
          },
          title: {
            display: true,
            text: 'Delta Cell Voltage Over Time',
          },
        },
        scales: {
          x: {
            title: {
              display: true,
              text: 'Time',
            },
          },
          y: {
            title: {
              display: true,
              text: 'Delta Cell Voltage (mV)',
            },
            beginAtZero: true,
          },
        },
      };
    
      return (
        <div className="delta-cellv-chart" style={{ height: '300px', marginBottom: '20px' }}>
          <Line data={chartData} options={options} />
        </div>
      );
    };
    
    export default DeltaCellVChart;
