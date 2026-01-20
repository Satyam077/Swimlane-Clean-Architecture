using System.Net.Http.Json;
using Swimlane.Core.DTOs;
using Swimlane.Core.Utilities;

namespace Swimlane.UI.Services
{
    public class TestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TestService> _logger;

        public TestService(HttpClient httpClient, ILogger<TestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<TestResponseDto>>> GetAllTestsAsync(GridRequestDto grid)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/test/get-test", grid);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<IEnumerable<TestResponseDto>>>();
                    return result ?? Response<IEnumerable<TestResponseDto>>.ErrorResponse(ErrorCode.Unknown, "Failed to deserialize response");
                }

                return Response<IEnumerable<TestResponseDto>>.ErrorResponse(ErrorCode.Unknown, "Failed to retrieve tests");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAllTestsAsync");
                return Response<IEnumerable<TestResponseDto>>.ErrorResponse(ErrorCode.Unknown, ex.Message);
            }
        }

        public async Task<Response<TestResponseDto>> GetTestByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/test/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<TestResponseDto>>();
                    return result ?? Response<TestResponseDto>.ErrorResponse(ErrorCode.NotFound, "Test not found");
                }

                return Response<TestResponseDto>.ErrorResponse(ErrorCode.NotFound, "Test not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetTestByIdAsync");
                return Response<TestResponseDto>.ErrorResponse(ErrorCode.Unknown, ex.Message);
            }
        }

        public async Task<Response<TestResponseDto>> UpsertAsync(TestRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/test", request);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<TestResponseDto>>();
                    return result ?? Response<TestResponseDto>.ErrorResponse(ErrorCode.Unknown, "Failed to save test");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return Response<TestResponseDto>.ErrorResponse(ErrorCode.Unknown, errorContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling UpsertAsync");
                return Response<TestResponseDto>.ErrorResponse(ErrorCode.Unknown, ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/test/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                    return result ?? Response<bool>.ErrorResponse(ErrorCode.Unknown, "Failed to delete test");
                }

                return Response<bool>.ErrorResponse(ErrorCode.Unknown, "Failed to delete test");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling DeleteAsync");
                return Response<bool>.ErrorResponse(ErrorCode.Unknown, ex.Message);
            }
        }
    }
}
