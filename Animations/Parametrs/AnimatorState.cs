using System.Collections.Generic;

namespace HECSFramework.Serialize
{
    [HECSResolver]
    public class AnimatorState
    {
        Dictionary<int, AnimatorParameter<bool>> boolParameters = new Dictionary<int, AnimatorParameter<bool>>();
        Dictionary<int, AnimatorParameter<float>> floatParameters = new Dictionary<int, AnimatorParameter<float>>();
        Dictionary<int, AnimatorParameter<int>> intParameters = new Dictionary<int, AnimatorParameter<int>>();

        public bool IsBoolDirty { get; protected set; }
        public int AnimatorID { get; protected set; }

        public void SetBool(int id, bool value)
        {
            boolParameters[id].Set(value);
        }

        public void SetFloat(int id, float value)
        {
            floatParameters[id].Set(value);
        }

        public void SetInt(int id, int value)
        {
            intParameters[id].Set(value);
        }
    }
}