﻿// <copyright file="StubRequestExecutor.cs" company="Stormpath, Inc.">
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

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Stormpath.SDK.Api;
using Stormpath.SDK.Http;
using Stormpath.SDK.Impl.Http;
using Stormpath.SDK.Tests.Common.Fakes;

namespace Stormpath.SDK.Tests.Fakes
{
    public class StubRequestExecutor
    {
        private readonly IRequestExecutor fakeRequestExecutor;
        private readonly string resourceJson;

        public StubRequestExecutor(string resourceJson, IClientApiKey clientApiKey = null)
        {
            this.resourceJson = resourceJson;
            this.fakeRequestExecutor = Substitute.For<IRequestExecutor>();

            // API Key
            this.fakeRequestExecutor.ApiKey.Returns(clientApiKey);

            // All GETs return 200 OK
            this.fakeRequestExecutor
                .ExecuteAsync(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Get), Arg.Any<CancellationToken>())
                .Returns(
                    Task.FromResult(new FakeHttpResponse(200, "OK", new HttpHeaders(), resourceJson, "application/json", transportError: false) as IHttpResponse));
            this.fakeRequestExecutor
                .Execute(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Get))
                .Returns(new FakeHttpResponse(200, "OK", new HttpHeaders(), resourceJson, "application/json", transportError: false));

            // All POSTs return 201 Created
            this.fakeRequestExecutor
                .ExecuteAsync(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Post), Arg.Any<CancellationToken>())
                .Returns(
                    Task.FromResult(new FakeHttpResponse(201, "Created", new HttpHeaders(), resourceJson, "application/json", transportError: false) as IHttpResponse));
            this.fakeRequestExecutor
                .Execute(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Post))
                .Returns(new FakeHttpResponse(201, "Created", new HttpHeaders(), resourceJson, "application/json", transportError: false));

            // All DELETEs return 204 No Content
            this.fakeRequestExecutor
                .ExecuteAsync(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Delete), Arg.Any<CancellationToken>())
                .Returns(
                    Task.FromResult(new FakeHttpResponse(204, "No Content", new HttpHeaders(), null, null, transportError: false) as IHttpResponse));
            this.fakeRequestExecutor
                .Execute(
                    Arg.Is<IHttpRequest>(req => req.Method == HttpMethod.Delete))
                .Returns(new FakeHttpResponse(204, "No Content", new HttpHeaders(), null, null, transportError: false));
        }

        internal IRequestExecutor Object => this.fakeRequestExecutor;
    }
}
