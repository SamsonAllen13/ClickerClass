using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HighTechClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				thoriumMod.Call("AddMartianItemID", Item.type);
			}

			ClickEffect.Discharge = ClickerSystem.RegisterClickEffect(Mod, "Discharge", null, null, 10, new Color(75, 255, 200), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < 4; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<HighTechClickerPro>(), damage, 0f, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(75, 255, 200));
			SetDust(Item, 226);
			AddEffect(Item, ClickEffect.Discharge);

			Item.damage = 86;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = 8;
		}
	}
}
