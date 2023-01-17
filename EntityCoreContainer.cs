using Components;
using System.Collections.Generic;

namespace HECSFramework.Core
{
    public abstract class EntityCoreContainer : IEntityContainer
    {
        public abstract string ContainerID { get; }
        public abstract List<IComponent> GetComponents();
        public abstract List<ISystem> GetSystems();

        public virtual void Init(IEntity entityForInit)
        {
            var entity = new Entity(ContainerID);
            entity.AddComponent(new ActorContainerID { ID = ContainerID });

            var components = GetComponents();
            var systems = GetSystems();

            foreach (var component in components)
            {
                if (component == null)
                    continue;

                entity.AddComponent(component);
            }

            foreach (var system in systems)
            {
                if (system == null)
                    continue;

                entity.AddHecsSystem(system, entity);
            }

            var resolver = new EntityResolver().GetEntityResolver(entity);
            entityForInit.LoadEntityFromResolver(resolver);
        }
    }
}