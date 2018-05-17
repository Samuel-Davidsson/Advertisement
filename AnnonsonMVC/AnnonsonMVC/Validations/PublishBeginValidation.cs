using AnnonsonMVC.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace AnnonsonMVC.Validations
{
    public class PublishBeginValidation : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    ArticelViewModel articelViewModel = (ArticelViewModel)validationContext.ObjectInstance;

        //    if (articelViewModel.PublishBegin > DateTime.Today)
        //    {
        //        return new ValidationResult("Du kan inte välja datum som har varit");
        //    }
        //    if (articelViewModel.PublishBegin > articelViewModel.PublishEnd)
        //    {
        //        return new ValidationResult("Ditt startdatum måste vara mindre än ditt slutdatum");
        //    }
        //    return ValidationResult.Success;
        //}
    }
}



