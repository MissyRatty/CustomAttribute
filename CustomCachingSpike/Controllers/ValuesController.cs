using CustomCachingSpike.Caching;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CustomCachingSpike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomCaching("Class Level Instance")]
    public class ValuesController : ControllerBase
    {
        
        [HttpGet]
        [CustomCaching("Method Level Instance")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var myParams = GetClassAttribute(typeof(ValuesController));
            return new string[] { "value1", "value2", myParams[0], myParams[1] };
        }

        private static string[] GetClassAttribute(Type type)
        {
            // Get the class-level attributes.

            CustomCachingAttribute customAttribute;

            // Put the instance of the attribute on the class level in the customAttribute object.
            customAttribute = (CustomCachingAttribute) Attribute.GetCustomAttribute(type, typeof(CustomCachingAttribute));
           
            string[] val = { string.Empty, string.Empty };

            if (customAttribute != null)
            {
                //get class level property
               val[0] = customAttribute.OptionalParameter;
            }

            // Get the method-level attributes.
            // Get all methods in this class with the CustomCachingAttribute
            MemberInfo[] memberInfos = type.GetMethods();
            foreach (var item in memberInfos)
            {
                customAttribute = (CustomCachingAttribute) Attribute.GetCustomAttribute(item, typeof(CustomCachingAttribute));
                int i = 1;

                //filtering out Class methods that don't have the CustomCachingAttribute
                if (customAttribute != null)
                {
                    val[i] = customAttribute.OptionalParameter;
                    i++;
                }
            }

            return val;
        }
    }
}
