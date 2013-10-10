using System;

namespace TinyFactoryGirl
{
    public class NotFoundDefinitionException : Exception
    {
        public NotFoundDefinitionException(Type type)
            : base("Can't find definition to " + type.Name)
        {
        }
    }
}
