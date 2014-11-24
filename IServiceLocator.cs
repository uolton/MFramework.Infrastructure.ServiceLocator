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
using System.Collections.Generic;
using MFramework.Infrastructure.ServiceLocator.InternalStorage;
using MFramework.Infrastructure.ServiceLocator.RegistrationPolicies;
using MFramework.Infrastructure.ServiceLocator.Registrations;

namespace MFramework.Infrastructure.ServiceLocator
{
    public interface IServiceLocator : IDisposable, IInstanceResolver, Microsoft.Practices.ServiceLocation.IServiceLocator
    {
        IServiceLocator Register(Action<IServiceLocator> serviceLocator);
        IServiceLocator Register(List<IRegistration> registration);
        IServiceLocator Register(IRegistration registration);
        IServiceLocator Register<TRegistrationPolicy>(List<IRegistration> registration) where TRegistrationPolicy : IRegistrationPolicy;
        IServiceLocator Register<TRegistrationPolicy>(IRegistration registration) where TRegistrationPolicy : IRegistrationPolicy;
        new object GetInstance(Type type);
        new object GetInstance(Type type, string key);
        void AddContext(object contextItem);
        IServiceLocatorStore Store { get; }
    }
}