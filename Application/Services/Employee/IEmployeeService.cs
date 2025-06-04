using Application.Dtos;

namespace Application.Services.Employee;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto dto);
    Task<EmployeeDto> DeleteEmployeeAsync(Guid employeeId);
}
