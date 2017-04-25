using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard
{
    public static class Collections
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(
            this IEnumerable<T> items,
            Func<T, string> nameSelector,
            Func<T, string> valueSelector,
            Func<T, bool> selected)
        {
            return items.OrderBy(item => nameSelector(item))
                .Select(item =>
                        new SelectListItem
                        {
                            Selected    = selected(item),
                            Text        = nameSelector(item),
                            Value       = valueSelector(item)
                        });
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }

            yield return value;
        }

        public static IEnumerable<T> Pop<T>(this IEnumerable<T> e, T value)
        {
            yield return value;

            foreach (var cur in e)
            {
                yield return cur;
            }
        }
    }
}