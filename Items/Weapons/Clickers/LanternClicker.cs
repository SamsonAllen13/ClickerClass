using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LanternClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Incinerate = ClickerSystem.RegisterClickEffect(Mod, "Incinerate", null, null, 15, new Color(255, 155, 65), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 8; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(3f, 6f), ModContent.ProjectileType<LanternClickerPro>(), (int)(damage * 0.5f), 1f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.9f);
			SetColor(Item, new Color(200, 125, 75));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.Incinerate);

			Item.damage = 60;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
		}
		
		//TODO Diver - Make this drop from Mourning Wood 10%
	}
}
