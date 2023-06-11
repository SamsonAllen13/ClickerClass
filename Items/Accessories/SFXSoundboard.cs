using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class SFXSoundboard : SFXButtonBase
	{
		public static void PlaySound(int stack)
		{
			//Random item sound
			SoundStyle style = new SoundStyle("Terraria/Sounds/Item_" + Main.rand.Next(1, 101)) with { PitchVariance = .5f, };
			SoundEngine.PlaySound(style);
		}

		public override LocalizedText Tooltip => this.GetLocalization("Tooltip");

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickerSystem.RegisterSFXButton(this, PlaySound);
		}

		public sealed override void SafeSetDefaults()
		{
			Item.maxStack = 1;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;
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
