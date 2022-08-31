using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.HandsOn)]
	public class ClickingGlove : ClickerItem
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
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accClickingGlove = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Silk, 15).AddTile(TileID.Anvils).Register();
		}
	}
}
