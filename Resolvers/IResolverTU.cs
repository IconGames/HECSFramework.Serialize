
namespace HECSFramework.Serialize
{
    /// <summary>
    /// this interface should be using for custom resolvers,
    /// its just for consistent signature same as codogen 
    /// </summary>
    /// <typeparam name="T">ResolverType</typeparam>
    /// <typeparam name="U">TypeForResolving</typeparam>
    public interface IResolver<T, U> where T : IResolver
    {
        T In(ref U data);
    }
}