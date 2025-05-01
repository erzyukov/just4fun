namespace Ecrys.Loader
{
	using R3;


	public class LoaderModel
	{
		// Model
		public ReactiveProperty<string>		TaskMessage			= new();
		public ReactiveProperty<float>		Progress			= new();
		public ReactiveProperty<int>		CurrentTask			= new( -1 );
	}
}

