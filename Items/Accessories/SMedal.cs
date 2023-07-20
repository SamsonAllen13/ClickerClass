using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class SMedal : ClickerItem
	{
		public static readonly int ChargeMeterMax = 200;
		public static readonly int ChargeMeterStep = 20;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accSMedalItem = Item;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<AMedal>(), 1).AddIngredient(ModContent.ItemType<FMedal>(), 1).AddIngredient(ItemID.SoulofFright, 6).AddIngredient(ItemID.SoulofSight, 6).AddIngredient(ItemID.SoulofMight, 6).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
