using FluentValidation;

namespace StudentManagement.Web.Models.Validations
{
    public class AddStudentViewModelValidator : AbstractValidator<AddStudentViewModel>
    {
        public AddStudentViewModelValidator()
        {
            RuleFor(student => student.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z]+$");
            
            RuleFor(student => student.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isValidDomain);

            RuleFor(student => student.Phone)
                .NotEmpty()
                .Matches("^[0-9]+$");
        }

        public bool isValidDomain(string email)
        {
            var domainpart = email.Split('@').LastOrDefault();
            return domainpart != null && domainpart.Contains('.');
        }
    }
}
