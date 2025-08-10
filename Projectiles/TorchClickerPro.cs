using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class TorchClickerPro : ClickerProjectile
	{
		public int Timer
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public bool Spawned
		{
			get => Projectile.localAI[0] == 1f;
			set => Projectile.localAI[0] = value ? 1f : 0f;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Arcane);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Fire);
			MoRSupportHelper.RegisterElement(Projectile, MoRSupportHelper.Holy);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			getTorchInfo(Main.player[Projectile.owner], out int dustType, out Color dustColor, out float dustScale);

			int index = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, dustColor, dustScale);
			Dust dust = Main.dust[index];
			dust.position.X = Projectile.Center.X;
			dust.position.Y = Projectile.Center.Y;
			dust.velocity *= 0f;
			dust.noGravity = true;

			if (!Spawned)
			{
				Spawned = true;

				float max = 30f;
				int i = 0;
				while (i < max)
				{
					Vector2 vector2 = Vector2.Zero;
					vector2 += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / max)) * new Vector2(2f, 2f);
					vector2 = vector2.RotatedBy(Projectile.velocity.ToRotation(), default(Vector2));
					int index2 = Dust.NewDust(Projectile.Center, 0, 0, dustType, 0f, 0f, 0, dustColor, dustScale);
					Dust dust2 = Main.dust[index2];
					dust2.noGravity = true;
					dust2.position = Projectile.Center + vector2;
					dust2.velocity = vector2.SafeNormalize(Vector2.UnitY) * 2f;
					i++;
				}
			}
		}

		private void getTorchInfo(Player player, out int dustType, out Color dustColor, out float dustScale)
		{
			dustType = DustID.Torch;
			dustColor = default(Color);
			dustScale = 1.5f;

			// Modded biomes
			if (ThoriumModSupportHelper.InAquaticDepths(player))
			{
				dustType = DustID.GemSapphire;
				dustScale = 1f;
				return;
			}
			if (CalamityModSupportHelper.InBiome(player, "abyss"))
			{
				dustType = DustID.RainbowTorch;
				dustColor = new Color(100, 130, 150);
				dustScale = 1f;
				return;
			}
			if (CalamityModSupportHelper.InBiome(player, "astral infection"))
			{
				dustType = DustID.RainbowTorch;
				dustColor = new Color(255, 95, 48);
				dustScale = 1f;
				return;
			}
			if (CalamityModSupportHelper.InBiome(player, "brimstone crags"))
			{
				dustType = DustID.RainbowTorch;
				dustColor = new Color(190, 255, 60);
				dustScale = 1f;
				return;
			}
			if (CalamityModSupportHelper.InBiome(player, "sulphurous sea"))
			{
				dustType = DustID.RainbowTorch;
				dustColor = new Color(190, 255, 60);
				dustScale = 1f;
				return;
			}
			if (CalamityModSupportHelper.InBiome(player, "sunken sea"))
			{
				dustType = DustID.RainbowTorch;
				dustColor = new Color(170, 255, 255);
				dustScale = 1f;
				return;
			}
			if (MoRSupportHelper.InWasteland(player))
			{
				dustType = ModLoader.GetMod("Redemption").TryFind<ModDust>("WastelandTorchDust", out ModDust wastelandDust) ? wastelandDust.Type : DustID.Torch;
			}

			// Vanilla biomes
			if (player.ZoneShimmer)
			{
				dustType = DustID.ShimmerTorch;
				return;
			}
			if (player.ZoneHallow)
			{
				if (player.ZoneRockLayerHeight)
				{
					dustType = DustID.HallowedTorch;
					return;
				}
				else
				{
					dustType = DustID.RainbowTorch;
					dustColor = Main.DiscoColor;
					dustScale = 1f;
					return;
				}
			}
			else if (player.ZoneCorrupt)
			{
				if (player.ZoneRockLayerHeight)
				{
					dustType = DustID.CursedTorch;
					return;
				}
				else
				{
					dustType = DustID.CorruptTorch;
					return;
				}
			}
			else if (player.ZoneCrimson)
			{
				if (player.ZoneRockLayerHeight)
				{
					dustType = DustID.IchorTorch;
					return;
				}
				else
				{
					dustType = DustID.CrimsonTorch;
					return;
				}
			}
			else if (player.ZoneDungeon)
			{
				dustType = DustID.BoneTorch;
				return;
			}
			else if (player.ZoneUnderworldHeight)
			{
				dustType = DustID.DemonTorch;
				return;
			}
			else if (player.ZoneGlowshroom)
			{
				dustType = DustID.MushroomTorch;
				return;
			}
			else if (player.ZoneJungle || player.ZoneLihzhardTemple)
			{
				dustType = DustID.JungleTorch;
				return;
			}
			else if (player.ZoneUndergroundDesert || player.ZoneDesert)
			{
				dustType = DustID.DesertTorch;
				return;
			}
			else if (player.ZoneSnow)
			{
				dustType = DustID.IceTorch;
				return;
			}
			else if (player.ZoneBeach)
			{
				dustType = DustID.CoralTorch;
				return;
			}
		}
	}
}
