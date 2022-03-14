using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FieldAttribute : Attribute
{
    public readonly int Queue;
    public readonly Type CustomResolverType;

    public FieldAttribute(int queue)
    {
        Queue = queue;
    }

    public FieldAttribute(int queue, Type customResolverType) : this(queue)
    {
        CustomResolverType = customResolverType;
    }
}
