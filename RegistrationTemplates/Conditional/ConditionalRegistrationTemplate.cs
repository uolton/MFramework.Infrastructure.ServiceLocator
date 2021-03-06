/*   Copyright 2009 - 2010 Marcus Bratton

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

using MFramework.Infrastructure.ServiceLocator.InternalStorage;
using MFramework.Infrastructure.ServiceLocator.Registrations;
using MFramework.Infrastructure.ServiceLocator.Resolution.Pipeline;

namespace MFramework.Infrastructure.ServiceLocator.RegistrationTemplates.Conditional
{
    public class ConditionalRegistrationTemplate : AbstractRegistrationTemplate
    {
        public override void Register(IServiceLocatorAdapter adapter, IServiceLocatorStore store, IRegistration registration, ResolutionPipeline pipeline)
        {
            var mappedFromType = registration.GetMappedFromType();
            var mappedToType = registration.GetMappedToType();

            adapter.RegisterFactoryMethod(mappedFromType, () => pipeline.Execute(mappedFromType));
			RegisterLazy(adapter, mappedFromType, pipeline);

			RegisterContextual(adapter, mappedFromType);

            if (!mappedToType.IsInterface)
            {
                adapter.Register(mappedToType, mappedToType);
                RegisterLazy(adapter, mappedToType, pipeline);
				RegisterContextual(adapter, mappedToType);
            }
        }
    }
}