using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface IGameModifier
	{
		void ConnectToGame(int playerIndex);
		void OpenCard(PlayerData requestingPlayer, int cardId);
		void ActivateAbility(PlayerData requestingPlayer);
		void AllowGameRestart(PlayerData requestingPlayer);
	}

	public partial class GameState : IGameModifier
	{
		public void ConnectToGame(int playerIndex)
		{
			_fullGameState.Players[playerIndex].Connected = true;

			if (_fullGameState.Players.All(p => p.Connected))
				RestartGame();
		}

		public void OpenCard(PlayerData requestingPlayer, int cardId)
		{
			var card = _fullGameState.TableCards.First(c => c.Id == cardId);
			OpenCard(requestingPlayer, card);
		}

		public void OpenCard(PlayerData requestingPlayer, CardData cardData)
		{
			if (requestingPlayer.PlayingOrder != GetRoundProtagonist())
				return;

			if (_fullGameState.GameFinished)
				return;

			if (_fullGameState.ShownCards.Contains(cardData))
				return;

			_fullGameState.PlayedCards.Add(cardData);
			_fullGameState.ShownCards.Add(cardData);
			CardOpened.Invoke(cardData);

			if (_fullGameState.PlayedCards.Count % 2 == 0)
			{
				var lastTwoCards = _fullGameState.PlayedCards.TakeLast(2);

				if (lastTwoCards.Select(x => x.Type).Distinct().Count() == 1)
					MatchCards(lastTwoCards);

				else
				{
					_fullGameState.ShownCards.Clear();
					CardClosed.Invoke(lastTwoCards.First());
					CardClosed.Invoke(lastTwoCards.Last());
				}

				_fullGameState.RoundNumber++;
				RoundChanged.Invoke(_fullGameState.RoundNumber);
			}

		}

		public void ActivateAbility(PlayerData requestingPlayer)
		{

		}

		private void MatchCards(IEnumerable<CardData> cards)
		{
			CardsMatched.Invoke(cards.ToArray());
			_fullGameState.ShownCards.Clear();

			var type = cards.First().Type;
			var combinedEffect = cards.Sum(x => x.Strength);

			switch (type)
			{
				case CardType.Attack:
					_fullGameState.Players[GetRoundAntagonist()].Health -= combinedEffect;
					PlayerDamaged.Invoke(_fullGameState.Players[GetRoundAntagonist()], combinedEffect);

					if (_fullGameState.Players[GetRoundAntagonist()].Health < 0)
						FinishGame(_fullGameState.Players[GetRoundAntagonist()]);

					break;

				case CardType.Healing:
					_fullGameState.Players[GetRoundProtagonist()].Health += combinedEffect;
					_fullGameState.Players[GetRoundProtagonist()].Health 
						= Mathf.Min(_fullGameState.Players[GetRoundProtagonist()].Health, _fullGameState.Players[GetRoundProtagonist()].MaxHealth);

					PlayerHealed.Invoke(_fullGameState.Players[GetRoundProtagonist()], combinedEffect);
					break;

				case CardType.Magic:
					_fullGameState.Players[GetRoundProtagonist()].Mana += combinedEffect;
					_fullGameState.Players[GetRoundProtagonist()].Mana 
						= Mathf.Min(_fullGameState.Players[GetRoundProtagonist()].Mana, _fullGameState.Players[GetRoundProtagonist()].MaxMana);

					PlayerMana.Invoke(_fullGameState.Players[GetRoundProtagonist()], combinedEffect);
					break;
			}

			ReplaceCards(cards);
		}

		private void ReplaceCards(IEnumerable<CardData> cardsToReplace)
		{
			var replacedCards = new List<CardData>();

			foreach (var card in cardsToReplace)
			{
				var newCard = new CardData()
				{
					Id = card.Id,
					Type = (CardType)Random.Range(0, 3),
					Strength = Random.Range(0, 5)
				};

				replacedCards.Add(newCard);
				_fullGameState.TableCards[card.Id] = newCard;
			}

			CardsReplaced.Invoke(replacedCards.ToArray());
		}

		private void FinishGame(PlayerData losingPlayer)
		{
			_fullGameState.GameFinished = true;
			PlayerLost.Invoke(losingPlayer);
		}

		public void AllowGameRestart(PlayerData requestingPlayer)
		{
			if (!_fullGameState.GameFinished)
				return;

			for (int i = 0; i < _fullGameState.Players.Length; i++)
				if (_fullGameState.Players[i].Id == requestingPlayer.Id) 
					_fullGameState.Players[i].AllowedRestart = true;

			if (_fullGameState.Players.All(p => p.AllowedRestart))
				RestartGame();
		}

		private void RestartGame()
		{
			_fullGameState = CreateData();
			GameStarted.Invoke(_fullGameState.TableCards.Count, _fullGameState.Players);
		}
	}
}