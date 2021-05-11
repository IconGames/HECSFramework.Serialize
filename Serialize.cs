using Components;
using HECSFramework.Core;
using System;
using System.Collections.Generic;

namespace HECSFramework.Serialize
{
    public interface HECSResolver
    {
        int GetTypeHash { get; }
        public void To(IEntity entity);
    }  
    
    public interface HECSResolver<T> : HECSResolver
    {
        void InitData(T data);
    }

    public interface IEntityContainer
    {
    }

    public struct EntityDataContainer
    {
        public List<HECSResolver> ComponentsData;
        public List<int> SystemsData;
        public ActorContainerResolver ActorContainer;

        public EntityDataContainer(IEntity entity)
        {
            ComponentsData = new List<HECSResolver>(16);
            SystemsData = new List<int>(8);

            var actorContainer = entity.GetHECSComponent<ActorContainerID>();

            if (actorContainer != null)
                DataToEntitiesHelper.TryGetComponentResolver(actorContainer.GetTypeHashCode, actorContainer, out ActorContainer);
            else
                ActorContainer = default;


            foreach (var s in entity.GetAllSystems)
            {
                if (s != null)
                    SystemsData.Add(s.GetTypeHashCode);
            }

            foreach (var c in entity.GetAllComponents)
            {
                if (c != null)
                {
                    DataToEntitiesHelper.TryGetComponentResolver(c.GetTypeHashCode, c,  out HECSResolver resolver);
                    ComponentsData.Add(resolver);
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FieldAttribute : Attribute
{
    public int indexOfField;

    public FieldAttribute(int indexOfField)
    {
        this.indexOfField = indexOfField;
    }
}

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class ResolverAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class CustomResolverAttribute : Attribute
{
}