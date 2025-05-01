namespace Ecrys.Loader
{
	using Cysharp.Threading.Tasks;

	public interface ILoaderTask
	{
		string Message			{get;}
		
		UniTask Load();
	}
}