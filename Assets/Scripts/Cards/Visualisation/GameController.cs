
using System.Collections.Generic;
using System.Linq;
using Witches.Cards.System;
using Witches.Cards.System.Data;
using Witches.Cards.Visualisation.Promises;
using Witches.Cards.Visualisation.Views;
using Witches.Utilities;

namespace Witches.Cards.Visualisation
{
	public class GameController
	{
		private readonly FieldView _fieldView;
		private readonly IGameModifier _gameStateModifier;
		private readonly IGameObserver _gameStateObserver;
		private readonly GameVisualisationQueue _gameVisualisationQueue;

		private IDictionary<int, CardView> _cardViews = new Dictionary<int, CardView>();
		private IDictionary<string, PlayerView> _playerViews;
		private PlayerData _selfPlayerData;

		public GameController(
			FieldView fieldView,
			IGameModifier gameStateModifier,
			IGameObserver gameStateObserver,
			GameVisualisationQueue gameVisualisationQueue)
		{
			_fieldView = fieldView;
			_gameStateModifier = gameStateModifier;
			_gameStateObserver = gameStateObserver;
			_gameVisualisationQueue = gameVisualisationQueue;

			gameStateObserver.EventStream += ProcessGameEvent;
			gameStateModifier.ConnectToGame(0);
		}

		private void ProcessGameEvent(GameEvent gameEvent)
		{
			switch (gameEvent.Type)
			{
				case GameEventType.GameStarted:
					InitializePlayers(gameEvent.Players);
					InitializeField(gameEvent.CardsNumber);
					_fieldView.EndPopup.Close();
					break;

				case GameEventType.CardOpened:
					EnqueueProcess(gameEvent, _cardViews[gameEvent.Card.Id].ShowFront(gameEvent.Card));
					break;

				case GameEventType.CardClosed:
					EnqueueProcess(gameEvent, _cardViews[gameEvent.Card.Id].ShowBack());
					break;

				case GameEventType.PlayerDamaged:
					EnqueueProcess(gameEvent, _playerViews[gameEvent.Player.Id].ReceiveDamage(gameEvent.Player, gameEvent.DamageAmount));
					break;

				case GameEventType.PlayerHealed:
					EnqueueProcess(gameEvent, _playerViews[gameEvent.Player.Id].ReceiveHealing(gameEvent.Player, gameEvent.HealingAmount));
					break;

				case GameEventType.PlayerMana:
					EnqueueProcess(gameEvent, _playerViews[gameEvent.Player.Id].ReceiveMana(gameEvent.Player, gameEvent.ManaAmount));
					break;

				case GameEventType.RoundChanged:
					EnqueueProcess(gameEvent, Promise.Create(r =>
					{
						_fieldView.Players[0].SetAsRoundOwner(gameEvent.RoundNumber % 2 == 0);
						_fieldView.Players[1].SetAsRoundOwner(gameEvent.RoundNumber % 2 == 1);
						r.Resolve();
					}));
					EnqueueProcess(gameEvent, _fieldView.Round.ChangeRound(gameEvent.RoundNumber));
					break;

				case GameEventType.PlayerLost:
					_fieldView.EndPopup.Show(gameEvent.Player.Id != _selfPlayerData.Id);
					break;

				case GameEventType.CardsMatched:
					break;

				case GameEventType.CardsReplaced:
					var cards = gameEvent.Cards;
					foreach (var card in cards)
						EnqueueProcess(gameEvent, _cardViews[card.Id].Reshuffle(card));
					break;
			}
		}

		private void EnqueueProcess(GameEvent gameEvent, Promise promise)
		{
			var process = new EventVisualisationProcess(gameEvent, promise);
			_gameVisualisationQueue.Enqueue(process);
		}

		private void InitializePlayers(PlayerData[] players)
		{
			_selfPlayerData = players[0];
			_fieldView.Players[0].SetData(players[0]);
			_fieldView.Players[0].SetAsRoundOwner(true);

			_fieldView.Players[1].SetData(players[1]);
			_fieldView.Players[1].SetAsRoundOwner(false);

			_playerViews = new Dictionary<string, PlayerView>();
			_playerViews.Add(players[0].Id, _fieldView.Players[0]);
			_playerViews.Add(players[1].Id, _fieldView.Players[1]);
		}

		private void InitializeField(int cardsCount)
		{
			_fieldView.EndPopup.OnAllowRematch += () => _gameStateModifier.AllowGameRestart(_selfPlayerData);

			foreach (var cardView in _cardViews?.Values)
				UnityEngine.GameObject.Destroy(cardView.gameObject);

			_cardViews = Enumerable.Range(0, cardsCount)
				.Select(x => _fieldView.SpawnCard(x))
				.Do(x => x.OnClick += OnCardClicked)
				.ToDictionary(x => x.CardIndex);
		}

		private void OnCardClicked(int cardIndex)
		{
			_gameStateModifier.OpenCard(_selfPlayerData, cardIndex);
		}
	}
}