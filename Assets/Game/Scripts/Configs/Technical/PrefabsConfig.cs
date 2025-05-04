namespace Ecrys.Configs
{
	using Game;
	using Sirenix.OdinInspector;
	using System.Collections.Generic;
	using UnityEngine;


	[CreateAssetMenu( fileName = "Prefabs", menuName = "Configs/Prefabs", order = (int)EConfig.Prefabs )]
	public class PrefabsConfig : SerializedScriptableObject
	{
		public Dictionary<EUnit, Transform>			Units			= new();
		public Dictionary<EBuilding, Transform>		Buildings		= new();
	}
}
