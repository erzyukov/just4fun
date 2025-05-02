namespace Game.Installers
{
	using Game.Camera;
	using Game.Core;
	using Game.Inputs;
	using UnityEngine;
	using Zenject;


	public class GameplayInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Debug.Log("[GameplayInstaller] InstallBindings ");

			// GameplayModel
			Container
				.Bind<GameplayModel>()
				.AsSingle();

			// GameplayCamera
			Container
				.BindInterfacesTo<GameplayCamera>()
				.FromComponentInHierarchy()
				.AsSingle();

			// DragCameraController
			Container
				.BindInterfacesTo<DragCameraController>()
				.AsSingle();

			// TouchHandle
			Container
				.BindInterfacesTo<TouchHandle>()
				.AsSingle();
		}
	}
}