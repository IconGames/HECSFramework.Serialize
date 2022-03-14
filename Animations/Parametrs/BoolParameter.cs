namespace HECSFramework.Serialize
{
    public sealed partial class BoolParameter : AnimatorParameter<bool>
    {
        public BoolParameter(int parametrName) : base(parametrName)
        {
        }

        protected override void LocalSet(bool value)
        {
            Value = value;
        }
    }
}