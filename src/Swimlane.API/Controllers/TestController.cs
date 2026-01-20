using Microsoft.AspNetCore.Mvc;
using Swimlane.Application.IServices;
using Swimlane.Core.DTOs;

namespace Swimlane.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly ILogger<TestController> _logger;

        public TestController(ITestService testService, ILogger<TestController> logger)
        {
            _testService = testService;
            _logger = logger;
        }

        [HttpPost("get-test")]
        public async Task<IActionResult> GetAllTest([FromBody] GridRequestDto grid)
        {
            try
            {
                var result = await _testService.GetAllTestsAsync(grid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tests");
                return BadRequest(new { message = "An error occurred while retrieving tests" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _testService.GetTestByIdAsync(id);
                
                if (!result.Success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test with ID: {Id}", id);
                return BadRequest(new { message = "An error occurred while retrieving the test" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] TestRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _testService.UpsertAsync(request);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error upserting test");
                return BadRequest(new { message = "An error occurred while saving the test" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _testService.DeleteAsync(id);

                if (!result.Success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test with ID: {Id}", id);
                return BadRequest(new { message = "An error occurred while deleting the test" });
            }
        }
    }
}
