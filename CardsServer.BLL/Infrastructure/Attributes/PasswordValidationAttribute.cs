using System.ComponentModel.DataAnnotations;

namespace CardsServer.BLL.Infrastructure.Attributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string password)
            {
                if (password.Length >= 8)   
                    return true;
                else
                    ErrorMessage = "Пароль должен состоять из 8 символов и более!";
            }
            return false;
        }

    }
}
