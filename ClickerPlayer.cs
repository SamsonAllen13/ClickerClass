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

		public override void ResetEffects()
		{
			clickerColor = new Color(0, 0, 0 , 0);
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
			
			if (player.HeldItem.modItem is ClickerItem clickerItem && clickerItem.isClicker)
			{
				clickerSelected = true;
				if (clickerItem.radiusBoost > 0f)
				{
					clickerRadius += clickerItem.radiusBoost;
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
			
			MiscEffects.visible = true;
		}
		
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
	}
}