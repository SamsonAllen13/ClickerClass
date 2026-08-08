using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace ClickerClass.UI
{
	[LocalizeEnum(Category = $"UI.{nameof(ClickerCatalogueUI)}")]
	public enum CatalogueSorting : int
	{
		Name_Ascending,
		Name_Descending,
		Damage_Ascending,
		Damage_Descending,
		Rarity_Ascending,
		Rarity_Descending,
		Clicks_Ascending,
		Clicks_Descending
	}

	internal class ClickerCatalogueUI : InterfaceResource
	{
		public ClickerCatalogueUI() : base("ClickerClass: Clicker Catalogue UI", InterfaceScaleType.UI)
		{
			string category = $"UI.{nameof(ClickerCatalogueUI)}.";
			PreviousModText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{category}PreviousMod"));
			NextModText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{category}NextMod"));
			ProgressText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{category}Progress"));
			SortingByText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{category}SortingBy"));
			DemonHandText = Language.GetOrRegister(ClickerClass.mod.GetLocalizationKey($"{category}DemonHand"));
		}

		public const int MAX_FADE_TIME = 35;
		public const int FADE_DELAY = 5;
		public static int FadeTime { get; internal set; }
		private int _delay = 0;
		public static bool SortThisTick = false;

		//Textures
		//TODO automatically size background using the vanilla methods used for tooltips etc
		private Lazy<Asset<Texture2D>> sheetAsset = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Background"));
		private Lazy<Asset<Texture2D>> sheetAsset2 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Slots"));
		private Lazy<Asset<Texture2D>> sheetAsset3 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Progress"));
		private Lazy<Asset<Texture2D>> sheetAsset4 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_PageButton"));
		private Lazy<Asset<Texture2D>> sheetAsset5 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_SortButton"));
		private Lazy<Asset<Texture2D>> sheetAsset6 = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/Catalogue_Icons"));
		private Lazy<Asset<Texture2D>> defaultIconSmallAsset = new(() => ModContent.Request<Texture2D>("ClickerClass/UI/PlaceholderSmallModIcon"));

		public LocalizedText PreviousModText { get; private set; }
		public LocalizedText NextModText { get; private set; }
		public LocalizedText ProgressText { get; private set; }
		public LocalizedText SortingByText { get; private set; }
		public LocalizedText DemonHandText { get; private set; }
		
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
				if (_delay == 0)
				{
					SortThisTick = true;
				}

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

			Mod mod = clickerPlayer.clickerCatalogueMod;

			if (SortThisTick)
			{
				SortThisTick = false;
				ClickerSystem.SortBy(clickerPlayer.FoundClickersUI, clickerPlayer.clickerCatalogueSorting, mod);
			}

			// Make clone to avoid glitches during enumeration. Sorting should happen just before this is accessed
			var list = new List<int>(ClickerSystem.SortedClickerWeapons);
			if (list.Count == 0 || 
				ClickerSystem.ObtainmentConditionsByMod.TryGetValue(mod.Name, out var funcList) && funcList.Count(func => !func()) >= ClickerSystem.GetClickerCountFromMod(mod.Name))
			{
				//If no items, or the number of conditions (one per item) is equal to the number of items from the mod (= all unobtainable)
				//The second case can only happen if the condition is evaluated dynamically while the page is open, which is discouraged by the API documentation for that reason
				//soft-reset from errors, default to Clicker Class and re-sort
				SortThisTick = true;
				clickerPlayer.clickerCatalogueSorting = CatalogueSorting.Name_Ascending;
				clickerPlayer.clickerCatalogueMod = ClickerClass.mod;
				throw new Exception($"Clicker Catalogue was not populated with items. Sort mode: {clickerPlayer.clickerCatalogueSorting}, Mod: {mod.Name}");
			}

			// Fill calc has to be before mod switching buttons
			// Percentage of bar filled
			float fill = (float)clickerPlayer.FoundClickersUI.Count / ClickerSystem.SortedClickerWeapons.Count + 0.00001f;
			bool catalogueComplete = fill >= 1f;

			if (mod == ClickerClass.mod && !clickerPlayer.obtainedCollectorsClicker && catalogueComplete)
			{
				//Defer spawning to a non-UI method for compatibility with High FPS Support mod
				clickerPlayer.spawnCollectorsClicker = true;
			}

			for (int k = 0; k < list.Count; k++)
			{
				Item item = ContentSamples.ItemsByType[list[k]];

				//Draw slot background
				texture = slotAsset.Value;
				frame = texture.Frame(1, 7);
				origin = frame.Size() / 2;

				Color colorBackground = Color.White;
				float gradient = (float)k / list.Count;
				CatalogueSorting check = clickerPlayer.clickerCatalogueSorting;

				frame.Y = frame.Height * 6;

				if (check == CatalogueSorting.Rarity_Ascending || check == CatalogueSorting.Rarity_Descending)
				{
					colorBackground = item.rare switch
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
						_ => Color.White,
					};

					if (colorBackground == Color.White && RarityLoader.GetRarity(item.rare) is ModRarity modRarity)
					{
						colorBackground = modRarity.RarityColor;
					}
				}
				else if (check == CatalogueSorting.Damage_Ascending)
				{
					colorBackground = Color.Lerp(new Color(255, 255, 155), new Color(255, 60, 60), gradient);
				}
				else if (check == CatalogueSorting.Damage_Descending)
				{
					colorBackground = Color.Lerp(new Color(255, 255, 155), new Color(255, 60, 60), 1f - gradient);
				}
				else
				{
					frame.Y = frame.Height * 0;
				}

				if (clickerPlayer.chosenSecondClicker == item.type)
				{
					colorBackground = Color.White;
					frame.Y = frame.Height * 4;
				}

				color = colorBackground * alphaMult;
				
				Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				
				//Draw the clicker's texture
				//TODO - Clicker Catalogue - Remove magic numbers
				texture = TextureAssets.Item[item.type].Value;
				Vector2 offSet = new Vector2(10, 8);
				
				bool hasClicker = false;
				
				color = Color.Black * alphaMult;
				if (clickerPlayer.FoundClickersUI.Contains(item.type))
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
				if (clickerPlayer.chosenSecondClicker != item.type)
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
						if (clickerPlayer.chosenSecondClicker != item.type)
						{
							frame.Y = frame.Height * 3;
							Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
						}

						if (Main.mouseLeft && Main.mouseLeftRelease)
						{
							if (clickerPlayer.chosenSecondClicker == item.type)
							{
								clickerPlayer.chosenSecondClicker = -1;
								SoundEngine.PlaySound(SoundID.MenuTick, player.position);
							}
							else
							{
								clickerPlayer.chosenSecondClicker = item.type;
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
							_ => Color.White,
						};
					}

					if (rarityColor == Color.White && RarityLoader.GetRarity(item.rare) is ModRarity modRarity)
					{
						rarityColor = modRarity.RarityColor;
					}

					s = $"[c/{(rarityColor * alpha).Hex3()}:{s}]";

					if (ClickerSystem.IsClickerWeapon(item, out var clickerItem))
					{
						int effectCount = 0;
						foreach (var name in clickerItem.itemClickEffects)
						{
							effectCount++;
							if (ClickerSystem.IsClickEffect(name, out ClickEffect effect))
							{
								s += $"\n{effect.ToTooltip(clickerPlayer.GetClickAmountTotal(clickerItem, name), alpha, true).Text}";
							}
						}

						if (clickerPlayer.chosenSecondClicker == item.type)
						{
							string colorFormat = (new Color(255, 50, 50) * alpha).Hex3();
							s += $"\n{DemonHandText.Format(colorFormat, effectCount)}";
						}

						if (check == CatalogueSorting.Clicks_Ascending || check == CatalogueSorting.Clicks_Descending)
						{
							string colorFormat = (new Color(252, 210, 44) * alpha).Hex3();
							int clicksTotal = clickerPlayer.clickerTotalPerItem.TryGetValue(item.type, out var value) ? value.Value : 0;
							string clicks = LangHelper.GetLocalization("Tooltip.TotalClicks").Format(colorFormat, clicksTotal);
							s += $"\n{clicks}";
						}
					}

					UICommon.TooltipMouseText(s);
				}
				else if (hoverSpot.Contains(Main.mouseX, Main.mouseY) && !hasClicker)
				{
					frame.Y = frame.Height * 2;
					Main.spriteBatch.Draw(texture, position + new Vector2(offSetX, offSetY), frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
					
					if (ClickerSystem.TryGetHintTooltipText(item.type, out var hintTooltip))
					{
						UICommon.TooltipMouseText(hintTooltip.ToString());
					}
				}

				//If you have chosen this clicker, make the slot border look 'selected'
				if (clickerPlayer.chosenSecondClicker == item.type)
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

			//Reset
			//Draw Mod's small_icon
			texture = mod.SmallModIcon != null ? mod.SmallModIcon.Value : defaultIconSmallAsset.Value.Value;
			frame = texture.Frame(1, 1);
			origin = frame.Size() / 2;

			position.Y += -40;

			Rectangle hoverSpotPage = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
			if (hoverSpotPage.Contains(Main.mouseX, Main.mouseY))
			{
				frame.Y = frame.Height * 2;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

				UICommon.TooltipMouseText(mod.DisplayNameClean);
			}

			Main.spriteBatch.Draw(texture, position, null, color, 0f, origin, 1f, SpriteEffects.None, 0f);

			//If there is more than one other mod with clickers and if atleast one is obtainable
			if (AnyOtherObtainableClickers())
			{
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

					string name = GetNextMod(mod, backward: true);
					var nextMod = ModLoader.GetMod(name);

					UICommon.TooltipMouseText(PreviousModText.Format(nextMod.DisplayNameClean));

					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						SortThisTick = true;
						clickerPlayer.clickerCatalogueMod = nextMod;
					}
				}
				
				position.X += 30;
				
				frame.Y = frame.Height * 1;
				Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);
				
				hoverSpotPage = new Rectangle((int)position.X - frame.Width / 2, (int)position.Y - frame.Height / 2, frame.Width, frame.Height);
				if (hoverSpotPage.Contains(Main.mouseX, Main.mouseY))
				{
					frame.Y = frame.Height * 2;
					Main.spriteBatch.Draw(texture, position, frame, color, 0f, origin, 1f, SpriteEffects.None, 0f);

					string name = GetNextMod(mod);
					var nextMod = ModLoader.GetMod(name);

					UICommon.TooltipMouseText(NextModText.Format(nextMod.DisplayNameClean));

					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						SortThisTick = true;
						clickerPlayer.clickerCatalogueMod = nextMod;
					}
				}
			}
			else
			{
				//TODO - Clicker Catalogue - Remove magic numbers
				//The same numbers from the if block, to make sure the subsequent draws are positioned properly
				position.X += 32;
				position.X += 30;
			}


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

				UICommon.TooltipMouseText(ProgressText.Format(mod.DisplayNameClean, endResult));
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

				UICommon.TooltipMouseText(SortingByText.Format(ClickerClass.GetEnumText(clickerPlayer.clickerCatalogueSorting)));

				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					SortThisTick = true;
					MiscHelper.CycleEnum(ref clickerPlayer.clickerCatalogueSorting);
				}
				else if (Main.mouseRight && Main.mouseRightRelease)
				{
					SortThisTick = true;
					MiscHelper.CycleEnum(ref clickerPlayer.clickerCatalogueSorting, backwards: true);
				}
			}
			return true;
		}

		public override int GetInsertIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Active && layer.Name.Equals("Vanilla: Ingame Options"));
		}

		private static string GetNextMod(Mod mod, bool backward = false)
		{
			int currentIndex = ClickerSystem.SortedModsByClickerWeaponCount.IndexOf(mod.Name);
			int iterations = ClickerSystem.SortedModsByClickerWeaponCount.Count;
			string name = mod.Name;
			for (int i = 1; i < iterations; i++)
			{
				int index = (currentIndex + (backward ? -i : i) + iterations) % iterations;
				name = ClickerSystem.SortedModsByClickerWeaponCount[index];
				if (!ClickerSystem.ObtainmentConditionsByMod.TryGetValue(name, out var funcList) || funcList.Count(func => !func()) < ClickerSystem.GetClickerCountFromMod(name))
				{
					break;
				}
			}
			return name;
		}

		private static bool AnyOtherObtainableClickers()
		{
			if (ClickerSystem.SortedModsByClickerWeaponCount.Count <= 1)
			{
				return false;
			}

			foreach (var name in ClickerSystem.SortedModsByClickerWeaponCount)
			{
				if (name != ClickerClass.mod.Name &&
					(!ClickerSystem.ObtainmentConditionsByMod.TryGetValue(name, out var funcList) || funcList.Any(func => func())))
				{
					return true;
				}
			}
			return false;
		}
	}
}
