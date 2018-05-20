using AnnonsonMVC.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.Validations
{
    public class PublishBeginValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ArticelViewModel articelViewModel = (ArticelViewModel)validationContext.ObjectInstance;
            ArticleEditViewModel articleEditViewModel = (ArticleEditViewModel)validationContext.ObjectInstance;

            if (articleEditViewModel.PublishBegin > articleEditViewModel.PublishEnd)
            {
                return new ValidationResult("Ditt startdatum måste vara mindre än ditt slutdatum");
            }
            return ValidationResult.Success;
        }
        }
    }




