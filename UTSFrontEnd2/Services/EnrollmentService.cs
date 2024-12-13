using System.Net.Http.Json;

namespace UTSFrontEnd2.Services
{
    public class EnrollmentService
    {
        private readonly HttpClient _httpClient;

        public EnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EnrollInstructor(int instructorId, int courseId)
        {
            try
            {
                var enrollmentData = new { instructorId, courseId };
                var response = await _httpClient.PostAsJsonAsync("enrollments", enrollmentData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Instructor successfully enrolled in course.");
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to enroll instructor: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during enrollment: {ex.Message}");
            }

            return false;
        }
    }
}
