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

using MFramework.Infrastructure.ServiceLocator.Registrations.Stores;
using MFramework.Infrastructure.ServiceLocator.RegistrationTemplates;
using MFramework.Infrastructure.ServiceLocator.RegistrationTemplates.Conditional;

namespace MFramework.Infrastructure.ServiceLocator.Registrations.ConditionalInjection
{
    public class InjectionRegistration<TService> : TypedRegistration
    {
        private readonly ConditionalRegistrationTemplate conditionalRegistrationTemplate = new ConditionalRegistrationTemplate();
        private readonly ConditionalRegistrationStore conditionalRegistrationStore = new ConditionalRegistrationStore();

        public InjectionRegistration() : base(typeof (TService))
        {
        }

        public override IRegistrationStore GetRegistrationStore()
        {
            return conditionalRegistrationStore;
        }

        public override IRegistrationTemplate GetRegistrationTemplate()
        {
            return conditionalRegistrationTemplate;
        }
    }
}