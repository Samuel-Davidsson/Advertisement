using AnnonsonMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.Validations
{
    public class PublishBeginValidationForEdit : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ArticleEditViewModel articleEditViewModel = (ArticleEditViewModel)validationContext.ObjectInstance;

            if (articleEditViewModel.PublishBegin > articleEditViewModel.PublishEnd)
            {
                return new ValidationResult("Ditt startdatum måste vara mindre än ditt slutdatum.");
            }
            return ValidationResult.Success;
        }
    }
}
