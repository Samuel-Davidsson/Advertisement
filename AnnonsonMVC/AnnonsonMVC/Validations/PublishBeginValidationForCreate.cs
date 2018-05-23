using AnnonsonMVC.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.Validations
{
    public class PublishBeginValidationForCreate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ArticleCreateViewModel articelCreateViewModel = (ArticleCreateViewModel)validationContext.ObjectInstance;

            if (articelCreateViewModel.PublishBegin > articelCreateViewModel.PublishEnd)
            {
                return new ValidationResult("Ditt startdatum måste vara mindre än ditt slutdatum.");
            }
            if (articelCreateViewModel.PublishBegin < DateTime.Today)
            {
                return new ValidationResult("Startdatumet har redan passerat.");
            }
            return ValidationResult.Success;            
        }
   }
}




