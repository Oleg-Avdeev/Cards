using UnityEngine;
using Witches.Cards.LocalBot;
using Witches.Cards.System;
using Witches.Cards.Visualisation;
using Witches.Cards.Visualisation.Views;

namespace Witches.Cards
{
	public class CardsGameScreen : MonoBehaviour
	{
		[SerializeField] private GameVisualisationQueue GameVisualisationQueue;
		[SerializeField] private FieldView Field;

		private void Awake()
		{
			var gameState = GameState.CreateNew();
			var gameController = new GameController(Field, gameState, gameState, GameVisualisationQueue);
			var localBot = new LocalBotController(gameState, gameState);
		}	
	}
}