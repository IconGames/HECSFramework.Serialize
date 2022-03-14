namespace HECSFramework.Serialize
{
    public sealed partial class IntParameter : AnimatorParameter<int>
    {
        public IntParameter(int parametrName) : base(parametrName)
        {
        }

        protected override void LocalSet(int value)
        {
            Value = value;
        }
    }
}