using HECSFramework.Serialize;

namespace HECSFramework.Core
{
    public static partial class DataHelper
    {
        private static ResolverTypesMap resolverMaps = new ResolverTypesMap();

        public static bool TryGetComponentResolver<T, U>(int typeCode, T component, out U resolver) where T : IComponent where U: HECSResolver
            => resolverMaps.TryGetComponentResolver(typeCode, component, out resolver);

        public static IEntity GetEntityCopy(IEntity entity)
        {
            var newEntity = new Entity(entity.ID, entity.WorldId);

            foreach (var c in entity.GetAllComponents)
            {
                if (c == null)
                    continue;

                var newComp = TypesMap.GetComponentFromFactory(c.GetTypeHashCode);
                newEntity.AddHecsComponent(newComp);

                if (TryGetComponentResolver(c.GetTypeHashCode, c, out HECSResolver hECSResolver))
                    hECSResolver.To(newEntity);
            }

            foreach (var s in entity.GetAllSystems)
            {
                var newSys = TypesMap.GetSystemFromFactory(s.GetTypeHashCode);
                newEntity.AddHecsSystem(newSys);
            }

            return newEntity;
        }
    }
}