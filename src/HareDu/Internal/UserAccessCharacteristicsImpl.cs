﻿// Copyright 2012-2013 Albert L. Hives, Chris Patterson, et al.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace HareDu.Internal
{
    using System.Collections.Generic;
    using Contracts;

    internal class UserAccessCharacteristicsImpl :
        UserAccessCharacteristics
    {
        public UserAccessCharacteristicsImpl()
        {
            Tags = new List<string>();
        }

        private List<string> Tags { get; set; }

        public void None()
        {
            Tags.Add(UserPermissionTag.None);
        }

        public void Administrator()
        {
            Tags.Add(UserPermissionTag.Administrator);
        }

        public void Monitoring()
        {
            Tags.Add(UserPermissionTag.Monitoring);
        }

        public void Management()
        {
            Tags.Add(UserPermissionTag.Management);
        }

        public override string ToString()
        {
            if (Tags.Contains(UserPermissionTag.None) || (Tags.Count <= 0))
                return UserPermissionTag.None;

            return string.Join(",", Tags);
        }
    }
}