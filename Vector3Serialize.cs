using MessagePack;
using System;
using System.Numerics;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public partial struct Vector3Serialize : IEquatable<Vector3Serialize>
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

        public Vector3Serialize(Vector3 vector3)
        {
            X = vector3.X;
            Y = vector3.Y;
            Z = vector3.Z;
        }
        
        public Vector3 AsNumericsVector() 
            => new Vector3(X, Y, Z);

        public Vector2 AsNumericsVector2() 
            => new Vector2(X, Z);

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        public bool Equals(Vector3Serialize other)
            => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }
}