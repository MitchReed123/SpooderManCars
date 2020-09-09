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
                "2. Create a Manufacturer\n" +
                "3. Update a Manufacturer\n" +
                "4. Delete a Manufactuerer\n" +
                "5. View all\n" +
                "18. Exit");
            return Console.ReadLine();
        }
        public void Launch(string input)
        {
            switch (input)
            {
                case "1":
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
                    break;
                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                    break;
                case "12":
                    break;
                case "13":
                    break;
                case "14":
                    break;
                case "15":
                    break;
                case "16":
                    break;
                case "17":
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

        public void ViewAllGarages() //Get
        {
            Console.Clear();
            Console.Write("Spoodering.....");

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
            Task<HttpResponseMessage> getTask = httpClient.GetAsync($"https://localhost:{localHost}/api/Garage/");
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
            Task<HttpResponseMessage> deleteTask = httpClient.DeleteAsync($"https://localhost:{localHost}/api/Garage/{id}");
            HttpResponseMessage response = deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Garage deleted");
            }
            else { Console.WriteLine("Invalid ID"); }
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
