using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass
{
	public sealed class ClickEffect : ICloneable
	{
		public Mod Mod { get; private init; }

		public string InternalName { get; private init; }

		public string UniqueName => ClickerSystem.UniqueEffectName(Mod, InternalName);

		public LocalizedText DisplayName { get; init; }

		public LocalizedText Description { get; init; }

		public int Amount { get; private init; }

		public Func<Color> ColorFunc { get; private init; }

		public Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> Action { get; private set; }

		public bool PreHardMode { get; private set; }

		private static void DoNothingAction(Player a, EntitySource_ItemUse_WithAmmo b, Vector2 c, int d, int e, float f) { }

		public ClickEffect(Mod mod, string internalName, int amount, Func<Color> colorFunc, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, object[] nameArgs = null, object[] descriptionArgs = null)
		{
			Mod = mod ?? throw new Exception("No mod specified");
			InternalName = internalName ?? throw new Exception("No internal name specified");
			Amount = amount;
			ColorFunc = colorFunc;
			Action = action ?? DoNothingAction;
			PreHardMode = preHardMode;

			string category = "ClickEffect";
			string name = InternalName;
			string uniqueName = ClickerSystem.UniqueEffectName(Mod, name);
			if (!ClickerSystem.TryGetClickEffectName(uniqueName, out LocalizedText displayName))
			{
				//First initialization and binding
				DisplayName = Language.GetOrRegister(Mod.GetLocalizationKey($"{category}.{name}.Name"), PrettyPrintName);
				if (nameArgs != null)
				{
					DisplayName = DisplayName.WithFormatArgs(nameArgs);
				}
			}
			else
			{
				DisplayName = displayName;
			}

			if (!ClickerSystem.TryGetClickEffectDescription(uniqueName, out LocalizedText description))
			{
				//First initialization and binding
				Description = Language.GetOrRegister(Mod.GetLocalizationKey($"{category}.{name}.Description"), () => $"${ClickerSystem.UnknownText.Key}");
				if (descriptionArgs != null)
				{
					Description = Description.WithFormatArgs(descriptionArgs);
				}
			}
			else
			{
				Description = description;
			}
		}

		public object Clone()
		{
			//name and desc args are not required for cloning as they are cached already
			return new ClickEffect(Mod, InternalName, Amount, ColorFunc, (Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float>)Action.Clone(), PreHardMode);
		}

		public TooltipLine ToTooltip(int amount, float alpha, bool showDesc)
		{
			string color = (ColorFunc() * alpha).Hex3();
			string text = LangHelper.GetText("Common.Tooltips.Clicks", amount);
			text += $": [c/{color}:{DisplayName.Value}]";
			if (showDesc && Description.Value != Description.Key)
			{
				text += $" - {Description.Value}";
			}
			return new TooltipLine(ClickerClass.mod, $"ClickEffect_{UniqueName}", text);
		}

		public string PrettyPrintName()
		{
			return Regex.Replace(InternalName, "([A-Z])", " $1").Trim();
		}

		public override string ToString()
		{
			return $"{DisplayName.Value}: {Description.Value}".Substring(0, 20);
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
				["Action"] = Action,
				["PreHardMode"] = PreHardMode,
			};
		}

		/// <summary>
		/// Loads commonly used click effects
		/// </summary>
		internal static void LoadMiscEffects()
		{
			ClickEffect.DoubleClick = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick", 10, Color.White, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, source, position, type, damage, knockBack);
			},
			preHardMode: true);

			ClickEffect.DoubleClick2 = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick2", 8, Color.White, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, source, position, type, damage, knockBack);
			},
			preHardMode: true);

			static void DoubleClick(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item37, position);
				Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
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
		public static string ArcaneEnchantment { get; internal set; } = string.Empty;
		public static string AutoClick { get; internal set; } = string.Empty;
		public static string BalloonDefense { get; internal set; } = string.Empty;
		public static string BloodSucker { get; internal set; } = string.Empty;
		public static string Bold { get; internal set; } = string.Empty;
		public static string Bombard { get; internal set; } = string.Empty;
		public static string Burrow { get; internal set; } = string.Empty;
		public static string Charm { get; internal set; } = string.Empty;
		public static string Collision { get; internal set; } = string.Empty;
		public static string Conqueror { get; internal set; } = string.Empty;
		public static string Curse { get; internal set; } = string.Empty;
		public static string CursedEruption { get; internal set; } = string.Empty;
		public static string DarkBurst { get; internal set; } = string.Empty;
		public static string Dazzle { get; internal set; } = string.Empty;
		public static string Discharge { get; internal set; } = string.Empty;
		public static string DoubleClick { get; internal set; } = string.Empty;
		public static string DoubleClick2 { get; internal set; } = string.Empty;
		public static string DustDevil { get; internal set; } = string.Empty;
		public static string Embrittle { get; internal set; } = string.Empty;
		public static string Entangle { get; internal set; } = string.Empty;
		public static string Flurry { get; internal set; } = string.Empty;
		public static string Freeze { get; internal set; } = string.Empty;
		public static string Fritz { get; internal set; } = string.Empty;
		public static string HappyHeart { get; internal set; } = string.Empty;
		public static string Haste { get; internal set; } = string.Empty;
		public static string HolyNova { get; internal set; } = string.Empty;
		public static string HotWings { get; internal set; } = string.Empty;
		public static string Illuminate { get; internal set; } = string.Empty;
		public static string Incinerate { get; internal set; } = string.Empty;
		public static string Inferno { get; internal set; } = string.Empty;
		public static string Infest { get; internal set; } = string.Empty;
		public static string Insanity { get; internal set; } = string.Empty;
		public static string Lacerate { get; internal set; } = string.Empty;
		public static string Linger { get; internal set; } = string.Empty;
		public static string Mania { get; internal set; } = string.Empty;
		public static string Nab { get; internal set; } = string.Empty;
		public static string OgreGold { get; internal set; } = string.Empty;
		public static string PartyTime { get; internal set; } = string.Empty;
		public static string Peekaboo { get; internal set; } = string.Empty;
		public static string PetalStorm { get; internal set; } = string.Empty;
		public static string PharaohsCommand { get; internal set; } = string.Empty;
		public static string PhaseReach { get; internal set; } = string.Empty;
		public static string Presents { get; internal set; } = string.Empty;
		public static string Rainbolt { get; internal set; } = string.Empty;
		public static string RazorsEdge { get; internal set; } = string.Empty;
		public static string Regenerate { get; internal set; } = string.Empty;
		public static string SeaSpray { get; internal set; } = string.Empty;
		public static string ShadowLash { get; internal set; } = string.Empty;
		public static string Siphon { get; internal set; } = string.Empty;
		public static string Smite { get; internal set; } = string.Empty;
		public static string SolarFlare { get; internal set; } = string.Empty;
		public static string Spiral { get; internal set; } = string.Empty;
		public static string Splash { get; internal set; } = string.Empty;
		public static string Spores { get; internal set; } = string.Empty;
		public static string StarSlice { get; internal set; } = string.Empty;
		public static string StarStorm { get; internal set; } = string.Empty;
		public static string Starfall { get; internal set; } = string.Empty;
		public static string StickyHoney { get; internal set; } = string.Empty;
		public static string StingingThorn { get; internal set; } = string.Empty;
		public static string Swarm { get; internal set; } = string.Empty;
		public static string TheClick { get; internal set; } = string.Empty;
		public static string Totality { get; internal set; } = string.Empty;
		public static string ToxicRelease { get; internal set; } = string.Empty;
		public static string Transcend { get; internal set; } = string.Empty;
		public static string Trap { get; internal set; } = string.Empty;
		public static string TrueStrike { get; internal set; } = string.Empty;
		public static string WebSplash { get; internal set; } = string.Empty;
		public static string WildMagic { get; internal set; } = string.Empty;
		public static string WyvernsRoar { get; internal set; } = string.Empty;
		public static string Yoink { get; internal set; } = string.Empty;
		#endregion

		public static void Unload()
		{
			//Invokes the static constructor to redefine 'Registered Effects'
			typeof(ClickEffect).TypeInitializer?.Invoke(null, null);
		}
	}
}
