using HECSFramework.Core;

namespace Components
{
    public partial class TransformComponent : BaseComponent
    {
        [Field(0)]
        public Vector3Serialize PositionSave;

        [Field(1)]
        public Vector3Serialize RotationSave;

        partial void InfoUpdated();
    }
}