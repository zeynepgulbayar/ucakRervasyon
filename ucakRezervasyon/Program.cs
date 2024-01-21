
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Airplane
{
    public int AirplaneId { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string SerialNumber { get; set; }
    public int Capacity { get; set; }
}

class Location
{
    public int LocationId { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AirportCode { get; set; }
    public bool IsActive { get; set; }
}

class Flight
{
    public int FlightId { get; set; }
    public int LocationId { get; set; }
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public Airplane Airplane { get; set; }
    public bool IsActive { get; set; }
}

class Reservation
{
    public int Id { get; set; }
    public Flight Flight { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int TicketNumber { get; set; }
}

class FlightReservationProgram
{
    static List<Airplane> airplanes = new List<Airplane>();
    static List<Location> locations = new List<Location>();
    static List<Flight> flights = new List<Flight>();
    static List<Reservation> reservations = new List<Reservation>();

    static void Main()
    {
        do
        {
            ReadData();
            SampleData();
            Console.WriteLine("Destination city: ");
            string destinationCity = Console.ReadLine();

            Location destinationLocation = locations.Find(l => l.City.Equals(destinationCity, StringComparison.OrdinalIgnoreCase) && l.IsActive);

            if (destinationLocation != null)
            {
                List<Flight> availableFlights = flights.FindAll(f => f.LocationId == destinationLocation.LocationId && f.IsActive);

                if (availableFlights.Any())
                {
                    Console.WriteLine($"{availableFlights.Count} flights found");

                    for (int i = 0; i < availableFlights.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {availableFlights[i].Date:dd/MM/yyyy hh:mm:ss tt} - {availableFlights[i].Time:dd/MM/yyyy hh:mm:ss tt} Flight ID: {availableFlights[i].FlightId} {availableFlights[i].Airplane.Brand} {availableFlights[i].Airplane.Model} {availableFlights[i].Airplane.SerialNumber} ({availableFlights[i].Airplane.Capacity} seats available)");
                    }

                    Console.WriteLine("Select a flight (enter the number in square brackets): ");
                    int selectedFlightIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (selectedFlightIndex >= 0 && selectedFlightIndex < availableFlights.Count)
                    {
                        Flight selectedFlight = availableFlights[selectedFlightIndex];

                        string ReadNonEmptyString(string prompt)
                        {
                            string input;
                            do
                            {
                                Console.WriteLine($"{prompt}: ");
                                input = Console.ReadLine();
                            } while (string.IsNullOrEmpty(input));

                            return input;
                        }

                        int ReadValidIntegerInput(string prompt)
                        {
                            int result = 0;
                            bool isValidInput;

                            do
                            {
                                Console.WriteLine($"{prompt}: ");
                                string input = Console.ReadLine();

                                isValidInput = !string.IsNullOrEmpty(input) && int.TryParse(input, out result);

                                if (!isValidInput)
                                {
                                    Console.WriteLine("Error: Enter a valid number.");
                                }

                            } while (!isValidInput);

                            return result;
                        }

                        if (selectedFlight.Airplane.Capacity > reservations.Count(r => r.Flight.FlightId == selectedFlight.FlightId))
                        {
                            int newTicketNumber = reservations.Count + 1;
                            Reservation newReservation = new Reservation
                            {
                                Id = reservations.Count + 1,
                                Flight = selectedFlight,
                                FirstName = ReadNonEmptyString("First Name"),
                                LastName = ReadNonEmptyString("Last Name"),
                                Age = ReadValidIntegerInput("Age"),
                                TicketNumber = newTicketNumber
                            };

                            reservations.Add(newReservation);
                            selectedFlight.Airplane.Capacity--;

                            string locationInfo = $"{destinationLocation.City} Airport";
                            string flightInfo = $"{selectedFlight.Airplane.Brand} {selectedFlight.Airplane.Model} model airplane";

                            Console.WriteLine($" Id:{selectedFlight.FlightId} {selectedFlight.Time:|dd/MM/yyyy| |hh:mm:sstt|}-{selectedFlight.Date:|dd/MM/yyyy| |hh:mm:sstt|}\n  {locationInfo} from will depart.\n Flight information: {flightInfo} for Flight No: {selectedFlight.FlightId}.\n A reservation has been made for 1 seat. Ticket no: {newTicketNumber}");
                        }
                        else
                        {
                            Console.WriteLine("Sorry, no available seats for the selected flight.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid flight selected.");
                    }
                }
                else
                {
                    Console.WriteLine("No available flights for the destination.");
                }
            }
            else
            {
                Console.WriteLine("Invalid location.");
            }

            
            SaveData();
            Console.WriteLine("Do you want to continue? (y/n)");

        } 
        while (Console.ReadLine().Trim().Equals("y", StringComparison.OrdinalIgnoreCase));
    }

    static void ReadData()
    {
        if (File.Exists("airplanes.json"))
        {
            string airplanesJson = File.ReadAllText("airplanes.json");
            airplanes = JsonSerializer.Deserialize<List<Airplane>>(airplanesJson);
        }

        if (File.Exists("locations.json"))
        {
            string locationsJson = File.ReadAllText("locations.json");
            locations = JsonSerializer.Deserialize<List<Location>>(locationsJson);
        }

        if (File.Exists("flights.json"))
        {
            string flightsJson = File.ReadAllText("flights.json");
            flights = JsonSerializer.Deserialize<List<Flight>>(flightsJson);
        }

        if (File.Exists("reservations.json"))
        {
            string reservationsJson = File.ReadAllText("reservations.json");
            reservations = JsonSerializer.Deserialize<List<Reservation>>(reservationsJson);
        }
    }

    static void SaveData()
    {
        string airplanesJson = JsonSerializer.Serialize(airplanes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("airplanes.json", airplanesJson);

        string locationsJson = JsonSerializer.Serialize(locations, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("locations.json", locationsJson);

        string flightsJson = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("flights.json", flightsJson);

        string reservationsJson = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("reservations.json", reservationsJson);
    }

    static void SampleData()
    {
        ReadData();
        if (airplanes.Count == 0)
        {
            airplanes.Add(new Airplane { AirplaneId = 1, Model = "737", Brand = "Boeing", SerialNumber = "12345", Capacity = 100 });
            airplanes.Add(new Airplane { AirplaneId = 2, Model = "A320", Brand = "Airbus", SerialNumber = "67890", Capacity = 150 });
            airplanes.Add(new Airplane { AirplaneId = 3, Model = "747", Brand = "Boeing", SerialNumber = "54321", Capacity = 200 });
            airplanes.Add(new Airplane { AirplaneId = 4, Model = "A380", Brand = "Airbus", SerialNumber = "98765", Capacity = 300 });
        }

        if (locations.Count == 0)
        {
            locations.Add(new Location { LocationId = 1, City = "Istanbul", Country = "Turkey", AirportCode = "IST", IsActive = true });
            locations.Add(new Location { LocationId = 2, City = "Ankara", Country = "Turkey", AirportCode = "ESB", IsActive = true });
            locations.Add(new Location { LocationId = 3, City = "Paris", Country = "France", AirportCode = "CDG", IsActive = true });
            locations.Add(new Location { LocationId = 4, City = "New York", Country = "USA", AirportCode = "JFK", IsActive = true });
        }

        if (flights.Count == 0)
        {
            DateTime now = DateTime.Now;

            foreach (Location location in locations)
            {
                for (int i = 0; i < 3; i++)
                {
                    int airplaneIndex = i % airplanes.Count;
                    DateTime date = now.AddDays(i + 1);
                    DateTime time = now.AddHours(12).AddMinutes(30 * i);

                    flights.Add(new Flight
                    {
                        FlightId = flights.Count + 1,
                        LocationId = location.LocationId,
                        Date = date,
                        Time = time,
                        Airplane = airplanes[airplaneIndex],
                        IsActive = true
                    });
                }
            }
        }

        if (reservations.Count == 0)
        {
            foreach (Flight flight in flights)
            {
                reservations.Add(new Reservation
                {
                    Id = reservations.Count + 1,
                    Flight = flight,
                    FirstName = "Sample",
                    LastName = "User",
                    Age = 25,
                    TicketNumber = reservations.Count + 1
                });
            }
        }
        SaveData();
    }
}
