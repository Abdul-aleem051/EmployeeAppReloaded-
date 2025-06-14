using System;
using Application.ContractMapping;
using Application.Dtos;
using Data.Context;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.EmployeeModule;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeAppDbContext _context;

    public EmployeeService(EmployeeAppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var data = new CreateEmployeeDto
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId
        };

        var employee = data.ToModel();

        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee.ToDto();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured while trying to create Employee Data.", ex);
            return new EmployeeDto();
        }
    }

    public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto dto)
    {
        var employee = await _context.Employees.FindAsync(dto.Id);

        if (employee is null)
        {
            return false;
        }

        try
        {
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occured while trying to update Employee Data.", ex);
            return false;
        }
    }

    public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
    {
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null)
        {
            return false;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _context.Employees.ToListAsync();
        return employees.Select(e => e.ToDto()).ToList();
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(d => d.Id == employeeId);

        if (employee == null)
        {
            return null!;
        }

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            DepartmentId = employee.DepartmentId,
        };
    }
}
