using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	[AutoloadEquip(EquipType.Body)]
	public class MechanicalSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Suit");
			Tooltip.SetDefault("?");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 18;
			item.height = 18;
			item.value = 0000;
			item.rare = 3;
			item.defense = 0;
		}

		public override void UpdateEquip(Player player)
		{
			
		}
	}
}