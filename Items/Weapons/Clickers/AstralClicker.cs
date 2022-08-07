using System;
using ClickerClass.Dusts;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using ClickerClass.Utilities;
using ClickerClass.DrawLayers;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AstralClicker : ClickerWeapon
	{
		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Spiral = ClickerSystem.RegisterClickEffect(Mod, "Spiral", null, null, 15, new Color(150, 150, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<AstralClickerPro>(), (int)(damage * 4f), 0f, player.whoAmI);
			});

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));

				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = glowmask.Value,
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 50) * 0.7f
				});
			}
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(150, 150, 225));
			SetDust(Item, ModContent.DustType<MiceDust>());
			AddEffect(Item, ClickEffect.Spiral);

			Item.damage = 90;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = 10;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value.Value, new Color(255, 255, 255, 50) * 0.7f, rotation, scale);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 18).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
