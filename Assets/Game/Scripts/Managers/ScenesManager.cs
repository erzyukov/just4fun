namespace Ecrys.Managers
{
	using Cysharp.Threading.Tasks;
	using ObservableCollections;
	using R3;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using Zenject;
	using Ecrys.Configs;
	using Ecrys.Utilities;
	using UnityEngine.ResourceManagement.ResourceProviders;
	using Ecrys.Core;


	public interface IScenesManager
	{
		ReactiveCommand<EScene> SceneLoading { get; }
		ReactiveCommand<EScene> SceneLoaded { get; }
		ReactiveCommand<EScene> SceneUnloaded { get; }
		ObservableDictionary<EScene, float>	Progress {get;}

		UniTask LoadScene( EScene sceneType, bool setActive );

		void UnloadActiveScene();

		UniTask FirstLoadAsync();
	}


	public class ScenesManager : MonoBehaviour, IScenesManager
	{
		[Inject] IScopes			_scopes;
		[Inject] GameModel			_model;
		[Inject] ScenesConfig		_scenesConfig;

		EScene			_activeSceneType = EScene.None;

#region IScenesManager

		public ReactiveCommand<EScene> SceneLoading				{ get; } = new();
		public ReactiveCommand<EScene> SceneLoaded				{ get; } = new();
		public ReactiveCommand<EScene> SceneUnloaded			{ get; } = new();
		public ObservableDictionary<EScene, float>	Progress	{get;} = new();

		
		public async UniTask FirstLoadAsync()
		{
			_model.FinalLoadingScene = _scenesConfig.DefaultFinalScene;

			await LoadScene( EScene.Main, false );

			await LoadScene( _model.FinalLoadingScene, true );
		}
		

		public async UniTask LoadScene( EScene sceneType, bool setActive )
		{

			UnloadActiveScene();

			SceneLoading.Execute( sceneType );
			
			var sceneRef	= _scenesConfig.Scenes[ sceneType ];
			ILifeTime lt	= _scopes.GetSceneScope( sceneType );

			SceneInstance sceneInstance		= await sceneRef.LoadSceneTaskAsync( lt, LoadSceneMode.Additive );
			Scene scene						= sceneInstance.Scene;

			await UniTask.WaitWhile( () => !scene.isLoaded );

			if (setActive)
			{
				SceneManager.SetActiveScene( scene );
				_activeSceneType	= sceneType;
			}

			SceneLoaded.Execute( sceneType );
		}

		public void UnloadActiveScene()
		{
			if (_activeSceneType == EScene.None)
				return;

			_scopes.ReleaseSceneScope( _activeSceneType );

			SceneUnloaded.Execute( _activeSceneType );

			_activeSceneType = EScene.None;
		}

#endregion

	}
}

