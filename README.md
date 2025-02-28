# Supermarket Poster Generator

A web application for New Zealand supermarkets to automatically generate promotional posters by importing product data via CSV. Features include customizable designs, role-based access control, and high-quality poster exports.

## Features

- CSV import with validation for product data
- Drag-and-drop poster customization with templates
- Role-based access control (Non-member, Member, Admin)
- Local-first architecture with cloud migration readiness
- High-quality poster export in PNG/PDF formats

## Tech Stack

- **Frontend**: React (JavaScript)
- **Backend**: ASP.NET Core 7
- **Database**: SQLite (local)
- **Image Processing**: ImageSharp
- **PDF Generation**: QuestPDF
- **Authentication**: JWT

## Project Structure

```
├── backend/                 # ASP.NET Core backend
│   ├── src/                # Source code
│   └── tests/              # Unit and integration tests
├── frontend/               # React frontend
│   ├── src/               # Source code
│   └── public/            # Static assets
└── docs/                  # Documentation
```

## Getting Started

### Prerequisites

- .NET 7 SDK
- Node.js 18+
- npm or yarn
- SQLite

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend/src
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Run migrations:
   ```bash
   dotnet ef database update
   ```

4. Start the backend server:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

## Development

- Backend API runs on `https://localhost:5001`
- Frontend development server runs on `http://localhost:3000`

## License

MIT