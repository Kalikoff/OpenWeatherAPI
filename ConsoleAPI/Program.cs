using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ConsoleAPI {
    class Program {
        public static string CountryID;

        static void Main(string[] args) {
            Console.WriteLine("Введите ID города: ");
            CountryID = Console.ReadLine(); // Москва - 524901

            try {
                ConnectAsync().Wait();
                Console.WriteLine("Успешно");
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                Console.Write("Город не найден или что-то пошло не так");
            }

            Console.ReadKey();
        }

        public static async Task ConnectAsync() {
            WebRequest request = WebRequest.Create(
                "https://api.openweathermap.org/data/2.5/weather?id=" +
                CountryID +
                "&units=metric&APPID=??????");
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();
            string answer = "";

            using (Stream s = response.GetResponseStream()) {
                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    answer = await reader.ReadToEndAsync();
                }
            }

            response.Close();

            WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(answer);
            Console.WriteLine("Средняя температура в данный момент в городе " + response_global.name + " = " + response_global.main.temp);
            Console.WriteLine("Погода за окном: " + response_global.weather[0].main + " Описание: " + response_global.weather[0].description);
        }
    }
}