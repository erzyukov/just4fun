namespace Game.Inputs
{
	using UnityEngine;
	using R3;
	using System;
	using Zenject;
	using Game.Managers;
	using Ecrys.Utilities;
	using static UnityEditor.PlayerSettings;
	using Game.Core;

	public class TouchHandle : IInitializable, IDisposable
	{
		[Inject] IInputManager _inputManager;
		[Inject] GameplayModel _gameplayModel;

		private CompositeDisposable _disposables = new();
		public void Initialize()
		{
			_inputManager.Camera.Move.PerformedAsObservable().Subscribe(ctx =>
			{
				Vector2 pos = ctx.ReadValue<Vector2>();
				_gameplayModel.TouchDelta.Value = pos;
			}).AddTo(_disposables);
		}

		public void Dispose() => _disposables.Dispose();
		/*
		Camera.Move.PerformedAsObservable()
        .Subscribe(OnMovementPerformed );

		Camera.Move.CanceledAsObservable()
        .Subscribe(CanceledPerformed );
		*/
	}
}