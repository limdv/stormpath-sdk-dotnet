﻿// <copyright file="SyncGroupCreationExtensions.cs" company="Stormpath, Inc.">
// Copyright (c) 2015 Stormpath, Inc.
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
using Stormpath.SDK.Group;

namespace Stormpath.SDK.Sync
{
    public static class SyncGroupCreationExtensions
    {
        /// <summary>
        /// Synchronously creates a new <see cref="IGroup"/>.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="group">The group to create.</param>
        /// <returns>The new <see cref="IGroup"/>.</returns>
        public static IGroup CreateGroup(this IGroupCreationActions source, IGroup group)
            => (source as IGroupCreationActionsSync).CreateGroup(group);

        /// <summary>
        /// Synchronously creates a new <see cref="IGroup"/>.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="group">The group to create.</param>
        /// <param name="creationOptionsAction">
        /// An inline builder for an instance of <see cref="IGroupCreationOptions"/>, which will be used when sending the request.
        /// </param>
        /// <returns>The new <see cref="IGroup"/>.</returns>
        public static IGroup CreateGroup(this IGroupCreationActions source, IGroup group, Action<GroupCreationOptionsBuilder> creationOptionsAction)
            => (source as IGroupCreationActionsSync).CreateGroup(group, creationOptionsAction);

        /// <summary>
        /// Synchronously reates a new <see cref="IGroup"/>.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="group">The group to create.</param>
        /// <param name="creationOptions">An <see cref="IGroupCreationOptions"/> instance to use when sending the request.</param>
        /// <returns>The new <see cref="IGroup"/>.</returns>
        public static IGroup CreateGroup(this IGroupCreationActions source, IGroup group, IGroupCreationOptions creationOptions)
            => (source as IGroupCreationActionsSync).CreateGroup(group, creationOptions);
    }
}