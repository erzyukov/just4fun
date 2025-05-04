namespace Game.Buildings
{
	using UnityEngine;
	using Zenject;

	public class CastleFacadeFactory : IFactory<CastleFacadeFactory.Args, ICastleFacade>
	{
		public struct Args
		{
			public Transform	Prefab;
			public ETeam		Team;
		}

		[Inject] DiContainer		_container;

		public ICastleFacade Create( Args args )
		{
			_container.Unbind<ETeam>();
			_container.BindInstance( args.Team );

			ICastleFacade unit	= _container.InstantiatePrefabForComponent<ICastleFacade>( args.Prefab );

			return unit;
		}
	}
}
