// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace back_end_application
{
    class BackEndApplication
    {
        private static ServiceClient s_serviceClient;
        private readonly static string s_myDeviceId = "";
        private readonly static string s_connectionString = "";

        private static async Task InvokeMethod()
        {
            var methodInvocation = new CloudToDeviceMethod("SetTelemetryInterval") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("10");
            var response = await s_serviceClient.InvokeDeviceMethodAsync(s_myDeviceId, methodInvocation);
            Console.WriteLine("Response status: {0}, payload:", response.Status);
            Console.WriteLine(response.GetPayloadAsJson());
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("IoT Cloud to Device - Simulated device.\n");
            s_serviceClient = ServiceClient.CreateFromConnectionString(s_connectionString);
            InvokeMethod().GetAwaiter().GetResult();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}