using Newtonsoft.Json;
using SpooderManCars.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using SpooderManCars.Models.ManufacturerModels;
using SpooderManCars.Models;
using SpooderManCars.Models.RacingModels;
using System.Runtime.CompilerServices;

namespace SpooderManConsole
{
    public class ConsoleUI
    {
        HttpClient httpClient = new HttpClient();
        private static bool loggedIn = false;
        private bool runMenu = true;
        public static string aPIUrl = "localhost:44336";
        private static string _token;
        public void Run()
        {
            Menu();
        }
        public void Menu()
        {
            //LocalHostGet();
            while (!loggedIn)
            {
                Login(LoginMenu());
            }
            while (runMenu)
            {
                Launch(MenuAndInput(ItemMenu()));
            }
        }

        public void LocalHostGet()
        {
            Console.WriteLine("What's the local host 5 digit access number?");
            aPIUrl = GetSafeLocalHost();
        }

        public void Login(string input)
        {
            switch (input)
            {
                case "1":
                    Registering();
                    break;
                case "2":
                    LoggingIn();
                    break;
                case "3":
                    TesterLogin();
                    break;
            }

        }
        public string LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("Login Page\n" +
                "1. Create User\n" +
                "2. Login\n" +
                "3. Use test account");
            return Console.ReadLine();
        }

        // Each table has 5 endpoints. Post/Put/Get/Get{id}/Delete
        public string ItemMenu()
        {
            bool properChoice = false;
            while (!properChoice)
            {
            Console.Clear();
                Console.WriteLine("Welcome to the SpooderMan Cars collection database\n" +
                    "Which set of objects would you want to interact with?\n" +
                    "1. Manufacturers\n" +
                    "2. Racing Teams\n" +
                    "3. Your Garages\n" +
                    "4. Your cars\n" +
                    "5. Exit Program");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "1":
                    case "m":
                        return "M";
                    case "2":
                    case "r":
                        return "R";
                    case "3":
                    case "g":
                        return "G";
                    case "4":
                    case "c":
                        return "C";
                    case "e":
                    case "exit":
                    case "5":
                        runMenu = false;
                        return "E";
                }
                Console.WriteLine("Invalid entry");
            }
            return "Error";
        }
        public string MenuAndInput(string choice)
        {
            string item = "EntityError";
            switch (choice)
            {
                case "M":
                    item = "Manufacturer";
                    break;
                case "R":
                    item = "Racing Team";
                    break;
                case "G":
                    item = "Garage";
                    break;
                case "C":
                    item = "car";
                    break;
                case "E":
                case "Error":
                    return "E";
            }

            Console.Clear();
            Console.WriteLine("SpooderMan Cars Database\n" +
                $"1. View all {item}s\n" +
                $"2. View a {item}\n" +
                $"3. Create a {item} entry\n" +
                $"4. Update a {item} entry\n" +
                $"5. Remove a {item}\n" +
                "6. Back");
            return choice + Console.ReadLine();
        }

        public void Launch(string input)
        {
            switch (input)
            {
                case "M1":
                    ViewAllManufacturer();
                    break;
                case "M2":
                    ViewAManufacturer();
                    break;
                case "M3":
                    AddAManfacturer();
                    break;
                case "M4":
                    UpdateAManufacturer();
                    break;
                case "M5":
                    DeleteAManufacturer();
                    break;
                case "R1":
                    GetRaces();
                    break;
                case "R2":
                    GetRacesId();
                    break;
                case "R3":
                    AddRaces();
                    break;
                case "R4":
                    UpdateRaces();
                    break;
                case "R5":
                    DeleteRaces();
                    break;
                case "G1":
                    ViewAllGarages();
                    break;
                case "G2":
                    ViewAGarage();
                    break;
                case "G3":
                    AddAGarage();
                    break;
                case "G4":
                    UpdateAGarage();
                    break;
                case "G5":
                    DeleteAGarage();
                    break;
                case "C1":
                    ViewAllCars();
                    break;
                case "C2":
                    ViewACar();
                    break;
                case "C3":
                    AddACar();
                    break;
                case "C4":
                    UpdateACar();
                    break;
                case "C5":
                    DeleteACar();
                    break;
                case "M6":
                case "R6":
                case "G6":
                case "C6":
                    break;
                case "E":
                case "EntityError6":
                    runMenu = false;
                    break;
            }
            Console.WriteLine("Please input a correct choice type");
        }

        private static async Task Register()
        {
            Console.Clear();
            Console.Write("Email: ");
            Dictionary<string, string> register = new Dictionary<string, string>()
            {
                {"Email", Console.ReadLine() }
            };
            Console.Write("Password: ");
            register.Add("Password", Console.ReadLine());

            Console.Write("Confirm Password: ");
            register.Add("ConfirmPassword", Console.ReadLine());

            Console.Write("First Name: ");
            register.Add("FirstName", Console.ReadLine());

            Console.Write("Last Name: ");
            register.Add("LastName", Console.ReadLine());

            HttpClient httpClient = new HttpClient();
            var registerInfo = new HttpRequestMessage(HttpMethod.Post, $"https://{aPIUrl}/api/Account/Register");
            registerInfo.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await httpClient.SendAsync(registerInfo);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Registered");
            }
            else
            {
                Console.WriteLine("Nope");
            }

        }
        public static void Registering()
        {
            var task = Register();
            task.Wait();
        }
        private static async Task Login()
        {
            Console.Clear();
            Dictionary<string, string> login = new Dictionary<string, string>
            {
                {"grant_type", "password" }
            };
            Console.Write("Email: ");
            login.Add("Username", Console.ReadLine());

            Console.Write("Password: ");
            login.Add("Password", Console.ReadLine());
            Console.WriteLine("Logging in.....");

            HttpClient httpClient = new HttpClient();

            var newToken = new HttpRequestMessage(HttpMethod.Post, $"https://{aPIUrl}/token");
            newToken.Content = new FormUrlEncodedContent(login.AsEnumerable());
            var response = await httpClient.SendAsync(newToken);
            var tokenString = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Tokens>(tokenString).Value;
            _token = token;

            newToken.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            if (response.IsSuccessStatusCode)
            {
                loggedIn = true;
                Console.WriteLine("Logged In");
            }
            else
            {
                Console.WriteLine("Nope ");
            }
        }

        public static void LoggingIn()
        {
            //Catch and wait method
            var task = Login();
            task.Wait();
        }

        public static async Task Tester()
        {
            //Register test account for first time runners
            Console.Clear();
            Console.WriteLine("Setting up testing account");
            HttpClient httpClient = new HttpClient();
            Dictionary<string, string> register = new Dictionary<string, string>()
            {
                {"Email", "TestAccount@Spooderman.com"},
                {"Password", "TestSpooderMan1!"},
                {"ConfirmPassword","TestSpooderMan1!"},
                {"FirstName","Peter"},
                {"LastName","Parker"}
            };
            var registerInfo = new HttpRequestMessage(HttpMethod.Post, $"https://{aPIUrl}/api/Account/Register");
            registerInfo.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await httpClient.SendAsync(registerInfo);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("New Tester Registered");
            }
            else
            {
                Console.WriteLine("Already Registered");
            }
            //login
            Console.WriteLine("Logging into test account");
            Dictionary<string, string> login = new Dictionary<string, string>
            {
                {"grant_type", "password" },
                {"Username", "TestAccount@Spooderman.com" },
                {"Password", "TestSpooderMan1!" }
            };

            var newToken = new HttpRequestMessage(HttpMethod.Post, $"https://{aPIUrl}/token");
            newToken.Content = new FormUrlEncodedContent(login.AsEnumerable());
            var loginResponse = await httpClient.SendAsync(newToken);
            var tokenString = await loginResponse.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Tokens>(tokenString).Value;
            _token = token;

            newToken.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            if (loginResponse.IsSuccessStatusCode)
            {
                loggedIn = true;
                Console.WriteLine("Logged In");
            }
            else
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(loginResponse.StatusCode);
                Console.ReadLine();
            }
        }

        public static void TesterLogin()
        {
            var task = Tester();
            task.Wait();
        }


        public void ViewAllCars() //Get
        {
            Console.Clear();
            Console.Write("Connecting.....");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                Console.WriteLine("Car ID   Make    Model     Year     Car Type     Transmission Size     Manufacturer");
                List<CarItem> cars = httpClient.GetAsync($"https://{aPIUrl}/api/Car/").Result.Content.ReadAsAsync<List<CarItem>>().Result;
                foreach (CarItem car in cars)
                {
                    Console.WriteLine($"{car.Id,-3} {car.Make,-8} {car.Model,-8} {car.Year,-6} {car.CarType, -8} {car.Transmission, -8} {car.Manufacturer.CompanyName}");
                }
            }
            Console.ReadKey();
        }
        public void ViewACar() //Get/{id}
        {
            Console.Clear();
            Console.WriteLine("Enter car ID");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                CarItem car = httpClient.GetAsync($"https://{aPIUrl}/api/Car/{id}").Result.Content.ReadAsAsync<CarItem>().Result;
                if (car != null)
                {
                    Console.WriteLine($"{car.Id, -3} {car.Make, -8} {car.Model, -8} {car.Year,-6} {car.Transmission}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Console.ReadKey();
        }
        public void AddACar() //Post
        {
            Console.Clear();
            Dictionary<string, string> newRest = new Dictionary<string, string>();

            Console.Write("Manufacturer Id: ");
            string manufacturerId = GetSafeInterger().ToString();
            newRest.Add("ManufacturerId", manufacturerId);

            Console.Write("Garage Id: ");
            string garageId = GetSafeInterger().ToString();
            newRest.Add("GarageId", garageId);
            Console.Write("Car make: ");
            string make = Console.ReadLine();
            newRest.Add("Make", make);
            Console.Write("Car model: ");
            string model = Console.ReadLine();
            newRest.Add("Model", model);
            Console.Write("Year: ");
            string year = Console.ReadLine();
            newRest.Add("Year", year);

            string carType = GetCarType();
            newRest.Add("CarType", carType);

            Console.Write("Transmission size: ");
            string tranny = Console.ReadLine();
            newRest.Add("Transmission", tranny);

            Console.Write("Car value: $");
            decimal value = GetSafeDecimal();
            newRest.Add("CarValue", value.ToString());

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newRest);

            // Needs auth token added
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://{aPIUrl}/api/Car/", newRestHTTP);
            if (response.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else { Console.WriteLine("Fail"); }
            Console.ReadKey();
        }

        public void UpdateACar() //Put
        {
            Console.Clear();
            Console.WriteLine("Enter car ID to update");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            CarItem oldCar = new CarItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldCar = httpClient.GetAsync($"https://{aPIUrl}/api/Car/{id}").Result.Content.ReadAsAsync<CarItem>().Result;
                if (oldCar != null)
                {
                    Console.WriteLine($"{oldCar.Id,-3} {oldCar.Make,-8} {oldCar.Model,-8} {oldCar.Year,-6} {oldCar.Transmission}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Dictionary<string, string> newCar = new Dictionary<string, string>();
            newCar.Add("Id", id.ToString());

            Console.Write("Manufacturer Id: ");
            string manufacturerId = GetSafeInterger().ToString();
            newCar.Add("ManufacturerId", manufacturerId);

            Console.Write("Garage Id: ");
            string garageId = GetSafeInterger().ToString();
            newCar.Add("GarageId", garageId);

            Console.Write("Car make: ");
            string make = Console.ReadLine();
            newCar.Add("Make", make);
            Console.Write("Car model: ");
            string model = Console.ReadLine();
            newCar.Add("Model", model);

            Console.Write("Year: ");
            string year = Console.ReadLine();
            newCar.Add("Year", year);

            string carType = GetCarType();
            newCar.Add("CarType", carType);

            Console.Write("Transmission size: ");
            string tranny = Console.ReadLine();
            newCar.Add("Transmission", tranny);

            Console.Write("Car value: $");
            decimal value = GetSafeDecimal();
            newCar.Add("CarValue", value.ToString());

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newCar);
            // Needs auth token added

            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://{aPIUrl}/api/Car/", newRestHTTP);
            Console.WriteLine(putResponse.Result.StatusCode);
            if (putResponse.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else { Console.WriteLine("Fail"); }
            Console.ReadKey();
        }

        public void DeleteACar() //Delete
        {
            Console.Clear();
            Console.WriteLine("Enter car ID to delete");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://{aPIUrl}/api/Car/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Car deleted");
            }
            else { Console.WriteLine("Invalid ID"); }
            Console.ReadKey();
        }
        public void ViewAllManufacturer() //Get
        {
            Console.Clear();
            Console.Write("Connecting.....");

            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<ManufacturerListItem> manufacturers = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/").Result.Content.ReadAsAsync<List<ManufacturerListItem>>().Result;
                foreach (ManufacturerListItem manufacturer in manufacturers)
                {

                    Console.WriteLine($" {manufacturer.Id,-3} {manufacturer.CompanyName} {manufacturer.Locations} {manufacturer.Founded}");
                }
            }
            Console.ReadKey();
        }

        public void ViewAManufacturer() //Get/{id}
        {
            Console.Clear();
            Console.WriteLine("Enter manufacturer ID");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                ManufacturerListItem manufacturer = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/{id}").Result.Content.ReadAsAsync<ManufacturerListItem>().Result;
                if (manufacturer != null)
                {
                    Console.WriteLine($" {manufacturer.Id} {manufacturer.CompanyName} {manufacturer.Locations} {manufacturer.Founded}");
                    foreach (var car in manufacturer.Cars)
                    {
                        Console.WriteLine($"{car.Id,-3} {car.Make,-8} {car.Model,-8} {car.Year,-6} {car.Transmission}");
                    }
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Console.ReadKey();
        }


        public void AddAManfacturer() //Post
        {
            Console.Clear();
            Dictionary<string, string> newRest = new Dictionary<string, string>();
            //Console.Write("Manufacturer Id: ");
            //string manufacturerId = GetSafeInterger().ToString();
            //newRest.Add("ManufacturerId", manufacturerId);

            Console.Write("Company Name: ");
            string companyName = Console.ReadLine();
            newRest.Add("CompanyName", companyName);

            Console.Write("Locations: ");
            string location = Console.ReadLine();
            newRest.Add("Locations", location);

            Console.Write("Date Founded: ");
            string founded = Console.ReadLine();
            DateTime dateFounded = Convert.ToDateTime(founded);
            newRest.Add("Founded", dateFounded.ToString());

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newRest);

            // Needs auth token added
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://{aPIUrl}/api/Manufacturer/", newRestHTTP);
            if (response.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else
            {
                Console.WriteLine(response.Result.StatusCode);
                Console.WriteLine("Fail");
            }
            Console.ReadKey();
        }

        public void UpdateAManufacturer() //Put
        {
            Console.Clear();
            Console.WriteLine("Enter manufaturer ID to update");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            ManufacturerListItem oldManufacturer = new ManufacturerListItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldManufacturer = httpClient.GetAsync($"https://{aPIUrl}/api/Manufacturer/{id}").Result.Content.ReadAsAsync<ManufacturerListItem>().Result;
                if (oldManufacturer != null)
                {
                    Console.WriteLine($" {oldManufacturer.Id,-3} {oldManufacturer.CompanyName} {oldManufacturer.Locations} {oldManufacturer.Founded}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Dictionary<string, string> newManufacturer = new Dictionary<string, string>();
            newManufacturer.Add("Id", id.ToString());

            Console.Write("Company Name: ");
            string companyName = Console.ReadLine();
            newManufacturer.Add("CompanyName", companyName);

            Console.Write("Locations: ");
            string location = Console.ReadLine();
            newManufacturer.Add("Locations", location);

            Console.Write("Date Founded: ");
            string founded = Console.ReadLine();
            DateTime dateFounded = Convert.ToDateTime(founded);
            newManufacturer.Add("Founded", dateFounded.ToString());
            Console.Clear();
            Console.WriteLine("Sending...");
            HttpContent newRestHTTP = new FormUrlEncodedContent(newManufacturer);

            // Needs auth token added
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://{aPIUrl}/api/Manufacturer/", newRestHTTP);
            if (putResponse.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else { Console.WriteLine("Fail"); }
            Console.ReadKey();
        }
        public void DeleteAManufacturer() //Delete
        {
            Console.Clear();
            Console.WriteLine("Enter manufacturer ID to delete");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://{aPIUrl}/api/Manufacturer/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Manufacturer deleted");
            }
            else { Console.WriteLine("Invalid ID"); }
            Console.ReadKey();
        }
        public void ViewAllGarages() //Get
        {
            Console.Clear();
            Console.Write("Spoodering.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<GarageItem> garages = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/").Result.Content.ReadAsAsync<List<GarageItem>>().Result;
                foreach (GarageItem garage in garages)
                {
                    Console.WriteLine($" {garage.Id,-3} {garage.Name} {garage.Location} {garage.CollectionValue}");
                }
            }
            Console.ReadKey();
        }
        public void ViewAGarage() //Get/{id}
        {
            Console.Clear();
            Console.WriteLine("Enter Garage ID");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/{id}");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                GarageItem garage = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/{id}").Result.Content.ReadAsAsync<GarageItem>().Result;
                if (garage != null)
                {
                    Console.WriteLine($"{garage.Id} {garage.Name} {garage.Location} {garage.CollectionValue}\n");
                    foreach (CarItem car in garage.CarCollection)
                    {
                        Console.WriteLine($"{car.Id,-3} {car.Make,-8} {car.Model,-8} {car.Year,-6} {car.Transmission}"); ;
                    }

                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Console.ReadKey();
        }
        public void AddAGarage() //Post
        {
            Console.Clear();
            Dictionary<string, string> newRest = new Dictionary<string, string>();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            newRest.Add("Name", name);

            Console.Write("Location: ");
            string location = Console.ReadLine();
            newRest.Add("Location", location);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newRest);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://{aPIUrl}/api/Garage/", newRestHTTP);
            if (response.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else { Console.WriteLine("Fail"); }
            Console.ReadKey();
        }

        public void UpdateAGarage() //Put
        {
            Console.Clear();
            Console.WriteLine("Enter Garage ID to update");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/");
            HttpResponseMessage response = getTask.Result;
            GarageItem oldGarage = new GarageItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldGarage = httpClient.GetAsync($"https://{aPIUrl}/api/Garage/{id}").Result.Content.ReadAsAsync<GarageItem>().Result;
                if (oldGarage != null)
                {
                    Console.WriteLine($" {oldGarage.Name} {oldGarage.Location}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Dictionary<string, string> newGarage = new Dictionary<string, string>();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            newGarage.Add("Id", id.ToString());

            Console.Write("Name: ");
            string name = Console.ReadLine();
            newGarage.Add("Name", name);

            Console.Write("Location: ");
            string location = Console.ReadLine();
            newGarage.Add("Location", location);
            Console.Clear();
            Console.WriteLine("Sending...");
            HttpContent newRestHTTP = new FormUrlEncodedContent(newGarage);

            // Needs auth token added
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://{aPIUrl}/api/Garage/", newRestHTTP);
            if (putResponse.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else { Console.WriteLine("Fail"); }
            Console.ReadKey();
        }

        public void DeleteAGarage() //Delete
        {
            Console.Clear();
            Console.WriteLine("Enter Garage ID to delete");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://{aPIUrl}/api/Garage/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Garage deleted");
            }
            else
            {
                Console.WriteLine(deleteTask.Result.StatusCode);
                Console.WriteLine("Invalid ID");
            }
            Console.ReadKey();
        }

        private string GetCarType()
        {
            Console.Write("Type of Car: (1)Compact (2)Minivan (3)Luxury\n" +
                "(4)Sport (5)SUV (6)Exotic: ");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        return "Compact";
                    case "2":
                        return "Minivan";
                    case "3":
                        return "Luxury";
                    case "4":
                        return "Sport";
                    case "5":
                        return "SUV";
                    case "6":
                        return "Exotic";
                }
                Console.WriteLine("Invalid selection. Please try again");
            }
        }

        public void GetRaces()
        {
            Console.Clear();
            Console.Write("Spoodering.....");

            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<RacingItem> races = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/").Result.Content.ReadAsAsync<List<RacingItem>>().Result;
                foreach (RacingItem race in races)
                {

                    Console.WriteLine($"{race.Id, -4}  {race.RaceEvent} {race.TeamName} {race.BasedOutOF} {race.Drivers}");
                }
            }
            Console.ReadKey();
        }

        public void GetRacesId()
        {
            Console.Clear();
            Console.WriteLine("Enter Race ID");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/{id}");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                RacingItem racing = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/{id}").Result.Content.ReadAsAsync<RacingItem>().Result;
                if (racing != null)
                {
                    Console.WriteLine($" {racing.Drivers} {racing.BasedOutOF} {racing.RaceEvent} {racing.TeamName}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Console.ReadKey();
        }
        public void AddRaces()
        {
            Console.Clear();
            Dictionary<string, string> newRace = new Dictionary<string, string>();
            Console.Write("Manufacturer Id: ");
            string manufacturerId = GetSafeInterger().ToString();
            newRace.Add("ManufacturerID", manufacturerId);

            Console.Write("Team Name: ");
            string teamName = Console.ReadLine();
            newRace.Add("TeamName", teamName);

            Console.Write("Based Out Of: ");
            string basedoutof = Console.ReadLine();
            newRace.Add("BasedOutOf", basedoutof);

            Console.Write("Drivers: ");
            string drivers = Console.ReadLine();
            newRace.Add("Drivers", drivers);

            string raceType = GetRaceType();
            newRace.Add("RaceEvent", raceType);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRaceHTTP = new FormUrlEncodedContent(newRace);

            // Needs auth token added
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://{aPIUrl}/api/Racing/", newRaceHTTP);
            if (response.Result.IsSuccessStatusCode)
            {

                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine(response.Result.StatusCode);
                Console.WriteLine("Fail");
            }
            Console.ReadKey();
        }

        private string GetRaceType()
        {
            Console.Write("Type of Race:\n" +
                "(1)F1\n" +
                "(2)Nascar\n" +
                "(3)IndyCar\n" +
                "(4)Drag Racing\n" +
                "(5)SportsCarChampionship\n ");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        return "F1";
                    case "2":
                        return "Nascar";
                    case "3":
                        return "IndyCar";
                    case "4":
                        return "DragRacing";
                    case "5":
                        return "SportsCarChampionship";
                }
                Console.WriteLine("Invalid selection. Please try again");
            }
        }

        public void UpdateRaces()
        {
            Console.Clear();
            Console.WriteLine("Enter Race ID to update");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/{id}");
            HttpResponseMessage response = getTask.Result;
            RacingItem oldRacing = new RacingItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldRacing = httpClient.GetAsync($"https://{aPIUrl}/api/Racing/{id}").Result.Content.ReadAsAsync<RacingItem>().Result;
                if (oldRacing != null)
                {
                    Console.WriteLine($" {oldRacing.ManufacturerID, -4} {oldRacing.TeamName} {oldRacing.BasedOutOF}  {oldRacing.Drivers} {oldRacing.RaceEvent}");
                }
                else { Console.WriteLine("Invalid ID"); }
            }
            Console.Clear();
            Dictionary<string, string> newRace = new Dictionary<string, string>();

            string Id = id.ToString();
            newRace.Add("Id", Id);

            Console.Write("Manufacturer Id: ");
            string manufacturerId = GetSafeInterger().ToString();
            newRace.Add("ManufacturerID", manufacturerId);

            Console.Write("Team Name: ");
            string teamName = Console.ReadLine();
            newRace.Add("TeamName", teamName);

            Console.Write("Based Out Of: ");
            string basedoutof = Console.ReadLine();
            newRace.Add("BasedOutOf", basedoutof);

            Console.Write("Drivers: ");
            string drivers = Console.ReadLine();
            newRace.Add("Drivers", drivers);

            string raceType = GetRaceType();
            newRace.Add("RaceEvent", raceType);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRaceHTTP = new FormUrlEncodedContent(newRace);

            // Needs auth token added
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://{aPIUrl}/api/Racing/{id}", newRaceHTTP);
            if (putResponse.Result.IsSuccessStatusCode) { Console.WriteLine("Success"); }
            else
            {
                Console.WriteLine(putResponse.Result.StatusCode);
                Console.WriteLine("Fail");
            }
            Console.ReadKey();
        }

        public void DeleteRaces()
        {
            Console.Clear();
            Console.WriteLine("Enter Racing ID to delete");
            int id = GetSafeInterger();
            Console.Write("Connecting.....");
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://{aPIUrl}/api/Racing/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Team deleted");
            }
            else { Console.WriteLine("Invalid ID"); }
            Console.ReadKey();
        }


        public decimal GetSafeDecimal()
        {
            if (decimal.TryParse(Console.ReadLine(), out decimal d))
            {
                return d;
            }
            else
            {
                Console.WriteLine("Invalid entry");
                return GetSafeDecimal();
            }
        }
        public int GetSafeInterger()
        {
            if (int.TryParse(Console.ReadLine(), out int d))
            {
                return d;
            }
            else
            {
                Console.WriteLine("Invalid entry");
                return GetSafeInterger();
            }
        }

        public string GetSafeLocalHost()
        {
            if (int.TryParse(Console.ReadLine(), out int d))
            {
                string e = $"localhost:{d}";
                return e;
            }
            else
            {
                Console.WriteLine("Invalid entry, default value of 44336 used");
                return "localhost:44336";
            }
        }

    }
    public class Tokens
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }
    }
}
