namespace Ecrys.Core
{
	using Ecrys.Utilities;
	using Sirenix.Utilities;
	using System;
	using System.Collections.Generic;
	using Zenject;


	public interface IScopes
	{
		ILifeTime GetSceneScope( EScene scene );
		void RestartScope( EScene scene	);
		void ReleaseSceneScope( EScene scene );
	}

	public class Scopes : IScopes, IInitializable
	{
		private Dictionary<EScene, LifeTime> _scopes = new();

		public void Initialize()
		{
			var scenes = (EScene[])Enum.GetValues( typeof( EScene ) );
			scenes.ForEach( s => _scopes.Add( s, new() ) );
		}

		public ILifeTime GetSceneScope( EScene scene )
		=>
			_GetSceneScope( scene );

		public void ReleaseSceneScope( EScene scene )
		=>
			_GetSceneScope( scene ).Release();

		public void RestartScope( EScene scene )
		=>
			_scopes[ scene ].Restart();

		LifeTime _GetSceneScope( EScene scene )
		{
			UpdateScope( scene );
			return _scopes[ scene ];
		}

		private void UpdateScope( EScene scene )
		{
			if (_scopes[ scene ] != null && _scopes[ scene ].isTerminated)
				_scopes[ scene ] = new();
		}
	}
}
