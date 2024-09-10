using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementConsumingWebApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchoolManagementConsumingWebApi.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _client;

        public StudentController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
            _client = new HttpClient(clientHandler);
        }

        public async Task<IActionResult> ViewAttendance()
        {
            string url = "https://localhost:44375/api/Student/GetAttendance";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var attendances = JsonConvert.DeserializeObject<List<Attendance>>(jsonResponse);
                return View(attendances);
            }

            return View(new List<Attendance>()); // Return an empty list if the request fails
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance(Attendance attendance)
        {
            string url = "https://localhost:44375/api/Student/GetAttendance";
            var jsondata = JsonConvert.SerializeObject(attendance);
            StringContent content = new StringContent(jsondata, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Attendance submitted successfully.";
            }
            else
            {
                ViewData["Message"] = "An error occurred while submitting attendance.";
            }

            // Redirect to the same action to display the updated list
            return RedirectToAction("ViewAttendance");
        }


        public async Task<IActionResult> ViewAssignment()
        {
            string url = "https://localhost:44375/api/Student/GetAssignment";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var assignments = JsonConvert.DeserializeObject<List<Assignment>>(jsonResponse);
                return View(assignments);
            }

            return View(new List<Assignment>()); // Return an empty list if the request fails
        }

        public async Task<IActionResult> ViewTimeTable()
        {
            string url = "https://localhost:44375/api/Student/GetTimeTable";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var assignments = JsonConvert.DeserializeObject<List<TimeTable>>(jsonResponse);
                return View(assignments);
            }

            return View(new List<TimeTable>()); // Return an empty list if the request fails
        }


    }
}
