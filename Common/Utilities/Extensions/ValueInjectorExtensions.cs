using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omu.ValueInjecter
{
    public static class ValueInjectorExtensions
    {
        public static IEnumerable<TTo> InjectFrom<TFrom, TTo>(this IEnumerable<TFrom> from) where TTo : new()
        {
            return from.Select(x => new TTo().InjectFrom(x)).Cast<TTo>();
        }

        public static IEnumerable<TTo> InjectFrom<TFrom, TTo, TCInjector>(this IEnumerable<TFrom> from)
            where TCInjector : IValueInjection, new() where TTo : new()
        {
            return from.Select(x => new TTo().InjectFrom(x).InjectFrom<TCInjector>(x)).Cast<TTo>();
        }
    }
}
