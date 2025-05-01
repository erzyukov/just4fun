namespace Ecrys.UiManager
{
	using System;
	using Zenject;


	public interface IUiNavigator<TScreen, TPopup> where TScreen : Enum where TPopup : Enum
	{
		void Open( TScreen screen );
		void OpenClose( TPopup popup, bool toOpen );
		bool IsOpened( TPopup popup );
		bool IsOpened( TScreen screen );
	}

	public class UiNavigator<TScreen, TPopup> : IUiNavigator<TScreen, TPopup>
		where TScreen : Enum where TPopup : Enum
	{
		[Inject] UiModel<TScreen, TPopup>		_uiModel;

		public void Open( TScreen screen )
		{
			_uiModel.Screen.Value		= screen;
		}


		public void OpenClose( TPopup popup, bool toOpen )
		{
			var popups		= _uiModel.Popups;

			if (toOpen)
			{
				if (!popups.Contains( popup ))
					popups.Add( popup );
			}
			else
				popups.Remove( popup );
		}

		public bool IsOpened( TPopup popup )
		=>
			_uiModel.Popups.Contains( popup );

		public bool IsOpened( TScreen screen )
		=>
			_uiModel.Screen.Value.Equals( screen );
	}
}
