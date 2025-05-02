namespace Game.Inputs
{
	using Game.Camera;
	using Game.Core;
	using R3;
	using System;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Zenject;

	public class DragCameraController : IInitializable, IDisposable
	{
		[Inject] IGameplayCamera _camera;
		[Inject] GameplayModel _gameplayModel;

		private CompositeDisposable _disposables = new();

		private Vector2 lastTouchPosition;
		private bool isDragging = false;
		public float dragSpeed = 0.01f;

		public void Initialize()
		{
			_gameplayModel.TouchDelta.Subscribe(OnTouchDeltaChanged).AddTo(_disposables);
		}

		public void Dispose() => _disposables.Dispose();

		private void OnTouchDeltaChanged(Vector2 delta)
		{
			Vector3 move = new Vector3(delta.x, 0, delta.y) * dragSpeed;
			Vector3 pos = _camera.Transform.position;
			_camera.SetPosition(pos + move);
		}
	}
}