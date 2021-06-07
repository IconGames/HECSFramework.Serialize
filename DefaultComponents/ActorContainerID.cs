using HECSFramework.Core;
using Components;
using System;
using MessagePack;

namespace Components
{
    [CustomResolver]
    public partial class ActorContainerID
    {
    }
}

namespace HECSFramework.Core
{
	[MessagePackObject]
	public struct ActorContainerIDResolver : IResolver<ActorContainerID>, IData
	{
		[Key(0)]
		public String ID;

		public ActorContainerIDResolver In(ref ActorContainerID actorcontainerid)
		{
			this.ID = actorcontainerid.ID;
			return this;
		}
		public void Out(ref ActorContainerID actorcontainerid)
		{
			actorcontainerid.ID = this.ID;
		}

        public void Out(ref IEntity entity)
        {
			var local = entity.GetHECSComponent<ActorContainerID>();
			Out(ref local);
        }
    }
}