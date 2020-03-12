namespace SquirrelBytes.DOTS
{
	public static class DOTSUtil
	{

		/// <summary>  Interchanges A and B values in the pair so AB becomes BA  </summary>
		public static void PairSwitch(this ref EntityPair pair)
		{
			var tempA = pair.EntityA;
			pair.EntityA = pair.EntityB;
			pair.EntityB = tempA;
		}


		/// <summary> This function is used inside the "for" conditional block, for repeating the interactions between entity components if possible </summary>
		/// <param name="index">"for" index, used to defire the interaction direction</param><param name="getterA">ComponentDataFromEntity A</param><param name="getterB">ComponentDataFromEntity B</param>
		/// <param name="a">Entity A</param><param name="b">Entity B</param>
		public static bool TryInteract<T, Y> (this EntityPair pair, 
		      	int index, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB, out Entity a, out Entity b) 
			where T : struct, IComponentData where Y : struct, IComponentData
		{
			bool switched = index > 0;
			a = switched ? pair.EntityB : pair.EntityA;
			b = switched ? pair.EntityA : pair.EntityB;

			return index < 2 && getterA.Exists(a) & getterB.Exists(b);
		}


		/// <summary> This function is used to switch the pair and check the interaction compatibility between their components
		/// can be used as condition for an iteration that ocurs two times for change the component compatibility check direction </summary>
		///<param name = "getterA" > ComponentDataFromEntity A</param><param name = "getterB" > ComponentDataFromEntity B</param>
		public static bool SwitchAndTryInteract<T, Y>(this ref EntityPair pair, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB) where T : struct, IComponentData where Y : struct, IComponentData
		{
			pair.PairSwitch();
			return getterA.Exists(pair.EntityA) & getterB.Exists(pair.EntityB);
		}
	}
}
