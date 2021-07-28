using System;
using System.Numerics;
using MessagePack;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public partial struct Vector2Serialize
    {
        [Key(0)]
        public float X;
        [Key(1)]
        public float Y;

        public Vector2Serialize(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2Serialize(Vector2 vector2)
        {
            X = vector2.X;
            Y = vector2.Y;
        }
        
        public Vector2 AsNumericsVector() 
            => new Vector2(X, Y);
    }
}