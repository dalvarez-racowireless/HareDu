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

namespace HareDu.Concerns
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Common.Logging;
    using Contracts;
    using Internal;
    using Model;

    internal class QueueClientImpl :
        HareDuResourcesBase,
        QueueClient
    {
        public QueueClientImpl(HttpClient client, ILog logger) :
            base(client, logger)
        {
            Binding = new QueueBindingClientImpl(client, logger);
        }

        public QueueBindingClient Binding { get; private set; }

        public Task<IEnumerable<Queue>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            LogInfo(
                "Sent request to return all information on all queues on all virtual hosts on current RabbitMQ server.");

            string url = "api/queues";

            return base.Get(url, cancellationToken).As<IEnumerable<Queue>>(cancellationToken);
        }

        public Task<ServerResponse> New(string queue, Action<QueueTarget> target, Action<QueueBehavior> behavior,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var argsImpl = new QueueBehaviorImpl();
            behavior(argsImpl);

            var targetImpl = new QueueTargetImpl();
            target(targetImpl);

            string url = string.Format("api/queues/{0}/{1}", targetImpl.VirtualHost.SanitizeVirtualHostName(), queue);

            LogInfo(string.IsNullOrEmpty(argsImpl.Node)
                        ? string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}'.", queue,
                            targetImpl.VirtualHost)
                        : string.Format(
                            "Sent request to RabbitMQ server to create queue '{0}' on virtual host '{1}' on node '{2}'.",
                            queue, targetImpl.VirtualHost, argsImpl.Node));

            return base.Put(url, argsImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(Action<QueueTarget> target, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new QueueTargetImpl();
            target(targetImpl);

            string url = string.Format("api/queues/{0}/{1}", targetImpl.VirtualHost.SanitizeVirtualHostName(), targetImpl.Queue);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete queue '{0}' from virtual host '{1}'.",
                                  targetImpl.Queue, targetImpl.VirtualHost));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Purge(Action<QueueTarget> target, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            var targetImpl = new QueueTargetImpl();
            target(targetImpl);

            string url = string.Format("api/queues/{0}/{1}/contents", targetImpl.VirtualHost.SanitizeVirtualHostName(), targetImpl.Queue);

            LogInfo(string.Format("Sent request to RabbitMQ server to purge the contents of queue '{0}'.", targetImpl.Queue));

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }

    public interface QueueTarget
    {
        void Source(string queue, string virtualHost);
        void Source(string virtualHost);
    }

    internal class QueueTargetImpl : QueueTarget
    {
        public string Queue { get; private set; }

        public string VirtualHost { get; private set; }
        public void Source(string queue, string virtualHost)
        {
            Queue = queue;
            VirtualHost = virtualHost;
        }
        public void Source(string virtualHost)
        {
            VirtualHost = virtualHost;
        }
    }
}