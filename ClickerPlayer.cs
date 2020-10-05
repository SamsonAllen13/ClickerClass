using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ClickerClass.Items;
using ClickerClass.Buffs;
using ClickerClass.NPCs;

namespace ClickerClass
{
	public partial class ClickerPlayer : ModPlayer
	{
		//Key presses
		public double pressedAutoClick;
		public int clickerClassTime = 0;
		
		public Color clickerColor = new Color(0, 0, 0 , 0);
		public bool clickerMiceSetAllowed = true;
		public bool clickerMiceSet = false;
		public bool clickerInRange = false;
		public bool clickerSelected = false;
		public bool clickerAutoClickAcc = false;
		public bool clickerAutoClick = false;
		public float clickerDamage = 1f;
		public float clickerBonusPercent = 1f;
		public float clickerRadius = 1f;
		public int clickerTotal = 0;
		public int clickerDamageFlat = 0;
		public int clickerBonus = 0;
		public int clickerCrit = 4;
		public int clickAmount = 0;
		public int clickerMiceSetTimer = 0;

		public override void ResetEffects()
		{
			clickerColor = new Color(0, 0, 0 , 0);
			clickerMiceSetAllowed = true;
			clickerMiceSet = false;
			clickerInRange = false;
			clickerSelected = false;
			clickerAutoClickAcc = false;
			clickerDamage = 1f;
			clickerBonusPercent = 1f;
			clickerRadius = 1f;
			clickerDamageFlat = 0;
			clickerCrit = 4;
			clickerBonus = 0;
		}
		
		public override void Initialize()
		{
			clickerTotal = 0;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"clickerTotal", clickerTotal}
			};
		}

		public override void Load(TagCompound tag)
		{
			clickerTotal = tag.GetInt("clickerTotal");
		}
		
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			// checks for frozen, webbed and stoned
			if (player.CCed)
			{
				return;
			}

			if (ClickerClass.AutoClickKey.JustPressed)
			{
				if (Math.Abs(clickerClassTime - pressedAutoClick) > 60)
				{
					pressedAutoClick = clickerClassTime;
					
					Main.PlaySound(SoundID.MenuTick, player.position);
					clickerAutoClick = clickerAutoClick ? false : true;
				}
			}
		}

		public override void PostUpdateEquips()
		{
			clickerClassTime++;
			if (clickerClassTime > 36000)
			{
				clickerClassTime = 0;
			}
			
			if (!clickerAutoClickAcc)
			{
				clickerAutoClick = false;
			}
			
			if (clickerMiceSetTimer > 0)
			{
				clickerMiceSetTimer--;
			}
			
			if (player.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
			{
				clickerSelected = true;
				if (clickerItem.radiusBoost > 0f || clickerItem.radiusBoostPrefix > 0f)
				{
					clickerRadius += clickerItem.radiusBoost + clickerItem.radiusBoostPrefix;
				}
				if (Vector2.Distance(Main.MouseWorld, player.Center) < 100 * clickerRadius && Collision.CanHit(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1))
				{
					clickerInRange = true;
				}
				clickerColor = clickerItem.clickerColorItem;
			}
			
			if (player.HasBuff(ModContent.BuffType<Haste>()))
			{
				player.armorEffectDrawShadow = true;
			}
			
			int head = 0;
			int body = 1;
			int legs = 2;
			int vanityHead = 10;
			int vanityBody = 11;
			int vanityLegs = 12;

			Item itemHead = player.armor[head];
			Item itemBody = player.armor[body];
			Item itemLegs = player.armor[legs];

			Item itemVanityHead = player.armor[vanityHead];
			Item itemVanityBody = player.armor[vanityBody];
			Item itemVanityLegs = player.armor[vanityLegs];

			if (player.wereWolf || player.merman)
			{
				clickerMiceSet = false;
			}
			if (itemVanityHead.type > 0)
			{
				if (itemVanityHead.type != ModContent.ItemType<MiceMask>())
				{
					clickerMiceSet = false;
				}
			}
			if (itemVanityBody.type > 0)
			{
				if (itemVanityBody.type != ModContent.ItemType<MiceSuit>())
				{
					clickerMiceSet = false;
				}
			}
			if (itemVanityLegs.type > 0)
			{
				if (itemVanityLegs.type != ModContent.ItemType<MiceBoots>())
				{
					clickerMiceSet = false;
				}
			}
		}
		
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (player.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
			{
				if (target.GetGlobalNPC<ClickerGlobalNPC>().embrittle)
				{
					damage += 8;
				}
			}
		}

		public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			
		}
		
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int index = layers.IndexOf(PlayerLayer.MiscEffectsFront);
			if (index != -1)
			{
				layers.Insert(index + 1, MiscEffects);
			}
			index = layers.IndexOf(PlayerLayer.HeldItem);
			if (index != -1)
			{
				layers.Insert(index + 1, WeaponGlow);
			}
			index = layers.IndexOf(PlayerLayer.Head);
			if (index != -1)
			{
				layers.Insert(index + 1, HeadGlow);
			}
			index = layers.IndexOf(PlayerLayer.Legs);
			if (index != -1)
			{
				layers.Insert(index + 1, LegsGlow);
			}
			index = layers.IndexOf(PlayerLayer.Body);
			if (index != -1)
			{
				layers.Insert(index + 1, BodyGlow);
			}
			index = layers.IndexOf(PlayerLayer.Arms);
			if (index != -1)
			{
				layers.Insert(index + 1, ArmsGlow);
			}
			
			MiscEffects.visible = true;
			WeaponGlow.visible = true;
			HeadGlow.visible = true;
			LegsGlow.visible = true;
			ArmsGlow.visible = true;
		}
		
		//Head
		public static readonly PlayerLayer HeadGlow = new PlayerLayer("ClickerClass", "HeadGlow", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;
			
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}

			if (modPlayer.clickerMiceSet && modPlayer.clickerMiceSetAllowed)
			{
				texture = mod.GetTexture("Gores/MiceMask_Glow");
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.headOrigin, drawPlayer.bodyFrame, color, drawPlayer.headRotation, drawInfo.headOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.headArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Body
		public static readonly PlayerLayer BodyGlow = new PlayerLayer("ClickerClass", "BodyGlow", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;
			
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}

			if (modPlayer.clickerMiceSet && modPlayer.clickerMiceSetAllowed)
			{
				if (drawPlayer.Male)
				{
					texture = mod.GetTexture("Gores/MiceSuit_Glow");
				}
				else
				{
					texture = mod.GetTexture("Gores/MiceSuitFemale_Glow");
				}
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.bodyArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Arms
		public static readonly PlayerLayer ArmsGlow = new PlayerLayer("ClickerClass", "ArmsGlow", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;
			
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}

			if (modPlayer.clickerMiceSet && modPlayer.clickerMiceSetAllowed)
			{
				texture = mod.GetTexture("Gores/MiceSuitArm_Glow");
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.bodyArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Legs
		public static readonly PlayerLayer LegsGlow = new PlayerLayer("ClickerClass", "LegsGlow", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;
			
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}

			if (modPlayer.clickerMiceSet && modPlayer.clickerMiceSetAllowed)
			{
				texture = mod.GetTexture("Gores/MiceBoots_Glow");
			}
			
			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.legOrigin, drawPlayer.legFrame, color, drawPlayer.legRotation, drawInfo.legOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.legArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});
		
		public static readonly PlayerLayer MiscEffects = new PlayerLayer("ClickerClass", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (drawInfo.shadow != 0f)
			{
				return;
			}
			
			if (!drawPlayer.dead)
			{
				if (modPlayer.clickerSelected)
				{
					bool phaseCheck = false;
					if (drawPlayer.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
					{
						if (clickerItem.itemClickerEffect.Contains("Phase Reach"))
						{
							phaseCheck = true;
						}
					}
					
					if (!phaseCheck)
					{
						float size = modPlayer.clickerRadius;
						Texture2D texture = mod.GetTexture("Gores/ClickerRadius100");
						if (modPlayer.clickerRadius >= 3f)
						{
							texture = mod.GetTexture("Gores/ClickerRadius200");
							size /= 2;
							if (modPlayer.clickerRadius >= 5f)
							{
								texture = mod.GetTexture("Gores/ClickerRadius300");
								size = size * 0.67f;
							}
						}
						int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
						int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
						float glow = modPlayer.clickerInRange ? 0.6f : 0f;
						DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, modPlayer.clickerColor * (0.2f + glow), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), size, SpriteEffects.None, 0);
						Main.playerDrawData.Add(data);
					}
				}
			}
		});
		
		public static readonly PlayerLayer WeaponGlow = new PlayerLayer("ClickerClass", "WeaponGlow", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ClickerClass");
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (drawInfo.shadow != 0f || drawPlayer.dead || drawPlayer.frozen || drawPlayer.itemAnimation <= 0)
			{
				return;
			}

			//Fragment Pickaxe
			if (drawPlayer.HeldItem.type == mod.ItemType("MicePickaxe"))
			{
				Texture2D weaponGlow = mod.GetTexture("Gores/MicePickaxe_Glow");
				Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
				Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
				DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(drawData);
			}

			//Fragment Hamaxe
			if (drawPlayer.HeldItem.type == mod.ItemType("MiceHamaxe"))
			{
				Texture2D weaponGlow = mod.GetTexture("Gores/MiceHamaxe_Glow");
				Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
				Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
				DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(drawData);
			}
		});
	}
}