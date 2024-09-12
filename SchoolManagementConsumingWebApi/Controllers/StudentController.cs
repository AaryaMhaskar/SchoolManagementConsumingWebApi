using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementConsumingWebApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<IActionResult> ViewFeeStatus()
        {
            string url = "https://localhost:44375/api/Student/GetFeeStatus";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var assignments = JsonConvert.DeserializeObject<List<Student>>(jsonResponse);
                return View(assignments);
            }

            return View(new List<Student>()); // Return an empty list if the request fails
        }


        public async Task<IActionResult> ViewStudentProfile()
        {
            string url = "https://localhost:44375/api/Student/GetStudentProfile";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var assignments = JsonConvert.DeserializeObject<List<Student>>(jsonResponse);
                return View(assignments);
            }

            return View(new List<Student>()); // Return an empty list if the request fails
        }

        public IActionResult displayStudentData(int id)
        {
            Student student = new Student();
            string url = $"https://localhost:44375/api/Student/GetStudentById/{id}";
            HttpResponseMessage res = _client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                var jsondata = res.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(jsondata);
                return View(student);
            }
            else
            {
                TempData["Msg"] = "Couldnt fetch user try again later";
                return View();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentData(Student s)
        {
            // Construct the URL for updating the student
            string url = $"https://localhost:44375/api/Student/UpdateStudent";

            // Serialize the student object to JSON
            var jsondata = JsonConvert.SerializeObject(s);
            StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");

            try
            {
                // Send the PUT request asynchronously
                HttpResponseMessage msg = await _client.PutAsync(url, stringContent);

                // Log the response status code and content for debugging
                var statusCode = msg.StatusCode;
                var responseContent = await msg.Content.ReadAsStringAsync();

                // Debug logs
                Console.WriteLine($"Status Code: {statusCode}");
                Console.WriteLine($"Response Content: {responseContent}");

                if (msg.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewStudentProfile");
                }
                else
                {
                    // Display or log detailed error message
                    TempData["Message"] = $"Failed to update student data. Status Code: {statusCode}. Response: {responseContent}";
                    return View(s);
                }
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions
                TempData["Message"] = $"An error occurred: {ex.Message}";
                return View(s);
            }
        }



    }

            


        }

    

