using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class AimbotModule : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 50000;
			Item.rare = 6;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += 0.10f;
			clickerPlayer.clickerRadius += 0.2f;
			clickerPlayer.accAimbotModule = true;
			clickerPlayer.accAimbotModule2 = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			ClickerPlayer clickerPlayer = Main.LocalPlayer.GetModPlayer<ClickerPlayer>();

			bool enabled = clickerPlayer.accAimbotModule2Toggle;

			tooltips.Add(new TooltipLine(Mod, "AimbotEnabled", LangHelper.GetText("Tooltip.AimbotModule" + (enabled ? "Enabled" : "Disabled")))
			{
				OverrideColor = enabled ? Color.Lerp(Color.Red, Color.White, 0.6f) : Color.Gray
			});
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<AimAssistModule>(), 1).AddIngredient(ItemID.AvengerEmblem, 1).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
