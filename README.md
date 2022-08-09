# Cards! — A Very Reliable PvP Card Game Implementation

## Server — Clients

## Game State and Functions

- void ConnectToGame(int playerIndex);
- void OpenCard(PlayerData requestingPlayer, int cardId);
- void ActivateAbility(PlayerData requestingPlayer);
- void AllowGameRestart(PlayerData requestingPlayer);

## Side Effects and UI

GameEventType.cs:

- GameStarted,
- RoundChanged,
- CardOpened,
- CardClosed,
- CardsReplaced,
- CardsMatched,
- PlayerDamaged,
- PlayerHealed,
- PlayerMana,
- PlayerLost,