using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CosmicClicker : ClickerWeapon
	{
		public static readonly int ShadowAmount = 12;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Celestial, projsInheritItemElements: true);
			
			ClickEffect.CosmicFlux = ClickerSystem.RegisterClickEffect(Mod, "CosmicFlux", 6, () => Color.Lerp(Color.White, new Color(Main.DiscoR, 0, Main.DiscoB), 0.5f), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				
				switch (clickerPlayer.itemCosmicClickerStage)
				{
					case 0:
						{
							Vector2 pos = position;

							int index = -1;
							for (int i = 0; i < Main.maxNPCs; i++)
							{
								NPC npc = Main.npc[i];
								if (npc.active && npc.CanBeChasedBy() && npc.DistanceSQ(pos) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
								{
									index = i;
								}
							}
							Vector2 vector = index == -1 ? pos - player.Center : Main.npc[index].Center - pos;
							
							float speed = 6f;
							float mag = vector.Length();
							if (mag > speed)
							{
								mag = speed / mag;
								vector *= mag;
							}
							
							const float numberProjectiles = 3;
							float rotation = MathHelper.ToRadians(15);
							position += Vector2.Normalize(vector) * 15f;

							for (int i = 0; i < numberProjectiles; i++)
							{
								Vector2 perturbedSpeed = vector.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
								Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CosmicClickerPro>(), damage, knockBack, player.whoAmI);
							}
						}
						break;
					case 1:
						{
							Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<CosmicClickerPro2>(), damage, knockBack, player.whoAmI);
						}
						break;
					case 2:
						{
							bool spawnEffects = true;
							float num102 = 12f;
							int num103 = 0;
							
							while ((float)num103 < num102)
							{
								Vector2 vector12 = Vector2.UnitX * 0f;
								vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(10f, 10f);
								
								float hasSpawnEffects = spawnEffects ? 1f : 0f;
								Projectile.NewProjectile(source, position + vector12, vector12.SafeNormalize(Vector2.UnitY) * 10f, ModContent.ProjectileType<CosmicClickerPro4>(), (int)(damage * 0.5f), knockBack, player.whoAmI, hasSpawnEffects);
								spawnEffects = false;
								
								int num = num103;
								num103 = num + 1;
							}
						}
						break;
				}
				
				clickerPlayer.itemCosmicClickerStage++;
				if (clickerPlayer.itemCosmicClickerStage > 2)
				{
					clickerPlayer.itemCosmicClickerStage = 0;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6.15f);
			SetColor(Item, new Color(175, 220, 255));
			SetDust(Item, 221);
			AddEffect(Item, ClickEffect.CosmicFlux);

			Item.damage = 88;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
