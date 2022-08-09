using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Witches.Cards.System.Data;
using Witches.Cards.Visualisation.Promises;

namespace Witches.Cards.Visualisation.Views
{
	public class CardView : MonoBehaviour
	{
		[SerializeField] private Text StrengthText;
		[SerializeField] private Image BackgroundImage;
		[SerializeField] private Button CardButton;

		[SerializeField] private Color _backColor;
		[SerializeField] private Color _healColor;
		[SerializeField] private Color _damageColor;
		[SerializeField] private Color _manaColor;

		private Color _frontColor;

		public event Action<int> OnClick;
		public int CardIndex { get; private set; }

		public void SetIndex(int index)
		{
			CardIndex = index;
			CardButton.onClick.RemoveAllListeners();
			CardButton.onClick.AddListener(() => OnClick.Invoke(index));
		}

		public void SetData(CardData cardData)
		{
			StrengthText.text = cardData.Strength.ToString();

			switch (cardData.Type)
			{
				case CardType.Attack: _frontColor = _damageColor; break;
				case CardType.Healing: _frontColor = _healColor; break;
				case CardType.Magic: _frontColor = _manaColor; break;
			}

			StrengthText.enabled = false;
			BackgroundImage.color = _backColor;
		}

		public Promise ShowFront(CardData cardData)
		{
			return Promise.Create((resolver) =>
			{
				SetData(cardData);
				StrengthText.enabled = true;
				BackgroundImage.color = _frontColor;
				resolver.Resolve();
			});
		}

		public Promise ShowBack()
		{
			return Promise.Create((resolver) =>
			{
				DOVirtual.DelayedCall(1f, () =>
				{
					StrengthText.enabled = false;
					BackgroundImage.color = _backColor;
				});

				resolver.Resolve();
			});
		}

		public Promise Reshuffle(CardData cardData)
		{
			return Promise.Create((resolver) =>
			{
				var currentX = transform.position.x;

				transform.DOMoveX(currentX + 10, 0.5f).OnComplete(() => {
					SetData(cardData);
				});
				transform.DOMoveX(currentX, 0.5f).SetDelay(1f).OnComplete(() => {
					resolver.Resolve();
				});
					
			});
		}
	}
}