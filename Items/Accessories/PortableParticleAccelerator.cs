using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items.Accessories
{
	public class PortableParticleAccelerator : ClickerItem
	{
		public static readonly int InnerRadiusRatio = 50;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(InnerRadiusRatio);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 0, 90, 0);
			Item.rare = ItemRarityID.Pink;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accPortableParticleAccelerator = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HallowedBar, 8).AddIngredient(ItemID.Cog, 10).AddIngredient(ItemID.Wire, 25).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
