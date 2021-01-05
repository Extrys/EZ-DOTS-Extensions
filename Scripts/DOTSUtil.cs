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

		public static bool TryInteract<T, Y>
			(this EntityPair pair, ref int index, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB, out Entity a, out Entity b) 
			where T : struct, IComponentData where Y : struct, IComponentData
		{
			bool switched = index > 0;
			a = switched ? pair.EntityB : pair.EntityA;
			b = switched ? pair.EntityA : pair.EntityB;

			return index++ < 2 && getterA.Exists(a) & getterB.Exists(b);
		}

		public static bool SwitchAndTryInteract<T, Y>(this ref EntityPair pair, ComponentDataFromEntity<T> getterA, ComponentDataFromEntity<Y> getterB) 
			where T : struct, IComponentData where Y : struct, IComponentData
		{
			pair.PairSwitch();
			return getterA.Exists(pair.EntityA) & getterB.Exists(pair.EntityB);
		}
		
		
		
      		public static void AddHybridComponentsToNewEntity(out Entity entity, params object[] objects)
		{
			EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			entity = manager.CreateEntity();

			int iterations = objects.Length;
			for (int i = 0; i < iterations; i++)
				manager.AddComponentObject(entity, objects[i]);
		}

		public static void AddComponentObject(this Entity entity, params object[] objects)
		{
			EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			int iterations = objects.Length;
			for (int i = 0; i < iterations; i++)
				manager.AddComponentObject(entity, objects[i]);
		}

		public static void AddComponentDatasToNewEntity<T>(out Entity entity, params T[] objects) where T : struct, IComponentData
		{
			EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			entity = manager.CreateEntity();

			int iterations = objects.Length;
			for (int i = 0; i < iterations; i++)
				manager.AddComponentData(entity, objects[i]);
		}

		public static void AddComponentData<T>(this Entity entity, params T[] objects) where T : struct, IComponentData
		{
			EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			int iterations = objects.Length;
			for (int i = 0; i < iterations; i++)
				manager.AddComponentData(entity, objects[i]);
		}

		public static void AddHybridComponentsToNewEntity(params object[] objects) => AddHybridComponentsToNewEntity(out _, objects);
		public static void AddHybridComponentsToNewEntity<T>(params T[] objects) where T : struct, IComponentData => AddComponentDatasToNewEntity(out _, objects);
	}
}
