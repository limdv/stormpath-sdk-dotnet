﻿// <copyright file="DefaultIdSiteSyncCallbackHandler_tests.cs" company="Stormpath, Inc.">
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
using NSubstitute;
using Shouldly;
using Stormpath.SDK.Api;
using Stormpath.SDK.Cache;
using Stormpath.SDK.Http;
using Stormpath.SDK.IdSite;
using Stormpath.SDK.Impl.DataStore;
using Stormpath.SDK.Impl.Http;
using Stormpath.SDK.Impl.IdSite;
using Stormpath.SDK.Tests.Helpers;
using Xunit;

namespace Stormpath.SDK.Tests.IdSite
{
    public class DefaultIdSiteSyncCallbackHandler_tests : IDisposable
    {
        private IInternalDataStore dataStore;

        public void Dispose()
        {
            this.dataStore.Dispose();
        }

        [Theory]
        [MemberData(nameof(CallbackHandlerTests.TestCases), MemberType = typeof(CallbackHandlerTests))]
        public void Handle_response(string id_, string jwtResponse, string expectedStatus, bool isNewAccount, string expectedState)
        {
            IAccountResult accountResultFromListener = null;

            var listener = new InlineIdSiteSyncResultListener(
                onAuthenticated: result =>
                {
                    if (expectedStatus == IdSiteResultStatus.Authenticated)
                    {
                        accountResultFromListener = result;
                    }
                    else
                    {
                        throw new InvalidOperationException("This method should not have been executed");
                    }
                },
                onLogout: result =>
                {
                    if (expectedStatus == IdSiteResultStatus.Logout)
                    {
                        accountResultFromListener = result;
                    }
                    else
                    {
                        throw new InvalidOperationException("This method should not have been executed");
                    }
                },
                onRegistered: result =>
                {
                    if (expectedStatus == IdSiteResultStatus.Registered)
                    {
                        accountResultFromListener = result;
                    }
                    else
                    {
                        throw new InvalidOperationException("This method should not have been executed");
                    }
                });

            var testApiKey = ClientApiKeys.Builder().SetId("2EV70AHRTYF0JOA7OEFO3SM29").SetSecret("goPUHQMkS4dlKwl5wtbNd91I+UrRehCsEDJrIrMruK8").Build();
            var fakeRequestExecutor = Substitute.For<IRequestExecutor>();
            fakeRequestExecutor.ApiKey.Returns(testApiKey);

            this.dataStore = TestDataStore.Create(fakeRequestExecutor, CacheProviders.Create().InMemoryCache().Build());

            var request = new DefaultHttpRequest(HttpMethod.Get, new CanonicalUri($"https://foo.bar?{IdSiteClaims.JwtResponse}={jwtResponse}"));

            IIdSiteSyncCallbackHandler callbackHandler = new DefaultIdSiteSyncCallbackHandler(this.dataStore, request);
            callbackHandler.SetResultListener(listener);

            var accountResult = callbackHandler.GetAccountResult();

            // Validate result
            (accountResult as DefaultAccountResult).Account.Href.ShouldBe("https://api.stormpath.com/v1/accounts/7Ora8KfVDEIQP38KzrYdAs");
            (accountResultFromListener as DefaultAccountResult).Account.Href.ShouldBe("https://api.stormpath.com/v1/accounts/7Ora8KfVDEIQP38KzrYdAs");

            accountResult.IsNewAccount.ShouldBe(isNewAccount);
            accountResultFromListener.IsNewAccount.ShouldBe(isNewAccount);

            accountResult.State.ShouldBe(expectedState);
            accountResultFromListener.State.ShouldBe(expectedState);

            var expectedResultStatus = IdSiteResultStatus.Parse(expectedStatus);
            accountResult.Status.ShouldBe(expectedResultStatus);
            accountResultFromListener.Status.ShouldBe(expectedResultStatus);
        }
    }
}
