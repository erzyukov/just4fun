namespace Ecrys.UiManager
{
	using UnityEngine;


	public interface IUiViewBase
	{
		void SetActive( bool isActive );
	}


	public class UiViewBase : MonoBehaviour, IUiViewBase
	{
		public void SetActive( bool isActive )		=> gameObject.SetActive( isActive );
	}
}

