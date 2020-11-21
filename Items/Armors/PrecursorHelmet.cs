using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class PrecursorHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Precursor Helmet");
			Tooltip.SetDefault("Increases click damage by 8%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 50000;
			item.rare = 8;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.08f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("PrecursorBreastplate") && legs.type == mod.ItemType("PrecursorGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Clicking causes an additional delayed fiery click at 25% the damage";
			player.GetModPlayer<ClickerPlayer>().clickerPrecursorSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarTabletFragment, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}