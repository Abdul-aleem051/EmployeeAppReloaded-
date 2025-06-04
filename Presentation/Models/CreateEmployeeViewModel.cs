using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class CreateEmployeeViewModel
{
    [Required(ErrorMessage = "First name is required.")]
    [Display(Name ="First Name")]
    public string FirstName { get; set; } = default!;


    [Required(ErrorMessage = "Last name is required.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = default!;


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email provided")]
    public string Email { get; set; } = default!;

    [Required]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; }

    [Required]
    public decimal Salary { get; set; }

    [Required]
    [Display(Name = "Department")]
    public Guid DepartmentId { get; set; }

    [BindNever]
    [ValidateNever]
    public IEnumerable<SelectListItem> Departments { get; set; } = default!;
}
