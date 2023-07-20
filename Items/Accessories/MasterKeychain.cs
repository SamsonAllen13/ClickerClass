using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class MasterKeychain : ClickerItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 50, 0);
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accHotKeychain = true;
			clickerPlayer.EnableClickEffect(ClickEffect.ClearKeychain);
			clickerPlayer.EnableClickEffect(ClickEffect.StickyKeychain);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<StickyKeychain>(), 1).AddIngredient(ModContent.ItemType<ClearKeychain>(), 1).AddIngredient(ModContent.ItemType<HotKeychain>(), 1).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
