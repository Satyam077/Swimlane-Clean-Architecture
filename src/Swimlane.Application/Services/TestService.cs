using Swimlane.Application.IServices;
using Swimlane.Core.Abstractions;
using Swimlane.Core.DTOs;
using Swimlane.Core.Utilities;

namespace Swimlane.Application.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<Response<IEnumerable<TestResponseDto>>> GetAllTestsAsync(GridRequestDto grid)
        {
            return await _testRepository.GetAllTestsAsync(grid);
        }

        public async Task<Response<TestResponseDto>> GetTestByIdAsync(Guid id)
        {
            return await _testRepository.GetTestByIdAsync(id);
        }

        public async Task<Response<TestResponseDto>> UpsertAsync(TestRequestDto request)
        {
            return await _testRepository.UpsertAsync(request);
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            return await _testRepository.DeleteAsync(id);
        }
    }
}
