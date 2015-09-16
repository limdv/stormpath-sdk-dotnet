﻿// <copyright file="AsyncQueryableExtensions.cs" company="Stormpath, Inc.">
//      Copyright (c) 2015 Stormpath, Inc.
// </copyright>
// <remarks>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.SDK.Impl.Linq;
using Stormpath.SDK.Impl.Resource;
using Stormpath.SDK.Linq;

namespace Stormpath.SDK
{
    /// <summary>
    /// Provides a set of static methods for querying asynchronous data structures
    /// that implement <see cref="IAsyncQueryable{T}"/>.
    /// </summary>
    public static class AsyncQueryableExtensions
    {
        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedAsyncQueryable{T}"/> whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncQueryable<TSource> OrderBy<TSource, TKey>(this IAsyncQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return (IOrderedAsyncQueryable<TSource>)source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.OrderBy, (IQueryable<TSource>)null, keySelector),
                    source.Expression,
                    Expression.Quote(keySelector)));
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedAsyncQueryable{T}"/> whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncQueryable<TSource> OrderByDescending<TSource, TKey>(this IAsyncQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return (IOrderedAsyncQueryable<TSource>)source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.OrderByDescending, (IQueryable<TSource>)null, keySelector),
                    source.Expression,
                    Expression.Quote(keySelector)));
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the function represented by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">An <see cref="IOrderedAsyncQueryable{T}"/> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>An <see cref="IOrderedAsyncQueryable{T}"/> whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncQueryable<TSource> ThenBy<TSource, TKey>(this IAsyncQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return (IOrderedAsyncQueryable<TSource>)source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.ThenBy, (IOrderedQueryable<TSource>)null, keySelector),
                    source.Expression,
                    Expression.Quote(keySelector)));
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the function represented by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">An <see cref="IOrderedAsyncQueryable{T}"/> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>An <see cref="IOrderedAsyncQueryable{T}"/> whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncQueryable<TSource> ThenByDescending<TSource, TKey>(this IAsyncQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return (IOrderedAsyncQueryable<TSource>)source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.ThenByDescending, (IOrderedQueryable<TSource>)null, keySelector),
                    source.Expression,
                    Expression.Quote(keySelector)));
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> that contains elements that occur after the specified index in the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IAsyncQueryable<TSource> Skip<TSource>(this IAsyncQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.Skip, (IQueryable<TSource>)null, count),
                    source.Expression,
                    Expression.Constant(count)));
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> that contains the specified number of elements from the start of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IAsyncQueryable<TSource> Take<TSource>(this IAsyncQueryable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.Take, (IQueryable<TSource>)null, count),
                    source.Expression,
                    Expression.Constant(count)));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref=IAsyncQueryable{TSource}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> that contains elements from the input sequence that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncQueryable<TSource> Where<TSource>(this IAsyncQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return source.Provider.CreateQuery(
                LinqHelper.MethodCall(
                    LinqHelper.GetMethodInfo(Queryable.Where, (IQueryable<TSource>)null, predicate),
                    source.Expression,
                    Expression.Quote(predicate)));
        }

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IAsyncQueryable{T}"/> that contains the elements to be counted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the number of elements in the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="InvalidOperationException">The underlying provider does not support this operation.</exception>
        public static async Task<int> CountAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var collection = source as CollectionResourceQueryable<TSource>;
            if (collection == null)
                throw new InvalidOperationException("This queryable is not a supported collection resource.");

            if (!await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
                return 0;

            return collection.Size;
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IAsyncQueryable{T}"/> to return the first element of.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the first element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">The <paramref name="source"/> sequence is empty.</exception>
        public static async Task<TSource> FirstAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
                throw new InvalidOperationException("The sequence has no elements.");

            return source.CurrentPage.First();
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IAsyncQueryable{T}"/> to return the first element of.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is <c>default(TSource)</c> if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
                return default(TSource);

            return source.CurrentPage.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> to return the single element of.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the single element of the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> has more than one element.</exception>
        public static async Task<TSource> SingleAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
                throw new InvalidOperationException("The sequence has no elements.");

            return source.CurrentPage.Single();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence, or a default value if the sequence is
        /// empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> to return the single element of.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the single element of the input sequence, or <c>default(TSource)</c> if the sequence contains no elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> has more than one element.</exception>
        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
                return default(TSource);

            return source.CurrentPage.SingleOrDefault();
        }

        /// <summary>
        /// Asynchronously returns the input sequence as a <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> to get items from.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is a list of all items from the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async Task<List<TSource>> ToListAsync<TSource>(this IAsyncQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var results = new List<TSource>();
            while (await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
            {
                results.AddRange(source.CurrentPage);
            }

            return results;
        }

        /// <summary>
        /// Asynchronously iterates over the input sequence and performs the specified action on each element of the <see cref="IAsyncQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> containing items to operate on.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task indicating that the asynchronous operation is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
        public static Task ForEachAsync<TSource>(this IAsyncQueryable<TSource> source, Action<TSource> action, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return source.ForEachAsync((item, unused_) => action(item), cancellationToken);
        }

        /// <summary>
        /// Asynchronously iterates over the input sequence and performs the specified action on each indexed element of the <see cref="IAsyncQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IAsyncQueryable{T}"/> containing items to operate on.</param>
        /// <param name="action">The action to perform on the element with the specified index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task indicating that the asynchronous operation is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
        public static async Task ForEachAsync<TSource>(this IAsyncQueryable<TSource> source, Action<TSource, int> action, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var index = 0;
            while (await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
            {
                foreach (var item in source.CurrentPage)
                {
                    action(item, index++);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
