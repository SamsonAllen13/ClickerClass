using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class AncientClickingGlove : ClickerItem
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
			Item.value = Item.sellPrice(0, 0, 70, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accAncientClickingGlove = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<ClickingGlove>(), 1).AddIngredient(ItemID.AncientCloth, 3).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
