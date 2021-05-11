using HECSFramework.Serialize;

namespace HECSFramework.Core
{
    public static partial class DataToEntitiesHelper
    {
        private static ResolverTypesMap resolverMaps = new ResolverTypesMap();

        public static bool TryGetComponentResolver<T, U>(int typeCode, T component, out U resolver) where T : IComponent where U: HECSResolver
            => resolverMaps.TryGetComponentResolver(typeCode, component, out resolver);
    }
}