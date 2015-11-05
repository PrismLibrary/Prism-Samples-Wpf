

using System;
using System.Windows;

namespace StockTraderRI.Infrastructure
{
    /// <summary>
    /// Helper class used to traverse the Visual Tree.
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Traverses the Visual Tree upwards looking for the ancestor that satisfies the <paramref name="predicate"/>.
        /// </summary>
        /// <param name="dependencyObject">The element for which the ancestor is being looked for.</param>
        /// <param name="predicate">The predicate that evaluates if an element is the ancestor that is being looked for.</param>
        /// <returns>
        /// The ancestor element that matches the <paramref name="predicate"/> or <see langword="null"/>
        /// if the ancestor was not found.
        /// </returns>
        public static DependencyObject FindAncestor(DependencyObject dependencyObject, Func<DependencyObject, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (predicate(dependencyObject))
            {
                return dependencyObject;
            }

            DependencyObject parent = null;
            parent = LogicalTreeHelper.GetParent(dependencyObject);

            if (parent != null)
            {
                return FindAncestor(parent, predicate);
            }

            return null;
        }
    }
}