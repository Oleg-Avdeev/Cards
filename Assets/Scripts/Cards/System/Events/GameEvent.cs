using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public struct GameEvent: IRoundChangedEvent, ICardOpenedEvent, ICardsMatchedEvent, IPlayerDamagedEvent, IPlayerHealedEvent
	{
		public GameEventType Type { get; }
		public int CardsNumber { get; }
		public int RoundNumber { get; }
		public CardData Card { get; }
		public CardData[] Cards { get; }
		public PlayerData Player { get; }
		public PlayerData[] Players { get; }
		public int DamageAmount { get; }
		public int HealingAmount { get; }
		public int ManaAmount { get; }
		
		public GameEvent(GameEventType type, 
		int cardsNumber = default,
		int roundNumber = default,
		CardData card = default,
		CardData[] cards = default,
		PlayerData player = default,
		PlayerData[] players = default,
		int damageAmount = default,
		int healingAmount = default,
		int manaAmount = default
		)
		{
			Type = type;
			RoundNumber = roundNumber;
			CardsNumber = cardsNumber;
			Card = card;
			Cards = cards;
			Player = player;
			Players = players;
			DamageAmount = damageAmount;
			HealingAmount = healingAmount;
			ManaAmount = manaAmount;
		}
	}
}
