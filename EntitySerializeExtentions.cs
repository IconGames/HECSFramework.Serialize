using Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static void LoadEntityFromResolver(this IEntity entity, EntityResolver entityResolver, bool addComponent = true)
        {
            foreach (var c in entityResolver.Components)
            {
                var componentResolver = c;
                EntityManager.ResolversMap.LoadComponentFromContainer(ref componentResolver, ref entity, addComponent);
            }

            foreach (var s in entityResolver.Systems)
            {
                var newSys = EntityManager.ResolversMap.GetSystemFromContainer(s);

                if (entity.GetAllSystems.Any(x => x.GetTypeHashCode == newSys.GetTypeHashCode))
                    continue;

                entity.AddHecsSystem(newSys);
            }

            entity.SetGuid(entityResolver.Guid);
        }

        public static IEntity GetEntityFromResolver(this EntityResolver entityResolver, int worldIndex = 0, bool addComponent = true)
        {
            var data = new UnPackEntityResolver(entityResolver);
            var id = data.Components.FirstOrDefault(x => x is ActorContainerID) as ActorContainerID;

            Entity entity;
            
            if (id != null)
                entity = new Entity(id.ID, worldIndex);
            else
                entity = new Entity(entityResolver.Guid.ToString());

            data.InitEntity(entity);
            entity.SetGuid(entityResolver.Guid);

            return entity;
        }

        public static Task LoadEntityFromResolver(this IEntity entity, EntityResolver entityResolver)
        {
            foreach (var c in entityResolver.Components)
            {
                var componentResolver = c;
                EntityManager.ResolversMap.LoadComponentFromContainer(ref componentResolver, ref entity, true);
            }
                
            foreach (var s in entityResolver.Systems)
            {
                var newSys = EntityManager.ResolversMap.GetSystemFromContainer(s);

                if (entity.GetAllSystems.Any(x => x.GetTypeHashCode == newSys.GetTypeHashCode))
                    continue;

                entity.AddHecsSystem(newSys);
            }

            return Task.CompletedTask;
        }

        public static List<IEntity> GetEntitiesFromResolvers(this List<EntityResolver> entitiesResolvers, int worldIndex = 0)
        {
            var list = new List<IEntity>(entitiesResolvers.Count);

            foreach (var e in entitiesResolvers)
                list.Add(EntityManager.ResolversMap.GetEntityFromResolver(e, worldIndex));

            return list;
        }

        public static List<EntityResolver> GetEntityResolvers(this List<IEntity> entities)
        {
            var list = new List<EntityResolver>(entities.Count);

            foreach (var e in entities)
                list.Add(new EntityResolver().GetEntityResolver(e));

            return list;
        }
    }
}