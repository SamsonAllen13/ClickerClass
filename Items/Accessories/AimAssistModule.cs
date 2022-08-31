using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class AimAssistModule : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accAimbotModule = true;
		}
	}
}
