using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ShadowLash = ClickerSystem.RegisterClickEffect(mod, "ShadowLash", null, null, 10, new Color(150, 100, 255), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 5; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<UmbralClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.75f);
			SetColor(item, new Color(150, 100, 255));
			SetDust(item, 27);
			AddEffect(item, ClickEffect.ShadowLash);

			item.damage = 20;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 200000;
			item.rare = 3;
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
