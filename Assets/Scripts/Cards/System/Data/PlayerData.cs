namespace Witches.Cards.System.Data
{
	public struct PlayerData
	{
		public string Id;
		public string Name;
		public bool Connected;
		public int PlayingOrder;
		
		public int Health;
		public int Mana;
		
		public int MaxHealth;
		public int MaxMana;

		public bool AllowedRestart;
	}
}