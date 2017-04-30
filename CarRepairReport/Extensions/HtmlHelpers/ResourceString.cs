namespace CarRepairReport.Extensions.HtmlHelpers
{
    using System;
    using System.Web;

    public class ResourceString : MarshalByRefObject, IHtmlString
    {
        private readonly string resourceString;

        public ResourceString(string resourceString)
        {
            this.resourceString = resourceString;
        }

        public string ToHtmlString()
        {
            return this.resourceString;
        }

        public override string ToString()
        {
            return this.resourceString;
        }
    }
}