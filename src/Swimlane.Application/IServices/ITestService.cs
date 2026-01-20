using Swimlane.Core.DTOs;
using Swimlane.Core.Utilities;

namespace Swimlane.Application.IServices
{
    public interface ITestService
    {
        Task<Response<IEnumerable<TestResponseDto>>> GetAllTestsAsync(GridRequestDto grid);
        Task<Response<TestResponseDto>> GetTestByIdAsync(Guid id);
        Task<Response<TestResponseDto>> UpsertAsync(TestRequestDto request);
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
