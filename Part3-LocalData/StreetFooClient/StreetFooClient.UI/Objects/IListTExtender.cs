using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient.UI
{
    public static class IListTExtender
    {
        public static void ReplaceContents<T>(this IList<T> items, IEnumerable<T> replacements)
        {
            items.Clear();
            foreach (T item in replacements)
                items.Add(item);
        }
    }
}
