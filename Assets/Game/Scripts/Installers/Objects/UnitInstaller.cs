namespace Game.Installers
{
	using UnityEngine;
	using UnityEngine.AI;
	using Zenject;
	using Game.Units;

	public class UnitInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{

			// NavMeshAgent
			Container
				.Bind<NavMeshAgent>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<UnitMove>()
				.AsSingle();
		}
	}
}