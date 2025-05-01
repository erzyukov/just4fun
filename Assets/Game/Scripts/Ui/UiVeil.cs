namespace Ecrys.UI
{
	using Cysharp.Threading.Tasks;
	using UnityEngine;

	public interface IUiVeil
	{
		UniTask DoVeil();
		UniTask DoUnveil();
	}

	public class UiVeil : MonoBehaviour, IUiVeil
	{
		[SerializeField] GameObject		_viel;

#region IUiVeil

		public UniTask DoVeil()
		{
			_viel.gameObject.SetActive(true);

			return UniTask.CompletedTask;
		}

		public UniTask DoUnveil()
		{
			_viel.gameObject.SetActive(false);

			return UniTask.CompletedTask;
		}

#endregion
	}
}