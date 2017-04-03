namespace CarRepairReport.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RegexValidationAttribute : ValidationAttribute
    {
        private string pattern;
        
        public RegexValidationAttribute(string pattern, string errorMsgLangKey)
        {
            this.pattern = pattern;
            this.ErrorMessage = errorMsgLangKey;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var valueAsString = value.ToString();

            if (string.IsNullOrWhiteSpace(valueAsString))
            {
                return false;
            }

            Regex regex = new Regex(this.pattern);

            var result = regex.IsMatch(valueAsString);

            return result;
        }
    }
}
