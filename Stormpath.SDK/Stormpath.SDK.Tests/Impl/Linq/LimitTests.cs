﻿// <copyright file="LimitTests.cs" company="Stormpath, Inc.">
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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stormpath.SDK.Account;

namespace Stormpath.SDK.Tests.Impl.Linq
{
    [TestClass]
    public class LimitTests
    {
        private static string url = "http://f.oo";
        private static string resource = "bar";

        [TestMethod]
        [TestCategory("Impl.Linq")]
        public void Take_with_constant_becomes_limit()
        {
            var harness = TestHarness<IAccount>.Create<IAccount>(url, resource);

            harness.Queryable
                .Take(10)
                .ToList();

            harness.WasCalledWithArguments("limit=10");
        }

        [TestMethod]
        [TestCategory("Impl.Linq")]
        public void Take_with_variable_becomes_limit()
        {
            var harness = TestHarness<IAccount>.Create<IAccount>(url, resource);

            var limit = 20;
            harness.Queryable
                .Take(limit)
                .ToList();

            harness.WasCalledWithArguments("limit=20");
        }

        [TestMethod]
        [TestCategory("Impl.Linq")]
        public void Take_with_function_becomes_limit()
        {
            var harness = TestHarness<IAccount>.Create<IAccount>(url, resource);

            var limitFunc = new Func<int>(() => 25);
            harness.Queryable
                .Take(limitFunc())
                .ToList();

            harness.WasCalledWithArguments("limit=25");
        }

        [TestMethod]
        [TestCategory("Impl.Linq")]
        public void Take_multiple_calls_are_LIFO()
        {
            var harness = TestHarness<IAccount>.Create<IAccount>(url, resource);

            harness.Queryable
                .Take(10).Take(5)
                .ToList();

            // Expected behavior: the last call will be kept
            harness.WasCalledWithArguments("limit=5");
        }
    }
}
