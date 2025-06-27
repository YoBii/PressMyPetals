using System;
using System.Collections.Generic;
using System.Linq;

namespace PressMyPetals {
    internal static class Util {
        internal static IEnumerable<T> Traverse<T>(this IEnumerable<T> items,
Func<T, IEnumerable<T>> childSelector, Func<T, IEnumerable<T>> childSelector2) {
            var stack = new Stack<T>(items);
            while (stack.Any()) {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                    stack.Push(child);
                foreach (var child2 in childSelector2(next))
                    stack.Push(child2);
            }
        }
    }
}
