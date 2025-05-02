namespace Ecrys.Installers
{
	using Ecrys.Core;
	using Ecrys.Managers;
	using Game.Managers;
	using Zenject;


	public class ProjectInstaller : MonoInstaller
	{
	    public override void InstallBindings()
	    {
			// Bootstrap
			Container
				.BindInterfacesTo< Bootstrap >()
				.AsSingle();

			InstallModels();

			// Scopes
			Container
				.BindInterfacesTo< Scopes >()
				.AsSingle();

			// ScenesManager
			Container
				.BindInterfacesTo< ScenesManager >()
				.FromNewComponentOnNewGameObject()
				.AsSingle()
				.NonLazy()
				.IfNotBound();
			
			Install_Input();

			Install_Factories();
		}

		private void InstallModels()
		{
			// GameModel
			Container
				.Bind< GameModel >()
				.AsSingle();
		}

		private void Install_Input()
		{
			// InputManager
			Container
				.BindInterfacesTo< InputManager >()
				.AsSingle();
		}

		private void Install_Factories()
		{
		}
	}
}
