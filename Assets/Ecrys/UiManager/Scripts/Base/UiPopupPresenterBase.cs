namespace Ecrys.UiManager
{
	using ObservableCollections;
	using R3;
	using System;
	using Zenject;


	public abstract class UiPopupPresenterBase<TView, TScreen, TPopup> : IInitializable, IDisposable
		where TView : IUiViewBase where TScreen : Enum where TPopup : Enum
	{
		[Inject] protected TView			View;
		[Inject] UiModel<TScreen, TPopup>	_uiModel;

		protected CompositeDisposable Disposables = new();


		protected abstract TPopup	Popup		{get;}


		public virtual void Initialize()
		{
			_uiModel.Popups.ObserveCountChanged()
				.Select( _ => _uiModel.Popups.Contains( Popup ) )
				.Subscribe( OnStateChange )
				.AddTo( Disposables );
		}

		public void Dispose()		=> Disposables.Dispose();


		protected void Close()
		{
			_uiModel.Popups.Remove( Popup );
		}

		protected virtual void OnOpened() { }
		

		protected virtual void OnClosed() { }

		private void OnStateChange( bool value )
		{
			View.SetActive( value );

			if (value)	OnOpened();
			else		OnClosed();
		}
	}
}

