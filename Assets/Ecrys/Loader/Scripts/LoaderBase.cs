namespace Ecrys.Loader
{
	using Cysharp.Threading.Tasks;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Zenject;


	public abstract class LoaderBase : IInitializable
	{
		[Inject] IEnumerable<ILoaderTask>			_loadingTasks;
		[Inject] LoaderModel						_loadingModel;


		public virtual void Initialize()
		{
			LoadGame().Forget();
		}

		public abstract UniTask OnLoadingComplete();

		async UniTask LoadGame()
		{
			SetProgress( 0.0f );

			foreach (var task in _loadingTasks)
			{
				UpdateLoadingModel( task.Message );

				try
				{
					await task.Load();
				}
				catch (Exception e)
				{
					Debug.LogError( e );
					return;
				}
			}

			SetProgress( 1.0f );

			await UniTask.Delay( TimeSpan.FromSeconds( 0.5f ) );

			await OnLoadingComplete();
		}

		void UpdateLoadingModel( string taskName )
		{
			_loadingModel.TaskMessage.Value		= taskName;
			_loadingModel.CurrentTask.Value		++;

			float progress = (float) _loadingModel.CurrentTask.Value / _loadingTasks.Count();
			
			_loadingModel.Progress.Value		= progress;
		}

		void SetProgress( float value )			=> _loadingModel.Progress.Value = value;
	}
}
