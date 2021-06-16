using System.Linq;

namespace HECSFramework.Core
{
    public static class EntitySerializeExtentions
    {
        public static IEntity CopyEntity (this IEntity entity)
        {
            var save = new EntityResolver().GetEntityResolver(entity);
            var copy = new Entity(entity.ID);
            var unpack = new UnPackEntityResolver(save);
            unpack.InitEntity(copy);
            return copy;
        }

        public static void LoadEntityFromResolver(this IEntity entity, EntityResolver entityResolver)
        {
            foreach (var c in entityResolver.Components)
                EntityManager.ResolversMap.LoadComponentFromContainer(c, ref entity, true);

            foreach (var s in entityResolver.Systems)
            {
                var newSys = EntityManager.ResolversMap.GetSystemFromContainer(s);

                if (entity.GetAllSystems.Any(x => x.GetTypeHashCode == newSys.GetTypeHashCode))
                    continue;

                entity.AddHecsSystem(newSys);
            }
        }
    }
}