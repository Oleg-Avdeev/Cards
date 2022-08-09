using UnityEngine;
using UnityEngine.UI;
using Witches.Cards.System.Data;
using Witches.Cards.Visualisation.Promises;

namespace Witches.Cards.Visualisation.Views
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField] private Text HealthText;
		[SerializeField] private Text ManaText;
		[SerializeField] private GameObject IsOwnerObject;

		public void SetData(PlayerData playerData)
		{
			HealthText.text = playerData.Health.ToString();
			ManaText.text = playerData.Mana.ToString();
		}

		public void SetAsRoundOwner(bool isOwner)
		{
			IsOwnerObject.SetActive(isOwner);
		}

		public Promise ReceiveDamage(PlayerData playerData, int damage)
		{
			return Promise.Create((resolver) =>
			{
				HealthText.text = playerData.Health.ToString();
				resolver.Resolve();
			});
		}

		public Promise ReceiveHealing(PlayerData playerData, int healing)
		{
			return Promise.Create((resolver) =>
			{
				HealthText.text = playerData.Health.ToString();
				resolver.Resolve();
			});
		}

		public Promise ReceiveMana(PlayerData playerData, int amount)
		{
			return Promise.Create((resolver) =>
			{
				ManaText.text = playerData.Mana.ToString();
				resolver.Resolve();
			});
		}

		public Promise SpendMana(PlayerData playerData, int amount)
		{
			return Promise.Create((resolver) =>
			{
				ManaText.text = playerData.Mana.ToString();
				resolver.Resolve();
			});
		}
	}
}