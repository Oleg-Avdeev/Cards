using UnityEngine;

namespace Witches.Cards.Visualisation.Views
{
	public class FieldView : MonoBehaviour
	{
		[SerializeField] private RectTransform CardContainer;
		[SerializeField] private EndPopupView EndPopupView;
		[SerializeField] private PlayerView[] PlayerViews;
		[SerializeField] private RoundView RoundView;
		[SerializeField] private CardView CardPrefab;

		public RoundView Round => RoundView;
		public PlayerView[] Players => PlayerViews;
		public EndPopupView EndPopup => EndPopupView;

		public CardView SpawnCard(int cardIndex)
		{
			var cardView = Instantiate(CardPrefab, CardContainer);
			cardView.SetIndex(cardIndex);
			return cardView;
		}
	}
}