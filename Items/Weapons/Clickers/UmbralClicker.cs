using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ShadowLash = ClickerSystem.RegisterClickEffect(Mod, "ShadowLash", null, null, 10, new Color(150, 100, 255), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<UmbralClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(150, 100, 255));
			SetDust(Item, 27);
			AddEffect(Item, ClickEffect.ShadowLash);

			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 200000;
			Item.rare = 3;
		}

		/*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DarkClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SinisterClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		*/
	}
}
