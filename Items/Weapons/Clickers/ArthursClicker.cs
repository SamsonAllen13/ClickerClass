using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ArthursClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.HolyNova = ClickerSystem.RegisterClickEffect(Mod, "HolyNova", null, null, 12, new Color(255, 225, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy() && target.DistanceSQ(Main.MouseWorld) < 350 * 350 && Collision.CanHit(target.Center, 1, 1, Main.MouseWorld, 1, 1))
					{
						Vector2 vector = target.Center - Main.MouseWorld;
						float speed = 8f;
						float mag = vector.Length();
						if (mag > speed)
						{
							mag = speed / mag;
							vector *= mag;
						}
						Projectile.NewProjectile(source, target.Center, Vector2.Zero, ModContent.ProjectileType<ArthursClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI);
					}
				}

				//Can't properly offload this into projectiles as the visuals spawn on the cursor, but projectiles don't
				SoundEngine.PlaySound(SoundID.NPCHit, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 5);

				float max = 100f;
				int i = 0;
				while (i < max)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max)) * new Vector2(2f, 2f);
					vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
					int index = Dust.NewDust(Main.MouseWorld, 0, 0, 87, 0f, 0f, 0, default(Color), 2f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.position = Main.MouseWorld + vector12;
					dust.velocity = vector12.SafeNormalize(Vector2.UnitY) * 15f;
					i++;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.5f);
			SetColor(Item, new Color(255, 225, 0));
			SetDust(Item, 87);
			AddEffect(Item, ClickEffect.HolyNova);

			Item.damage = 36;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 210000;
			Item.rare = 5;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HallowedBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
