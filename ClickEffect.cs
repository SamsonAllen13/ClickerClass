using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass
{
	public sealed class ClickEffect : ICloneable
	{
		public string InternalName { get; private set; }

		public string DisplayName { get; private set; }

		public string Description { get; private set; }

		public int Amount { get; private set; }

		public Color Color { get; private set; }

		public Action<Player, Vector2, int, int, float> Action { get; private set; }

		public ClickEffect(string internalName, string displayName, string description, int amount, Color color, Action<Player, Vector2, int, int, float> action)
		{
			InternalName = internalName;
			DisplayName = displayName;
			Description = description;
			Amount = amount;
			Color = color;
			Action = action ?? (new Action<Player, Vector2, int, int, float>((a, b, c, d, e) => { }));
		}

		public object Clone()
		{
			return new ClickEffect(InternalName, DisplayName, Description, Amount, Color, (Action<Player, Vector2, int, int, float>)Action.Clone());
		}

		public TooltipLine ToTooltip(int amount, float alpha, bool showDesc)
		{
			string color = (Color * alpha).Hex3();
			string text;
			if (amount > 1)
			{
				text = $"{amount} clicks: ";
			}
			else
			{
				text = "1 click: ";
			}
			text += $"[c/" + color + ":" + DisplayName + "]";
			if (showDesc)
			{
				text  += $" - {Description}";
			}
			return new TooltipLine(ClickerClass.mod, $"ClickEffect:{InternalName}", text);
		}

		public override string ToString()
		{
			return $"{InternalName} / {DisplayName}: {Description.Substring(0, 20)}...";
		}

		public Dictionary<string, object> ToDictionary()
		{
			return new Dictionary<string, object>
			{
				["InternalName"] = InternalName,
				["DisplayName"] = DisplayName,
				["Description"] = Description,
				["Amount"] = Amount,
				["Color"] = Color,
				["Action"] = Action
			};
		}

		/// <summary>
		/// Loads commonly used click effects
		/// </summary>
		internal static void LoadMiscEffects()
		{
			ClickEffect.DoubleClick = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick", "Double Click", "Deals damage twice with one attack", 10, Color.White, delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, position, type, damage, knockBack);
			});

			ClickEffect.DoubleClick2 = ClickerSystem.RegisterClickEffect(ClickerClass.mod, "DoubleClick2", "Double Click", "Deals damage twice with one attack", 8, Color.White, delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				DoubleClick(player, position, type, damage, knockBack);
			});

			void DoubleClick(Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 37);
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);
			}
		}

		#region Registered Effects
		//Acc
		public static string StickyKeychain { get; internal set; } = string.Empty;
		public static string ChocolateChip { get; internal set; } = string.Empty;

		//Clicker
		public static string TrueStrike { get; internal set; } = string.Empty;
		public static string HolyNova { get; internal set; } = string.Empty;
		public static string Spiral { get; internal set; } = string.Empty;
		public static string Lacerate { get; internal set; } = string.Empty;
		public static string Illuminate { get; internal set; } = string.Empty;
		public static string Bombard { get; internal set; } = string.Empty;
		public static string ToxicRelease { get; internal set; } = string.Empty;
		public static string Haste { get; internal set; } = string.Empty;
		public static string DoubleClick { get; internal set; } = string.Empty;
		public static string DoubleClick2 { get; internal set; } = string.Empty;
		public static string CursedEruption { get; internal set; } = string.Empty;
		public static string Infest { get; internal set; } = string.Empty;
		public static string Dazzle { get; internal set; } = string.Empty;
		public static string DarkBurst { get; internal set; } = string.Empty;
		public static string Totality { get; internal set; } = string.Empty;
		public static string Freeze { get; internal set; } = string.Empty;
		public static string Linger { get; internal set; } = string.Empty;
		public static string Discharge { get; internal set; } = string.Empty;
		public static string StickyHoney { get; internal set; } = string.Empty;
		public static string SolarFlare { get; internal set; } = string.Empty;
		public static string Conqueror { get; internal set; } = string.Empty;
		public static string Collision { get; internal set; } = string.Empty;
		public static string Embrittle { get; internal set; } = string.Empty;
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
		public static string WildMagic { get; internal set; } = string.Empty;
		#endregion

		public static void Unload()
		{
			//Invokes the static constructor to redefine 'Registered Effects'
			typeof(ClickEffect).TypeInitializer?.Invoke(null, null);
		}
	}
}
