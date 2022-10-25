using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ToddBoothroyd_BowlingGame
{
    public static class EnumeratedExtensions
    {
        /// <summary>
        /// Extension method allows the retrieval of the description attribute
        /// allowing a string to be associated with an enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T en) where T : IConvertible
        {
            if (en is Enum)
            {
                Type type = en.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == en.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memberInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memberInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                            return descriptionAttribute.Description;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Extension helper to return int from enum
        /// </summary>
        /// <typeparam name="Enum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt<Enum>(this Enum value)
        {
            Type type = typeof(Enum);

            if (!type.IsEnum)
                throw new ArgumentException($"{type} is not an enum.");

            return Convert.ToInt32(value);
        }
    }
}
