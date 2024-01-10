using Newtonsoft.Json;
using Ramos_ApiPublica.Model;
using System.Text.Json.Serialization;

namespace Ramos_ApiPublica.Views;

public partial class ClimaActual : ContentPage
{
	public ClimaActual()
	{
		InitializeComponent();
	}

    private async void OnConsultarClicked(object sender, EventArgs e)
    {
		string latitud = lat.Text;
		string longitud = lon.Text;

		if (Connectivity.NetworkAccess != NetworkAccess.Internet) 
		{
			return;
		}
		using(var httpclient = new HttpClient()) 
		{
			string url = "https://api.openweathermap.org/data/2.5/weather?lat="+latitud+"&lon="+longitud+ "&appid=dbc149b6f7cf80e507e48c24f912c3ac";
			var response = await httpclient.GetAsync(url);
			if (response.IsSuccessStatusCode) 
			{
				var json = await response.Content.ReadAsStringAsync();
				var clima = JsonConvert.DeserializeObject<Rootobject>(json);
				weatherLabel.Text = clima.weather[0].main;
			}
		}
    }
}