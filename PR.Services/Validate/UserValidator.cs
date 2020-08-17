using FluentValidation;
using PR.Services.ModelResponse;
using PR.Tools.String;

namespace PR.Services.Validate
{
    /// <summary>
    /// Validador para el modelo UserViewModel
    /// </summary>
    public class UserAppValidator : AbstractValidator<UserViewModel>
    {
        public UserAppValidator()
        {
            RuleFor(x => x.UserId).
                NotNull().WithMessage("User Id is not null").
                WithName("User Id");

            RuleFor(x => x.DocumentTypeId).NotNull().WithMessage("Document Type Id not null").
                WithName("Document Type Id");

            RuleFor(x => x.UserNickName).
                NotNull().WithMessage("Nick Name is not null").
                MaximumLength(50).WithMessage("Nick Name cannot exceed 50 characters").
                MinimumLength(4).WithMessage("Nick Name must be at least 4 characters long").
                WithName("Nick Name");

            RuleFor(x => x.UserDocumentId).
                NotNull().WithMessage("Number Document is not null").
                MaximumLength(20).WithMessage("Number Document cannot exceed 20 characters").
                MinimumLength(5).WithMessage("Number Document must be at least 5 characters long").
                WithName("Number Document");

            RuleFor(x => x.UserName).
                NotNull().WithMessage("User Name is not null").
                MaximumLength(50).WithMessage("User Name cannot exceed 50 characters").
                MinimumLength(3).WithMessage("User Name must be at least 3 characters long").
                WithName("User Name");

            RuleFor(x => x.UserLastName).
                NotNull().WithMessage("User Last Name is not null").
                Length(3, 50).WithMessage("User Last must be between 3 and 10 characters").
                MaximumLength(50).WithMessage("User Last Name cannot exceed 50 characters").
                MinimumLength(3).WithMessage("User Last Name must be at least 3 characters long").
                WithName("User Last Name");

            RuleFor(x => x.UserEmail).
                EmailAddress().WithMessage("User Email is not valide").
                MaximumLength(300).WithMessage("User Email Name cannot exceed 300 characters").
                WithName("User Email");

            When(x => !x.UserPassword.IsEmpty(), () =>
            {
                RuleFor(y => y).Must(z => ConfirmPasswordUser(z)).WithMessage("Passwords do not match").
                WithName("User Password");
            });
        }

        /// <summary>
        /// Verifica que las 2 contraseñas sean iguales
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns>true si son iguales</returns>
        private bool ConfirmPasswordUser(UserViewModel user)
        {
            return user.UserPassword.Equals(user.ConfirmPassword) && !user.UserPassword.IsEmpty();
        }
    }
}