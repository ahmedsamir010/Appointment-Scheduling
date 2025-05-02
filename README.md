# Appointments API

A simple RESTful API to manage appointments.

🔗 **Swagger UI**: [https://addappointment.runasp.net/swagger/index.html](https://addappointment.runasp.net/swagger/index.html)

## Endpoints

- `GET /api/appointments` – Get all appointments.
- `GET /api/appointments/{id}` – Get a specific appointment.
- `PUT /api/appointments/{id}` – Update an appointment (only if status is Scheduled).
- `DELETE /api/appointments/{id}` – Cancel an appointment.

## Appointment Model

- `Id` (int) – Primary key, auto-increment  
- `CustomerName` (string) – Required, max 100 characters  
- `DateTime` (DateTime) – Required, must be a future date/time  
- `Status` (enum) – Scheduled, Completed, Canceled  
- `Notes` (string) – Optional, max 500 characters  

## Rules

- ❌ No appointments in the past  
- ❌ No duplicate appointments for the same customer at the same date/time  
- ✏️ Only appointments with status `Scheduled` can be updated  

## Run the Project

1. Open **Visual Studio**.  
2. Clone the project from GitHub.  
3. Open the solution file in Visual Studio.  
4. Set the correct **Startup Project** if needed.  
5. Click **Run (F5)** to start the API.  
6. Test the API using [Swagger](https://addappointment.runasp.net/swagger/index.html) or Postman.
