using System.Linq;
using UnityEngine;
using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public partial class GameState
	{
		private GameData _fullGameState;

		private int GetRoundProtagonist() => _fullGameState.RoundNumber % 2;
		private int GetRoundAntagonist() => (_fullGameState.RoundNumber + 1) % 2;

		private GameState(GameData gameData) 
		{
			_fullGameState = gameData;
			InitializeObservableEventStream();
		}

		public static GameState CreateNew()
		{
			var data = CreateData();
			var state = new GameState(data);

			return state;
		}

		private static GameData CreateData()
		{
			var cards = Enumerable.Range(0, 16).Select(i => new CardData() {
				Id = i, Type = (CardType)Random.Range(0, 3), Strength = Random.Range(1, 5)
			}).ToList();

			var players = new PlayerData[] {
				new PlayerData() { Id = "1", MaxHealth = 20, Health = 20, MaxMana = 20, Mana = 0, Name = "1", PlayingOrder = 0 },
				new PlayerData() { Id = "2", MaxHealth = 20, Health = 20, MaxMana = 20, Mana = 0, Name = "2", PlayingOrder = 1 },
			};

			var data = new GameData(string.Empty) {
				TableCards = cards,
				Players = players
			};

			return data;
		}
	}
}