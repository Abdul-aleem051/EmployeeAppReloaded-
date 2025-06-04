using Application.Dtos;
using Application.Services.Department;
using Application.Services.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.DtoMapping;
using Presentation.Models;


namespace Presentation.Controllers;

public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public EmployeeController(
        IEmployeeService employeeService,
        IDepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var employeeDtos = await _employeeService.GetAllEmployeesAsync();
        var employeeViewModels = employeeDtos.ToViewModel();

        var viewModel = new EmployeesViewModel
        {
            Employees = employeeViewModels
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var employeeDto = await _employeeService.GetEmployeeByIdAsync(id);

        if (employeeDto == null)
        {
            return NotFound();
        }

        var viewModel = new EmployeeDetailViewModel
        {
            Id = employeeDto.Id,
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            HireDate = employeeDto.HireDate,
            Salary = employeeDto.Salary,
            DepartmentId = employeeDto.DepartmentId
        };

        return View(viewModel);
    }


    public async Task<IActionResult> Create()
    {
        var viewModel = new CreateEmployeeViewModel
        {
            Departments = await GetDepartmentSelectList()
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new CreateEmployeeViewModel
            {
                Departments = await GetDepartmentSelectList()
            };

            return View(viewModel);
        }

        var dto = new CreateEmployeeDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            HireDate = model.HireDate,
            Salary = model.Salary,
            DepartmentId = model.DepartmentId
        };

        var result = await _employeeService.CreateEmployeeAsync(dto);

        if (result == null)
        {
            SetFlashMessage("An error occurred while creating the department. Please try again.", "error");
            return View(model);
        }

        SetFlashMessage("Employee data created successfully.", "success");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }


        var model = new UpdateEmployeeViewModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            DepartmentId = employee.DepartmentId,
             Departments = await GetDepartmentSelectList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            SetFlashMessage("Fix the errors before submitting.", "error");
            return View(model);
        }

        var dto = new UpdateEmployeeDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            HireDate = model.HireDate,
            Salary = model.Salary,
            DepartmentId = model.DepartmentId,
        };

        var employeeDto = new EmployeeDto
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId
           

        };

        var result = await _employeeService.UpdateEmployeeAsync(employeeDto);

        if (result == null)
        {
            SetFlashMessage("An error occurred while updating the employee record. Please try again.", "error");
            return View(model);
        }

        SetFlashMessage("Employee data updated successfully.", "success");
        return RedirectToAction(nameof(Index));

    }
    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var employee = _employeeService.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        _employeeService.DeleteEmployeeAsync(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<SelectListItem>> GetDepartmentSelectList()
    {
        var data = await _departmentService.GetAllDepartmentsAsync();

        return data.Departments.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        });
    }
}
