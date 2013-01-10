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

namespace HareDu
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    internal class NodeClientImpl :
        HareDuClientBase,
        NodeClient
    {
        public NodeClientImpl(ClientInitParamsImpl args) : base(args)
        {
        }

        public Task<IEnumerable<Node>> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            LogInfo("Sent request to return all information pertaining to all nodes on RabbitMQ cluster.");

            string url = "api/nodes";

            return base.Get(url, cancellationToken).Response<IEnumerable<Node>>(cancellationToken);
        }

        public Task<Node> Get(string nodeName, CancellationToken cancellationToken = new CancellationToken())
        {
            Arg.Validate(nodeName, "nodeName",
                         () =>
                         LogError(
                             "GetNodeOnCluster method threw an ArgumentNullException exception because node name was invalid (i.e. empty, null, or all whitespaces)"));

            LogInfo("Sent request to return all information pertaining to all nodes on RabbitMQ cluster.");

            string url = string.Format("api/nodes/{0}", nodeName);

            return base.Get(url, cancellationToken).Response<Node>(cancellationToken);
        }
    }
}