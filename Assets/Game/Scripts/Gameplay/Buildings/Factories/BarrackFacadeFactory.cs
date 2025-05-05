namespace Game.Buildings
{
	using UnityEngine;
	using Zenject;

	public class BarrackFacadeFactory : IFactory<BarrackFacadeFactory.Args, IBarrackFacade>
	{
		public struct Args
		{
			public Transform	Prefab;
			public ETeam		Team;
		}

		[Inject] DiContainer		_container;

		public IBarrackFacade Create( Args args )
		{
			_container.Unbind<ETeam>();
			_container.BindInstance( args.Team );

			IBarrackFacade unit	= _container.InstantiatePrefabForComponent<IBarrackFacade>( args.Prefab );

			return unit;
		}
	}
}
