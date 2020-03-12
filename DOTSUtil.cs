namespace SquirrelBytes.DOTS
{
	public static class DOTSUtil
	{
		
		public static void PairSwitch(this ref EntityPair pair)
		{
			var tempA = pair.EntityA;
			pair.EntityA = pair.EntityB;
			pair.EntityB = tempA;
		}
			
		public static bool TryInteract<T, Y>(this EntityPair pair,
			int index, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB, out Entity a, out Entity b) 
			where T : struct, IComponentData where Y : struct, IComponentData
			{
				bool switched = index > 0;
				a = switched ? pair.EntityB : pair.EntityA;
				b = switched ? pair.EntityA : pair.EntityB;

				return index < 2 && getterA.Exists(a) & getterB.Exists(b);
		    	}

		public static bool SwitchAndTryInteract<T, Y>
			(this ref EntityPair pair, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB) 
			where T : struct, IComponentData where Y : struct, IComponentData
			{
				pair.PairSwitch();
				return getterA.Exists(pair.EntityA) & getterB.Exists(pair.EntityB);
			}
	}
}
