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
using System.Linq;
using MFramework.Infrastructure.ServiceLocator.ExtensionMethods;
using MFramework.Infrastructure.ServiceLocator.InternalStorage;
using MFramework.Infrastructure.ServiceLocator.Registrations.Stores;
using MFramework.Infrastructure.ServiceLocator.RegistrationTemplates;
using MFramework.Infrastructure.ServiceLocator.RegistrationTemplates.Named;
using MFramework.Infrastructure.ServiceLocator.Resolution;
using MFramework.Infrastructure.ServiceLocator.Resolution.Pipeline;

namespace MFramework.Infrastructure.ServiceLocator.Registrations.Named
{
    public class NamedRegistration<TBaseService> : NamedRegistration
    {
        public NamedRegistration(string key) : base(typeof(TBaseService), key)
        {
            
        }
    }

    public class NamedRegistration : TypedRegistration, INamedRegistration
    {
        private readonly string key;
        private readonly NamedRegistrationTemplate namedRegistrationTemplate = new NamedRegistrationTemplate();
        private readonly NamedRegistrationStore namedRegistrationStore = new NamedRegistrationStore();

        public NamedRegistration(Type to, Type from, string key) : base(from)
        {
            this.key = key;
            MapsTo(to);
        }

        public NamedRegistration(Type from, string key) : base(from)
        {
            this.key = key;
        }

        public string Key
        {
            get { return key; }
        }

        public override IRegistrationStore GetRegistrationStore()
        {
            return namedRegistrationStore;
        }

        public override IRegistrationTemplate GetRegistrationTemplate()
        {
            return namedRegistrationTemplate;
        }

        protected override PipelineResult GetResult(object instance)
        {
            var result = base.GetResult(instance);

            result.Name = this.key;

            return result;
        }

        protected override IActivationStrategy GetActivationStrategy()
        {
            return new NamedActivationStrategy(this.key, this.GetMappedToType());
        }

        public override bool Equals(IRegistration registration)
        {
            if (!(registration is NamedRegistration)) return false;

            var namedRegistration = registration as NamedRegistration;

            return namedRegistration.Key == this.key && base.Equals(registration);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Key.GetHashCode();
        }

        private class NamedActivationStrategy : IActivationStrategy
        {
            private readonly string name;
            private readonly Type type;

            public NamedActivationStrategy(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }

            public object Resolve(IInstanceResolver locator, IServiceLocatorStore context)
            {
                var stores = context.All<IResolutionStore>().ToList();
                var items = new List<IResolutionArgument>();
                stores.ForEach(x => items.AddRange(x.Items));
                
                return locator.GetInstance(type, name, items.OfType<IResolutionArgument, IResolutionArgument>());
            }
        }
    }
}