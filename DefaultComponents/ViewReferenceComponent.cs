using HECSFramework.Core;

namespace Components
{
    public partial class ViewReferenceComponent : BaseComponent
    {
        [Field(0)]
        public string AssetReferenceGuid = string.Empty;
    }
}