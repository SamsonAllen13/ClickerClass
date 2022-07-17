using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class RegalClickingGlove : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetAccessoryType(Item, ClickerAccessoryType.ClickingGlove);
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 35000;
			Item.rare = 6;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accRegalClickingGlove = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<AncientClickingGlove>(), 1).AddIngredient(ItemID.HallowedBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
