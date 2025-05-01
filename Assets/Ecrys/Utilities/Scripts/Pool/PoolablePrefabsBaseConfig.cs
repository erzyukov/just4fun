namespace Ecrys.Utilities
{
	using Sirenix.OdinInspector;
	using System;
	using System.Collections.Generic;
	using UnityEngine.AddressableAssets;


	[Serializable]
	public struct PoolablePrefabData<T> where T : Enum
	{
		public T Type;
		public int DefaultPoolSize;
		public int MaxPoolSize;
		public AssetReferenceGameObject prefabRef;
	}


	public abstract class PoolablePrefabsBaseConfig<T> : SerializedScriptableObject where T : Enum
	{
		public Dictionary<T, PoolablePrefabConfig> References = new();
	}
}
