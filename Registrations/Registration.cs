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
using MFramework.Infrastructure.ServiceLocator.ResolutionRules;

namespace MFramework.Infrastructure.ServiceLocator.Registrations
{
    public abstract class Registration : IRegistration
    {
        protected abstract IActivationStrategy GetActivationStrategy();
        protected IActivationRule rule;
        public abstract IRegistrationStore GetRegistrationStore();
        public abstract IRegistrationTemplate GetRegistrationTemplate();
        public abstract Type GetMappedFromType();
        public abstract object GetMappedTo();
        public abstract Type GetMappedToType();
        public abstract void MapsTo(object target);

        public void SetActivationRule(IActivationRule rule)
        {
            this.rule = rule;
        }

        public virtual bool IsValid(IInstanceResolver locator, IServiceLocatorStore context)
        {
            return rule != null && rule.GetRuleEvaluationStrategy().IsValid(rule, locator, context);
        }

        public virtual object ResolveWith(IInstanceResolver locator, IServiceLocatorStore context, PostResolutionPipeline pipeline)
        {
            object instance = null;

            instance = GetActivationStrategy().Resolve(locator, context);

            if(pipeline != null)
            {
                var result = GetResult(instance);

                pipeline.Execute(result);
            }

            return instance;
        }

        protected virtual PipelineResult GetResult(object instance)
        {
            return new PipelineResult
                       {
                           MappedTo = GetMappedToType(),
                           MappedFrom = GetMappedFromType(),
                           Result = instance
                       };
        }

        public virtual bool Equals(IRegistration registration)
        {
            return registration.GetType() == this.GetType() && 
                   registration.GetMappedFromType() == this.GetMappedFromType() &&
                   registration.GetMappedToType() == this.GetMappedToType();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.GetMappedFromType().GetHashCode() ^
                   this.GetMappedToType().GetHashCode();
        }
    }
}