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
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Contracts;
    using Model;

    public interface PolicyResources :
        ResourceClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Policy>> GetAll(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualHost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Policy>> GetAll(Action<VirtualHostTarget> virtualHost,
                                         CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="virtualHost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Policy> Get(Action<PolicyTarget> policy, Action<VirtualHostTarget> virtualHost,
                         CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="characteristics"></param>
        /// <param name="virtualHost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ServerResponse> New(Action<PolicyTarget> policy, Action<PolicyCharacteristics> characteristics,
                                 Action<VirtualHostTarget> virtualHost,
                                 CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="virtualHost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ServerResponse> Delete(Action<PolicyTarget> policy, Action<VirtualHostTarget> virtualHost,
                                    CancellationToken cancellationToken = default(CancellationToken));
    }
}