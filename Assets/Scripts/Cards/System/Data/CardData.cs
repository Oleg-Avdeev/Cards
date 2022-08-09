
namespace Witches.Cards.System.Data
{
	public enum CardType 
	{
		Attack, 
		Healing,
		Magic,
	}

	public struct CardData 
	{
		public int Id;
		public int Strength;
		public CardType Type;
	}
}