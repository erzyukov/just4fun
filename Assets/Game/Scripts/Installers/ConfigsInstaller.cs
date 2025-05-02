namespace Ecrys.Installers
{
	using UnityEngine;
	using Zenject;
	using Ecrys.Configs;


	public class ConfigsInstaller : MonoInstaller
	{
		[SerializeField] RootConfig			_rootConfig;


	    public override void InstallBindings()
	    {
			var rc		= _rootConfig;

			// Gameplay

			// Lobby
			
			// Visual

			// Technical
			Container.BindInstance( rc.ScenesConfig );
			Container.BindInstance( rc.PrefabsConfig );
			
			// Modules

			// Economics

			// Monetization
		}
	}
}
