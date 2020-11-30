using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MythrilClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mythril Clicker");

			ClickEffect.Embrittle = ClickerSystem.RegisterClickEffect(mod, "Embrittle", "Embrittle", "Inflicts the Embrittle debuff, increasing the amount of damage enemies take from clickers by a flat 8", 10, new Color(125, 225, 125, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 101);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<MythrilClickerPro>(), 0, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.95f);
			SetColor(item, new Color(125, 225, 125, 0));
			SetDust(item, 49);
			AddEffect(item, ClickEffect.Embrittle);

			item.damage = 25;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 103500;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MythrilBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
