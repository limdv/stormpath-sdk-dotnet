﻿// <copyright file="FirstAsync_tests.cs" company="Stormpath, Inc.">
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
using System.Threading.Tasks;
using Shouldly;
using Stormpath.SDK.Account;
using Stormpath.SDK.Tests.Common.Fakes;
using Xunit;

namespace Stormpath.SDK.Tests.Linq
{
    public class FirstAsync_tests : Linq_test<IAccount>
    {
        [Fact]
        public async Task Returns_first_item()
        {
            this.InitializeClientWithCollection(new List<IAccount>()
                {
                    TestAccounts.LukeSkywalker,
                    TestAccounts.HanSolo
                });

            var luke = await this.Queryable.FirstAsync();

            luke.Surname.ShouldBe("Skywalker");
        }

        [Fact]
        public async Task Limits_result_to_one_item()
        {
            this.InitializeClientWithCollection(new List<IAccount>()
                {
                    TestAccounts.LukeSkywalker,
                    TestAccounts.HanSolo
                });

            var luke = await this.Queryable.FirstAsync();

            this.ShouldBeCalledWithArguments("limit=1");
        }

        [Fact]
        public async Task Throws_when_no_items_exist()
        {
            // TODO This should be InvalidOperationException, but under Mono it throws NullReferenceException for some undetermined reason
            await Should.ThrowAsync<Exception>(async () =>
            {
                var jabba = await this.Queryable.FirstAsync();
            });
        }
    }
}
