﻿// <copyright file="DefaultResourceFactory_tests.cs" company="Stormpath, Inc.">
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
using NSubstitute;
using Shouldly;
using Stormpath.SDK.Account;
using Stormpath.SDK.Impl.Account;
using Stormpath.SDK.Impl.DataStore;
using Stormpath.SDK.Resource;
using Xunit;

namespace Stormpath.SDK.Tests.Impl
{
    public class DefaultResourceFactory_tests
    {
        private readonly IDataStore fakeDataStore;
        private readonly IResourceFactory factory;

        public DefaultResourceFactory_tests()
        {
            fakeDataStore = Substitute.For<IDataStore>();

            factory = new DefaultResourceFactory(fakeDataStore);
        }

        [Fact]
        public void Should_throw_for_unsupported_type()
        {
            Should.Throw<ApplicationException>(() =>
            {
                var bad = factory.Instantiate<IResource>();
            });
        }

        [Fact]
        public void Creating_IAccount_returns_DefaultAccount()
        {
            IAccount account = factory.Instantiate<IAccount>();
            account.ShouldBeOfType<DefaultAccount>();
        }
    }
}
