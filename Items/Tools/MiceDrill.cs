using ClickerClass.Projectiles;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	public class MiceDrill : ModItem
	{
		public static Asset<Texture2D> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			ItemID.Sets.IsDrill[Type] = true; //This causes the following changes:
			/*
			 * useTime = (int)((double)useTime * 0.6);
				if (useTime < 1)
					useTime = 1;

				useAnimation = (int)((double)useAnimation * 0.6);
				if (useAnimation < 1)
					useAnimation = 1;

				tileBoost--;
			 */


			if (!Main.dedServ)
			{
				glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
			}

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 54;
			Item.height = 26;
			Item.DamageType = DamageClass.Melee;
			Item.damage = 50;
			Item.knockBack = 0f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 25;
			Item.useTime = 4;
			Item.pick = 225;
			Item.UseSound = SoundID.Item23;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 7, 0, 0);
			Item.tileBoost += 3;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shootSpeed = 32f;
			Item.shoot = ModContent.ProjectileType<MiceDrillPro>();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, new Color(255, 255, 255, 0) * 0.8f, rotation, scale);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 12).AddIngredient(ItemID.LunarBar, 10).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
