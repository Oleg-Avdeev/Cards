
using System;
using System.Collections.Generic;

namespace Witches.Cards.System.Data
{
	public struct GameData
	{
		public string Id;
		public DateTime StartTime;

		public List<CardData> FullDeck;
		public List<CardData> PlayedCards;
		public List<CardData> TableCards;
		public List<CardData> ShownCards;

		public DateTime RoundStartTime;
		public int RoundNumber;
		public int RoundOwner;

		public PlayerData[] Players;
		public bool GameFinished;

		public GameData(string id)
		{
			Id = id;

			GameFinished = false;
			RoundNumber = 0;
			RoundOwner = 0;

			FullDeck = new List<CardData>();
			PlayedCards = new List<CardData>();
			TableCards = new List<CardData>();
			ShownCards = new List<CardData>();

			StartTime = DateTime.Now;
			RoundStartTime = DateTime.Now;

			Players = new PlayerData[] {
				new PlayerData(),
				new PlayerData{}
			};
		}
	}
}