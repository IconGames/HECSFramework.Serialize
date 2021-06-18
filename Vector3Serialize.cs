using MessagePack;

namespace HECSFramework.Core
{
    [MessagePackObject]
    public struct Vector3Serialize
    {
        [Key(0)]
        public float X;
        [Key(1)]
        public float Y;
        [Key(2)]
        public float Z;

        public Vector3Serialize(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}