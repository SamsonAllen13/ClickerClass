using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LihzahrdClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.SolarFlare = ClickerSystem.RegisterClickEffect(Mod, "SolarFlare", null, null, 10, new Color(200, 75, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<LihzarhdClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 4f);
			SetColor(Item, new Color(200, 75, 0));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.SolarFlare);

			Item.damage = 56;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 300000;
			Item.rare = 7;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LunarTabletFragment, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
