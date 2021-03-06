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

namespace HareDu.Tests
{
    using System;
    using System.Net;
    using NUnit.Framework;
    using Resources;

    [TestFixture]
    public class PermissionsTests :
        HareDuTestBase
    {
        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Create_User_Persmissions()
        {
            var response = Client
                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Permissions
                .Set(x => x.User(string.Format("{0}1", Settings.Default.Username)),
                     x =>
                         {
                             x.Configure(".*");
                             x.Read(".*");
                             x.Write(".*");
                         },
                     x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Delete_User_Permissions()
        {
            var response = Client
                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Permissions
                .Delete(x => x.User(Settings.Default.Username), x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_All_User_Permissions()
        {
            var data = Client
                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Permissions
                .GetAll()
                .Data();

            foreach (var permission in data)
            {
                Console.WriteLine("Virtual Host: {0}", permission.VirtualHost);
                Console.WriteLine("User: {0}", permission.User);
                Console.WriteLine("Configure Permissions: {0}", permission.Configure);
                Console.WriteLine("Read Permissions: {0}", permission.Read);
                Console.WriteLine("Write Permissions: {0}", permission.Write);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test, Category("Integration"), Explicit]
        public void Verify_Can_Return_User_Permissions()
        {
            var data = Client
                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Permissions
                .Get(x => x.User(Settings.Default.Username), x => x.VirtualHost(Settings.Default.VirtualHost))
                .Data();

            Console.WriteLine("Virtual Host: {0}", data.VirtualHost);
            Console.WriteLine("User: {0}", data.User);
            Console.WriteLine("Configure Permissions: {0}", data.Configure);
            Console.WriteLine("Read Permissions: {0}", data.Read);
            Console.WriteLine("Write Permissions: {0}", data.Write);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }

        [Test, Category("Integration"), Explicit]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Verify_Throws_Exception_When_Permissions_Null()
        {
            var response = Client
                .Factory<UserResources>(
                    x => x.Credentials(Settings.Default.LoginUsername, Settings.Default.LoginPassword))
                .Permissions
                .Set(x => x.User(Settings.Default.Username), null, x => x.VirtualHost(Settings.Default.VirtualHost))
                .Response();
        }
    }
}