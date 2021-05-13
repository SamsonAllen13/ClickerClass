using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TitaniumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.RazorsEdge = ClickerSystem.RegisterClickEffect(mod, "RazorsEdge", null, null, 12, new Color(150, 150, 150), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int index = 0; index < 5; index++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<TitaniumClickerPro>(), (int)(damage * 0.75f), 0f, player.whoAmI, index, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 3.25f);
			SetColor(item, new Color(150, 150, 150));
			SetDust(item, 146);
			AddEffect(item, ClickEffect.RazorsEdge);

			item.damage = 44;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 161000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TitaniumBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
