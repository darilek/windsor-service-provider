/*
 * Copyright 2020 Lukas Tines <ltines@euroffice.co.uk>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
 
/*
 * heavily based on https://github.com/volosoft/castle-windsor-ms-adapter/blob/master/src/Castle.Windsor.MsDependencyInjection/MsCompatibleCollectionResolver.cs
 * Copyright (c) 2016 Volosoft
 */


using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace WindsorServiceProvider
{
    public class NetCoreCollectionResolver : CollectionResolver
    {
        public NetCoreCollectionResolver(IKernel kernel, bool allowEmptyCollections = true) : base(kernel, allowEmptyCollections)
        {
        }

        public override bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model,
            DependencyModel dependency)
        {
            if (kernel.HasComponent(dependency.TargetItemType))
            {
                return false;
            }

            return base.CanResolve(context, contextHandlerResolver, model, dependency);
        }

        public override object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model,
            DependencyModel dependency)
        {
            Array items = base.Resolve(context, contextHandlerResolver, model, dependency) as Array;
            if(items != null)
                Array.Reverse(items);

            return items;
        }
    }
}