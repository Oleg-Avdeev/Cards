using Witches.Cards.System.Data;

namespace Witches.Cards.System.Rules
{

	public class OpenCardRule
	{
		// public RuleType RuleType => RuleType.OpenCard;

		// public GameData Apply(GameData gameData, CardData cardData)
		// {
		// 	gameData.PlayedCards.Add(cardData);
		// 	CardOpened.Invoke(cardData);

		// 	if (gameData.PlayedCards.Count % 2 == 0)
		// 	{
		// 		var lastTwoCards = gameData.PlayedCards.TakeLast(2);
				
		// 		if (lastTwoCards.Select(x => x.Type).Distinct().Count() == 1)
		// 			MatchCards(lastTwoCards);
				
		// 		_fullGameState.RoundNumber++;
		// 		RoundChanged.Invoke(_fullGameState.RoundNumber);
		// 	}

		// 	return gameData;
		// }
	}
}