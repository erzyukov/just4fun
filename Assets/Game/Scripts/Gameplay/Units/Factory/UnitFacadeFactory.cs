namespace Game.Units
{
	using UnityEngine;
	using Zenject;

	public class UnitFacadeFactory : IFactory<UnitFacadeFactory.Args, IUnitFacade>
	{
		public struct Args
		{
			public Transform	Prefab;
			public EUnit		Type;
		}

		[Inject] DiContainer		_container;

		public IUnitFacade Create( Args args )
		{
			_container.Unbind<EUnit>();
			_container.BindInstance( args.Type );

			IUnitFacade unit	= _container.InstantiatePrefabForComponent<IUnitFacade>( args.Prefab );

			return unit;
		}
	}
}
