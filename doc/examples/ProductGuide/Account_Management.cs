﻿// <copyright file="Account_Management.cs" company="Stormpath, Inc.">
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

using System.Threading.Tasks;
using Stormpath.SDK.Client;
using Stormpath.SDK.Directory;

namespace DocExamples.ProductGuide
{
    public class Account_Management
    {
        IClient client = null;

        public async Task CreateDirectory()
        {
            #region create_cloud_directory.cs
            var captainsDirectory = await client.CreateDirectoryAsync(
                "Captains", 
                "Captains from a variety of stories",
                DirectoryStatus.Enabled);
            #endregion
        }
    }
}
