# EoLTestMock

A full-stack solution for End-of-Line (EoL) testing and CAN message simulation, featuring a .NET 8 backend and a modern React frontend powered by Vite.

**⚠️ This project is under development. It is intended as a mock/simulated version for prototyping, testing, and demonstration purposes. Features, APIs, and structure may change.**

---

## Features

- **Backend (.NET 8, ASP.NET Core)**
  - Parses DBC files and handles CAN messages using DbcParserLib
  - Packs and unpacks CAN messages
  - Provides RESTful API endpoints for frontend integration
  - Data models for Brick, Power, and BMS data

- **Frontend (React + Vite)**
  - User-friendly interface for interacting with CAN data and test results
  - Visualizes decoded CAN messages and system status
  - Communicates with backend via HTTP API

---

## Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- Node.js & npm (for frontend)
- Visual Studio Code or Visual Studio

---

## Getting Started

### 1. Clone the repository

```sh
git clone https://github.com/MilanSavani010/EoLTestMock.git
cd EoLTestMock
```

---

### 2. Backend Setup

```sh
cd backend
dotnet restore
dotnet build
dotnet run
```

The backend will start on the configured port (see `backend/Properties/launchSettings.json`).

---

### 3. Frontend Setup

```sh
cd frontend
npm install
npm run dev
```

By default, the frontend (Vite) runs on port **5173**.  
If you want to use a different port, edit `vite.config.ts`:

```typescript
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000 // Change to your desired port
  }
})
```

---

## Project Structure

```
EoLTestMock/
│
├── backend/         # ASP.NET Core backend
│   └── ...          
│
├── frontend/        # React + Vite frontend
│   ├── src/
│   ├── vite.config.ts
│   └── ...
│
└── README.md
```

---

## Usage

- Access the frontend in your browser (default: [http://localhost:5173](http://localhost:5173) or as configured).
- The frontend communicates with the backend API for CAN message operations and test data.

---

## License

MIT License

---

*Feel free to expand this README with API documentation, usage examples, or contribution guidelines as your project grows!*
