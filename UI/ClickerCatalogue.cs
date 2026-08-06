using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;
using static ClickerClass.ClickerPlayer;

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
		private Lazy<Asset<Texture2D>> sheetAsset6 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Icons"));

		//TODO - Allow other mods to use their icon_small
		private Lazy<Asset<Texture2D>> sheetAsset7 = new(() => ModContent.Request<Texture2D>("ClickerClass/icon_small"));

		public LocalizedText MouseoverText { get; private set; }
		
		public LocalizedText HintText { get; private set; }
		
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

			Asset<Texture2D> iconAsset;
			iconAsset = sheetAsset6.Value;

			Asset<Texture2D> modAsset;
			modAsset = sheetAsset7.Value;

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
				Item itemType = list[k];

				//Draw slot background
				texture = slotAsset.Value;
				frame = texture.Frame(1, 7);
				origin = frame.Size() / 2;

				Color colorBackground = Color.White;
				float gradient = (float)k / ClickerClass.mod.totalClickers.Count;
				CatalogueSorting check = (CatalogueSorting)clickerPlayer.clickerCatalogueSorting;

				frame.Y = frame.Height * 6;

				if (check == ClickerPlayer.CatalogueSorting.Rarity_Ascending || check == ClickerPlayer.CatalogueSorting.Rarity_Descending)
				{
					colorBackground = itemType.rare switch
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

					//TODO - Remove Hardcoded Rarity color | Figure out modded rarity in above switch
					colorBackground = itemType.type == ModContent.ItemType<CollectorsClicker>() ? new Color(255, 135, 0) * 0.9f : colorBackground * 0.9f;
					
				}
				else if (check == ClickerPlayer.CatalogueSorting.Damage_Ascending)
				{
					colorBackground = Color.Lerp(new Color(255, 255, 155), new Color(255, 60, 60), gradient);
				}
				else if (check == ClickerPlayer.CatalogueSorting.Damage_Descending)
				{
					colorBackground = Color.Lerp(new Color(255, 255, 155), new Color(255, 60, 60), 1f - gradient);
				}
				else
				{
					frame.Y = frame.Height * 0;
				}

				if (clickerPlayer.chosenSecondClicker == itemType.type)
				{
					colorBackground = Color.White;
					frame.Y = frame.Height * 4;
				}

				color = colorBackground * alphaMult;
				
				Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				
				//Draw the clicker's texture
				//TODO - Clicker Catalogue - Remove magic numbers
				texture = TextureAssets.Item[itemType.type].Value;
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
				frame = texture.Frame(1, 7);
				origin = frame.Size() / 2;
				color = Color.White * alphaMult;

				//If you have chosen this clicker, make the slot look 'selected'
				if (clickerPlayer.chosenSecondClicker != itemType.type)
				{
					frame.Y = frame.Height * 1;
					Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				}

				//Draw hover text
				Vector2 currentClickerPosition = position + new Vector2(offSetX, offSetY);
				Rectangle hoverSpot = new Rectangle((int)currentClickerPosition.X - frame.Width / 2, (int)currentClickerPosition.Y - frame.Height / 2, frame.Width, frame.Height);
				if (hoverSpot.Contains(Main.mouseX, Main.mouseY) && hasClicker)
				{
					if (clickerPlayer.consumedDemonHand)
					{
						if (clickerPlayer.chosenSecondClicker != itemType.type)
						{
							frame.Y = frame.Height * 3;
							Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
						}

						if (Main.mouseLeft && Main.mouseLeftRelease)
						{
							if (clickerPlayer.chosenSecondClicker == itemType.type)
							{
								clickerPlayer.chosenSecondClicker = -1;
								SoundEngine.PlaySound(SoundID.MenuTick, player.position);
							}
							else
							{
								clickerPlayer.chosenSecondClicker = itemType.type;
								SoundEngine.PlaySound(SoundID.Item129, player.position);
							}
						}
					}
					else
					{
						frame.Y = frame.Height * 2;
						Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
					}

					float alpha = Main.mouseTextColor / 255f;
					Item item = ContentSamples.ItemsByType[itemType.type];
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
					
					Item item = ContentSamples.ItemsByType[itemType.type];
					string hintProcess = RemoveSpecialCharacters("Items." + item.Name + $".Hint");
					HintText = ClickerClass.mod.GetLocalization(hintProcess);
					string hint = HintText.Format();
					UICommon.TooltipMouseText("???\n" + hint);
				}

				//If you have chosen this clicker, make the slot border look 'selected'
				if (clickerPlayer.chosenSecondClicker == itemType.type)
				{
					frame.Y = frame.Height * 5;
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

			//TODO Clicker Catalogue - Only draw if other mods are enabled
			//Reset
			//Draw Mod's small_icon
			texture = modAsset.Value;
			frame = texture.Frame(1, 1);
			origin = frame.Size() / 2;

			position.Y += -40;

			Rectangle hoverSpotPage = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotPage.Contains(Main.mouseX, Main.mouseY))
			{
				frame.Y = frame.Height * 2;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

				//TODO - A given Mod's Display name
				string s = "Clicker Class";
				UICommon.TooltipMouseText(s);
			}

			Main.spriteBatch.Draw(texture, position, null, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			//Reset
			//Draw Page Buttons
			texture = pageAsset.Value;
			frame = texture.Frame(1, 3);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;

			//Add offset to page buttons
			//TODO - Clicker Catalogue - Remove magic numbers
			position.X += 32;

			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			hoverSpotPage = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotPage.Contains(Main.mouseX, Main.mouseY))
			{
				frame.Y = frame.Height * 2;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

				string s = "Previous Mod: ";
				UICommon.TooltipMouseText(s);
			}
			
			position.X += 30;
			
			frame.Y = frame.Height * 1;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			hoverSpotPage = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotPage.Contains(Main.mouseX, Main.mouseY))
			{
				frame.Y = frame.Height * 2;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

				string s = "Next Mod: ";
				UICommon.TooltipMouseText(s);
			}

			//TODO Clicker Catalogue - Localize Text / Allow modded %
			//Reset
			//Draw Progress Bar
			texture = progressAsset.Value;
			frame = texture.Frame(1, 3);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;
			bool hoveringProgress = false;

			//Add offset to Progress Bar
			//TODO Clicker Catalogue - Remove magic numbers
			position.X += 192;
		
			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			frame.Y = frame.Height * 1;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			
			// Percentage of bar filled
			float fill = (float)clickerPlayer.foundClickers.Count / ClickerClass.mod.totalClickers.Count;
			bool catalogueComplete = fill == 1f;

			// Change the width of the frame so it only draws part of the bar
			frame.Width = (int)(frame.Width * fill);
			frame.Y = frame.Height * 2;

			if (catalogueComplete)
			{
				color = Color.Lerp(Color.White, Main.DiscoColor, 0.75f);
			}

			Rectangle hoverSpotProgress = new Rectangle((int)position.X - texture.Width / 2, (int)position.Y - frame.Height / 2, texture.Width, frame.Height);
			if (hoverSpotProgress.Contains(Main.mouseX, Main.mouseY))
			{
				hoveringProgress = true;
			}

			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			//Reset
			//Draw bag/trophy icon
			texture = iconAsset.Value;
			frame = texture.Frame(1, 2);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;
			position.Y -= 1;
			position.X -= 134;

			if (catalogueComplete)
			{
				frame.Y = frame.Height * 1;
			}

			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			frame.Width = texture.Width;

			hoverSpotProgress = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotProgress.Contains(Main.mouseX, Main.mouseY))
			{
				hoveringProgress = true;
			}

			if (hoveringProgress)
			{
				float fillPercent = fill * 100f;
				float endResult = (float)Math.Round(fillPercent, 2);
				
				//TODO - Given mod's display name
				string s = "Clicker Class - Clickers Collected: " + endResult + "%";
				UICommon.TooltipMouseText(s);
			}
			
			//Reset
			//Draw Sorting Button
			texture = sortAsset.Value;
			frame = texture.Frame(1, 2);
			origin = frame.Size() / 2;
			color = Color.White * alphaMult;

			//Add offset to sort button
			//TODO - Clicker Catalogue - Remove magic numbers
			position.Y += 1;
			position.X += 340;
		
			frame.Y = frame.Height * 0;
			Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			Rectangle hoverSpotSort = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotSort.Contains(Main.mouseX, Main.mouseY))
			{
				frame.Y = frame.Height * 1;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

				CatalogueSorting check = (CatalogueSorting)clickerPlayer.clickerCatalogueSorting;
				string s = "Sorting by: " + check;
				UICommon.TooltipMouseText(s);

				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					clickerPlayer.clickerCatalogueSorting++;
					if (clickerPlayer.clickerCatalogueSorting > clickerPlayer.clickerCatalogueSortingMax)
					{
						clickerPlayer.clickerCatalogueSorting = 0;
					}
				}
			}
			return true;
		}
		
		//TODO Clicker Catalogue - Temporary way to allow the hint localization strings to function. Probably wont work with cross-mod compat...
		public static string RemoveSpecialCharacters(string str)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				if ((str[i] >= '0' && str[i] <= '9')
					|| (str[i] >= 'A' && str[i] <= 'z'
						|| (str[i] == '.' || str[i] == '_')))
					{
						sb.Append(str[i]);
					}
			}

			return sb.ToString();
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Ingame Options"));
		}
	}
}
