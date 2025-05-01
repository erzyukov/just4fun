namespace Ecrys.Loader
{
	using Cysharp.Threading.Tasks;
	using Ecrys.UI;
	using Zenject;

	public class Loader : LoaderBase
	{
		[Inject] IUiVeil							_uiVeil;

		public override UniTask OnLoadingComplete()
		{
			return _uiVeil.DoUnveil();
		}
	}
}
