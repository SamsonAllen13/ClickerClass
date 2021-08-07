using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	public class MiceHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (!Main.dedServ)
			{
				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 7;
			Item.useAnimation = 28;
			Item.tileBoost = 4;
			Item.axe = 30;
			Item.hammer = 100;
			Item.useStyle = 1;
			Item.knockBack = 7f;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 14).AddIngredient(ItemID.LunarBar, 12).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
