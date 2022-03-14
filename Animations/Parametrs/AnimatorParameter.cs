namespace HECSFramework.Serialize
{
    public abstract partial class AnimatorParameter
    {
        protected int parameterHashCode;
        public bool IsDirty;

        public AnimatorParameter(int parameterID)
        {
            parameterHashCode = parameterID;
        }
    }
}