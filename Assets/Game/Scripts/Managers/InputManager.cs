namespace Game.Managers
{
 	using Zenject;


	public interface IInputManager
	{
		Controls.CameraActions			Camera			{get;}
	}

	public class InputManager : IInputManager, IInitializable
	{
		Controls _controls;


		public void Initialize()
		{
			_controls		= new Controls();

			Camera			= _controls.Camera;

			Camera			.Enable();
		}

#region IInputSystemManager

		public Controls.CameraActions		Camera				{ get; private set; }

#endregion
    
	}
}
