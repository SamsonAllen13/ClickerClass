using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class SFXSoundboard : SFXButtonBase
	{
		public override bool UsesCommonTooltip => false;

		public sealed override void SafeSetDefaults()
		{
			Item.maxStack = 1;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}
		
		public override void UpdateInventory(Player player) 
		{
			player.GetModPlayer<ClickerPlayer>().accSFXButtonSoundboard = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accSFXButtonSoundboard = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<SFXButtonA>(), 1).AddIngredient(ModContent.ItemType<SFXButtonB>(), 1)
						   .AddIngredient(ModContent.ItemType<SFXButtonC>(), 1).AddIngredient(ModContent.ItemType<SFXButtonD>(), 1)
						   .AddIngredient(ModContent.ItemType<SFXButtonE>(), 1).AddIngredient(ModContent.ItemType<SFXButtonF>(), 1)
						   .AddIngredient(ModContent.ItemType<SFXButtonG>(), 1).AddIngredient(ModContent.ItemType<SFXButtonH>(), 1)
			.AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
