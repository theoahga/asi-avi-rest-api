using System.ComponentModel.DataAnnotations;

namespace asi_avi_rest_api.Models.EntityFramework.Complexity
{
    public class CodePostalComplexity : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var mobile = value as string;

            if (string.IsNullOrWhiteSpace(mobile) || mobile.Length != 5 || !mobile.All(c => int.TryParse(c.ToString(), out _)))
            {
                ErrorMessage = "Le Code Postal doit contenir 5 chiffres";
                return false;
            }

            return true;
        }
    }

}