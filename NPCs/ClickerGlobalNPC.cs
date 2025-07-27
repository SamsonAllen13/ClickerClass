using ClickerClass.Buffs;
using ClickerClass.DropRules.DropConditions;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Consumables;
using ClickerClass.Items.Misc;
using ClickerClass.Items.Placeable;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.NPCs
{
	public class ClickerGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool charmed = false;
		public bool crystalSlime = false;
		public bool crystalSlimeFatigue = false;
		public bool embrittle = false;
		public bool frozen = false;
		public bool gouge = false;
		public bool honeySlow = false;
		public bool oozed = false;
		public bool seafoam = false;
		public bool stunned = false;
		public int mouseTrapped = 0;

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
			charmed = false;
			crystalSlime = false;
			crystalSlimeFatigue = false;
			embrittle = false;
			frozen = false;
			gouge = false;
			honeySlow = false;
			oozed = false;
			seafoam = false;
			stunned = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (mouseTrapped > 0)
			{
				if (npc.lifeRegen > 0) { npc.lifeRegen = 0; }
				npc.lifeRegen -= 30 * mouseTrapped;
				damage = 15 * mouseTrapped;
				
				mouseTrapped = 0;
				//Normally one would reset this in ResetEffects but due to how this field is set and update order, it's required to reset here.
				//This means it's not useable for any visuals in PostAI or similar
			}
			if (gouge)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 2 * Gouge.DamageOverTime;
				damage = 10;
			}
			if (seafoam)
			{
				npc.velocity.X *= 0.90f;
				npc.velocity.Y += !npc.noTileCollide ? 0.10f : 0.01f;
			}
			if (honeySlow)
			{
				npc.velocity.X *= 0.75f;
				npc.velocity.Y += !npc.noTileCollide ? 0.10f : 0.01f;
			}
			if (oozed)
			{
				npc.velocity.X *= 0.5f;
				npc.velocity.Y += !npc.noTileCollide ? 0.15f : 0.025f;
			}
			if (frozen || stunned)
			{
				npc.position = npc.oldPosition;
				npc.frameCounter = 0;
			}
		}
		
		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (charmed)
			{
				modifiers.SourceDamage *= 1f - (MindfulClicker.CharmReduction / 100f);
			}
		}

		public override void ModifyGlobalLoot(GlobalLoot globalLoot)
		{
			globalLoot.Add(ItemDropRule.ByCondition(new DungeonKeyCondition(), ModContent.ItemType<DungeonKey>(), 2500));
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			//HOW TO SEE DROP RATES IN THE BESTIARY:
			/*
			 * 1. Get the "GamerMod (Debug Tools)" mod
			 * 2. Enter a world, type "/bestiary" in chat
			 * 3. If it says "done?", leave the world (so that the 100% progress saves on that world)
			 * 4. ???
			 * 5. profit (check bestiary for drops)
			 * 
			 * You need to do this for every world you are using (ideally a normal and an expert world)
			 * You only have to do this once or when new NPCs get added. New drops to existing NPCs do not need redoing the steps
			 */

			//This method is called once when the game loads (per NPC), so you can't make dynamic checks based on world state like "npc.value > 0f" here

			Conditions.NotExpert notExpert = new Conditions.NotExpert();
			Conditions.IsExpert isExpert = new Conditions.IsExpert();

			switch (npc.type)
			{
				/* Biomes */
				#region Snow/Ice biome
				case NPCID.IceMimic:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AimAssistModule>(), 4));
					}
					break;
				#endregion

				#region Glowing Mushroom biome
				case NPCID.SporeSkeleton:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MyceliumClicker>(), 40));
					}
					break;
				#endregion

				#region Dungeon
				case NPCID.DarkCaster:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Milk>(), 15));
					}
					break;
				#endregion

				#region Underworld
				case NPCID.FireImp:
					{
						DropHelper.NPCExpertGetsRerolls(npcLoot, ModContent.ItemType<ImpishClicker>(), 35);
					}
					break;
				#endregion

				#region Hallow
				case NPCID.Gastropod:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChocolateChip>(), 20));
					}
					break;
				#endregion

				#region Graveyard
				case NPCID.MaggotZombie:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TriggerFinger>(), 18));
					}
					break;
				#endregion

				/* Events */
				#region Windy Day
				case NPCID.WindyBalloon:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BalloonClicker>(), 10));
					}
					break;
				#endregion

				#region Rain
				case NPCID.IceGolem:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlizzardClicker>(), 2));
					}
					break;
				#endregion

				#region Sandstorm
				case NPCID.SandElemental:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandstormClicker>(), 2));
					}
					break;
				#endregion

				#region Blood Moon
				case NPCID.BloodZombie:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HemoClicker>(), 25));
					}
					break;
				case NPCID.Drippler: goto case NPCID.BloodZombie;
				case NPCID.BloodNautilus:
					{
						npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SpiralClicker>(), 2, 1));
					}
					break;
				#endregion

				#region Solar Eclipse
				case NPCID.Frankenstein:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EclipticClicker>(), 36));
					}
					break;
				case NPCID.SwampThing: goto case NPCID.Frankenstein;
				#endregion

				#region Goblin Army
				case NPCID.GoblinSorcerer:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowyClicker>(), 20));
					}
					break;
				#endregion

				#region Pirate Invasion
				case NPCID.PirateCaptain:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CaptainsClicker>(), 8));
					}
					break;
				case NPCID.PirateShip:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoldenTicket>(), 4));
					}
					break;
				#endregion

				#region Pumpkin Moon
				case NPCID.MourningWood:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WitchClicker>(), 10));
					}
					break;
				case NPCID.Pumpking:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LanternClicker>(), 10));
					}
					break;
				#endregion

				#region Frost Moon
				case NPCID.SantaNK1:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NaughtyClicker>(), 10));
					}
					break;
				case NPCID.IceQueen:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrozenClicker>(), 10));
					}
					break;
				#endregion

				#region Martian Madness
				case NPCID.MartianSaucerCore:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HighTechClicker>(), 4));
					}
					break;
				#endregion

				#region Old One's Army
				case NPCID.DD2DarkMageT1:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneClicker>(), 5));
					}
					break;
				case NPCID.DD2DarkMageT3: goto case NPCID.DD2DarkMageT1;
				case NPCID.DD2OgreT2:
					{
						npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnottyClicker>(), 5));
					}
					break;
				case NPCID.DD2OgreT3: goto case NPCID.DD2OgreT2;
				case NPCID.DD2Betsy:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<DraconicClicker>(), 4));
					}
					break;
				#endregion

				#region Lunar Events
				case NPCID.LunarTowerSolar:
					{
						LeadingConditionRule notExpertRule = new LeadingConditionRule(notExpert);
						LeadingConditionRule isExpertRule = new LeadingConditionRule(isExpert);
						int miceFragment = ModContent.ItemType<MiceFragment>();

						var dropParametersNormal = new DropOneByOne.Parameters()
						{
							ChanceNumerator = 1,
							ChanceDenominator = 1,
							MinimumStackPerChunkBase = 1,
							MaximumStackPerChunkBase = 1,
							MinimumItemDropsCount = 3,
							MaximumItemDropsCount = 15
						};
						var dropParametersExpert = dropParametersNormal;
						dropParametersExpert.MinimumItemDropsCount = 5;
						dropParametersExpert.MaximumItemDropsCount = 22;

						isExpertRule.OnSuccess(new DropOneByOne(miceFragment, dropParametersExpert));
						notExpertRule.OnSuccess(new DropOneByOne(miceFragment, dropParametersNormal));

						npcLoot.Add(isExpertRule);
						npcLoot.Add(notExpertRule);
					}
					break;
				case NPCID.LunarTowerVortex: goto case NPCID.LunarTowerSolar;
				case NPCID.LunarTowerNebula: goto case NPCID.LunarTowerSolar;
				case NPCID.LunarTowerStardust: goto case NPCID.LunarTowerSolar;
				#endregion

				#region Torch God
				case NPCID.TorchGod:
					{
						LeadingConditionRule neverDropsRule = new LeadingConditionRule(new Conditions.NeverTrue());
						neverDropsRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TorchClicker>()));
						npcLoot.Add(neverDropsRule);
					}
					break;
				#endregion

				/* Bosses */
				#region Bosses
				case NPCID.KingSlime:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<StickyKeychain>(), 4));
					}
					break;
				case NPCID.EyeofCthulhu:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<WatchfulClicker>(), 4));
					}
					break;
				case NPCID.EaterofWorldsHead:
					{
						LeadingConditionRule eowNotExpertRule = new LeadingConditionRule(new Conditions.LegacyHack_IsBossAndNotExpert());
						eowNotExpertRule.OnSuccess(new CommonDrop(ModContent.ItemType<BurrowingClicker>(), 4));
						npcLoot.Add(eowNotExpertRule);
					}
					break;
				case NPCID.EaterofWorldsBody: goto case NPCID.EaterofWorldsHead;
				case NPCID.EaterofWorldsTail: goto case NPCID.EaterofWorldsHead;
				case NPCID.BrainofCthulhu:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<MindfulClicker>(), 4));
					}
					break;
				case NPCID.Deerclops:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<CyclopsClicker>(), 4));
					}
					break;
				case NPCID.QueenBee:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<StingingClicker>(), 4));
					}
					break;
				case NPCID.WallofFlesh:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<BurningSuperDeathClicker>(), 4));
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<ClickerEmblem>(), 4));
					}
					break;
				case NPCID.QueenSlimeBoss:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<ClearKeychain>(), 4));
					}
					break;
				case NPCID.Retinazer:
					{
						LeadingConditionRule notExpertRule = new LeadingConditionRule(notExpert);

						notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.MissingTwin(), ModContent.ItemType<BottomlessBoxofPaperclips>(), 4));

						npcLoot.Add(notExpertRule);
					}
					break;
				case NPCID.Spazmatism: goto case NPCID.Retinazer;
				case NPCID.TheDestroyer:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<BottomlessBoxofPaperclips>(), 4));
					}
					break;
				case NPCID.SkeletronPrime: goto case NPCID.TheDestroyer;
				case NPCID.Plantera:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<FloralClicker>(), 4));
					}
					break;
				case NPCID.DukeFishron:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<SeafoamClicker>(), 4));
					}
					break;
				case NPCID.HallowBoss:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<RainbowClicker>(), 4));
					}
					break;
				case NPCID.MoonLordCore:
					{
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<LordsClicker>()));
						npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<TheClicker>(), 5));
					}
					break;
					#endregion
			}
		}

		/*
		//Old code to compare with
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
		*/

		public override void SetupTravelShop(int[] shop, ref int nextSlot)
		{
			shop[nextSlot++] = ModContent.ItemType<ButtonMasher>();
			shop[nextSlot++] = ModContent.ItemType<Soda>();

			//Player-based conditions are checked by looking at every online player at the time of shop creation
			bool mlPainting = false;
			bool cookiePainting = false;

			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (!player.active)
				{
					continue;
				}

				if (!mlPainting && player.GetModPlayer<ClickerPlayer>().paintingCondition_MoonLordDefeatedWithClicker)
				{
					mlPainting = true;
				}

				if (!cookiePainting && player.GetModPlayer<ClickerPlayer>().paintingCondition_Clicked100Cookies)
				{
					cookiePainting = true;
				}
			}

			if (mlPainting)
			{
				shop[nextSlot++] = ModContent.ItemType<Galaxies>();
			}

			if (cookiePainting)
			{
				shop[nextSlot++] = ModContent.ItemType<ConfectioneryMice>();
			}

			if (NPC.AnyNPCs(NPCID.TaxCollector))
			{
				shop[nextSlot++] = ModContent.ItemType<Papa>();
			}
		}

		public override void ModifyShop(NPCShop shop)
		{
			switch (shop.NpcType)
			{
				case NPCID.Merchant:
					shop.Add(ModContent.ItemType<Cookie>());
					break;
				case NPCID.Demolitionist:
					shop.Add(ModContent.ItemType<BigRedButton>(), Condition.Hardmode);
					break;
				case NPCID.Cyborg:
					shop.Add(ModContent.ItemType<DesktopComputer>());
					break;
				case NPCID.Mechanic:
					shop.Add(ModContent.ItemType<HandCream>());
					shop.Add(ModContent.ItemType<MagnetClicker>());
					break;
				case NPCID.GoblinTinkerer:
					shop.Add(ModContent.ItemType<MousePad>());
					break;
				case NPCID.PartyGirl:
					shop.Add(ModContent.ItemType<SFXButtonA>(), Condition.MoonPhaseFull);
					shop.Add(ModContent.ItemType<SFXButtonB>(), Condition.MoonPhaseWaningGibbous);
					shop.Add(ModContent.ItemType<SFXButtonC>(), Condition.MoonPhaseThirdQuarter);
					shop.Add(ModContent.ItemType<SFXButtonD>(), Condition.MoonPhaseWaningCrescent);
					shop.Add(ModContent.ItemType<SFXButtonE>(), Condition.MoonPhaseNew);
					shop.Add(ModContent.ItemType<SFXButtonF>(), Condition.MoonPhaseWaxingCrescent);
					shop.Add(ModContent.ItemType<SFXButtonG>(), Condition.MoonPhaseFirstQuarter);
					shop.Add(ModContent.ItemType<SFXButtonH>(), Condition.MoonPhaseWaxingGibbous);
					break;
				case NPCID.Painter:
					shop.Add(ModContent.ItemType<OutsideTheCave>(), ClickerConditions.ClickerSelected, ClickerConditions.ClickerTotalExceeds(1000));
					shop.Add(ModContent.ItemType<ABlissfulDay>(), ClickerConditions.ClickerSelected, ClickerConditions.ClickerTotalExceeds(2500));
					break;
				case NPCID.Stylist:
					shop.Add(ModContent.ItemType<ClickerEffectHairDye>(), ClickerConditions.ClickerSelected, ClickerConditions.ClickerTotalExceeds(2500));
					shop.Add(ModContent.ItemType<ClickSpeedHairDye>(), ClickerConditions.ClickerSelected, ClickerConditions.ClickerTotalExceeds(5000));
					break;
				case NPCID.BestiaryGirl:
					shop.Add(ModContent.ItemType<CritterClicker>(), ClickerConditions.BestiaryFilledPercent(20));
					break;
				case NPCID.SkeletonMerchant:
					shop.Add(ModContent.ItemType<CandleClicker>());
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
			if (seafoam)
			{
				if (!Main.rand.NextBool(4))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 99, npc.velocity.X * 0.10f, 0.20f, 50);
					Dust dust2 = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 101, npc.velocity.X * 0.10f, 0.20f, 50, default(Color), 1f);
					if (Main.rand.NextBool(4))
					{
						dust.scale *= 0.5f;
						dust2.scale *= 0.25f;
					}
				}
				drawColor.R = (byte)(drawColor.R * 0.20f);
				drawColor.G = (byte)(drawColor.G * 0.55f);
				drawColor.B = (byte)(drawColor.B * 0.75f);
			}
			if (oozed)
			{
				if (!Main.rand.NextBool(4))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 188, npc.velocity.X * 0.10f, 0.20f, 100);
					Dust dust2 = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 256, npc.velocity.X * 0.10f, 0.20f, 100, default(Color), 1.2f);
					if (Main.rand.NextBool(4))
					{
						dust.scale *= 0.5f;
						dust2.scale *= 0.25f;
					}
				}
				drawColor.R = (byte)(drawColor.R * 0.35f);
				drawColor.G = (byte)(drawColor.G * 0.75f);
				drawColor.B = (byte)(drawColor.B * 0.10f);
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
			if (crystalSlime)
			{
				if (!Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 71, 0f, 0f, 255, default(Color), 0.4f);
					dust.noGravity = true;
					dust.velocity *= 0f;
					dust.fadeIn = 1.25f;
				}
				drawColor.R = (byte)(drawColor.R * 1f);
				drawColor.G = (byte)(drawColor.G * 1f);
				drawColor.B = (byte)(drawColor.B * 1f);
			}
			
			if (charmed)
			{
				drawColor = NPC.buffColor(drawColor, 1f, 0.4f, 0.8f, 1f);
			}
		}

		public override void PostAI(NPC npc)
		{
			SetBossBuffImmunities(npc);
		}
	}
}
