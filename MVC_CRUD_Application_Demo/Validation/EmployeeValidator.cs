using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using MVC_CRUD_Application_Demo.Models;

namespace MVC_CRUD_Application_Demo.Validation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
//testing
            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.MiddleName).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage("MiddleName is required.");
            RuleFor(x => x.LastName).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage("Designation is required.");
        }
    }
}