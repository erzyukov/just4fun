namespace Ecrys.Configs
{
//	using Ecrys.Tools;
	using UnityEngine;


	[CreateAssetMenu( fileName = "Root", menuName = "Configs/Root", order = (int)EConfig.Root )]
	public class RootConfig : ScriptableObject
	{
		[Header("Technical")]
		public ScenesConfig				ScenesConfig;
	}
}
