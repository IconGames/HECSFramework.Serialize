﻿using MessagePack;
using System;
using System.Collections.Generic;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public struct EntityResolver
    {
        [Key(0)]
        public List<ResolverDataContainer> Systems;

        [Key(1)]
        public List<ResolverDataContainer> Components;

        [Key(2)]
        public Guid Guid;

        public EntityResolver GetEntityResolver(IEntity entity)
        {
            Systems = new List<ResolverDataContainer>(32);
            Components = new List<ResolverDataContainer>(32);
            Guid = entity.GUID;

            foreach (var c in entity.GetAllComponents)
            {
                if (c == null)
                    continue;

                Components.Add(EntityManager.ResolversMap.GetComponentContainer(c));
            }

            foreach (var s in entity.GetAllSystems)
                if (s != null)
                    Systems.Add(EntityManager.ResolversMap.GetSystemContainer(s));

            return this;
        }
    }
}
