using System;
using System.Collections.Generic;
using Components;
using HECSFramework.Core;

namespace HECSFramework.Serialize
{
    public struct AnimatorStateResolver : IResolver<AnimatorState>, IResolver<AnimatorStateResolver, AnimatorState>
    {
        public Dictionary<int, BoolParameterResolver> BoolStates;
        public Dictionary<int, IntParameterResolver> IntStates;
        public Dictionary<int, FloatParameterResolver> FloatStates;

        public int AnimatorID;

        public AnimatorStateResolver In(ref AnimatorState data)
        {
            BoolStates = new Dictionary<int, BoolParameterResolver>();
            IntStates = new Dictionary<int, IntParameterResolver>();
            FloatStates = new Dictionary<int, FloatParameterResolver>();
            AnimatorID = data.AnimatorID;
            data.Save(ref this);
            return this;
        }

        public void Out(ref AnimatorState data)
        {
            data.Load(ref this);
        }

        public void Out(ref IEntity entity)
        {
            entity.GetHECSComponent<AnimatorStateComponent>().State.Load(ref this);
        }
    }
}
