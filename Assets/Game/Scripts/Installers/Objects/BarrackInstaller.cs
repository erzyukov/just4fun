namespace Game.Installers
{
	using Game.Buildings;
	using Zenject;

	public class BarrackInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// BuildingView
			Container
				.BindInterfacesTo<BuildingView>()
				.FromComponentInHierarchy()
				.AsSingle();

			// BarrackUnitSpawner
			Container
				.BindInterfacesTo<BarrackUnitSpawner>()
				.AsSingle();
		}
	}
}