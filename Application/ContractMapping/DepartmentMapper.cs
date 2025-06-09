using Application.Dtos;
using Data.Model;

namespace Application.ContractMapping
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDto(this Department department)
        {
            if (department == null) return null!; 

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
        }

        public static Department ToModel(this CreateDepartmentDto createDepartmentDto)
        {
            if (createDepartmentDto == null) return null!;

            return new Department
            {
                Id = Guid.NewGuid(),
                Name = createDepartmentDto.Name,
                Description = createDepartmentDto.Description
            };
        }

        public static DepartmentsDto DepartmentsDto(this List<Department> departments)
        {
            if (departments == null || !departments.Any())
            {
                return new DepartmentsDto
                {
                    Departments = new List<DepartmentDto>() 
                };
            }

            return new DepartmentsDto
            {
                Departments = departments.Select(d => d.ToDto()).ToList()
            };
        }
    }
}
