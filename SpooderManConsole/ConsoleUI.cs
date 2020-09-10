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

namespace SpooderManConsole
{
    public class ConsoleUI
    {
        HttpClient httpClient = new HttpClient();
        private static bool loggedIn = false;
        private bool runMenu = true;
        public int localHost = 44336;
        private static string authToken;
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
                Launch(MenuAndInput());
            }
        }

        public void LocalHostGet()
        {
            Console.WriteLine("What's the local host 5 digit access number?");
            localHost = GetSafeLocalHost();
        }

        public void Login(string input)
        {
            switch (input)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    loggedIn = true;
                    authToken = "a1Hdyv1Ju4hxCfKO1CEGaThA5hc - sZ81aMy5vHZYL - uxbQ74dmkPQOKWbllwAFJcyAG6Qzy1w8EOaC14je8OSQipJi60Rm7MATLNIthW5nnNPsH3E1TT2hn7FaVAkmYsfh2m0qeEIvKKloGnVOGk8J7iC2ypWqsJa8IUvhEXuHQlr9UsmxSkGk1CFsji6Id - tsIjv2l0VvV - GjAKDYftKkPDyxmWcZgsLvuie9k2uM14aXUqrfBTaiQlsUp0AtrkgAc - n22TneeLH08ii7fRBnRpLfCyt - oAI2iOYY8Q8a7yxPnJ6n3gnd94TXxLE5Aa7fKkzPYICWbLszGUoQ1K0rfQpzOU7MCrYGG5ovaZNfQRAi3s6OEk6xS9_sCqnPw6khsutDOXiwmdqRHwGt38FWYwaoLK2_V3D0vq2OvodkDH - X85WIOzDjUmDiEyWYGlIFzBRV - nOv5c0P3h_KyC7JozFLm3VXpH - noAAvfFvyE";
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
        public string MenuAndInput()
        {
            Console.Clear();
            Console.WriteLine("SpooderMan Cars Database\n" +
                "1. View all your Manufacturers\n" +
                "2. View all your cars\n" +
                "3. View your garage info\n" +
                "4. View all racing teams\n" +
                "5. Look up a Manufacturer\n" +
                "6. Look up a Car\n" +
                "7. Look up a Racing Team\n" +
                "8. Create a Manufacturer\n" +
                "9. Update a Manufacturer\n" +
                "10. Delete a Manufactuerer\n" +
                "11. View all\n" +
                "18. Exit");
            return Console.ReadLine();
        }
        public void Launch(string input)
        {
            switch (input)
            {
                case "1":
                    ViewAllManufacturer();
                    break;
                case "2":
                    ViewAllCars();
                    break;
                case "3":
                    AddACar();
                    break;
                case "4":
                    UpdateACar();
                    break;
                case "5":
                    DeleteACar();
                    break;
                case "6":
                    AddAManfacturer();
                    break;
                case "7":
                    UpdateAManufacturer();
                    break;
                case "8":
                    DeleteAManufacturer();
                    break;
                case "9":
                    ViewAManufacturer();
                    break;
                case "10":
                    AddRaces();
                    break;
                case "11":
                    GetRaces();
                    break;
                case "12":
                    UpdateRaces();
                    break;
                case "13":
                    DeleteRaces();
                    break;
                case "14":
                    AddAGarage();
                    break;
                case "15":
                    ViewAllGarages();
                    break;
                case "16":
                    UpdateAGarage();
                    break;
                case "17":
                    DeleteAGarage();
                    break;
                case "18":
                    runMenu = false;
                    break;
            }
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
            var registerInfo = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44336/api/Account/Register");
            registerInfo.Content = new FormUrlEncodedContent(register.AsEnumerable());
            var response = await httpClient.SendAsync(registerInfo);

            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine("Registered");
                //Console.ReadLine();
                loggedIn = true;
            }
            else
            {
                Console.WriteLine("Nope");
            }

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

            HttpClient httpClient = new HttpClient();

            var newToken = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44336/token");
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
        public void ViewAllCars() //Get
        {
            Console.Clear();
            Console.Write("Connecting.....");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<CarItem> cars = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/").Result.Content.ReadAsAsync<List<CarItem>>().Result;
                foreach (CarItem car in cars)
                {
                    Console.WriteLine($"{car.Id} {car.Make} {car.Model} {car.Year} {car.Transmission}");
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                CarItem car = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/{id}").Result.Content.ReadAsAsync<CarItem>().Result;
                if (car != null)
                {
                    Console.WriteLine($"{car.Id} {car.Make} {car.Model} {car.Year} {car.Transmission}");
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

            Console.WriteLine("Transmission size: ");
            string tranny = Console.ReadLine();
            newRest.Add("Transmission", tranny);

            Console.WriteLine("Car value: $");
            decimal value = GetSafeDecimal();
            newRest.Add("CarValue", value.ToString());

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newRest);

            // Needs auth token added
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://localhost:{localHost}/api/Car/", newRestHTTP);
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/");
            HttpResponseMessage response = getTask.Result;
            CarItem oldCar = new CarItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldCar = httpClient.GetAsync($"https://localhost:{localHost}/api/Car/{id}").Result.Content.ReadAsAsync<CarItem>().Result;
                if (oldCar != null)
                {
                    Console.WriteLine($" {oldCar.Make} {oldCar.Model} {oldCar.Year} {oldCar.Transmission}");
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

            Console.WriteLine("Transmission size: ");
            string tranny = Console.ReadLine();
            newCar.Add("Transmission", tranny);

            Console.WriteLine("Car value: $");
            decimal value = GetSafeDecimal();
            newCar.Add("CarValue", value.ToString());

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newCar);
            // Needs auth token added

            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://localhost:{localHost}/api/Car/", newRestHTTP);
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
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://localhost:{localHost}/api/Car/{id}");
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

            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<ManufacturerListItem> manufacturers = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/").Result.Content.ReadAsAsync<List<ManufacturerListItem>>().Result;
                foreach (ManufacturerListItem manufacturer in manufacturers)
                {

                    Console.WriteLine($" {manufacturer.Id} {manufacturer.CompanyName} {manufacturer.Locations} {manufacturer.Founded}");
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                ManufacturerListItem manufacturer = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/{id}").Result.Content.ReadAsAsync<ManufacturerListItem>().Result;
                if (manufacturer != null)
                {
                    Console.WriteLine($" {manufacturer.Id} {manufacturer.CompanyName} {manufacturer.Locations} {manufacturer.Founded}");
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
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://localhost:{localHost}/api/Manufacturer/", newRestHTTP);
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/");
            HttpResponseMessage response = getTask.Result;
            ManufacturerListItem oldManufacturer = new ManufacturerListItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldManufacturer = httpClient.GetAsync($"https://localhost:{localHost}/api/Manufacturer/{id}").Result.Content.ReadAsAsync<ManufacturerListItem>().Result;
                if (oldManufacturer != null)
                {
                    Console.WriteLine($" {oldManufacturer.Id} {oldManufacturer.CompanyName} {oldManufacturer.Locations} {oldManufacturer.Founded}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Dictionary<string, string> newManufacturer = new Dictionary<string, string>();
            newManufacturer.Add("Id", id.ToString());

            //Console.Write("Manufacturer Id: ");
            //string manufacturerId = GetSafeInterger().ToString();
            //newManufacturer.Add("Id", manufacturerId);

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
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://localhost:{localHost}/api/Manufacturer/", newRestHTTP);
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
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://localhost:{localHost}/api/Manufacturer/{id}");
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<GarageItem> garages = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/").Result.Content.ReadAsAsync<List<GarageItem>>().Result;
                foreach (GarageItem garage in garages)
                {

                    Console.WriteLine($" {garage.Id} {garage.Name} {garage.Location} {garage.CollectionValue}");
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/{id}");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                GarageItem garage = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/{id}").Result.Content.ReadAsAsync<GarageItem>().Result;
                if (garage != null)
                {
                    Console.WriteLine($" {garage.Name} {garage.Location} {garage.CollectionValue} {garage.CarCollection}");
                }
                else { Console.WriteLine("Invalid ID"); }

            }
            Console.ReadKey();
        }
        public void AddAGarage() //Post
        {
            Console.Clear();
            Dictionary<string, string> newRest = new Dictionary<string, string>();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Console.Write("Name: ");
            string name = Console.ReadLine();
            newRest.Add("Name", name);

            Console.Write("Location: ");
            string location = Console.ReadLine();
            newRest.Add("Location", location);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRestHTTP = new FormUrlEncodedContent(newRest);

            // Needs auth token added
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://localhost:{localHost}/api/Garage/", newRestHTTP);
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/");
            HttpResponseMessage response = getTask.Result;
            GarageItem oldGarage = new GarageItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldGarage = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/{id}").Result.Content.ReadAsAsync<GarageItem>().Result;
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
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://localhost:{localHost}/api/Garage/", newRestHTTP);
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
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://localhost:{localHost}/api/Garage/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Garage deleted");
            }
            else {
                Console.WriteLine(deleteTask.Result.StatusCode);
                Console.WriteLine("Invalid ID"); }
            Console.ReadKey();
        }

        private string GetCarType()
        {
            Console.Write("Type of Car: (1)Compact (2)Minivan (3)Luxury\n" +
                "(4)Sport (5)SUV (6)Exotic");
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

            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                List<RacingItem> races = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/").Result.Content.ReadAsAsync<List<RacingItem>>().Result;
                foreach (RacingItem race in races)
                {

                    Console.WriteLine($" {race.Id}  {race.RaceEvent} {race.TeamName} {race.BasedOutOF} {race.Drivers} {race.Victories.Count}");
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/{id}");
            HttpResponseMessage response = getTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                RacingItem racing = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/{id}").Result.Content.ReadAsAsync<RacingItem>().Result;
                if (racing != null)
                {
                    Console.WriteLine($" {racing.Drivers} {racing.BasedOutOF} {racing.RaceEvent} {racing.TeamName} {racing.Victories}");
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
            newRace.Add("ManufacturerId", manufacturerId);

            Console.Write("Team Name: ");
            string teamName = Console.ReadLine();
            newRace.Add("TeamName", teamName);

            Console.Write("Based Out Of: ");
            string basedoutof = Console.ReadLine();
            newRace.Add("BasedOutOf", basedoutof);

            Console.Write("Victories: ");
            string victories = GetSafeInterger().ToString();
            newRace.Add("Victories[0]", victories);

            Console.Write("Drivers");
            string drivers = Console.ReadLine();
            newRace.Add("Drivers", drivers);

            string raceType = GetRaceType();
            newRace.Add("RaceEvent", raceType);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRaceHTTP = new FormUrlEncodedContent(newRace);

            // Needs auth token added
            Task<HttpResponseMessage> response = httpClient.PostAsync($"https://localhost:{localHost}/api/Racing/", newRaceHTTP);
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
                        return "SportsCarChamptionship";
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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/{id}");
            HttpResponseMessage response = getTask.Result;
            RacingItem oldRacing = new RacingItem();
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                oldRacing = httpClient.GetAsync($"https://localhost:{localHost}/api/Racing/{id}").Result.Content.ReadAsAsync<RacingItem>().Result;
                if (oldRacing != null)
                {
                    Console.WriteLine($" {oldRacing.ManufacturerID} {oldRacing.TeamName} {oldRacing.BasedOutOF} {oldRacing.Victories}  {oldRacing.Drivers} {oldRacing.RaceEvent}");
                }
                else { Console.WriteLine("Invalid ID"); }
            }
            Console.Clear();
            Dictionary<string, string> newRace = new Dictionary<string, string>();

            string Id = id.ToString();
            newRace.Add("Id", Id);

            Console.Write("Manufacturer Id: ");
            string manufacturerId = GetSafeInterger().ToString();
            newRace.Add("ManufacturerId", manufacturerId);

            Console.Write("Team Name: ");
            string teamName = Console.ReadLine();
            newRace.Add("TeamName", teamName);

            Console.Write("Based Out Of: ");
            string basedoutof = Console.ReadLine();
            newRace.Add("BasedOutOf", basedoutof);

            Console.Write("Drivers");
            string drivers = Console.ReadLine();
            newRace.Add("Drivers", drivers);

            string raceType = GetRaceType();
            newRace.Add("RaceEvent", raceType);

            Console.Clear();
            Console.WriteLine("Sending...");

            HttpContent newRaceHTTP = new FormUrlEncodedContent(newRace);

            // Needs auth token added
            Task<HttpResponseMessage> putResponse = httpClient.PutAsync($"https://localhost:{localHost}/api/Racing/{id}", newRaceHTTP);
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
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://localhost:{localHost}/api/Racing/{id}");
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
            decimal d;
            if (decimal.TryParse(Console.ReadLine(), out d))
            {
                return d;
            }
            else
            {
                Console.WriteLine("Invalid entry, default value of 100.50 used");
                return 100.50m;
            }
        }
        public int GetSafeInterger()
        {
            int d;
            if (int.TryParse(Console.ReadLine(), out d))
            {
                return d;
            }
            else
            {
                Console.WriteLine("Invalid entry, default value of 1 used");
                return 1;
            }
        }

        public int GetSafeLocalHost()
        {
            int d;
            if (int.TryParse(Console.ReadLine(), out d))
            {
                return d;
            }
            else
            {
                Console.WriteLine("Invalid entry, default value of 44336 used");
                return 44336;
            }
        }

    }
    public class Tokens
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }
    }
}
