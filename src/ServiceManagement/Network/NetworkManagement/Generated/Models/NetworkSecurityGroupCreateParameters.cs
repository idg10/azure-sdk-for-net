// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Linq;

namespace Microsoft.WindowsAzure.Management.Network.Models
{
    /// <summary>
    /// Parameters supplied to the Create Network Security Group operation.
    /// </summary>
    public partial class NetworkSecurityGroupCreateParameters
    {
        private string _label;
        
        /// <summary>
        /// Optional. Gets or sets description for the Network Security Group.
        /// The description can be up to 1024 characters in length.
        /// </summary>
        public string Label
        {
            get { return this._label; }
            set { this._label = value; }
        }
        
        private string _location;
        
        /// <summary>
        /// Required. Gets or sets the data center location where the Network
        /// Security Group will be created.
        /// </summary>
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }
        
        private string _name;
        
        /// <summary>
        /// Required. Gets or sets name for the Network Security Group that is
        /// unique to the subscription.
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the
        /// NetworkSecurityGroupCreateParameters class.
        /// </summary>
        public NetworkSecurityGroupCreateParameters()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the
        /// NetworkSecurityGroupCreateParameters class with required arguments.
        /// </summary>
        public NetworkSecurityGroupCreateParameters(string name, string location)
            : this()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            this.Name = name;
            this.Location = location;
        }
    }
}