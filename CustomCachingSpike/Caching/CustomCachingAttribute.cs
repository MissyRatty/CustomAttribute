using System;

namespace CustomCachingSpike.Caching
{

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class CustomCachingAttribute : Attribute
    {
        public CustomCachingAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public string OptionalParameter
        {
            get
            {
                return SomeLogicMethod(Value);
            }
            set
            {
                Value = value;
            }
        }


        private string SomeLogicMethod(string value)
        {
            string message = string.IsNullOrEmpty(value) ? "Parameter was empty" : "Parameter passed was " + value;
            return message;
        }

    }
}
