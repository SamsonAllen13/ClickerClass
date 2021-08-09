using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.NPCs
{
	public class ClickerGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool gouge = false;
		public bool frozen = false;
		public bool honeySlow = false;
		public bool embrittle = false;
		public bool stunned = false;

		public bool buffImmunitiesSet = false;

		private void SetBossBuffImmunities(NPC npc)
		{
			//Automatically make all bosses give immunities to the needed buffs
			if (!buffImmunitiesSet)
			{
				//This bool is important. As the IsBossOrRelated method includes a dynamic IsChild check which only works when npc.realLife is set,
				//The code in SetDefaults will not work. This method is therefore called in PostAI (which is guaranteed to run)
				buffImmunitiesSet = true;

				if (npc.active && npc.immortal || npc.IsBossOrRelated())
				{
					foreach (var buff in ClickerClass.BossBuffImmunity)
					{
						npc.buffImmune[buff] = true;
					}
				}
			}
		}

		public override void ResetEffects(NPC npc)
		{
			gouge = false;
			frozen = false;
			honeySlow = false;
			embrittle = false;
			stunned = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (gouge)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 60;
				damage = 10;
			}
			if (honeySlow)
			{
				npc.velocity.X *= 0.75f;
				npc.velocity.Y += !npc.noTileCollide ? 0.10f : 0.01f;
			}
			if (frozen)
			{
				npc.position = npc.oldPosition;
				npc.frameCounter = 0;
			}
			if (stunned)
			{
				npc.position = npc.oldPosition;
				npc.frameCounter = 0;
			}
		}

		public override void NPCLoot(NPC npc)
		{
			if (npc.type == NPCID.GoblinSorcerer && npc.value > 0f)
			{
				if (Main.rand.NextBool(20))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<ShadowyClicker>(), 1, false, -1);
				}
			}
			if ((npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing) && npc.value > 0f)
			{
				if (Main.rand.NextBool(25))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<EclipticClicker>(), 1, false, -1);
				}
			}
			if ((npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler) && npc.value > 0f)
			{
				if (Main.rand.NextBool(25))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<HemoClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.DarkCaster)
			{
				if (Main.rand.NextBool(15))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<Milk>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.Gastropod)
			{
				if (Main.rand.NextBool(20))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<ChocolateChip>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.SandElemental)
			{
				if (Main.rand.NextBool())
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<SandstormClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.IceGolem)
			{
				if (Main.rand.NextBool())
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<BlizzardClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.PirateCaptain)
			{
				if (Main.rand.NextBool(8))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<CaptainsClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.PirateShip)
			{
				if (Main.rand.NextBool(4))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<GoldenTicket>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.Pumpking)
			{
				if (Main.rand.NextBool(10))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<WitchClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.IceQueen)
			{
				if (Main.rand.NextBool(10))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<FrozenClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.MartianSaucerCore)
			{
				if (Main.rand.NextBool(4))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<HighTechClicker>(), 1, false, -1);
				}
			}
			if (!Main.expertMode)
			{
				if (npc.type == NPCID.MoonLordCore)
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<LordsClicker>(), 1, false, -1);

					if (Main.rand.NextBool(5))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<TheClicker>(), 1, false, -1);
					}
				}
				if (npc.type == NPCID.WallofFlesh)
				{
					if (Main.rand.NextBool(4))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<ClickerEmblem>(), 1, false, -1);
					}
				}
				if (npc.type == NPCID.KingSlime)
				{
					if (Main.rand.NextBool(4))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<StickyKeychain>(), 1, false, -1);
					}
				}
			}
			if (npc.type == NPCID.LunarTowerStardust || npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerVortex || npc.type == NPCID.LunarTowerNebula)
			{
				if (Main.expertMode)
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<MiceFragment>(), Main.rand.Next(5, 23));
				}
				else
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<MiceFragment>(), Main.rand.Next(3, 16));
				}
			}
		}

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			switch (type)
			{
				case NPCID.Merchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Cookie>());
					break;
				case NPCID.Demolitionist:
					if (Main.hardMode)
					{
						shop.item[nextSlot++].SetDefaults(ModContent.ItemType<BigRedButton>());
					}
					break;
				case NPCID.TravellingMerchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Soda>());
					break;
				case NPCID.Mechanic:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<HandCream>());
					break;
				case NPCID.GoblinTinkerer:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<MousePad>());
					break;
				case NPCID.SkeletonMerchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CandleClicker>());
					break;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (gouge)
			{
				if (!Main.rand.NextBool(4))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2.2f);
					Dust dust2 = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 54, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.6f);
					dust.noGravity = true;
					dust.velocity *= 1.2f;
					dust.velocity.Y -= 0.7f;
					dust2.noGravity = true;
					dust2.velocity *= 1.2f;
					dust2.velocity.Y -= 0.7f;
					if (Main.rand.NextBool(4))
					{
						dust.noGravity = false;
						dust.scale *= 0.3f;
						dust2.noGravity = false;
						dust2.scale *= 0.3f;
					}
				}
				drawColor.G = (byte)(drawColor.G * 0.35f);
				drawColor.B = (byte)(drawColor.B * 0.15f);
			}
			if (honeySlow)
			{
				if (Main.rand.NextFloat() < 0.66f)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 153, 0f, Main.rand.NextFloat(0.25f, 0.75f), 50, default(Color), 1.35f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].fadeIn = 1.1f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (frozen)
			{
				if (!Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 111, 0f, 0f, 0, default(Color), 0.75f);
					dust.noGravity = true;
					dust.velocity *= 0f;
					dust.fadeIn = 1.75f;
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.6f);
				drawColor.R = (byte)(drawColor.R * 0.35f);
				drawColor.G = (byte)(drawColor.G * 0.65f);
			}
			if (embrittle)
			{
				if (!Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 57, 0f, 0f, 255, default(Color), 0.65f);
					dust.shader = GameShaders.Armor.GetSecondaryShader(20, Main.LocalPlayer);
					dust.noGravity = true;
					dust.velocity *= 0f;
					dust.fadeIn = 1.75f;
				}
				drawColor.R = (byte)(drawColor.R * 1f);
				drawColor.G = (byte)(drawColor.G * 1f);
				drawColor.B = (byte)(drawColor.B * 1f);
			}
		}

		public override void PostAI(NPC npc)
		{
			SetBossBuffImmunities(npc);
		}
	}
}
