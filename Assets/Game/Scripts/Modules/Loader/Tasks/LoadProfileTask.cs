namespace Ecrys.Core
{
	using Cysharp.Threading.Tasks;
	using Zenject;
	using Ecrys.Loader;
	using Ecrys.Managers;

	public class LoadProfileTask : ILoaderTask
	{
		public string Message => "Loading profile...";
		
		//[Inject] IPlayerProfileManager		_profileManager;

		public UniTask Load()
		{
			return UniTask.CompletedTask;
			//return _profileManager.LoadAsync();
		}
	}
}