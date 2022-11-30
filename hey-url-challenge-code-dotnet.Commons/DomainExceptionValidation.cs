using System;
namespace hey_url_challenge_code_dotnet.Commons
{
    public class DomainExceptionValidation: Exception
    {
        public DomainExceptionValidation(string error): base(error)
        {
        }

        public static void When(bool hasError, string error, params object[] parameters)
        {
            if (hasError)
                throw new DomainExceptionValidation(string.Format(error, parameters));
        }

        public static string GetFieldRequiredMessage(object obj) =>
            string.Format(DomainExceptionValidation.REQUIRED_VALUE_MESSAGE, obj);

        public const string REQUIRED_VALUE_MESSAGE = "{0} value is required";
    }
}

