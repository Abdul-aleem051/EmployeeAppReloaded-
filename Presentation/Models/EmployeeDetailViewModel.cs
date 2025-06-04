using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Presentation.Models;

public class EmployeeDetailViewModel

{ 
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public Guid DepartmentId { get; set; } = default!;
    [BindNever]
    [ValidateNever]
    public IEnumerable<SelectListItem> Departments { get; set; } = default!;
}