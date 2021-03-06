﻿// <copyright file="SyncQueryableExtensions.cs" company="Stormpath, Inc.">
// Copyright (c) 2016 Stormpath, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Linq;
using Stormpath.SDK.Impl.Linq;
using Stormpath.SDK.Linq;

namespace Stormpath.SDK.Sync
{
    /// <summary>
    /// Provides synchronous access to the methods available on <see cref="IAsyncQueryable{TSource}"/>.
    /// </summary>
    public static class SyncQueryableExtensions
    {
        /// <summary>
        /// Provides synchronous access to a collection resource.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An asynchronous data source to query synchronously.</param>
        /// <returns>A <see cref="IQueryable{T}"/> that will execute all requests synchronously.</returns>
        public static IQueryable<TSource> Synchronously<TSource>(this IAsyncQueryable<TSource> source)
        {
            var collection = source as CollectionResourceQueryable<TSource>;
            if (collection == null)
            {
                throw new InvalidOperationException("This queryable is not a supported collection resource.");
            }

            return source as IQueryable<TSource>;
        }
    }
}
