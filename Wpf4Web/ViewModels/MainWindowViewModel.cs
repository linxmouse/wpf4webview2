using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Serilog;

namespace Wpf4Web.ViewModels
{
    public class MainWindowViewModel: ObservableObject
    {
        private readonly IConfiguration _configuration;
        public MainWindowViewModel()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsetttings.json");

            _configuration = builder.Build();
            if (Uri.TryCreate(_configuration["StartUrl"], UriKind.RelativeOrAbsolute, out Uri uri))
            {
                StartUri = uri;
            }
            else
            {
                Log.Warning("请填写正确的URL");
                MessageBox.Show("URL不正确, 请从浏览器复制后在填入!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Uri startUri;
        public Uri StartUri { get => startUri; set => SetProperty(ref startUri, value); }
    }
}