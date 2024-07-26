namespace MyWeatherApp.Models
{
    public class InvalidCityOrStateException : Exception
    {
        public InvalidCityOrStateException() { }

        public InvalidCityOrStateException(string message) : base(message) { }
    }

    public class NoLocationMatched : Exception
    {
        public NoLocationMatched() { }

        public NoLocationMatched(string message) : base(message) { }

    }


}

