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

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class PrivateFieldsIncludedAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSManualResolverAttribute : Attribute
{
    /// <summary>
    /// here we need to mention of type we should be resolved
    /// </summary>
    public Type ResolvedType;

    public HECSManualResolverAttribute(Type resolvedType)
    {
        ResolvedType = resolvedType;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HECSDefaultResolverAttribute : Attribute
{
}