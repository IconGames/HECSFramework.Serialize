using Components;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;

public partial interface IData { }

namespace HECSFramework.Core
{
    public delegate void ProcessResolverContainer(ref ResolverDataContainer dataContainerForResolving, ref IEntity entity);
    public delegate ResolverDataContainer GetContainer<T>(T component) where T : IComponent;

    public partial class ResolversMap
    {
        private Dictionary<int, IResolverProvider> resolvers;

        private GetContainer<IComponent> GetComponentContainerFunc;
        private static bool isMessagePackInited;

        /// <summary>
        /// Factory resolver data containers to components
        /// </summary>
        public Func<ResolverDataContainer, IComponent> GetComponentFromContainer { get; private set; }
        
        /// <summary>
        /// Its for resolving container when we alrdy have entity
        /// </summary>
        public ProcessResolverContainer ProcessResolverContainer { get; private set; }

        public void LoadDataFromContainer(ResolverDataContainer dataContainerForResolving, int worldIndex = 0) => LoadDataFromContainerSwitch(dataContainerForResolving, worldIndex);

        public void LoadComponentFromContainer(ResolverDataContainer resolverDataContainer, ref IEntity entity, bool checkForAvailable = false)
        {
            if (checkForAvailable)
            {
                TypesMap.GetComponentInfo(resolverDataContainer.TypeHashCode, out var mask);

                if (!entity.ContainsMask(ref mask.ComponentsMask))
                    entity.AddHecsComponent(TypesMap.GetComponentFromFactory(resolverDataContainer.TypeHashCode));
            }

            EntityManager.ResolversMap.ProcessResolverContainerRealisation(ref resolverDataContainer, ref entity);  
        }

        public ResolverDataContainer GetSystemContainer<T>(T system) where T: ISystem
        {
            return new ResolverDataContainer { Data = null, EntityGuid = system.Owner.GUID, Type = 1, TypeHashCode = system.GetTypeHashCode };
        }

        public ISystem GetSystemFromContainer(ResolverDataContainer resolverDataContainer)
        {
            return TypesMap.GetSystemFromFactory(resolverDataContainer.TypeHashCode);
        }

        public ResolverDataContainer GetComponentContainer<T>(T component) where T : IComponent => GetComponentContainerFunc(component);

        partial void LoadDataFromContainerSwitch(ResolverDataContainer dataContainerForResolving, int worldIndex);

        public IEntity GetEntityFromResolver(EntityResolver entityResolver, int worldIndex = 0)
        {
            var unpack = new UnPackEntityResolver(entityResolver);
            var actorID = unpack.Components.FirstOrDefault(x => x is ActorContainerID containerID);
            IEntity entity;

            if (actorID != null)
                entity = new Entity((actorID as ActorContainerID).ID, worldIndex);
            else
                entity = new Entity("LoadedFromResolver", worldIndex);

            entity.SetGuid(entityResolver.Guid);
            unpack.InitEntity(entity);
            return entity;
        }

        private ResolverDataContainer PackComponentToContainer<T>(T component, IData data) where T : IComponent 
        {
            return new ResolverDataContainer
            {
                Data = MessagePack.MessagePackSerializer.Serialize(component),
                EntityGuid = component.Owner.GUID,
                Type = 0,
                TypeHashCode = component.GetTypeHashCode,
            };
        }

        partial void InitPartialCommandResolvers();
    }

    [MessagePackObject, Serializable]
    public struct ResolverDataContainer : IData
    {
        /// <summary>
        /// 0 - Component, 1  - System, 2- Command
        /// </summary>
        [Key(0)]
        public int Type;
        
        [Key(1)]
        public int TypeHashCode;
        
        [Key(2)]
        public byte[] Data;
        
        [Key(3)]
        public Guid EntityGuid;

        [Key(4)]
        public bool IsSyncSelf;
    }

    public interface IResolverProvider
    {
        ResolverDataContainer GetDataContainer<T>(T data);
        void ResolveData(ResolverDataContainer data, ref IEntity entity);
    }

    public interface IResolver
    {
        void Out(ref IEntity entity);
    }

    public interface IResolver<T> : IResolver
    {
        void Out(ref T data);
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        public readonly int Queue;

        public FieldAttribute(int queue)
        {
            Queue = queue;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class GenerateResolverAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CustomResolverAttribute : Attribute
    {
    }
}