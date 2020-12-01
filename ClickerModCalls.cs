using ClickerClass.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass
{
	public static class ClickerModCalls
	{
		//All calls are "documented" on the ClickerClassExampleMod repo: https://github.com/SamsonAllen13/ClickerClassExampleMod/blob/master/ClickerCompat.cs
		//Not everything is exposed via API yet
		public static object Call(params object[] args)
		{
			ClickerClass clickerClass = ClickerClass.mod;
			Version latestVersion = clickerClass.Version;
			//Simplify code by resizing args
			Array.Resize(ref args, 25);
			string success = "Success";
			try
			{
				string message = args[0] as string;

				//Future-proofing. Allowing new info to be returned while maintaining backwards compat if necessary
				object apiString = args[1];
				Version apiVersion = apiString is string ? new Version(apiString as string) : latestVersion;

				//Starting index of all parameters for a call
				int index = 2;

				if (message == "SetClickerWeaponDefaults")
				{
					var item = args[index + 0] as Item;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.SetClickerWeaponDefaults(item);
					return success;
				}
				else if (message == "RegisterClickerProjectile")
				{
					var modProj = args[index + 0] as ModProjectile;
					if (modProj == null)
					{
						throw new Exception($"Call Error: The modProjectile argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.RegisterClickerProjectile(modProj);
					return success;
				}
				else if (message == "RegisterClickerItem")
				{
					var modItem = args[index + 0] as ModItem;
					if (modItem == null)
					{
						throw new Exception($"Call Error: The modItem argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.RegisterClickerItem(modItem);
					return success;
				}
				else if (message == "RegisterClickerWeapon")
				{
					var modItem = args[index + 0] as ModItem;
					var borderTexture = args[index + 1] as string;
					if (modItem == null)
					{
						throw new Exception($"Call Error: The modItem argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.RegisterClickerWeapon(modItem, borderTexture);
					return success;
				}
				//Mod mod, string internalName, string displayName, string description, int amount, Color color, Action<Player, Vector2, int, int, float> action
				else if (message == "RegisterClickEffect")
				{
					var mod = args[index + 0] as Mod;
					var internalName = args[index + 1] as string;
					var displayName = args[index + 2] as string;
					var description = args[index + 3] as string;
					var amount = args[index + 4] as int?;
					var color = args[index + 5] as Color?;
					var action = args[index + 6] as Action<Player, Vector2, int, int, float>;

					string nameOfargument = string.Empty;
					if (mod == null)
						nameOfargument = "mod";
					if (internalName == null)
						nameOfargument = "internalName";
					if (displayName == null)
						nameOfargument = "displayName";
					if (description == null)
						nameOfargument = "description";
					if (amount == null)
						nameOfargument = "amount";
					if (color == null)
						nameOfargument = "color";
					if (action == null)
						nameOfargument = "action";

					if (nameOfargument != string.Empty)
					{
						throw new Exception($"Call Error: The {nameOfargument} argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.RegisterClickEffect(mod, internalName, displayName, description, amount.Value, color.Value, action);
					return success;
				}
				else if (message == "GetPathToBorderTexture")
				{
					var type = args[index + 0] as int?;
					if (type == null)
					{
						throw new Exception($"Call Error: The type argument for the attempted message, \"{message}\" has returned null.");
					}
					return ClickerSystem.GetPathToBorderTexture(type.Value);
				}
				else if (message == "IsClickerProj")
				{
					var proj = args[index + 0] as Projectile;
					var type = args[index + 0] as int?; //Try another type variation because of overload

					if (proj != null)
					{
						return ClickerSystem.IsClickerProj(proj);
					}
					else if (type != null)
					{
						return ClickerSystem.IsClickerProj(type.Value);
					}
					else
					{
						throw new Exception($"Call Error: The projectile/type argument for the attempted message, \"{message}\" has returned null.");
					}
				}
				else if (message == "IsClickerItem")
				{
					var item = args[index + 0] as Item;
					var type = args[index + 0] as int?; //Try another type variation because of overload

					if (item != null)
					{
						return ClickerSystem.IsClickerItem(item);
					}
					else if (type != null)
					{
						return ClickerSystem.IsClickerItem(type.Value);
					}
					else
					{
						throw new Exception($"Call Error: The item/type argument for the attempted message, \"{message}\" has returned null.");
					}
				}
				else if (message == "IsClickerWeapon")
				{
					var item = args[index + 0] as Item;
					var type = args[index + 0] as int?; //Try another type variation because of overload

					if (item != null)
					{
						return ClickerSystem.IsClickerWeapon(item);
					}
					else if (type != null)
					{
						return ClickerSystem.IsClickerWeapon(type.Value);
					}
					else
					{
						throw new Exception($"Call Error: The item/type argument for the attempted message, \"{message}\" has returned null.");
					}
				}
				//Clicker Weapon/Item specifics now
				else if (message == "SetColor")
				{
					var item = args[index + 0] as Item;
					var color = args[index + 1] as Color?;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}
					if (color == null)
					{
						color = Color.White;
					}

					ClickerWeapon.SetColor(item, color.Value);
					return success;
				}
				else if (message == "SetDisplayTotalClicks")
				{
					var item = args[index + 0] as Item;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerItem.SetDisplayTotalClicks(item);
					return success;
				}
				else if (message == "SetDust")
				{
					var item = args[index + 0] as Item;
					var dust = args[index + 1] as int?;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}
					if (dust == null)
					{
						dust = 0;
					}

					ClickerWeapon.SetDust(item, dust.Value);
					return success;
				}
				else if (message == "SetEffect")
				{
					if (apiVersion < latestVersion)
					{
						var item = args[index + 0] as Item;
						var name = args[index + 1] as string;
						if (item == null)
						{
							throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
						}
						if (name == null)
						{
							name = string.Empty;
						}
						//Old mods could only register with old ClickerClass effect display names before
						if (ClickerSystem.GetNewNameFromOldDisplayName(name, out string internalName))
						{
							ClickerWeapon.AddEffect(item, internalName);
						}
						return success;
					}
					else
					{
						throw new Exception($"Use AddEffect instead, SetEffect is obsolete in {latestVersion}");
					}
				}
				else if (message == "SetRadius")
				{
					var item = args[index + 0] as Item;
					var radius = args[index + 1] as float?;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}
					if (radius == null)
					{
						radius = 0f;
					}

					ClickerWeapon.SetRadius(item, radius.Value);
					return success;
				}
				else if (message == "AddEffect")
				{
					var item = args[index + 0] as Item;
					var name = args[index + 1] as string;
					var names = args[index + 1] as IEnumerable<string>; //type variation
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}

					if (name != null)
					{
						ClickerWeapon.AddEffect(item, name);
						return success;
					}
					else if (names != null)
					{
						ClickerWeapon.AddEffect(item, names);
						return success;
					}
					else
					{
						throw new Exception($"Call Error: The name/names argument for the attempted message, \"{message}\" has returned null.");
					}
				}
				//Player specifics now
				else if (message == "GetPlayerStat")
				{
					var player = args[index + 0] as Player;
					var statName = args[index + 1] as string;
					if (statName == null)
					{
						throw new Exception($"Call Error: The statName argument for the attempted message, \"{message}\" has returned null.");
					}
					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					if (statName == "clickerRadius")
					{
						return clickerPlayer.clickerRadius;
					}
					else if (statName == "clickAmountTotal")
					{
						var item = args[index + 2] as Item;
						if (item == null)
						{
							throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
						}

						string name;

						if (apiVersion < latestVersion)
						{
							//Before it assumed the effect of the item used. Since 1.2.2 it is now a list, and requires a specific effect
							//For the purpose of backwards compatibility, assume the first effect on the clicker as the main one

							var effects = item.GetGlobalItem<ClickerItemCore>().itemClickEffects;
							name = effects.Count == 0 ? null : effects[0];
						}
						else
						{
							name = args[index + 3] as string;
						}

						if (name == null)
						{
							name = string.Empty;
						}

						return clickerPlayer.GetClickAmountTotal(item, name);
					}
					else if (statName == "clickAmount")
					{
						return clickerPlayer.clickAmount;
					}
					else if (statName == "clickerPerSecond")
					{
						return clickerPlayer.clickerPerSecond;
					}

					throw new Exception($"Call Error: The statName argument for the attempted message, \"{message}\" has no valid entry point.");
				}
				else if (message == "GetArmorSet")
				{
					var player = args[index + 0] as Player;
					var setName = args[index + 1] as string;
					if (setName == null)
					{
						throw new Exception($"Call Error: The setName argument for the attempted message, \"{message}\" has returned null.");
					}
					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					//setMotherboard, setOverclock, setPrecursor, setMice
					if (setName == "Motherboard")
					{
						return clickerPlayer.setMotherboard;
					}
					else if (setName == "Overclock")
					{
						return clickerPlayer.setOverclock;
					}
					else if (setName == "Precursor")
					{
						return clickerPlayer.setPrecursor;
					}
					else if (setName == "Mice")
					{
						return clickerPlayer.setMice;
					}

					throw new Exception($"Call Error: The setName argument for the attempted message, \"{message}\" has no valid entry point.");
				}
				else if (message == "GetAccessory")
				{
					var player = args[index + 0] as Player;
					var accName = args[index + 1] as string;
					if (accName == null)
					{
						throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has returned null.");
					}
					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					//accEnchantedLED, accEnchantedLED2, accHandCream, accGlassOfMilk, accCookie, accCookie2, accClickingGlove, accAncientClickingGlove, accRegalClickingGlove
					if (accName == "ChocolateChip")
					{
						if (apiVersion < latestVersion)
						{
							//Kept for backwards compatibility
							return clickerPlayer.HasClickEffect(ClickEffect.ChocolateChip);
						}
						throw new Exception("GetAccessory+'ChocolateChip' is obsolete, use HasClickEffect+'ClickerClass:ChocolateChip'");
					}
					else if (accName == "EnchantedLED")
					{
						return clickerPlayer.accEnchantedLED || clickerPlayer.accEnchantedLED2;
					}
					else if (accName == "HandCream")
					{
						return clickerPlayer.accHandCream;
					}
					else if (accName == "StickyKeychain")
					{
						if (apiVersion < latestVersion)
						{
							//Kept for backwards compatibility
							return clickerPlayer.HasClickEffect(ClickEffect.StickyKeychain);
						}
						throw new Exception("GetAccessory+'StickyKeychain' is obsolete, use HasClickEffect+'ClickerClass:StickyKeychain'");
					}
					else if (accName == "GlassOfMilk")
					{
						return clickerPlayer.accGlassOfMilk;
					}
					else if (accName == "Cookie")
					{
						return clickerPlayer.accCookie || clickerPlayer.accCookie2;
					}
					else if (accName == "ClickingGlove")
					{
						return clickerPlayer.accClickingGlove;
					}
					else if (accName == "AncientClickingGlove")
					{
						return clickerPlayer.accAncientClickingGlove;
					}
					else if (accName == "RegalClickingGlove")
					{
						return clickerPlayer.accRegalClickingGlove;
					}

					throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has no valid entry point.");
				}
				else if (message == "SetPlayerStat")
				{
					var player = args[index + 0] as Player;
					var statName = args[index + 1] as string;
					if (statName == null)
					{
						throw new Exception($"Call Error: The statName argument for the attempted message, \"{message}\" has returned null.");
					}
					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					if (statName == "clickerCritAdd")
					{
						var crit = args[index + 2] as int?;
						if (crit == null)
						{
							throw new Exception($"Call Error: The crit argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerCrit += crit.Value;
						clickerPlayer.clickerCrit = Math.Max(4, clickerPlayer.clickerCrit);
						return success;
					}
					else if (statName == "clickerDamageFlatAdd")
					{
						var flat = args[index + 2] as int?;
						if (flat == null)
						{
							throw new Exception($"Call Error: The flat argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerDamageFlat += flat.Value;
						clickerPlayer.clickerDamageFlat = Math.Max(0, clickerPlayer.clickerDamageFlat);
						return success;
					}
					else if (statName == "clickerDamageAdd")
					{
						var damage = args[index + 2] as float?;
						if (damage == null)
						{
							throw new Exception($"Call Error: The damage argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerDamage += damage.Value;
						clickerPlayer.clickerDamage = Math.Max(1f, clickerPlayer.clickerDamage);
						return success;
					}
					else if (statName == "clickerBonusAdd")
					{
						var bonus = args[index + 2] as int?;
						if (bonus == null)
						{
							throw new Exception($"Call Error: The bonus argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerBonus += bonus.Value;
						clickerPlayer.clickerBonus = Math.Max(1, clickerPlayer.clickerBonus);
						return success;
					}
					else if (statName == "clickerBonusPercentAdd")
					{
						var bonus = args[index + 2] as float?;
						if (bonus == null)
						{
							throw new Exception($"Call Error: The bonus argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerBonusPercent += bonus.Value;
						clickerPlayer.clickerBonusPercent = Math.Max(1f, clickerPlayer.clickerBonusPercent);
						return success;
					}
					else if (statName == "clickerRadiusAdd")
					{
						var radius = args[index + 2] as float?;
						if (radius == null)
						{
							throw new Exception($"Call Error: The radius argument for the attempted message, \"{message}\" has returned null.");
						}
						clickerPlayer.clickerRadius += radius.Value;
						clickerPlayer.clickerRadius = Math.Max(1f, clickerPlayer.clickerRadius);
						return success;
					}

					throw new Exception($"Call Error: The statName argument for the attempted message, \"{message}\" has no valid entry point.");
				}
				else if (message == "SetAccessory")
				{
					var player = args[index + 0] as Player;
					var accName = args[index + 1] as string;
					if (accName == null)
					{
						throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has returned null.");
					}
					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					//accEnchantedLED, accEnchantedLED2, accHandCream, accGlassOfMilk, accCookie, accCookie2, accClickingGlove, accAncientClickingGlove, accRegalClickingGlove
					if (accName == "ChocolateChip")
					{
						//Kept for backwards compatibility
						if (apiVersion < latestVersion)
						{
							clickerPlayer.EnableClickEffect(ClickEffect.ChocolateChip);
							return success;
						}
						throw new Exception("SetAccessory+'ChocolateChip' is obsolete, use EnableClickEffect+'ClickerClass:ChocolateChip'");
					}
					else if (accName == "EnchantedLED")
					{
						clickerPlayer.accEnchantedLED = true;
						return success;
					}
					else if (accName == "EnchantedLED2")
					{
						clickerPlayer.accEnchantedLED2 = true;
						return success;
					}
					else if (accName == "HandCream")
					{
						clickerPlayer.accHandCream = true;
						return success;
					}
					else if (accName == "StickyKeychain")
					{
						//Kept for backwards compatibility
						if (apiVersion < latestVersion)
						{
							clickerPlayer.EnableClickEffect(ClickEffect.StickyKeychain);
							return success;
						}
						throw new Exception("SetAccessory+'StickyKeychain' is obsolete, use EnableClickEffect+'ClickerClass:StickyKeychain'");
					}
					else if (accName == "GlassOfMilk")
					{
						clickerPlayer.accGlassOfMilk = true;
						return success;
					}
					else if (accName == "Cookie")
					{
						clickerPlayer.accCookie = true;
						return success;
					}
					else if (accName == "Cookie2")
					{
						clickerPlayer.accCookie2 = true;
						return success;
					}
					else if (accName == "ClickingGlove")
					{
						clickerPlayer.accClickingGlove = true;
						return success;
					}
					else if (accName == "AncientClickingGlove")
					{
						clickerPlayer.accAncientClickingGlove = true;
						return success;
					}
					else if (accName == "RegalClickingGlove")
					{
						clickerPlayer.accRegalClickingGlove = true;
						return success;
					}

					throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has no valid entry point.");
				}
				else if (message == "EnableClickEffect")
				{
					var player = args[index + 0] as Player;

					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

					var effectName = args[index + 1] as string;
					var effectNames = args[index + 1] as IEnumerable<string>; //type variation

					if (effectName != null)
					{
						clickerPlayer.EnableClickEffect(effectName);
						return success;
					}
					else if (effectNames != null)
					{
						clickerPlayer.EnableClickEffect(effectNames);
						return success;
					}
					else
					{
						throw new Exception($"Call Error: The effectName/effectNames argument for the attempted message, \"{message}\" has returned null.");
					}
				}
				else if (message == "HasClickEffect")
				{
					var player = args[index + 0] as Player;
					var effectName = args[index + 1] as string;

					if (player == null)
					{
						throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
					}
					if (effectName == null)
					{
						throw new Exception($"Call Error: The effectName argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
					return clickerPlayer.HasClickEffect(effectName);
				}
			}
			catch (Exception e)
			{
				clickerClass.Logger.Error($"Call Error: {e.StackTrace} {e.Message}");
			}
			return null;
		}
	}
}
