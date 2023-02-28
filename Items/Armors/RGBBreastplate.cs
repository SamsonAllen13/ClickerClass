using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class RGBBreastplate : ClickerItem
	{
		public static readonly int RadiusIncrease = 25;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RadiusIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 75) * 0.8f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 2 * RadiusIncrease / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.RainbowBrick, 40).AddIngredient(ItemID.CrystalShard, 25).AddTile(TileID.Anvils).Register();
		}
	}
}
