// Copyright 2012-2013 Albert L. Hives, Chris Patterson, et al.
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

namespace HareDu.Contracts
{
    public interface Arguments
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="value"></param>
        void Set<T>(string arg, T value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        void SetPerQueueExpiration(int milliseconds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        void SetQueueExpiration(int milliseconds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        void SetDeadLetterExchange(string exchange);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingKey"></param>
        void SetDeadLetterExchangeRoutingKey(string routingKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        void SetAlternateExchange(string exchange);
    }
}