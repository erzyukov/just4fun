namespace Ecrys.Core
{
	using PrimeTween;
	using UnityEngine;
	using Zenject;

	public class Bootstrap : IInitializable
	{
		public void Initialize()
		{
			Application.targetFrameRate			= 120;
			PrimeTweenConfig.SetTweensCapacity( 800 );
		}
	}
}
