using HECSFramework.Core;


public interface IResolverProvider
{
    ResolverDataContainer GetDataContainer<T>(T data);
    void ResolveData(ResolverDataContainer data, ref IEntity entity);
}
