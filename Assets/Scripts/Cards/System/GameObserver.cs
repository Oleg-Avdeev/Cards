using System;
using UnityEngine;
using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface IGameObserver
	{
		event Action<GameEvent> EventStream;
	}

	public partial class GameState : IGameObserver
	{
		public event Action<GameEvent> EventStream;

		private event Action<int> RoundChanged;
		private event Action<CardData> CardOpened;
		private event Action<CardData> CardClosed;
		private event Action<CardData[]> CardsReplaced;
		private event Action<CardData[]> CardsMatched;
		private event Action<PlayerData, int> PlayerDamaged;
		private event Action<PlayerData, int> PlayerHealed;
		private event Action<PlayerData, int> PlayerMana;
		private event Action<PlayerData> PlayerLost;
		private event Action<int, PlayerData[]> GameStarted;

		public GameData GetFullState() => _fullGameState;

		private void InitializeObservableEventStream()
		{
			GameStarted = (cardsNumber, players) => PushIntoStream(new GameEvent(GameEventType.GameStarted, cardsNumber: cardsNumber, players: players));
			RoundChanged = (round) => PushIntoStream(new GameEvent(GameEventType.RoundChanged, roundNumber: round));
			CardOpened = (card) => PushIntoStream(new GameEvent(GameEventType.CardOpened, card: card));
			CardClosed = (card) => PushIntoStream(new GameEvent(GameEventType.CardClosed, card: card));
			CardsReplaced = (cards) => PushIntoStream(new GameEvent(GameEventType.CardsReplaced, cards: cards));
			CardsMatched = (cards) => PushIntoStream(new GameEvent(GameEventType.CardsMatched, cards: cards));
			PlayerDamaged = (player, damage) => PushIntoStream(new GameEvent(GameEventType.PlayerDamaged, player: player, damageAmount: damage));
			PlayerHealed = (player, heal) => PushIntoStream(new GameEvent(GameEventType.PlayerHealed, player: player, healingAmount: heal));
			PlayerMana = (player, mana) => PushIntoStream(new GameEvent(GameEventType.PlayerMana, player: player, manaAmount: mana));
			PlayerLost = (player) => PushIntoStream(new GameEvent(GameEventType.PlayerLost, player: player ));
		}

		private void PushIntoStream(GameEvent gameEvent)
		{
			try
			{
				Debug.Log($"[Event Stream] : Pushing {gameEvent.Type} into event stream");
				EventStream.Invoke(gameEvent);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}
	}
}