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

using System;
using MFramework.Infrastructure.ServiceLocator.InternalStorage;
using MFramework.Infrastructure.ServiceLocator.Registrations.Stores;
using MFramework.Infrastructure.ServiceLocator.RegistrationTemplates;
using MFramework.Infrastructure.ServiceLocator.Resolution.Pipeline;

namespace MFramework.Infrastructure.ServiceLocator.Registrations
{
    public interface IRegistration : IEquatable<IRegistration>
    {
        void MapsTo(object implementationType);
        object GetMappedTo();
        Type GetMappedToType();
        Type GetMappedFromType();
        IRegistrationStore GetRegistrationStore();
        IRegistrationTemplate GetRegistrationTemplate();
        object ResolveWith(IInstanceResolver locator, IServiceLocatorStore context, PostResolutionPipeline pipeline);
        bool IsValid(IInstanceResolver locator, IServiceLocatorStore context);
    }
}