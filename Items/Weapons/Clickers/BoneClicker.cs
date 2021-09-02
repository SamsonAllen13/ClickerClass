using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BoneClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Lacerate = ClickerSystem.RegisterClickEffect(Mod, "Lacerate", null, null, 12, new Color(225, 225, 200), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<BoneClickerPro>(), damage, knockBack, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.1f);
			SetColor(Item, new Color(225, 225, 200));
			SetDust(Item, 216);
			AddEffect(Item, ClickEffect.Lacerate);

			Item.damage = 8;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.noMelee = true;
			Item.value = 15000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.FossilOre, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
