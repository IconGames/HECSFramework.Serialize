using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSResolverAttribute : Attribute
{
    public readonly Type ResolverType;

    public HECSResolverAttribute()
    {
    }

    public HECSResolverAttribute(Type resolverType)
    {
        ResolverType = resolverType;
    }
}