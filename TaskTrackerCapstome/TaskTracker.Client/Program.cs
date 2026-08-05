using System.Net.Http;

using var http = new HttpClient();
try
{
	var result = await http.GetStringAsync("http://localhost:5275/weatherforecast");
	Console.WriteLine(result);
}
catch (Exception ex)
{
	Console.WriteLine($"Request failed: {ex.Message}");
}
