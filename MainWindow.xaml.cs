using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace APIApp
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnPredictClick(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                string url = $"https://api.agify.io/?name={name}";
                try
                {
                    string response = await client.GetStringAsync(url);
                    var json = JObject.Parse(response);
                    string? age = json["age"]?.ToString(); // Use nullable type for 'age'
                    ResultText.Text = age != null
                        ? $"Predicted age for {name}: {age}"
                        : $"No age prediction available for {name}.";
                }
                catch
                {
                    ResultText.Text = "Failed to get prediction.";
                }
            }
            else
            {
                ResultText.Text = "Please enter a name.";
            }
        }
    }
}
