using ClickerClass.DrawLayers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class RainbowClicker : ClickerWeapon
	{
		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Celestial, projsInheritItemElements: true);

			ClickEffect.Rainbolt = ClickerSystem.RegisterClickEffect(Mod, "Rainbolt", 8, () => Color.Lerp(Color.White, Main.DiscoColor, 0.5f), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				// Rainbow Rod sparkles
				ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.RainbowRodHit, new ParticleOrchestraSettings
				{
					PositionInWorld = position
				}, player.whoAmI);

				bool spawnEffects = true;
				for (int k = 0; k < 4; k++)
				{
					//Projectile.NewProjectile(source, position, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)), ModContent.ProjectileType<RainbowClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI, hasSpawnEffects);

					Vector2 spawnVelocity = Main.rand.NextVector2Circular(1f, 1f) + Main.rand.NextVector2CircularEdge(3f, 3f);
					int projType = ProjectileID.FairyQueenMagicItemShot; //This projectile decreases damage by 20% each hit, fixed homing speed of 30
					float targetIndex = spawnEffects ? -2f : -1f; //Normally -1f, we intercept it
					float randomColor = player.miscCounterNormalized % 1f;
					Projectile.NewProjectile(source, position, spawnVelocity, projType, (int)(damage * 0.5f), knockBack, player.whoAmI, targetIndex, randomColor);
					spawnEffects = false;
				}
			});

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));

				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = glowmask.Value,
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 50) * 0.75f
				});
			}
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6.15f);
			SetColor(Item, new Color(255, 255, 255));
			SetDust(Item, 91);
			AddEffect(Item, ClickEffect.Rainbolt);

			Item.damage = 80;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value.Value, new Color(255, 255, 255, 50) * 0.75f, rotation, scale);
		}
	}

	public class RainbowClickerVanillaAdjustment : GlobalProjectile
	{
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == ProjectileID.FairyQueenMagicItemShot;
		}

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (source is not EntitySource_ItemUse_WithAmmo itemSource || !ClickerSystem.IsClickerItem(itemSource.Item))
			{
				return;
			}

			projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
			projectile.penetrate = -1; //3 by default
			projectile.timeLeft = 190; //240 by default, this controls its behavior: 140 starts homing
		}

		public override bool PreAI(Projectile projectile)
		{
			if (projectile.ai[0] == -2f)
			{
				projectile.ai[0] = -1f; //Initializer for no target
				SoundEngine.PlaySound(SoundID.Item43, projectile.Center);
			}
			return base.PreAI(projectile);
		}
	}
}
