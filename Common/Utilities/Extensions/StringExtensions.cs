// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Vipdukkan®">
//   Vipdukkan Tarafından Geliştirilmiştir.
// </copyright>
// <summary>
//   String işlemlerinde kullanılan yardımcı extension methodlar.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using Pandora.Utilities;

namespace System
{
    using Collections.Generic;

    /// <summary>
    /// String işlemlerinde kullanılan yardımcı extension methodlar.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Stringin boş veya null olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="text">Kontrol edilecek string.</param>
        /// <returns>Kontrol sonucu.</returns>
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmpty(this string text)
        {
            return String.IsNullOrEmpty(text);
        }

        /// <summary>
        /// String arrayi seperator ile birlikte birleştirir.
        /// </summary>
        /// <param name="text">Birleştirilecek string.</param>
        /// <param name="seperator">Kullanılacak seperator.</param>
        /// <returns>Birleştirilmiş string.</returns>
        public static string ConvertToDelimetedText(this string[] text, char seperator)
        {
            return StringUtils.ConvertToDelimetedText(text, seperator);
        }

        /// <summary>
        /// Girilen stringde verilen Expression'ı arar
        /// </summary>
        /// <param name="value">Arama yapılacak string.</param>
        /// <param name="regularExpression">Arama kriterini içeren expression.</param>
        /// <param name="matchEntirely">Tümünü Ara.</param>
        /// <returns>Arama sonucu.</returns>
        public static bool IsMatch(this string value, string regularExpression, bool matchEntirely)
        {
            return StringUtils.IsMatch(value, regularExpression, matchEntirely);
        }

        /// <summary>
        /// Verilen string arrayini seperator ile join eder.
        /// </summary>
        /// <param name="values">String arrayi.</param>
        /// <param name="seperator">Join işleminde kullanılacak seperator.</param>
        /// <returns>Joinlenmiş string.</returns>
        public static string Join(this string[] values, string seperator)
        {
            return StringUtils.Join(values, seperator);
        }

        /// <summary>
        /// Verilen stringi, seperatorüne göre ayırıp, T tipinden
        /// generic liste oluşturur.
        /// </summary>
        /// <typeparam name="T">Listenin tipi</typeparam>
        /// <param name="text">Listeye dönüştürülücek string.</param>
        /// <param name="seperator">Listenin elemanlarının ayrlıacağı seperator.</param>
        /// <returns>Generic liste.</returns>
        public static List<T> ToGenericList<T>(this string text, char seperator)
        {
            return CollectionUtils.CreateList<T>(text, seperator);
        }

        public static string EnsureMaximumLength(this string str, int maxLength)
        {
            return StringUtils.EnsureMaximumLength(str, maxLength);
        }

        public static string EnsureNotNullorEmpty(this string str)
        {
            return StringUtils.EnsureNotNullorEmpty(str);
        }

        public static string RemoveTurkishChar(this string str)
        {
            return StringUtils.RemoveTurkishChar(str);
        }

        public static string ToAliasString(this string s, char replaceChar = '_')
        {
            return StringUtils.ToAliasString(s, replaceChar);
        }

        public static bool IsValidEmail(this string email)
        {
            return StringUtils.IsValidEmail(email);
        }

        public static string ToEnglish(this string text)
        {
            return StringUtils.ToEnglish(text);
        }
    }
}
