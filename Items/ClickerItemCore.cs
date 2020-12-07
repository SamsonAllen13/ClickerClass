using ClickerClass.Buffs;
using ClickerClass.Dusts;
using ClickerClass.Prefixes;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ClickerClass.Items
{
	/// <summary>
	/// The class responsible for any clicker item related logic
	/// </summary>
	public class ClickerItemCore : GlobalItem
	{
		public override bool InstancePerEntity => true;

		/// <summary>
		/// A clickers color used for the radius
		/// </summary>
		public Color clickerRadiusColor = Color.White;

		/// <summary>
		/// The clickers effects
		/// </summary>
		public List<string> itemClickEffects = new List<string>();

		/// <summary>
		/// The clickers dust that is spawned on use
		/// </summary>
		public int clickerDustColor = 0;

		/// <summary>
		/// Displays total clicks in the tooltip
		/// </summary>
		public bool isClickerDisplayTotal = false;

		/// <summary>
		/// Additional range for this clicker (1f = 100 pixel, 1f by default from the player)
		/// </summary>
		public float radiusBoost = 0f;

		internal float radiusBoostPrefix = 0f;
		internal int clickBoostPrefix = 0;

		public override GlobalItem Clone(Item item, Item itemClone)
		{
			ClickerItemCore myClone = (ClickerItemCore)base.Clone(item, itemClone);
			myClone.clickerRadiusColor = clickerRadiusColor;
			myClone.itemClickEffects = new List<string>(itemClickEffects);
			myClone.clickerDustColor = clickerDustColor;
			myClone.clickBoostPrefix = clickBoostPrefix;
			myClone.isClickerDisplayTotal = isClickerDisplayTotal;
			myClone.radiusBoost = radiusBoost;
			myClone.radiusBoostPrefix = radiusBoostPrefix;
			return myClone;
		}

		public override float MeleeSpeedMultiplier(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					if (player.GetModPlayer<ClickerPlayer>().clickerAutoClick || item.autoReuse)
					{
						return 10f;
					}
					else
					{
						return 1f;
					}
				}
				else
				{
					return 9.5f;
				}
			}

			return base.MeleeSpeedMultiplier(item, player);
		}

		public override float UseTimeMultiplier(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					if (player.GetModPlayer<ClickerPlayer>().clickerAutoClick || item.autoReuse)
					{
						return 0.1f;
					}
					else
					{
						return 1f;
					}
				}
				else
				{
					return 0.15f;
				}
			}

			return base.UseTimeMultiplier(item, player);
		}

		public override bool CanUseItem(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (clickerPlayer.clickerAutoClick || player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					item.autoReuse = true;
				}
				else
				{
					item.autoReuse = false;
				}

				if (!clickerPlayer.HasClickEffect(ClickEffect.PhaseReach))
				{
					//collision
					Vector2 motherboardPosition = clickerPlayer.setMotherboardPosition;
					bool inRange = Vector2.Distance(Main.MouseWorld, player.Center) < clickerPlayer.ClickerRadiusReal && Collision.CanHit(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1);
					bool inRangeMotherboard = Vector2.Distance(Main.MouseWorld, motherboardPosition) < clickerPlayer.ClickerRadiusMotherboard && Collision.CanHit(motherboardPosition, 1, 1, Main.MouseWorld, 1, 1);
					//bool allowMotherboard = player.GetModPlayer<ClickerPlayer>().clickerMotherboardSet && player.altFunctionUse == 2;

					if (inRange || (inRangeMotherboard && player.altFunctionUse != 2))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return base.CanUseItem(item, player);
		}

		public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				flat += clickerPlayer.clickerDamageFlat;
				mult *= clickerPlayer.clickerDamage;
			}
		}

		public override void GetWeaponCrit(Item item, Player player, ref int crit)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				crit += player.GetModPlayer<ClickerPlayer>().clickerCrit;
			}
		}

		public override bool AltFunctionUse(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (clickerPlayer.setMice || clickerPlayer.setMotherboard)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return base.AltFunctionUse(item, player);
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (ClickerSystem.IsClickerItem(item))
			{
				Player player = Main.LocalPlayer;
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				int index;

				float alpha = Main.mouseTextColor / 255f;

				if (ClickerConfigClient.Instance.ShowClassTags)
				{
					index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
					if (index != -1)
					{
						tooltips.Insert(index + 1, new TooltipLine(mod, "ClickerTag", "-Clicker Class-")
						{
							overrideColor = Main.DiscoColor
						});
					}
				}

				if (isClickerDisplayTotal)
				{
					index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));

					if (index != -1)
					{
						string color = (new Color(252, 210, 44) * alpha).Hex3();
						tooltips.Add(new TooltipLine(mod, "TransformationText", $"Total clicks: [c/{color}:{clickerPlayer.clickerTotal}]"));
					}
				}

				if (ClickerSystem.IsClickerWeapon(item))
				{
					TooltipLine tooltip = tooltips.Find(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Damage"));
					if (tooltip != null)
					{
						string number = tooltip.text.Split(' ')[0];
						tooltip.text = $"{number} click damage";
					}

					//Show the clicker's effects
					//Then show ones missing through the players enabled effects (respecting overlap, ignoring the currently held clickers effect if its not the same type)
					List<string> effects = new List<string>(itemClickEffects);
					foreach (var name in ClickerSystem.GetAllEffectNames())
					{
						if (clickerPlayer.HasClickEffect(name, out ClickEffect effect) && !effects.Contains(name))
						{
							if (!(player.HeldItem.type != item.type && player.HeldItem.type != ItemID.None && player.HeldItem.GetGlobalItem<ClickerItemCore>().itemClickEffects.Contains(name)))
							{
								effects.Add(name);
							}
						}
					}

					if (effects.Count > 0)
					{
						index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Knockback"));

						if (index != -1)
						{
							//"Auto Select" key: player.controlTorch

							var keys = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[TriggerNames.SmartSelect];
							string key = keys.Count == 0 ? null : keys[0];

							//If has a key, but not pressing it, show the ForMoreInfo text
							//Otherwise, list all effects

							bool showDesc = key == null || player.controlTorch;

							foreach (var name in effects)
							{
								if (ClickerSystem.IsClickEffect(name, out ClickEffect effect))
								{
									tooltips.Insert(++index, effect.ToTooltip(clickerPlayer.GetClickAmountTotal(this, name), alpha, showDesc));
								}
							}

							if (!showDesc && ClickerConfigClient.Instance.ShowEffectSuggestion)
							{
								//Add ForMoreInfo as the last line
								index = tooltips.FindLastIndex(tt => tt.mod.Equals("Terraria") && tt.Name.StartsWith("Tooltip"));
								var ttl = new TooltipLine(mod, "ForMoreInfo", $"Hold 'Auto Select' key ({key}) while not auto-paused to show click effects")
								{
									overrideColor = Color.Gray
								};

								if (index != -1)
								{
									tooltips.Insert(++index, ttl);
								}
								else
								{
									tooltips.Add(ttl);
								}
							}
						}
					}
				}

				if (item.prefix < PrefixID.Count || !ClickerPrefix.ClickerPrefixes.Contains(item.prefix))
				{
					return;
				}

				int ttindex = tooltips.FindLastIndex(t => (t.mod == "Terraria" || t.mod == mod.Name) && (t.isModifier || t.Name.StartsWith("Tooltip") || t.Name.Equals("Material")));
				if (ttindex != -1)
				{
					if (radiusBoostPrefix != 0)
					{
						TooltipLine tt = new TooltipLine(mod, "PrefixClickerRadius", (radiusBoostPrefix > 0 ? "+" : "") + (int)((radiusBoostPrefix / 2) * 100) + "% base clicker radius")
						{
							isModifier = true,
							isModifierBad = radiusBoostPrefix < 0
						};
						tooltips.Insert(++ttindex, tt);
					}
					if (radiusBoostPrefix != 0)
					{
						TooltipLine tt = new TooltipLine(mod, "PrefixClickBoost", (clickBoostPrefix < 0 ? "" : "+") + clickBoostPrefix + " clicks required")
						{
							isModifier = true,
							isModifierBad = clickBoostPrefix > 0
						};
						tooltips.Insert(++ttindex, tt);
					}
				}
			}
		}

		public override bool NewPreReforge(Item item)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				radiusBoostPrefix = 0f;
				clickBoostPrefix = 0;
			}
			return base.NewPreReforge(item);
		}

		public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (item.maxStack == 1 && item.useStyle > 0)
				{
					return rand.Next(ClickerPrefix.ClickerPrefixes);
				}
			}
			return base.ChoosePrefix(item, rand);
		}

		public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (player.altFunctionUse == 2)
				{
					//Right click 
					if (clickerPlayer.setAbilityDelayTimer <= 0)
					{
						//Mice armor 
						if (clickerPlayer.setMice)
						{
							bool canTeleport = false;
							if (!clickerPlayer.HasClickEffect(ClickEffect.PhaseReach))
							{
								//collision
								if (Vector2.Distance(Main.MouseWorld, player.Center) < clickerPlayer.ClickerRadiusReal && Collision.CanHitLine(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1))
								{
									canTeleport = true;
								}
							}
							else
							{
								canTeleport = true;
							}

							if (canTeleport)
							{
								Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 115);
								Main.SetCameraLerp(0.1f, 0);
								player.Center = Main.MouseWorld;
								NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
								player.fallStart = (int)(player.position.Y / 16f);
								clickerPlayer.setAbilityDelayTimer = 60;

								float num102 = 50f;
								int num103 = 0;
								while ((float)num103 < num102)
								{
									Vector2 vector12 = Vector2.UnitX * 0f;
									vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 2f);
									vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
									int num104 = Dust.NewDust(Main.MouseWorld, 0, 0, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 2f);
									Main.dust[num104].noGravity = true;
									Main.dust[num104].position = Main.MouseWorld + vector12;
									Main.dust[num104].velocity = Vector2.Zero * 0f + vector12.SafeNormalize(Vector2.UnitY) * 4f;
									int num = num103;
									num103 = num + 1;
								}
							}
						}
						else if (clickerPlayer.setMotherboard)
						{
							Main.PlaySound(SoundID.Camera, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 0);
							clickerPlayer.SetMotherboardRelativePosition(Main.MouseWorld);
							clickerPlayer.setAbilityDelayTimer = 60;
						}
					}
					return false;
				}

				//Base 
				Main.PlaySound(SoundID.MenuTick, player.position);
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					clickerPlayer.Click();
				}

				//TODO dire: maybe "PreShoot" hook wrapping around the next NewProjectile

				//Spawn normal click damage
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);

				//Precursor armor set bonus
				if (clickerPlayer.setPrecursor)
				{
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<PrecursorPro>(), (int)(damage * 0.25f), knockBack, player.whoAmI);
				}

				//Overclock armor set bonus
				if (clickerPlayer.clickAmount % 100 == 0 && clickerPlayer.setOverclock)
				{
					Main.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 94);
					player.AddBuff(ModContent.BuffType<OverclockBuff>(), 180, false);
					for (int i = 0; i < 25; i++)
					{
						int num6 = Dust.NewDust(player.position, 20, 20, 90, 0f, 0f, 150, default(Color), 1.35f);
						Main.dust[num6].noGravity = true;
						Main.dust[num6].velocity *= 0.75f;
						int num7 = Main.rand.Next(-50, 51);
						int num8 = Main.rand.Next(-50, 51);
						Dust dust = Main.dust[num6];
						dust.position.X = dust.position.X + (float)num7;
						Dust dust2 = Main.dust[num6];
						dust2.position.Y = dust2.position.Y + (float)num8;
						Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
						Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
					}
				}

				bool autoClick = player.HasBuff(ModContent.BuffType<AutoClick>());
				bool overclock = player.HasBuff(ModContent.BuffType<OverclockBuff>());

				foreach (var name in ClickerSystem.GetAllEffectNames())
				{
					if (clickerPlayer.HasClickEffect(name, out ClickEffect effect))
					{
						//Find click amount
						int clickAmountTotal = clickerPlayer.GetClickAmountTotal(this, name);
						bool reachedAmount = clickerPlayer.clickAmount % clickAmountTotal == 0;

						if (!autoClick && (reachedAmount || overclock))
						{
							effect.Action?.Invoke(player, position, type, damage, knockBack);
						}
					}
				}

				return false;
			}
			return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}
