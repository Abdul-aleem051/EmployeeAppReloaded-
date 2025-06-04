using Application.Dtos;
using Data.Model;

namespace Application.ContractMapping
{
    public static class MapperPro
    {
        public static EmployeeDto ToDto(this Employee employee)
        {
            if (employee == null) return null!;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email  = employee.Email,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
        }

        public static Employee ToModel(this CreateEmployeeDto createEmployeeDto)
        {
            if (createEmployeeDto == null) return null!;

            return new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                Email = createEmployeeDto.Email,
                HireDate = createEmployeeDto.HireDate,
                Salary = createEmployeeDto.Salary,
                DepartmentId = createEmployeeDto.DepartmentId
            };
        }

        public static EmployeesDto EmployeesDto(this List<Employee> employees)
        {
            if (employees == null || !employees.Any())
            {
                return new EmployeesDto
                {
                    Employees = new List<EmployeeDto>()
                };
            }

            return new EmployeesDto
            {
                Employees = employees.Select(d => d.ToDto()).ToList()
            };
        }
    }
}
