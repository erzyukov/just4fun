namespace Ecrys.Utilities
{
	using UnityEngine;
	using UnityEngine.AddressableAssets;


	[CreateAssetMenu( fileName = "PoolPrefab", menuName = "Configs/Tools/PoolPrefab" )]
	public class PoolablePrefabConfig : ScriptableObject
	{
		public int DefaultPoolSize;
		public int MaxPoolSize;
		public AssetReferenceGameObject PrefabRef;
	}
}
