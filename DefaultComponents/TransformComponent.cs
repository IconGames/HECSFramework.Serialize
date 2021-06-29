using HECSFramework.Core;

namespace Components
{
    public partial class TransformComponent : IAfterSerializationComponent, IBeforeSerializationComponent
    {
        [Field(0)]
        public Vector3Serialize PositionSave;
        
        [Field(1)]
        public Vector3Serialize RotationSave;

        public void AfterSync()
        {
            throw new System.NotImplementedException();
        }

        public void BeforeSync()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            SetLoadedRotateAndPosition();
        }

        partial void SetLoadedRotateAndPosition();
    }
}