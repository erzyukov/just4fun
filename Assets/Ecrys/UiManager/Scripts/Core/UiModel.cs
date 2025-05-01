namespace Ecrys.UiManager
{
	using R3;
	using ObservableCollections;
	using System;

	public class UiModel<TScreen, TPopup> where TScreen : Enum where TPopup : Enum
	{
		public ReactiveProperty<TScreen>	Screen      = new();
		public ObservableList<TPopup>		Popups		= new();
	}
}

