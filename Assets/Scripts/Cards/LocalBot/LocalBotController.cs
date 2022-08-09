using DG.Tweening;
using UnityEngine;
using Witches.Cards.System;
using Witches.Cards.System.Data;

namespace Witches.Cards.LocalBot
{
	public class LocalBotController 
	{
		private readonly IGameModifier _gameStateModifier;

		private PlayerData _botData;
		private CardData?[] _knownField;

		public LocalBotController(IGameModifier gameStateModifier, IGameObserver gameStateObserver)
		{
			_gameStateModifier = gameStateModifier;

			gameStateObserver.EventStream += ProcessGameEvent;
			gameStateModifier.ConnectToGame(1);
		}

		private void ProcessGameEvent(GameEvent gameEvent)
		{
			switch (gameEvent.Type)
			{
				case GameEventType.GameStarted:
					_knownField = new CardData?[gameEvent.CardsNumber];
					_botData = gameEvent.Players[1];
					break;

				case GameEventType.CardOpened:
					_knownField[gameEvent.Card.Id] = gameEvent.Card;
					break;

				case GameEventType.CardClosed:
					break;

				case GameEventType.PlayerDamaged:
					break;

				case GameEventType.PlayerHealed:
					break;

				case GameEventType.PlayerMana:
					break;

				case GameEventType.RoundChanged:
					if (gameEvent.RoundNumber % 2 != _botData.PlayingOrder) return;
					Debug.Log($"[Bot] : Is considering making a move");
					
					var randomIndex = Random.Range(0, _knownField.Length);
					var secondIndex = (randomIndex + Random.Range(1, _knownField.Length)) % _knownField.Length;
					
					DOVirtual.DelayedCall(2f, () => _gameStateModifier.OpenCard(_botData, randomIndex));
					DOVirtual.DelayedCall(3f, () => _gameStateModifier.OpenCard(_botData, secondIndex));
					break;

				case GameEventType.CardsMatched:
					break;

				case GameEventType.PlayerLost:
					_gameStateModifier.AllowGameRestart(_botData);
					break;

				case GameEventType.CardsReplaced:
					foreach(var card in gameEvent.Cards)
						_knownField[card.Id] = null;
					break;
			}
		}
	}
}