using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface IPlayerDamagedEvent
	{
		PlayerData Player { get; }
		int DamageAmount { get; }
	}
}
