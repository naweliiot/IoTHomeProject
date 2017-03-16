﻿//using Microsoft.Framework.Configuration;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Table;
//using System.Threading.Tasks;
//using LogLevel = Microsoft.Framework.Logging.LogLevel;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace vunvulea_iot_core
{
    public class SystemStatus
    {
        private const string systemStatysTablename = "systemstatus";
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableClient;
        private readonly CloudTable systemStatusTable;

        public SystemStatus(string storageConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=vunvuleariotstorage;AccountKey=GmmafAcfKQBw3O+7l8xtGdtYt5/PnkIGHmJHy7YkD26VYrmQkrvzENGfBzxuudmFrMmyKf1gt7+vXQA80kemKw==;")
        {
            storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            systemStatusTable = tableClient.GetTableReference(systemStatysTablename);
        }

        public async Task<string> GetCurrentSystemStatusAsync(SystemStatusType systemStatusType)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<SystemStatusEntity>(
                SystemStatusEntity.PartitionKeyValue,
                Enum.GetName(typeof(SystemStatusType), systemStatusType).ToLower());

            TableResult retrievedResult = await systemStatusTable.ExecuteAsync(retrieveOperation);
            SystemStatusEntity retrivedEntity = (SystemStatusEntity)retrievedResult.Result;

            return retrivedEntity.Status;
        }
    }
}
