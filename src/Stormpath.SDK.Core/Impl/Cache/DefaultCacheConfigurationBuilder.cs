﻿// <copyright file="DefaultCacheConfigurationBuilder.cs" company="Stormpath, Inc.">
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
using Stormpath.SDK.Cache;

namespace Stormpath.SDK.Impl.Cache
{
    internal sealed class DefaultCacheConfigurationBuilder : ICacheConfigurationBuilder
    {
        private readonly string name;
        private TimeSpan? ttl;
        private TimeSpan? tti;

        public DefaultCacheConfigurationBuilder(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.name = name;
        }

        ICacheConfigurationBuilder ICacheConfigurationBuilder.WithTimeToLive(TimeSpan ttl)
        {
            this.ttl = ttl;
            return this;
        }

        ICacheConfigurationBuilder ICacheConfigurationBuilder.WithTimeToIdle(TimeSpan tti)
        {
            this.tti = tti;
            return this;
        }

        internal string Name => this.name;

        internal TimeSpan? TimeToLive => this.ttl;

        internal TimeSpan? TimeToIdle => this.tti;

        ICacheConfiguration ICacheConfigurationBuilder.Build()
            => new DefaultCacheConfiguration(this.Name, this.TimeToLive, this.TimeToIdle);
    }
}
