namespace Game.Inputs
{
	using Game.Camera;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Zenject;

	public class DragCameraController : ITickable
	{
		[Inject] IGameplayCamera _camera;

		private Vector2 lastTouchPosition;
		private bool isDragging = false;
		public float dragSpeed = 0.2f;

		public void Tick()
		{
			Debug.Log("Tick()");
			int tc = Input.touchCount;
			Debug.Log("Input.touchCount = " + tc);
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				// Провверим, не над UI ли тач
				//if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
				//	return;

				switch (touch.phase)
				{
					case TouchPhase.Began:
						lastTouchPosition = touch.position;
						isDragging = true;
						break;

					case TouchPhase.Moved:
						if (isDragging)
						{
							Vector2 delta = touch.position - lastTouchPosition;
							Vector3 move = new Vector3(-delta.x, -delta.y, 0) * dragSpeed * Time.deltaTime;
							Vector3 pos = _camera.Transform.position;
							_camera.SetPosition(pos + move);
						}
						break;
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
						isDragging = false;
						break;
				}
			}
		}
	}
}