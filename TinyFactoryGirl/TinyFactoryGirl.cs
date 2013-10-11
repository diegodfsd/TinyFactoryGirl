using System;
using System.Collections.Generic;

namespace TinyFactoryGirl
{
    public static class TinyFactoryGirl
    {
        static readonly IDictionary<string, Func<object>> Definitions = new Dictionary<string, Func<object>>();

        public static void Define<T>(Func<T> builder)
        {
            Define("", builder);
        }

        public static void Define<T>(string alias, Func<T> builder)
        {
            var key = GenerateKey<T>(alias);
            if (Definitions.ContainsKey(key))
                throw new AlreadyExistsDefinitionExceptiion(typeof(T));

            Definitions.Add(key, () => builder());
        }

        public static T Build<T>(string alias = "")
        {
            var key = GenerateKey<T>(alias);
            if (!Definitions.ContainsKey(key))
                throw new NotFoundDefinitionException(typeof(T));

            return (T)Definitions[key]();
        }

        public static T Build<T>(Action<T> @override)
        {
            return Build("", @override);
        }

        public static T Build<T>(string alias, Action<T> @override)
        {
            var instance = Build<T>(alias);
            @override(instance);

            return instance;
        }

        public static void ClearDefinitions()
        {
            Definitions.Clear();
        }

        private static string GenerateKey<T>(string alias = null)
        {
            var typeName = typeof(T).Name;

            if (string.IsNullOrWhiteSpace(alias))
                return typeName;

            return string.Concat(typeName, "_", alias);
        }
    }
}