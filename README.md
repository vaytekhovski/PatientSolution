# Project Setup

## Requirements
- Docker
- Docker Compose

## Running the Project

1. **Clone the repository:**
   ```sh
   git clone <repo-url>
   cd <repo-folder>
   ```

2. **Build and start the containers:**
   ```sh
   docker-compose up --build -d
   ```

3. **Access the API:**
   - Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - API Base URL: `http://localhost:5000`

4. **Stopping the containers:**
   ```sh
   docker-compose down
   ```

5. **Postman**.
   - The collection file for Postman is available at: [https://www.postman.com/sngjob/patient-solution/overview](https://www.postman.com/sngjob/patient-solution/overview).
   - Don't forget to select environment!
