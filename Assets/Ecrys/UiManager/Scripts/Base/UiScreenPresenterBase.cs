namespace Ecrys.UiManager
{
	using System;
	using R3;
	using Zenject;


	public abstract class UiScreenPresenterBase<TView, TScreen, TPopup> : IInitializable, IDisposable
		where TView : IUiViewBase where TScreen : Enum where TPopup : Enum
	{
		[Inject] protected TView					View;
		[Inject] private UiModel<TScreen, TPopup>	_uiModel;

		protected CompositeDisposable Disposables = new();
		protected CompositeDisposable OnOpenDisposables = new();


		protected abstract TScreen	Screen		{get;}


		public virtual void Initialize()
		{
			_uiModel.Screen
				.Select( s => s.Equals( Screen ) )
				.Subscribe( v =>
				{
					View.SetActive( v );

					if (v) OnOpened();
					else OnClosed();
				} )
				.AddTo( Disposables );
		}


		public virtual void Dispose()
		{
			OnOpenDisposables.Dispose();
			Disposables.Dispose();
		}


		protected virtual void OnOpened()
		{
			OnOpenDisposables.Clear();
		}
		

		protected virtual void OnClosed() { }
	}
}

