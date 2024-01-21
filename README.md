**Flight Reservation System**

The Flight Reservation System is a console application designed for making flight reservations to various destinations. Users can choose their desired location, view available flights, select a flight, and complete the reservation process.

**Usage**

To use the application, follow these steps:

1. Run the application and enter the destination city.
2. Browse available flights, selecting one by entering its ID.
3. Input passenger details, including first name, last name, and age.
4. Confirm the reservation.
5. Repeat the process for additional reservations or type "n" to exit.

**Features**

- View available flights for specific destinations.
- Make reservations for selected flights.
- Automatic seat availability check.

**Classes**

**Airplane**

Represents an airplane with properties such as **AirplaneId** , **Model** , **Brand** , **SerialNumber** , and **Capacity**.

**Location**

Represents a location with properties including **LocationId** , **City** , **Country** , **AirportCode** , and **IsActive**.

**Flight**

Represents a flight with properties like **FlightId** , **LocationId** , **Date** , **Time** , **Airplane** , and **IsActive**.

**Reservation**

Represents a reservation with properties such as **Id** , **Flight** , **FirstName** , **LastName** , **Age** , and **TicketNumber**.

**FlightReservationProgram**

Main class that contains the application logic, including methods for reading data from JSON files, saving data to JSON files, populating sample data, and handling the reservation process.

**Methods**

**Main**

The main method that drives the application's functionality. It includes the main loop allowing users to perform operations such as viewing available flights, making reservations, and exiting the program.

**ReadData**

Reads data from JSON files for airplanes, locations, flights, and reservations. Uses the **JsonSerializer** class for deserialization.

**SaveData**

Saves data to JSON files for airplanes, locations, flights, and reservations. Utilizes the **JsonSerializer** class for serialization.

**SampleData**

Populates the system with sample data, including airplanes, locations, flights, and reservations. This method is useful for testing and initial setup.

**ReadNonEmptyString**

Reads a non-empty string from the user, displaying the specified prompt until a valid input is provided.

**ReadValidIntegerInput**

Reads a valid integer input from the user, displaying the specified prompt until a valid input is provided.

