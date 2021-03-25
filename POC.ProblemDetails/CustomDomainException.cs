using System;

namespace POC.ProblemDetails
{
    public class CustomDomainException : Exception
    {
        public string CustomProperty { get; set; }
    }
}