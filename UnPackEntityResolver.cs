using System.Collections.Generic;

namespace HECSFramework.Core
{
    public struct UnPackEntityResolver
    {
        public List<IComponent> Components;
        public List<ISystem> Systems;

        public UnPackEntityResolver(EntityResolver entityResolver)
        {
            Components = new List<IComponent>(16);
            Systems = new List<ISystem>(6);
            var resolverMap = EntityManager.ResolversMap;

            foreach (var c in entityResolver.Components)
            {
                var component = EntityManager.ResolversMap.GetComponentFromContainer(c);

                if (component != null)
                    Components.Add(component);
            }

            foreach (var s in entityResolver.Systems)
            {
                var sLoaded = EntityManager.ResolversMap.GetSystemFromContainer(s);
                
                if (sLoaded != null)
                    Systems.Add(sLoaded);
            }
        }

        public void InitEntity(IEntity entity)
        {
            foreach (var c in Components)
            {
                if (c != null)
                    entity.AddHecsComponent(c);
            }

            foreach (var s in Systems)
            {
                if (s != null)
                    entity.AddHecsSystem(s);
            }
        }
    }
}