using System.Text;
using Newtonsoft.Json;

namespace DreamFactoryDemo.Services
{
    public class DreamFactoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public DreamFactoryService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        private string BaseUrl => _config["DreamFactory:BaseUrl"];
        private string ApiKey => _config["DreamFactory:ApiKey"];

        public async Task<string> GetTableDataAsync(string tableName)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{BaseUrl}/_table/{tableName}");

            request.Headers.Add("X-DreamFactory-API-Key", ApiKey);

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> InsertAsync(string tableName, object data)
        {
            var body = new { resource = new[] { data } };
            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{BaseUrl}/_table/{tableName}");

            request.Headers.Add("X-DreamFactory-API-Key", ApiKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetByIdAsync(string tableName, string id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{BaseUrl}/_table/{tableName}/{id}");

            request.Headers.Add("X-DreamFactory-API-Key", ApiKey);

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetAttendance(int empId, int? month, int? year)
        {
            var body = new
            {
                p_emp_id = empId,
                p_month = month,
                p_year = year
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");

            //var request = new HttpRequestMessage(
            //    HttpMethod.Post,
            //    $"{BaseUrl}/_proc/project_mgmt.usp_get_attendance_report");

            var request = new HttpRequestMessage(
            HttpMethod.Post,
             $"{BaseUrl}/_proc/project_mgmt.usp_get_attendance_report_v1?fetch=true");

            request.Headers.Add("X-DreamFactory-API-Key", ApiKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}