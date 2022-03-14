using HECSFramework.Serialize;

namespace HECSFramework.Serialize
{
    public sealed partial class FloatParameter : AnimatorParameter<float>
    {
        public FloatParameter(int parametrName) : base(parametrName)
        {
        }

        protected override void LocalSet(float value)
        {
            Value = value;
        }
    }
}
