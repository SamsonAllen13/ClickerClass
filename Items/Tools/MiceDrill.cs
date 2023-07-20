using ClickerClass.Projectiles;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	public class MiceDrill : ModItem
	{
		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			ItemID.Sets.IsDrill[Type] = true; // Needs to be mirrored in SetDefaults as the vanilla check for this runs before modded items

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));
			}
		}

		public override void SetDefaults()
		{
			//Stats copied from vanilla lunar fragment drills
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
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 7, 0, 0);
			Item.tileBoost += 3;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shootSpeed = 32f;
			Item.shoot = ModContent.ProjectileType<MiceDrillPro>();

			//IsDrill/IsChainsaw application:
			Item.useTime = (int)(Item.useTime * 0.6f);
			if (Item.useTime < 1)
			{

				Item.useTime = 1;
			}

			Item.useAnimation = (int)(Item.useAnimation * 0.6f);
			if (Item.useAnimation < 1)
			{
				Item.useAnimation = 1;
			}

			Item.tileBoost--;
			//End
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
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 12).AddIngredient(ItemID.LunarBar, 10).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
