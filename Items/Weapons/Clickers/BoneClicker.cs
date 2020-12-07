using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ClickerClass.Items.Weapons.Clickers
{
	public class BoneClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Bone Clicker");

			ClickEffect.Lacerate = ClickerSystem.RegisterClickEffect(mod, "Lacerate", "Lacerate", "Inflicts the Gouge debuff, dealing 30 damage per second", 12, new Color(225, 225, 200, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 71);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<BoneClickerPro>(), damage, knockBack, player.whoAmI);
				for (int k = 0; k < 10; k++)
				{
					Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 5, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.25f);
					dust.noGravity = true;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.1f);
			SetColor(item, new Color(225, 225, 200, 0));
			SetDust(item, 216);
			AddEffect(item, ClickEffect.Lacerate);

			item.damage = 13;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.noMelee = true;
			item.value = 15000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilOre, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
