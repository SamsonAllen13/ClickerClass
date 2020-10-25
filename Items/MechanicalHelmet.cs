using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	[AutoloadEquip(EquipType.Head)]
	public class MechanicalHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Helmet");
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

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MechanicalSuit") && legs.type == mod.ItemType("MechanicalBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Right click to place a 'mechanical sensor', which functions as a secondary clicker radius";
			player.GetModPlayer<ClickerPlayer>().clickerMechSet = true;
		}
	}
}