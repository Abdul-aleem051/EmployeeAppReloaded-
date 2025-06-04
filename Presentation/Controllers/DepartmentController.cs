using Application.Dtos;
using Application.Services.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.DtoMapping;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            var viewModel = departments.ToViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var departmentDto = await _departmentService.GetDepartmentByIdAsync(id);
            if (departmentDto == null) return NotFound();

            var viewModel = new DepartmentDetailViewModel
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description
                
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Please fill in all required fields correctly.", "error");
                return View(model);
            }

            var dto = new CreateDepartmentDto
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description
            };

            var result = await _departmentService.CreateDepartmentAsync(dto);

            if (result == null)
            {
                SetFlashMessage("An error occurred while creating the department. Please try again.", "error");
                return View(model);
            }

            SetFlashMessage("Department created successfully.", "success");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var model = new UpdateDepartmentViewModel
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Fix the errors before submitting.", "error");
                return View(model);
            }

            var dto = new UpdateDepartmentDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            var departmentDto = new DepartmentDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Employees = new List<EmployeeDto>()
            };

            var updated = await _departmentService.UpdateDepartmentAsync(departmentDto);

            if (!updated)
            {
                SetFlashMessage("Update failed. Try again.", "error");
                return View(model);
            }

            SetFlashMessage("Department updated successfully.", "success");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public  IActionResult Delete(Guid id)
        {
            var department =  _departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

             _departmentService.DeleteDepartmentAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
