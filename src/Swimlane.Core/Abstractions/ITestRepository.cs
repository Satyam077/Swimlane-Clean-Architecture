using Swimlane.Core.DTOs;
using Swimlane.Core.Entities;
using Swimlane.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swimlane.Core.Abstractions
{
    public interface ITestRepository
    {
        Task<Response<IEnumerable<TestResponseDto>>> GetAllTestsAsync(GridRequestDto grid);
        Task<Response<TestResponseDto>> GetTestByIdAsync(Guid id);
        Task<Response<TestResponseDto>> UpsertAsync(TestRequestDto request);
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
