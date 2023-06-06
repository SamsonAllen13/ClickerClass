using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class MotherboardHelmet : ClickerItem
	{
		public static readonly int RadiusIncrease = 20;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RadiusIncrease);

		public static LocalizedText SetBonusText { get; private set; }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			SetBonusText = this.GetLocalization("SetBonus");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 2 * RadiusIncrease / 100f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MotherboardSuit>() && legs.type == ModContent.ItemType<MotherboardBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = SetBonusText.ToString();
			player.GetModPlayer<ClickerPlayer>().setMotherboard = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup(ClickerRecipes.AnySilverBarGroup, 15).AddIngredient(ItemID.Wire, 25).AddTile(TileID.Anvils).Register();
		}
	}
}
