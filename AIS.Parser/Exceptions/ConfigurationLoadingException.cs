using System;

namespace AIS.Parser.Exceptions
{
    public class ConfigurationLoadingException : Exception
    {
        public ConfigurationLoadingException(Exception innerException)
            :base("Error when parsing configuration", innerException)
        {
        }
    }
}
