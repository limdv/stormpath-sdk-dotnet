﻿// <copyright file="FieldConverterResult.cs" company="Stormpath, Inc.">
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
using Stormpath.SDK.Shared;

namespace Stormpath.SDK.Impl.Serialization.FieldConverters
{
    internal sealed class FieldConverterResult : ImmutableValueObject<FieldConverterResult>
    {
        private readonly bool success = false;
        private readonly object value = null;
        private readonly Type type = null;

        public static readonly FieldConverterResult Failed = new FieldConverterResult(false);

        private FieldConverterResult(bool success)
        {
            if (success == true)
            {
                throw new Exception("Use this constructor only for failed results. For successful results, use ConverterResult(success: true, result: object)");
            }

            this.success = success;
        }

        public FieldConverterResult(bool success, object result)
        {
            this.success = success;
            this.value = result;
        }

        public FieldConverterResult(bool success, object result, Type type)
            : this(success, result)
        {
            this.type = type;
        }

        public bool Success => this.success;

        public object Value => this.value;

        public Type Type => this.type;
    }
}
