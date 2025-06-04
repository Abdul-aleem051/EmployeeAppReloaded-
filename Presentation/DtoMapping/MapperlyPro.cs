using Application.Dtos;
using Presentation.Models;

namespace Presentation.DtoMapping;

public static class MapperlyPro
{
    
    public static EmployeeViewModel ToViewModel(this EmployeeDto dto)
    {
        return new EmployeeViewModel()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId
        };
    }

    public static EmployeeDto ToDto(this EmployeeViewModel vm)
    {
        return new EmployeeDto()
        {
            Id = vm.Id,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Email = vm.Email,
            HireDate = vm.HireDate,
            Salary = vm.Salary,
            DepartmentId = vm.DepartmentId,
        };
    }

    public static List<EmployeeViewModel> ToViewModel(this List<EmployeeDto> dtos) =>
           dtos.Select(e => e.ToViewModel()).ToList();

    public static EmployeesDto ToDto(this EmployeesViewModel vm)
    {
        return new EmployeesDto()
        {
            Employees = vm.Employees.Select(d => d.ToDto()).ToList()
        };
    }
}
