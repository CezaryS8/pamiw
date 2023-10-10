﻿using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace P04WeatherForecastAPI.Client
{    public partial class MainWindow : Window
    {
        AccuWeatherService accuWeatherService;
        public MainWindow()
        {
            InitializeComponent();
            accuWeatherService = new AccuWeatherService();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            City[] cities= await accuWeatherService.GetLocations(txtCity.Text);

            // standardowy sposób dodawania elementów
            //lbData.Items.Clear();
            //foreach (var c in cities)
            //    lbData.Items.Add(c.LocalizedName);

            // teraz musimy skorzystac z bindowania danych bo chcemy w naszej kontrolce przechowywac takze id miasta 
            lbData.ItemsSource = cities;
        }
        

        private async void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCity= (City)lbData.SelectedItem;
            if(selectedCity != null)
            {
                var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                lblCityName.Content = selectedCity.LocalizedName;
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValue.Content = Convert.ToString(tempValue);

                HistoricalCurrentConditions[] historicalCurrentConditionsArray= await accuWeatherService.GetHistoricalCurrentConditions(selectedCity.Key);
                lbHistoricalCurrentConditions.ItemsSource = historicalCurrentConditionsArray;


                // lblCityKey.Content = selectedCity.Key;

                // var forecasts = await accuWeatherService.GetOneDayOfDailyForecasts(selectedCity.Key);
                // double minTempValue = forecasts.DailyForecasts.Temperature.Minimum.Value;
                // double minTempValue = forecasts.DailyForecasts.Temperature.Maximum.Value;
                // lblMinTemperatureValue.Content = Convert.ToString(minTempValue);
                // lblMaxTemperatureValue.Content = Convert.ToString(maxTempValue);
            }
        }
    }
}