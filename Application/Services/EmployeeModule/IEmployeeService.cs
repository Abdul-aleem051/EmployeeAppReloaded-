using Application.Dtos;

namespace Application.Services.EmployeeModule;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto dto);
    Task<bool> DeleteEmployeeAsync(Guid employeeId);
}
