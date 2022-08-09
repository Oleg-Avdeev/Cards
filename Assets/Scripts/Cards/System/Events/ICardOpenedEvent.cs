using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface ICardOpenedEvent
	{
		CardData Card { get; }
	}
}
