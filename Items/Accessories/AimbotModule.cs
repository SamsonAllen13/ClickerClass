using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class AimbotModule : ClickerItem
	{
		public static readonly int DamageIncrease = 10;
		public static readonly int RadiusIncrease = 10;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease, RadiusIncrease);

		public static LocalizedText EnabledText { get; private set; }
		public static LocalizedText DisabledText { get; private set; }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			EnabledText = this.GetLocalization("Enabled");
			DisabledText = this.GetLocalization("Disabled");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
			clickerPlayer.clickerRadius += 2 * RadiusIncrease / 100f;
			clickerPlayer.accAimbotModule = true;
			clickerPlayer.accAimbotModule2 = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			ClickerPlayer clickerPlayer = Main.LocalPlayer.GetModPlayer<ClickerPlayer>();

			bool enabled = clickerPlayer.accAimbotModule2Toggle;

			tooltips.Add(new TooltipLine(Mod, "AimbotEnabled", (enabled ? EnabledText : DisabledText).ToString())
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
