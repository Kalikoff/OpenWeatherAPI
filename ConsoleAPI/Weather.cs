namespace ConsoleAPI {
    public class Temperatura {
        public double temp;
    }

    public class WeatherNow {
        public string main;
        public string description;
    }

    public class WeatherResponse {
        public Temperatura main;
        public string name;
        public WeatherNow[] weather;
    }
}