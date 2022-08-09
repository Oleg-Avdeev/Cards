using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Witches.Cards.Visualisation.Views
{
	public class EndPopupView : MonoBehaviour
	{
		[SerializeField] private TMP_Text InfoText;
		[SerializeField] private TMP_Text OpponentState;
		[SerializeField] private Button AllowRematch;

		public event Action OnAllowRematch;

		public void Show(bool hasPlayerWon)
		{
			gameObject.SetActive(true);
			
			InfoText.text = hasPlayerWon ? "You won!" : "You Lost!";
			
			AllowRematch.onClick.RemoveAllListeners();
			AllowRematch.onClick.AddListener(() => OnAllowRematch());
		}

		public void OnOpponentDecidedToPlayAgain()
		{
			OpponentState.text = "Opponent is ready to play again";
		}

		public void Close()
		{
			gameObject.SetActive(false);
			OpponentState.text = "Opponent is considering playing again";
		}
	}
}