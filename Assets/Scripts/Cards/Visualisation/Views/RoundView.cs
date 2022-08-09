using UnityEngine;
using UnityEngine.UI;
using Witches.Cards.Visualisation.Promises;

namespace Witches.Cards.Visualisation.Views
{
	public class RoundView : MonoBehaviour
	{
		[SerializeField] private Text RoundNumber;

		public Promise ChangeRound(int roundNumber)
		{
			return Promise.Create((resolver) => {
				RoundNumber.text = roundNumber.ToString();
				resolver.Resolve();
			});
		}
	}
}