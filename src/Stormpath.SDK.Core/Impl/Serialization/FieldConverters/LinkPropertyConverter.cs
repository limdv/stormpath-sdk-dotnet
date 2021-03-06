﻿// <copyright file="LinkPropertyConverter.cs" company="Stormpath, Inc.">
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
using System.Collections.Generic;
using System.Linq;
using Stormpath.SDK.Impl.Resource;
using Map = System.Collections.Generic.IDictionary<string, object>;

namespace Stormpath.SDK.Impl.Serialization.FieldConverters
{
    internal sealed class LinkPropertyConverter : AbstractFieldConverter
    {
        public LinkPropertyConverter()
            : base(nameof(LinkPropertyConverter), appliesToTargetType: AnyType)
        {
        }

        protected override FieldConverterResult ConvertImpl(KeyValuePair<string, object> token)
        {
            var asEmbeddedObject = token.Value as Map;
            if (asEmbeddedObject == null)
            {
                return FieldConverterResult.Failed;
            }

            if (asEmbeddedObject.Count > 1)
            {
                return FieldConverterResult.Failed;
            }

            var firstItem = asEmbeddedObject.FirstOrDefault();
            var hasHref = string.Equals(firstItem.Key, "href", StringComparison.OrdinalIgnoreCase);

            if (!hasHref)
            {
                return FieldConverterResult.Failed;
            }

            return new FieldConverterResult(true, new LinkProperty(firstItem.Value.ToString()));
        }
    }
}
