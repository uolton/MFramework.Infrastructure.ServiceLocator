﻿/*   Copyright 2009 - 2010 Marcus Bratton

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
*/

using System;
using System.Collections.Generic;
using MFramework.Infrastructure.ServiceLocator.ExtensionMethods;
using MFramework.Infrastructure.ServiceLocator.InternalStorage;

namespace MFramework.Infrastructure.ServiceLocator.Resolution.Pipeline
{
    public class AdapterPipelineItem : BasePipelineItem
    {
        private readonly PostResolutionPipeline pipeline;

        public AdapterPipelineItem(Foundation foundation, IServiceLocatorAdapter serviceLocator,
                                   IServiceLocatorStore store, PostResolutionPipeline pipeline)
            : base(foundation, serviceLocator, store)
        {
            this.pipeline = pipeline;
        }

        public override PipelineResult Execute(Type requestedType, PipelineResult value)
        {
            if (value != null && value.Result != null) return value;

            var result = new PipelineResult();
            var type = requestedType;

            if (serviceLocator.HasTypeRegistered(type) || type.IsGenericType)
            {
                result.Name = value == null ? null : value.Name;
                result.MappedFrom = requestedType;
                result.MappedTo = type;

                var items = new List<IResolutionArgument>();
                
                foreach(var item in store.All<IResolutionStore>())
                {
                   if(item != null && item.Items != null && item.Items.Count > 0) items.AddRange(item.Items);
                }

                if(result.Name == null)
                {
                    result.Result = serviceLocator.GetInstance(type, items.OfType<ConstructorParameter, IResolutionArgument>());
                }
                else
                {
                    result.Result = serviceLocator.GetInstance(type, result.Name,items.OfType<ConstructorParameter, IResolutionArgument>());
                }

                result = pipeline.Execute(result);

                return result;
            }

            return null;
        }
    }
}