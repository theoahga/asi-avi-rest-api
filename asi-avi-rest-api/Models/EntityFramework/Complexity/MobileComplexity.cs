using System.ComponentModel.DataAnnotations;

namespace asi_avi_rest_api.Models.EntityFramework.Complexity
{
    public class MobileComplexity : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var mobile = value as string;

            if (string.IsNullOrWhiteSpace(mobile) || mobile.Length != 10 || !mobile.All(c => int.TryParse(c.ToString(), out _)))
            {
                ErrorMessage = "Le mobile doit contenir 10 chiffres";
                return false;
            }

            return true;
        }
    }

}