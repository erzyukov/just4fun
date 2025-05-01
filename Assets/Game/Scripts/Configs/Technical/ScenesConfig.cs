namespace Ecrys.Configs
{
	using Sirenix.OdinInspector;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.AddressableAssets;

	[CreateAssetMenu( fileName = "Scenes", menuName = "Configs/Scenes", order = (int)EConfig.Scenes )]
	public class ScenesConfig : SerializedScriptableObject
	{
		public Dictionary<EScene, AssetReference>	Scenes		= new();

		public EScene DefaultFinalScene;
	}
}
