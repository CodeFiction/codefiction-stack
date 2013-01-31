// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableUtils.cs" company="Vipdukkan®">
//   Vipdukkan Tarafından Geliştirilmiştir.
// </copyright>
// <summary>
//   IEnumerable'dan türemiş collection işlemlerinde
//   kullanılabilecek yardımcı methodları içeren class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFiction.Stack.Common.Utilities
{
    /// <summary>
    /// IEnumerable'dan türemiş collection işlemlerinde 
    /// kullanılabilecek yardımcı methodları içeren class.
    /// </summary>
    public static class EnumerableUtils
    {
        /// <summary>
        /// Predicate i listenin bütün elemanlar için doğrular.
        /// </summary>
        /// <typeparam name="T">Listenin tipi.</typeparam>
        /// <param name="list">Listenin kendisi.</param>
        /// <param name="predicate">Criterleri içeren predicate</param>
        /// <returns>Doğrulama sonucu.</returns>
        public static bool TrueForAll<T>(IEnumerable<T> list, Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var enumerator = list.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (!predicate(enumerator.Current))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verilen listeyi, verilen seperator ile birleştirir.
        /// </summary>
        /// <typeparam name="T">Listenin tipi.</typeparam>
        /// <param name="list">Listenin kendisi.</param>
        /// <param name="seperator">Kullanılacak seperator</param>
        /// <param name="func">String işleminde kullanılacak function. T tipinde item alıp string döner.</param>
        /// <returns>Seperator ile ayrılmış liste.</returns>
        public static string JoinToString<T>(IEnumerable<T> list, string seperator, Func<T, string> func)
        {
            if (list == null)
            {
                return string.Empty;
            }

            StringBuilder returnString = new StringBuilder();
            foreach (T t in list)
            {
                returnString.Append(func(t));
                returnString.Append(seperator);
            }

            if (returnString.Length == 0)
            {
                return string.Empty;
            }

            int seperatorCount = seperator.Length;
            returnString.Remove(returnString.Length - seperatorCount, seperatorCount);

            return returnString.ToString();
        }

        /// <summary>
        /// Verilen listeyi, verilen seperator ile birleştirir.
        /// </summary>
        /// <typeparam name="T">Listenin tipi.</typeparam>
        /// <param name="list">Listenin kendisi.</param>
        /// <param name="seperator">Kullanılacak seperator</param>
        /// <returns>Seperator ile ayrılmış liste.</returns>
        public static string JoinToString<T>(IEnumerable<T> list, string seperator)
        {
            return JoinToString(list, seperator, item => item.ToString());
        }
    }
}
