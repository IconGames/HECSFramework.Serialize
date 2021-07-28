using HECSFramework.Core;

namespace Components
{
    public partial class AppVersionComponent : IBeforeSerializationComponent, IAfterSerializationComponent
    {
        [Field(0)]
        public int VersionSerialize { get; set; }
        
        public void AfterSync()
        {
            Version = VersionSerialize;
        }

        public void BeforeSync()
        {
            VersionSerialize = Version;
        }
    }
}