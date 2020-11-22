using ClickerClass.Effects;
using ClickerClass.Items;
using ClickerClass.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		/// <summary>
		/// To prevent certain methods being called when they shouldn't
		/// </summary>
		internal static bool finalizedRegisterCompat = false;

		public override void Load()
		{
			finalizedRegisterCompat = false;
			mod = this;
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
			ClickerSystem.Load();

			if (!Main.dedServ)
			{
				LoadClient();
			}
		}

		public override void Unload()
		{
			finalizedRegisterCompat = false;
			ShaderManager.Unload();
			ClickerSystem.Unload();
			ClickerInterfaceResources.Unload();
			AutoClickKey = null;
			mod = null;
		}

		private void LoadClient()
		{
			ShaderManager.Load();
			ClickerInterfaceResources.Load();
		}

		private GameTime _lastUpdateUIGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			_lastUpdateUIGameTime = gameTime;
			ClickerInterfaceResources.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			ClickerInterfaceResources.AddDrawLayers(layers);

			//Remove Mouse Cursor
			if (Main.cursorOverride == -1 && ClickerConfigClient.Instance.ShowCustomCursors)
			{
				Player player = Main.LocalPlayer;
				if (ClickerCursor.CanDrawCursor(player.HeldItem))
				{
					for (int i = 0; i < layers.Count; i++)
					{
						if (layers[i].Name.Equals("Vanilla: Cursor"))
						{
							layers.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		public override object Call(params object[] args)
		{
			//TODO all calls
			// Simplify code by resizing args
			Array.Resize(ref args, 25);
			string success = "Success";
			try
			{
				string message = args[0] as string;

				//Future-proofing. Allowing new info to be returned while maintaining backwards compat if necessary
				object apiString = args[1];
				Version apiVersion = apiString is string ? new Version(apiString as string) : Version;

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
					if (modItem == null)
					{
						throw new Exception($"Call Error: The modItem argument for the attempted message, \"{message}\" has returned null.");
					}

					ClickerSystem.RegisterClickerWeapon(modItem);
					return success;
				}
				else if (message == "IsClickerProj")
				{
					var proj = args[index + 0] as Projectile;
					var type = args[index + 0] as int?; //Try another type variation because of overload
					if (proj == null || type == null)
					{
						throw new Exception($"Call Error: The projectile/type argument for the attempted message, \"{message}\" has returned null.");
					}
					else if (proj != null)
					{
						return ClickerSystem.IsClickerProj(proj);
					}
					else
					{
						return ClickerSystem.IsClickerProj(type.Value);
					}
				}
				else if (message == "IsClickerItem")
				{
					var item = args[index + 0] as Item;
					var type = args[index + 0] as int?; //Try another type variation because of overload
					if (item == null || type == null)
					{
						throw new Exception($"Call Error: The item/type argument for the attempted message, \"{message}\" has returned null.");
					}
					else if (item != null)
					{
						return ClickerSystem.IsClickerItem(item);
					}
					else
					{
						return ClickerSystem.IsClickerItem(type.Value);
					}
				}
				else if (message == "IsClickerWeapon")
				{
					var item = args[index + 0] as Item;
					var type = args[index + 0] as int?; //Try another type variation because of overload
					if (item == null || type == null)
					{
						throw new Exception($"Call Error: The item/type argument for the attempted message, \"{message}\" has returned null.");
					}
					else if (item != null)
					{
						return ClickerSystem.IsClickerWeapon(item);
					}
					else
					{
						return ClickerSystem.IsClickerWeapon(type.Value);
					}
				}
				//Clicker Weapon/Item specifics now
				else if (message == "SetAmount")
				{
					var item = args[index + 0] as Item;
					var amount = args[index + 1] as int?;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}
					if (amount == null)
					{
						amount = 1;
					}

					ClickerWeapon.SetAmount(item, amount.Value);
					return success;
				}
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
					//TODO big todo still
					var item = args[index + 0] as Item;
					var name = args[index + 1] as string;
					if (item == null)
					{
						throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
					}
					if (name == null)
					{
						name = ClickerItemCore.NULL;
					}

					ClickerWeapon.SetEffect(item, name);
					return success;
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
				//Clicker Player specifics now
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

						return clickerPlayer.GetClickAmountTotal(item);
					}
					else if (statName == "clickAmount")
					{
						return clickerPlayer.clickAmount;
					}
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
				}
			}
			catch (Exception e)
			{
				Logger.Error($"Call Error: {e.StackTrace} {e.Message}");
			}
			return null;
		}

		public override void AddRecipes()
		{
			ClickerRecipes.AddRecipes();
		}

		public override void PostAddRecipes()
		{
			finalizedRegisterCompat = true;
		}

		public override void AddRecipeGroups()
		{
			ClickerRecipes.AddRecipeGroups();
		}
	}
}
