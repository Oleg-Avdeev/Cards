using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface IPlayerHealedEvent
	{
		PlayerData Player { get; }
		int HealingAmount { get; }
	}
}
