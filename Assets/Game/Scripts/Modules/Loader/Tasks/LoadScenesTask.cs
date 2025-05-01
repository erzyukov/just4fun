namespace Ecrys.Core
{
	using Cysharp.Threading.Tasks;
	using Zenject;
	using Ecrys.Loader;
	using Ecrys.Managers;

	public class LoadScenesTask : ILoaderTask
	{
		public string Message => "Loading scenes...";
		
		[Inject] IScenesManager		_scenesManager;

		public UniTask Load()
		{
			return _scenesManager.FirstLoadAsync();
		}
	}
}