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
                {
                    Components.Add(component);
                    component.ComponentsMask =
                        TypesMap.GetComponentInfo(component.GetTypeHashCode, out var info) ? info.ComponentsMask : HECSMask.Empty;
                }
            }

            foreach (var s in entityResolver.Systems)
            {
                var sLoaded = EntityManager.ResolversMap.GetSystemFromContainer(s);
                
                if (sLoaded != null)
                    Systems.Add(sLoaded);
            }
        }

        public bool TryGetComponent<T>(out T component) where T : IComponent
        {
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent c = Components[i];
                if (c is T needed)
                {
                    component = needed;
                    return true;
                }
            }

            component = default;
            return false;
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