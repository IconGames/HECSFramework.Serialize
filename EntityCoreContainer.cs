using Components;
using System.Collections.Generic;

namespace HECSFramework.Core
{
    public abstract class EntityCoreContainer : IEntityContainer
    {
        public abstract string ContainerID { get; }
        private List<IComponent> cachedComponents;
        private List<ISystem> cachedSystems;

        public List<IComponent> Components
        {
            get
            {
                cachedComponents ??= GetComponents();
                return cachedComponents;
            }
        }

        public List<ISystem> Systems
        {
            get
            {
                cachedSystems ??= GetSystems();
                return cachedSystems;
            }
        }

        protected abstract List<IComponent> GetComponents();
        protected abstract List<ISystem> GetSystems();

        public virtual void Init(IEntity entityForInit)
        {
            var entity = new Entity(ContainerID);
            entity.AddHecsComponent(new ActorContainerID { ID = ContainerID });
            foreach (var component in Components)
            {
                if (component == null)
                    continue;

                entity.AddHecsComponent(component, entity);
            }

            foreach (var system in Systems)
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