using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSResolverAttribute : Attribute
{
    public readonly Type ResolverType;
    public readonly Type ResolverProvider;

    public HECSResolverAttribute()
    {
    }

    public HECSResolverAttribute(Type resolverType)
    {
        ResolverType = resolverType;
    }

    public HECSResolverAttribute(Type resolverType, Type resolverProvider) : this(resolverType)
    {
        ResolverProvider = resolverProvider;
    }
}