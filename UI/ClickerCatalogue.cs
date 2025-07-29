using ClickerClass.Items.Misc;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace ClickerClass.UI
{
	internal class ClickerCatalogueUI : InterfaceResource
	{
		public ClickerCatalogueUI() : base("ClickerClass: Clicker Catalogue UI", InterfaceScaleType.UI)
		{
			MouseoverText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey("UI.ClickerCatalogueUI"));
		}

		public const int MAX_FADE_TIME = 35;
		public const int FADE_DELAY = 5;
		public static int FadeTime { get; internal set; }
		private int _delay = 0;

		//Textures
		private Lazy<Asset<Texture2D>> sheetAsset = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Background"));
		private Lazy<Asset<Texture2D>> sheetAsset2 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Slots"));
		private Lazy<Asset<Texture2D>> sheetAsset3 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Progress"));
		private Lazy<Asset<Texture2D>> sheetAsset4 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_PageButton"));
		private Lazy<Asset<Texture2D>> sheetAsset5 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_SortButton"));

		public LocalizedText MouseoverText { get; private set; }

		public override void Update(GameTime gameTime)
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = Main.LocalPlayer.GetClickerPlayer();
			
			if (player.dead)
			{
				FadeTime = 0;
			}
			//TODO - Clicker Catalogue
			else if (player.HeldItem.type == ModContent.ItemType<ClickerCatalogue>())
			{
				FadeTime = MAX_FADE_TIME + FADE_DELAY;
				_delay++;
			}
			else if (FadeTime > 0)
			{
				FadeTime--;
				_delay = 0;
			}
		}

		protected override bool DrawSelf()
		{
			Player player = Main.LocalPlayer;
			ClickerPlayer clickerPlayer = player.GetClickerPlayer();
			
			if (player.dead || player.ghost || FadeTime == 0)
			{
				return true;
			}

			// Transparency Multiplier
			float alphaMult = Math.Min((float)FadeTime / MAX_FADE_TIME, 1);

			Asset<Texture2D> backgroundAsset;
			backgroundAsset = sheetAsset.Value;

			Asset<Texture2D> slotAsset;
			slotAsset = sheetAsset2.Value;
			
			Asset<Texture2D> progressAsset;
			progressAsset = sheetAsset3.Value;
			
			Asset<Texture2D> pageAsset;
			pageAsset = sheetAsset4.Value;
			
			Asset<Texture2D> sortAsset;
			sortAsset = sheetAsset5.Value;
			
			if (!backgroundAsset.IsLoaded || !slotAsset.IsLoaded || !progressAsset.IsLoaded || !pageAsset.IsLoaded || !sortAsset.IsLoaded)
			{
				return true;
			}

			if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
			{
				return true;
			}

			Texture2D texture = backgroundAsset.Value;
			Rectangle frame = texture.Frame(1, 1);
			Vector2 origin = frame.Size() / 2;

			Vector2 position = (player.Bottom + new Vector2(-80, -80 + player.gfxOffY)).Floor();
			Color color = Color.White * alphaMult;

			// Calculates UI position depending on UI scale
			position = Vector2.Transform(position - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;

			// Draw the background of the UI
			Main.spriteBatch.Draw(texture, position + new Vector2(253, 121), frame, color * 0.75f, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			int offSetX = 0;
			int offSetY = 0;
			
			var list = ClickerClass.mod.totalClickers.ToArray();
			for (int k = 0; k < list.Length; k++)
			{
				int itemType = list[k];
				
				//Draw slot background
				texture = slotAsset.Value;
				frame = texture.Frame(1, 6);
				origin = frame.Size() / 2;
				color = Color.White * alphaMult;
				
				frame.Y = clickerPlayer.chosenSecondClicker == itemType ? frame.Height * 3 : frame.Height * 0;
				Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				
				//Draw the clicker's texture
				//TODO - Clicker Catalogue - Remove magic numbers
				texture = TextureAssets.Item[itemType].Value;
				Vector2 offSet = new Vector2(10, 8);
				
				bool hasClicker = false;
				
				color = Color.Black * alphaMult;
				if (clickerPlayer.foundClickers.Contains(itemType))
				{
					color = Color.White * alphaMult;
					hasClicker = true;
				}
				
				//Draw clicker
				Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), null, color, 0f, origin - offSet, 1f, SpriteEffects.None, 0f);
				
				//Draw slot border
				texture = slotAsset.Value;
				frame = texture.Frame(1, 6);
				origin = frame.Size() / 2;
				color = Color.White * alphaMult;

				//If you have chosen this clicker, make the slot look 'selected'
				if (clickerPlayer.chosenSecondClicker != itemType)
				{
					frame.Y = frame.Height * 1;
					Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				}

				//Draw hover text
				Vector2 currentClickerPosition = position + new Vector2(offSetX, offSetY);
				Rectangle hoverSpot = new Rectangle((int)currentClickerPosition.X - frame.Width / 2, (int)currentClickerPosition.Y - frame.Height / 2, frame.Width, frame.Height);
				if (hoverSpot.Contains(Main.mouseX, Main.mouseY) && hasClicker)
				{
					if (clickerPlayer.chosenSecondClicker != itemType)
					{
						frame.Y = frame.Height * 2;
						Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
					}

					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						if (clickerPlayer.chosenSecondClicker == itemType)
						{
							clickerPlayer.chosenSecondClicker = -1;
						}
						else
						{
							clickerPlayer.chosenSecondClicker = itemType;
						}
					}

					float alpha = Main.mouseTextColor / 255f;
					Item item = ContentSamples.ItemsByType[itemType];
					string s = Lang.GetItemNameValue(item.type);

					Color rarityColor = Color.White;

					if (item.expert || item.rare == ItemRarityID.Expert)
					{
						rarityColor = Main.DiscoColor;
					}
					else if (item.master || item.rare == ItemRarityID.Master)
					{
						rarityColor = new Color(255, Main.masterColor * 200, 0f);
					}
					else if (!(item.expert || item.rare == ItemRarityID.Expert) && !(item.master || item.rare == ItemRarityID.Master))
					{
						rarityColor = item.rare switch
						{
							ItemRarityID.Gray => new Color(100, 100, 100),
							ItemRarityID.Blue => new Color(134, 134, 229),
							ItemRarityID.Green => new Color(146, 248, 146),
							ItemRarityID.Orange => new Color(233, 182, 136),
							ItemRarityID.LightRed => new Color(244, 144, 144),
							ItemRarityID.Pink => new Color(248, 146, 248),
							ItemRarityID.LightPurple => new Color(190, 144, 229),
							ItemRarityID.Lime => new Color(140, 241, 10),
							ItemRarityID.Yellow => new Color(249, 249, 9),
							ItemRarityID.Cyan => new Color(4, 195, 249),
							ItemRarityID.Red => new Color(225, 6, 67),
							ItemRarityID.Purple => new Color(178, 39, 253),
							ItemRarityID.Quest => new Color(241, 165, 0),
							_ => new Color(255, 255, 255),
						};
					}

					if (RarityLoader.GetRarity(item.rare) is ModRarity modRarity && rarityColor == Color.White)
					{
						rarityColor = modRarity.RarityColor;
					}

					s = $"[c/{(rarityColor * alpha).Hex3()}:{s}]";

					if (ClickerSystem.IsClickerWeapon(item, out var clickerItem))
					{
						foreach (var name in clickerItem.itemClickEffects)
						{
							if (ClickerSystem.IsClickEffect(name, out ClickEffect effect))
							{
								s += $"\n{effect.ToTooltip(clickerPlayer.GetClickAmountTotal(clickerItem, name), alpha, true).Text}";
							}
						}
					}

					UICommon.TooltipMouseText(s);
				}
				else if (hoverSpot.Contains(Main.mouseX, Main.mouseY) && !hasClicker)
				{
					frame.Y = frame.Height * 2;
					Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
					
					//TODO - Clicker Catalogue - Add Mod support option to allow a hint for every clicker
					string s = "???";
					UICommon.TooltipMouseText(s);
				}

				//If you have chosen this clicker, make the slot border look 'selected'
				if (clickerPlayer.chosenSecondClicker == itemType)
				{
					frame.Y = frame.Height * 4;
					Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				}

				//Increase X and Y offset to display clickers
				offSetX += 46;
				if (offSetX >= 46 * 12)
				{
					offSetX = 0;
					offSetY += 46;
				}
			}
			
			//TODO - Clicker Catalogue - Only draw if other mods are enabled | Enabled for testing purposes
			
			//Draw Page Buttons
			texture = pageAsset.Value;
			frame = texture.Frame(1, 3);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;

			//Add offset to page buttons
			//TODO - Clicker Catalogue - Remove magic numbers
			position.X += 12;
			position.Y += -40;
		
			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			position.X += 30;
			
			frame.Y = frame.Height * 1;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			//TODO - Clicker Catalogue - Allow other mods to have their own progress bar
			//Draw Progress Bar
			texture = progressAsset.Value;
			frame = texture.Frame(1, 3);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;

			//Add offset to Progress Bar
			//TODO - Clicker Catalogue - Remove magic numbers
			position.X += 212;
		
			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			frame.Y = frame.Height * 1;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			// Percentage of bar filled
			float fill = (float)clickerPlayer.foundClickers.Count / ClickerClass.mod.totalClickers.Count;

			// Change the width of the frame so it only draws part of the bar
			frame.Width = (int)((frame.Width - 8) * fill + 8);
			frame.Y = frame.Height * 2;
			
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			//TODO - Clicker Catalogue - Localize Text / Allow modded %
			frame.Width = texture.Width;
			Rectangle hoverSpotProgress = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotProgress.Contains(Main.mouseX, Main.mouseY))
			{
				float fillPercent = fill * 100f;
				float endResult = (float)Math.Round(fillPercent, 2);
				
				string s = "Clickers Collected: " + endResult + "%";
				UICommon.TooltipMouseText(s);
			}
			
			//TODO - Clicker Catalogue - When sorting by rarity, use slot background 5 (white) and color based on rarity color
			//Draw Sorting Button
			texture = sortAsset.Value;
			frame = texture.Frame(1, 2);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;

			//Add offset to sort button
			//TODO - Clicker Catalogue - Remove magic numbers
			position.X += 214;
		
			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			return true;
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Ingame Options"));
		}
	}
}
