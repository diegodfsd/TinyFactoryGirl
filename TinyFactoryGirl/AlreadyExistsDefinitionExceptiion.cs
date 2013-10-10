using System;

namespace TinyFactoryGirl
{
    public class AlreadyExistsDefinitionExceptiion : Exception
    {
        public AlreadyExistsDefinitionExceptiion(Type type)
            : base("Already exists another definition to " + type.Name)
        {
        }
    }
}