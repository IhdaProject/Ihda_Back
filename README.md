# Ihda Backend
اهْدِنَا الصِّرَاطَ الْمُسْتَقِيمَ
## Introduction
The Ihda Backend is the core server-side application for the Ihda project, designed to provide accurate prayer times and facilitate navigation to the nearest mosques. This backend powers the Ihda app, ensuring reliable and efficient data delivery to users worldwide.

## Features
- **Prayer Times API:** Fetch accurate prayer times based on user’s location.
- **Masjid Navigation:** Retrieve nearest masjid details with navigation instructions.
- **Multi-Language Support:** Arabic, Persian, Turkish, and more.
- **Global Coverage:** Compatible with locations worldwide.
- **Optimized Performance:** Built with scalability in mind for a smooth user experience.

## Technologies Used
- **Programming Language:** .NET 8
- **Database:** PostgreSQL
- **Authentication:** JWT
- **API Documentation:** Swagger
- **Hosting:** Dockerized deployment

## Installation

### Prerequisites
- .NET 8 SDK
- Docker and Docker Compose
- PostgreSQL Database

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ihda-backend.git
   cd ihda-backend
   ```

2. Set up environment variables by creating a `.env` file:
   ```env
   ASPNETCORE_ENVIRONMENT=Development
   JWT_SECRET=your_jwt_secret
   DB_HOST=localhost
   DB_PORT=5432
   DB_USER=your_db_user
   DB_PASSWORD=your_db_password
   DB_NAME=ihda_db
   ```

3. Build and run the application using Docker:
   ```bash
   docker-compose up --build
   ```

4. Access the API at:
    - Base URL: `http://localhost:5000`
    - Swagger Documentation: `http://localhost:5000/swagger`

## API Endpoints

### Prayer Times
```http
GET /api/prayertimes?latitude=XX&longitude=YY&date=YYYY-MM-DD
```
- **Parameters:**
    - `latitude`: User’s latitude.
    - `longitude`: User’s longitude.
    - `date`: (Optional) Date for which to retrieve prayer times.
- **Response:**
  ```json
  {
    "fajr": "05:00",
    "dhuhr": "12:30",
    "asr": "15:45",
    "maghrib": "18:15",
    "isha": "19:30"
  }
  ```

### Nearest Mosque
```http
GET /api/masjids/nearest?latitude=XX&longitude=YY
```
- **Parameters:**
    - `latitude`: User’s latitude.
    - `longitude`: User’s longitude.
- **Response:**
  ```json
  {
    "name": "Masjid Al-Falah",
    "address": "123 Main Street, City, Country",
    "distance": "1.2 km"
  }
  ```

## Screenshots

### Swagger API Documentation
![Swagger API Documentation](https://via.placeholder.com/800x400.png?text=Swagger+Documentation)

### Example Prayer Times Response
![Prayer Times Response](https://via.placeholder.com/800x400.png?text=Prayer+Times+API+Response)

## Contributing
We welcome contributions! Please follow these steps:
1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact
For questions or support, please contact us at support@ihda.com.
