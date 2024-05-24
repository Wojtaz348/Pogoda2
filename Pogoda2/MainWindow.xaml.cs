using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pogoda2;


public partial class MainWindow : Window
{
    private readonly HttpClient httpClient = new HttpClient();
    private string ApiKey = "f695b9814498876ee0220627a2fa06b7";
    private Uri ApiUrl = new Uri("https://openweathermap.org/");


    public MainWindow()
    {
        InitializeComponent();
    }
    private async void LoadWeatherData(string location)
    {
        try
        {
            var response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q=krak&appid=f695b9814498876ee0220627a2fa06b7");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JObject.Parse(content);
                UpdateUI(weatherData);
            }
            else
            {
                MessageBox.Show("Nie udało sie pobrac danych pogodowych");

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void UpdateUI(JObject weatherData)
    {
        var weatherDescription = weatherData["weather"][0]["description"].ToString();
        var temperature = Convert.ToDouble(weatherData["main"]["temp"]);
        var feelsLike = Convert.ToDouble(weatherData["main"]["feels"]);
        var humidity = Convert.ToInt32(weatherData["main"]["humidity"]);
        var clouds = Convert.ToInt32(weatherData["clouds"]["all"]);
        var rain = Convert.ToInt32(weatherData["main"]["rain"]);

        lblWeather.Text = $"Aktualna pogoda: {weatherDescription}";
        lblTemperature.Text = $"Temperatura: {temperature}°C";
        lblFeelsLike.Text = $"Odczuwalna temperatura: {feelsLike}°C";
        lblRain.Text = $"Wilgotność: {humidity}%";
        lblClouds.Text = $"Nasłonecznienie: {clouds}%";
        lblRain.Text = $"Deszcz: {rain}%";
    }
}