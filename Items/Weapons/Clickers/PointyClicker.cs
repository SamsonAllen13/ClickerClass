using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PointyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StingingThorn = ClickerSystem.RegisterClickEffect(mod, "StingingThorn", null, null, 8, new Color(100, 175, 75), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Vector2 pos = Main.MouseWorld;

				int index = -1;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.CanBeChasedBy() && npc.DistanceSQ(pos) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					Vector2 vector = Main.npc[index].Center - pos;
					float speed = 3f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
					}
					vector *= mag;
					Projectile.NewProjectile(pos, vector, ModContent.ProjectileType<PointyClickerPro>(), damage, knockBack, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.35f);
			SetColor(item, new Color(100, 175, 75));
			SetDust(item, 39);
			AddEffect(item, ClickEffect.StingingThorn);

			item.damage = 8;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 27000;
			item.rare = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddIngredient(ItemID.Stinger, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
