namespace Game.Installers
{
	using Game.Camera;
	using Game.Inputs;
	using Zenject;


	public class GameplayInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// GameplayCamera
			Container
				.BindInterfacesTo<GameplayCamera>()
				.FromComponentInHierarchy()
				.AsSingle();


			// DragCameraController
			Container
				.BindInterfacesTo<DragCameraController>()
				.AsSingle();
		}
	}
}