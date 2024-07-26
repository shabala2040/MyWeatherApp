using MyWeatherApp.Components.Pages;
using MyWeatherApp.Models;

namespace WeatherAppTests;

[TestClass]
public class WeatherAppTest
{
    [TestMethod]
    [DataRow("bad*", "CO")]
    [DataRow("Boulder _", "CO")]
    [DataRow("", "CO")]
    public void ValidateInputTest_InvalidCityName(string cityName, string state)
    {
        Home test = new Home();
        test.cityName = cityName;
        test.stateName = state;
        try
        {
            test.ValidateInput();
            Assert.Fail("An improper city name was given. An exception should have been thrown.");
        }
        catch (InvalidCityOrStateException ex)
        {
            Assert.AreEqual("Invalid city name.", ex.Message);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }

    }

    [TestMethod]
    [DataRow("Boulder", "colorado")]
    [DataRow("boulder", "Colorado")]
    [DataRow("Boulder", "Colorado")]
    [DataRow("Denver", "Colorado")]
    [DataRow("St. Inigoes", "Maryland")]
    public void ValidateInputTest_HappyPath(string cityName, string state)
    {
        Home home = new Home();
        home.cityName = cityName;
        home.stateName = state;

        try
        {
            home.ValidateInput();
        } catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }
        

    }

    [TestMethod]
    [DataRow("main", "colorado")]
    [DataRow("boulder", "united")]
    public void GetLatLon_LocationNotFound(string cityName, string state)
    {
        Home home = new Home();
        home.cityName = cityName;
        home.stateName = state;

        try
        {
            home.GetLatAndLon();
            Assert.Fail("An invalid location was sent to the API.");
        } catch (NoLocationMatched nl)
        {
                   Assert.AreEqual(home.CreateNoLocationExceptionMessage(cityName, state), nl.Message);
        } catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }

        if (home.foundLocation != null)
            Assert.Fail("Invalid loctaion was loaded into the object.");

    }

    [TestMethod]
    [DataRow("Boulder", "colorado")]
    [DataRow("franklin", "missouri")]
    public void GetLatLon_HappyPath(string cityName, string state)
    {
        Home home = new Home();
        home.cityName = cityName;
        home.stateName = state;

        try
        {
            home.GetLatAndLon();

            if (home.foundLocation == null)
                Assert.Fail("A valid location was passed but a result was not found.");

            if (!home.foundLocation.location.ToLower().Contains(cityName.ToLower().Replace(' ', '+')) ||
            !home.foundLocation.location.ToLower().Contains(state.ToLower().Replace(' ', '+')))
                Assert.Fail($"Location city name and state are not matched. Expected: {cityName}, {state}\tActual: {home.foundLocation.location}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }
    }

    

    [TestMethod]
    [DataRow("40.0218", "-10500.2639", "testing")]
    [DataRow("0", "0", "testing")]
    public void GetForecastURLs_TestInvalidLatLon(string lat, string lon, string locationName)
    {
        string[] reasonPhrases = { "Not Found", "Bad Request" };
        Home home = new Home();
        home.foundLocation = new GeocodeMapLocation();
        home.foundLocation.latitude = lat;
        home.foundLocation.longitude = lon;
        home.foundLocation.location = locationName;

        try
        {
            home.GetForecastURLs();
            Assert.Fail("An invalid latitude and longitude was sent to the API.");
        }
        catch (HttpRequestException hr)
        {
            
            bool reasonFound = false;
            foreach (string reason in reasonPhrases)
            {
                if (reason == hr.Message)
                    reasonFound = true;
            }

            Assert.IsTrue(reasonFound);

        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}: {ex.Source}");
        }

    }

    [TestMethod]
    [DataRow("40.0218", "-105.2639", "Boulder, Colorado")]
    public void GetForecastURLs_HappyPath(string lat, string lon, string locationName)
    {
        Home home = new Home();
        home.foundLocation = new GeocodeMapLocation();
        home.foundLocation.latitude = lat;
        home.foundLocation.longitude = lon;
        home.foundLocation.location = locationName;

        try
        {
            NWSPointProperties point = home.GetForecastURLs();

            if (point == null)
            {
                Assert.Fail("point object was not assigned");
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }
    }

    [TestMethod]
    [DataRow("https://api.weather.gov/gridpoints/BOU/54,75/forecast/hourl")]
    public void GetHourlyForecast_TestBadURLs(string hourlyForecastURL)
    {
        string[] reasonPhrases = { "Not Found", "Bad Request" };
        Home home = new Home();
        home.point = new NWSPointProperties();
        home.point.hourlyForecast = hourlyForecastURL;

        try
        {
            home.GetHourlyForecast();
            Assert.Fail("An invalid hourly forecast URL was sent to the API.");
        }
        catch (HttpRequestException hr)
        {

            bool reasonFound = false;
            foreach (string reason in reasonPhrases)
            {
                if (reason == hr.Message)
                    reasonFound = true;
            }

            Assert.IsTrue(reasonFound);

        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}: {ex.Source}");
        }

    }

    [TestMethod]
    [DataRow("https://api.weather.gov/gridpoints/BOU/54,75/forecast/hourly")]
    public void GetHourlyForecast_HappyPath(string hourlyForecastURL)
    {
        Home home = new Home();
        home.point = new NWSPointProperties();
        home.point.hourlyForecast = hourlyForecastURL;

        try
        {
            home.GetHourlyForecast();

            if (home.hourlyForecastProps == null)
            {
                Assert.Fail("hourlyForecastProps object was not assigned");
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }
    }


    [TestMethod]
    [DataRow("https://api.weather.gov/gridpoints/BOU/54,75/forecas")]
    public void GetFutureForecast_TestBadURLs(string futureForecastURL)
    {
        string[] reasonPhrases = { "Not Found", "Bad Request" };
        Home home = new Home();
        home.point = new NWSPointProperties();
        home.point.forecast = futureForecastURL;

        try
        {
            home.GetFutureForecast();
            Assert.Fail("An invalid forecast URL was sent to the API.");
        }
        catch (HttpRequestException hr)
        {

            bool reasonFound = false;
            foreach (string reason in reasonPhrases)
            {
                if (reason == hr.Message)
                    reasonFound = true;
            }

            Assert.IsTrue(reasonFound);

        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}: {ex.Source}");
        }

    }

    [TestMethod]
    [DataRow("https://api.weather.gov/gridpoints/BOU/54,75/forecast")]
    public void GetFutureForecast_HappyPath(string futureForecastURL)
    {
        Home home = new Home();
        home.point = new NWSPointProperties();
        home.point.forecast = futureForecastURL;

        try
        {
            home.GetFutureForecast();

            if (home.futureForecastProps == null)
            {
                Assert.Fail("futureForecastProps object was not assigned");
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Unexpected exception of type {ex.GetType()}: {ex.Message}");
        }
    }
}
