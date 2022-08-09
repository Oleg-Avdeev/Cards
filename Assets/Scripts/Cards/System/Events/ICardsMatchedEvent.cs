using System;
using UnityEngine;
using Witches.Cards.System.Data;

namespace Witches.Cards.System
{
	public interface ICardsMatchedEvent
	{
		CardData[] Cards { get; }
	}
}
