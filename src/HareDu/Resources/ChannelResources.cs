﻿// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
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

namespace HareDu.Resources
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface ChannelResources
    {
        /// <summary>
        /// Returns all the information pertaining to the open channels.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Channel>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the information pertaining to a specific channel.
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Channel> Get(string channelName, CancellationToken cancellationToken = default(CancellationToken));
    }
}