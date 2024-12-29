# RoomAdvisor

RoomAdvisor is a lightweight backend service that provides actionable advice for maintaining optimal indoor air quality and heating conditions. Based on real-time room data (e.g., temperature and humidity) and outdoor weather data, it recommends actions like ventilation (`lüften`) or heating adjustments (`heizen`). This service is designed to work as a standalone backend but can be easily integrated into apps or smart home systems for automated decision-making.

This project includes a ready-to-use Docker setup, making deployment and development seamless. Whether you're building a mobile app, enhancing smart home automation, or experimenting with backend development, RoomAdvisor offers a flexible and customizable foundation for indoor climate recommendations.

---

## Features

- Fetches weather data using latitude and longitude.
- Processes room data to recommend actions for air quality and heating.
- Simple, customizable, and deployable using Docker.

---

## API Endpoints

### 1. Get Current Weather

**Request:**
```
GET http://localhost:5161/api/weather/current?latitude=51.481846&longitude=7.216236
Accept: application/json
```

**Response Example:**
```json
{
  "temperature": 12.5,
  "humidity": 78
}
```

---

### 2. Get Room Advice

**Request:**
```
POST http://localhost:5161/api/room/get-advice?latitude=51.481844&longitude=7.216236
Content-Type: application/json

[
    { "name": "Bedroom", "temperature": 19.5, "humidity": 60 },
    { "name": "Kitchen", "temperature": 22.0, "humidity": 40 }
]
```

**Response Example:**
```json
{
  "recommendations": [
    {
      "room": "Bedroom",
      "action": "lüften",
      "duration": "10 minutes"
    },
    {
      "room": "Kitchen",
      "action": "heizen",
      "level": 2.5,
      "duration": "2 hours"
    }
  ]
}
```

---

## How to Use

### 1. Fork and Clone the Repository
Fork the repository to your GitHub account and clone it to your local machine:
```bash
git clone https://github.com/<your-username>/roomadvisor.git
cd roomadvisor
```

### 2. Run Locally with Docker
Use the provided `Dockerfile` to easily deploy the project:
```bash
docker compose up -d
```

This will:
- Build and run the backend service.
- Automatically update the container using Watchtower (already included in the `docker-compose.yml`).

The service will be available at `http://localhost:5161`.

---

## Customization

- Add new endpoints or modify existing ones in the `Controllers` folder.
- Change the Docker setup in the `docker-compose.yml` file as needed.
- Use the `.env` file to securely manage API keys like `GEMINI_API_KEY` and `OPEN_WEATHER_API_KEY`.

---

## Example Use Cases

1. **Mobile App Integration:** Use RoomAdvisor as a backend for a smart air quality app that alerts users when to ventilate or adjust heating.
2. **Smart Home Automation:** Integrate RoomAdvisor into a smart home system to automate ventilation or heating controls.
3. **Experimentation:** Build upon RoomAdvisor for academic projects or to prototype advanced climate recommendations.

---

## Contributing

Feel free to open issues, suggest features, or submit pull requests. Contributions are welcome!

---

## License

This project is licensed under the Apache 2.0 License.

---

**RoomAdvisor** is a basic service designed for simplicity and extensibility. Whether you're a developer, researcher, or enthusiast, you can easily adapt this project to fit your needs.
