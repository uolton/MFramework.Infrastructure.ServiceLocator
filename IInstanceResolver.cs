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
using MFramework.Infrastructure.ServiceLocator.Resolution;

namespace MFramework.Infrastructure.ServiceLocator
{
    public interface IInstanceResolver
	{
		object GetInstance(Type type, string key, params IResolutionArgument[] parameters);
        object GetInstance(Type type, params IResolutionArgument[] parameters);
        TService GetInstance<TService>(Type type, params IResolutionArgument[] arguments);
        TService GetInstance<TService>(string key, params IResolutionArgument[] arguments);
        TService GetInstance<TService>(params IResolutionArgument[] arguments);
        bool HasTypeRegistered(Type type);
    }
}