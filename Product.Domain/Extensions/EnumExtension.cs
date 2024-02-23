using Product.Domain.Extensions;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Product.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string Description(this Enum value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            DescriptionAttribute customAttribute = value.GetCustomAttribute<DescriptionAttribute>();

            if (customAttribute == null)
                return value.ToString();

            return customAttribute.Description;
        }

        public static T GetCustomAttribute<T>(this Enum value) where T : Attribute
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            FieldInfo field = value.GetType().GetField(value.ToString());
            return field.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Converte enum para string com o formato padrão de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToErrorCode(this Enum value) => Convert.ToInt32(value).ToErrorCode();
    }
}
