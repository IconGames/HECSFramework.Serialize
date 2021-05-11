using Components;
using HECSFramework.Core;
using System.Collections.Generic;

namespace HECSFramework.Serialize
{
    public partial class ResolverTypesMap
    {
        private Dictionary<int, object> resolvers;

        public bool TryGetComponentResolver<T, U>(int typeCode, T component, out U resolver) where T : IComponent where U: HECSResolver
        {
            if (resolvers.TryGetValue(typeCode, out object value))
            {
                var needed = (HECSResolver<T>)value;
                needed.InitData(component);
                resolver = (U)needed;
                return true;
            }
            else
            {
                resolver = default;
                return false;
            }
        }
    }

    public partial struct ActorContainerResolver : HECSResolver<ActorContainerID>
    {
        public string ContainerName;

        public int GetTypeHash { get; }

        public void InitData(ActorContainerID data)
        {
            ContainerName = data.ID;
        }

        public void To(IEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
