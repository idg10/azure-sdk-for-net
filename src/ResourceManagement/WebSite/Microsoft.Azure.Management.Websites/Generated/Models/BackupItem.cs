// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.14.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.WebSites.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Backup description
    /// </summary>
    public partial class BackupItem : Resource
    {
        /// <summary>
        /// Initializes a new instance of the BackupItem class.
        /// </summary>
        public BackupItem() { }

        /// <summary>
        /// Initializes a new instance of the BackupItem class.
        /// </summary>
        public BackupItem(string location, string id = default(string), string name = default(string), string type = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), int? backupItemId = default(int?), string storageAccountUrl = default(string), string blobName = default(string), string backupItemName = default(string), BackupItemStatus? status = default(BackupItemStatus?), long? sizeInBytes = default(long?), DateTime? created = default(DateTime?), string log = default(string), IList<DatabaseBackupSetting> databases = default(IList<DatabaseBackupSetting>), bool? scheduled = default(bool?), DateTime? lastRestoreTimeStamp = default(DateTime?), DateTime? finishedTimeStamp = default(DateTime?), string correlationId = default(string), long? websiteSizeInBytes = default(long?))
            : base(location, id, name, type, tags)
        {
            BackupItemId = backupItemId;
            StorageAccountUrl = storageAccountUrl;
            BlobName = blobName;
            BackupItemName = backupItemName;
            Status = status;
            SizeInBytes = sizeInBytes;
            Created = created;
            Log = log;
            Databases = databases;
            Scheduled = scheduled;
            LastRestoreTimeStamp = lastRestoreTimeStamp;
            FinishedTimeStamp = finishedTimeStamp;
            CorrelationId = correlationId;
            WebsiteSizeInBytes = websiteSizeInBytes;
        }

        /// <summary>
        /// Id of the backup.
        /// </summary>
        [JsonProperty(PropertyName = "properties.id")]
        public int? BackupItemId { get; set; }

        /// <summary>
        /// SAS URL for the storage account container which contains this
        /// backup
        /// </summary>
        [JsonProperty(PropertyName = "properties.storageAccountUrl")]
        public string StorageAccountUrl { get; set; }

        /// <summary>
        /// Name of the blob which contains data for this backup
        /// </summary>
        [JsonProperty(PropertyName = "properties.blobName")]
        public string BlobName { get; set; }

        /// <summary>
        /// Name of this backup
        /// </summary>
        [JsonProperty(PropertyName = "properties.name")]
        public string BackupItemName { get; set; }

        /// <summary>
        /// Backup status. Possible values for this property include:
        /// 'InProgress', 'Failed', 'Succeeded', 'TimedOut', 'Created',
        /// 'Skipped', 'PartiallySucceeded', 'DeleteInProgress',
        /// 'DeleteFailed', 'Deleted'.
        /// </summary>
        [JsonProperty(PropertyName = "properties.status")]
        public BackupItemStatus? Status { get; set; }

        /// <summary>
        /// Size of the backup in bytes
        /// </summary>
        [JsonProperty(PropertyName = "properties.sizeInBytes")]
        public long? SizeInBytes { get; set; }

        /// <summary>
        /// Timestamp of the backup creation
        /// </summary>
        [JsonProperty(PropertyName = "properties.created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Details regarding this backup. Might contain an error message.
        /// </summary>
        [JsonProperty(PropertyName = "properties.log")]
        public string Log { get; set; }

        /// <summary>
        /// List of databases included in the backup
        /// </summary>
        [JsonProperty(PropertyName = "properties.databases")]
        public IList<DatabaseBackupSetting> Databases { get; set; }

        /// <summary>
        /// True if this backup has been created due to a schedule being
        /// triggered.
        /// </summary>
        [JsonProperty(PropertyName = "properties.scheduled")]
        public bool? Scheduled { get; set; }

        /// <summary>
        /// Timestamp of a last restore operation which used this backup.
        /// </summary>
        [JsonProperty(PropertyName = "properties.lastRestoreTimeStamp")]
        public DateTime? LastRestoreTimeStamp { get; set; }

        /// <summary>
        /// Timestamp when this backup finished.
        /// </summary>
        [JsonProperty(PropertyName = "properties.finishedTimeStamp")]
        public DateTime? FinishedTimeStamp { get; set; }

        /// <summary>
        /// Unique correlation identifier. Please use this along with the
        /// timestamp while communicating with Azure support.
        /// </summary>
        [JsonProperty(PropertyName = "properties.correlationId")]
        public string CorrelationId { get; set; }

        /// <summary>
        /// Size of the original web app which has been backed up
        /// </summary>
        [JsonProperty(PropertyName = "properties.websiteSizeInBytes")]
        public long? WebsiteSizeInBytes { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
