using Microsoft.EntityFrameworkCore;
using Swimlane.Core.Abstractions;
using Swimlane.Core.DTOs;
using Swimlane.Core.Entities;
using Swimlane.Core.Utilities;
using Swimlane.Infrastructure.Databases;

namespace Swimlane.Infrastructure.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly DatabaseContext _context;

        public TestRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Response<IEnumerable<TestResponseDto>>> GetAllTestsAsync(GridRequestDto grid)
        {
            return await ResponseHelper.TryCatchAsync(async () =>
            {
                var query = _context.Tests.AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(grid.SearchTerm))
                {
                    query = query.Where(t => t.TestName.Contains(grid.SearchTerm));
                }

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(grid.SortBy))
                {
                    query = grid.SortBy.ToLower() switch
                    {
                        "testname" => grid.SortDescending
                            ? query.OrderByDescending(t => t.TestName)
                            : query.OrderBy(t => t.TestName),
                        "createddate" => grid.SortDescending
                            ? query.OrderByDescending(t => t.CreatedDate)
                            : query.OrderBy(t => t.CreatedDate),
                        _ => query.OrderByDescending(t => t.CreatedDate)
                    };
                }
                else
                {
                    query = query.OrderByDescending(t => t.CreatedDate);
                }

                // Apply pagination
                var tests = await query
                    .Skip((grid.PageNumber - 1) * grid.PageSize)
                    .Take(grid.PageSize)
                    .Select(t => new TestResponseDto
                    {
                        Id = t.Id,
                        TestName = t.TestName,
                        CreatedDate = t.CreatedDate,
                        UpdatedDate = t.UpdatedDate
                    })
                    .ToListAsync();

                return Response<IEnumerable<TestResponseDto>>.SuccessResponse(tests, "Tests retrieved successfully.");
            });
        }

        public async Task<Response<TestResponseDto>> GetTestByIdAsync(Guid id)
        {
            return await ResponseHelper.TryCatchAsync(async () =>
            {
                var test = await _context.Tests.FindAsync(id);

                if (test == null)
                {
                    return Response<TestResponseDto>.ErrorResponse(
                        ErrorCode.NotFound,
                        "Test not found.");
                }

                var response = new TestResponseDto
                {
                    Id = test.Id,
                    TestName = test.TestName,
                    CreatedDate = test.CreatedDate,
                    UpdatedDate = test.UpdatedDate
                };

                return Response<TestResponseDto>.SuccessResponse(response, "Test retrieved successfully.");
            });
        }

        public async Task<Response<TestResponseDto>> UpsertAsync(TestRequestDto request)
        {
            return await ResponseHelper.TryCatchAsync(async () =>
            {
                Test? test;
                bool isNew = request.Id == Guid.Empty;

                if (isNew)
                {
                    // Create new test
                    test = new Test
                    {
                        Id = Guid.NewGuid(),
                        TestName = request.TestName,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = null, // Set from current user context if available
                    };

                    await _context.Tests.AddAsync(test);
                }
                else
                {
                    // Update existing test
                    test = await _context.Tests.FindAsync(request.Id);

                    if (test == null)
                    {
                        return Response<TestResponseDto>.ErrorResponse(
                            ErrorCode.NotFound,
                            "Test not found.");
                    }

                    test.TestName = request.TestName;
                    test.UpdatedDate = DateTime.UtcNow;
                    test.UpdatedBy = null; // Set from current user context if available

                    _context.Tests.Update(test);
                }

                await _context.SaveChangesAsync();

                var response = new TestResponseDto
                {
                    Id = test.Id,
                    TestName = test.TestName,
                    CreatedDate = test.CreatedDate,
                    UpdatedDate = test.UpdatedDate
                };

                return Response<TestResponseDto>.SuccessResponse(
                    response,
                    isNew ? "Test created successfully." : "Test updated successfully.");
            });
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            return await ResponseHelper.TryCatchAsync(async () =>
            {
                var test = await _context.Tests.FindAsync(id);

                if (test == null)
                {
                    return Response<bool>.ErrorResponse(
                        ErrorCode.NotFound,
                        "Test not found.");
                }

                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();

                return Response<bool>.SuccessResponse(true, "Test deleted successfully.");
            });
        }
    }
}
