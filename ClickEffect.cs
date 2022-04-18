using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass
{
	public sealed class ClickEffect : ICloneable
	{
		public Mod Mod { get; private set; }

		public string InternalName { get; private set; }

		public string UniqueName => ClickerSystem.UniqueEffectName(Mod, InternalName);

		public string DisplayName { get; private set; }

		public string Description { get; private set; }

		public bool TryUsingTranslation { get; private set; }

		public int Amount { get; private set; }

		public Func<Color> ColorFunc { get; private set; }

		public Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> Action { get; private set; }

		public ClickEffect(Mod mod, string internalName, string displayName, string description, int amount, Func<Color> colorFunc, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action)
		{
			Mod = mod ?? throw new Exception("No mod specified");
			InternalName = internalName ?? throw new Exception("No internal name specified");
			DisplayName = displayName ?? LangHelper.GetText("Common.Unknown");
			Description = description ?? LangHelper.GetText("Common.Unknown");
			TryUsingTranslation = displayName == null || description == null;
			Amount = amount;
			ColorFunc = colorFunc;
			Action = action ?? (new Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float>((a, b, c, d, e, f) => { }));
		}

		public object Clone()
		{
			return new ClickEffect(Mod, InternalName, DisplayName, Description, Amount, ColorFunc, (Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float>)Action.Clone());
		}

		public TooltipLine ToTooltip(int amount, float alpha, bool showDesc)
		{
			string color = (ColorFunc() * alpha).Hex3();
			string text;
			if (amount > 1)
			{
				text = LangHelper.GetText("Common.Tooltips.MoreThanOneClick", amount);
			}
			else
			{
				text = LangHelper.GetText("Common.Tooltips.OneClick");
			}
			text += $": [c/" + color + ":" + DisplayName + "]";
			if (showDesc && Description != string.Empty)
			{
				text += $" - {Description}";
			}
			return new TooltipLine(ClickerClass.mod, $"ClickEffect_{UniqueName}", text);
		}

		internal void FinalizeLocalization()
		{
			if (TryUsingTranslation)
			{
				string keyBase = $"ClickEffect.{InternalName}";
				string value = LangHelper.GetTextByMod(Mod, $"{keyBase}.Name");
				if (!value.Contains(keyBase))
				{
					//A valid translation
					DisplayName = value;
				}

				value = LangHelper.GetTextByMod(Mod, $"{keyBase}.Description");
				if (!value.Contains(keyBase))
				{
					//A valid translation
					Description = value;
				}
			}
		}

		public override string ToString()
		{
			return $"{DisplayName}: {Description}".Substring(0, 20);
		}

		public Dictionary<string, object> ToDictionary()
		{
			return new Dictionary<string, object>
			{
				["Mod"] = Mod,
				["InternalName"] = InternalName,
				["UniqueName"] = UniqueName,
				["DisplayName"] = DisplayName,
				["Description"] = Description,
				["Amount"] = Amount,
				["ColorFunc"] = ColorFunc,
				["Action"] = Action
			};
		}

		/// <summary>
		/// Loads commonly used click effects
		/// </summary>
		internal static void LoadMiscEffects()
		{
			ClickEffect.DoubleClick = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick", null, null, 10, Color.White, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, source, position, type, damage, knockBack);
			});

			ClickEffect.DoubleClick2 = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick2", null, null, 8, Color.White, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, source, position, type, damage, knockBack);
			});

			void DoubleClick(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 37);
				Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI);
			}
		}

		#region Registered Effects
		//Acc
		public static string StickyKeychain { get; internal set; } = string.Empty;
		public static string ChocolateChip { get; internal set; } = string.Empty;
		public static string BigRedButton { get; internal set; } = string.Empty;
		public static string ClearKeychain { get; internal set; } = string.Empty;
		
		//Armor
		public static string ChromaticBurst { get; internal set; } = string.Empty;
		
		//Clicker
		public static string Smite { get; internal set; } = string.Empty;
		public static string TrueStrike { get; internal set; } = string.Empty;
		public static string HolyNova { get; internal set; } = string.Empty;
		public static string Spiral { get; internal set; } = string.Empty;
		public static string Lacerate { get; internal set; } = string.Empty;
		public static string HotWings { get; internal set; } = string.Empty;
		public static string Illuminate { get; internal set; } = string.Empty;
		public static string Incinerate { get; internal set; } = string.Empty;
		public static string BalloonDefense { get; internal set; } = string.Empty;
		public static string Bombard { get; internal set; } = string.Empty;
		public static string Starfall { get; internal set; } = string.Empty;
		public static string ToxicRelease { get; internal set; } = string.Empty;
		public static string Haste { get; internal set; } = string.Empty;
		public static string Yoink { get; internal set; } = string.Empty;
		public static string DoubleClick { get; internal set; } = string.Empty;
		public static string DoubleClick2 { get; internal set; } = string.Empty;
		public static string CursedEruption { get; internal set; } = string.Empty;
		public static string Fritz { get; internal set; } = string.Empty;
		public static string Infest { get; internal set; } = string.Empty;
		public static string BloodSucker { get; internal set; } = string.Empty;
		public static string Dazzle { get; internal set; } = string.Empty;
		public static string DarkBurst { get; internal set; } = string.Empty;
		public static string DustDevil { get; internal set; } = string.Empty;
		public static string Totality { get; internal set; } = string.Empty;
		public static string Freeze { get; internal set; } = string.Empty;
		public static string Linger { get; internal set; } = string.Empty;
		public static string Discharge { get; internal set; } = string.Empty;
		public static string StickyHoney { get; internal set; } = string.Empty;
		public static string SolarFlare { get; internal set; } = string.Empty;
		public static string Flurry { get; internal set; } = string.Empty;
		public static string Conqueror { get; internal set; } = string.Empty;
		public static string Collision { get; internal set; } = string.Empty;
		public static string Embrittle { get; internal set; } = string.Empty;
		public static string Spores { get; internal set; } = string.Empty;
		public static string PetalStorm { get; internal set; } = string.Empty;
		public static string Regenerate { get; internal set; } = string.Empty;
		public static string StingingThorn { get; internal set; } = string.Empty;
		public static string Inferno { get; internal set; } = string.Empty;
		public static string Curse { get; internal set; } = string.Empty;
		public static string AutoClick { get; internal set; } = string.Empty;
		public static string Siphon { get; internal set; } = string.Empty;
		public static string Splash { get; internal set; } = string.Empty;
		public static string StarStorm { get; internal set; } = string.Empty;
		public static string PhaseReach { get; internal set; } = string.Empty;
		public static string TheClick { get; internal set; } = string.Empty;
		public static string RazorsEdge { get; internal set; } = string.Empty;
		public static string ShadowLash { get; internal set; } = string.Empty;
		public static string WebSplash { get; internal set; } = string.Empty;
		public static string WildMagic { get; internal set; } = string.Empty;
		#endregion

		public static void Unload()
		{
			//Invokes the static constructor to redefine 'Registered Effects'
			typeof(ClickEffect).TypeInitializer?.Invoke(null, null);
		}
	}
}
