using ClickerClass.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class PrecursorHelmet : ClickerItem
	{
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
			return body.type == ModContent.ItemType<PrecursorBreastplate>() && legs.type == ModContent.ItemType<PrecursorGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = LangHelper.GetText("SetBonus.Precursor");
			player.GetModPlayer<ClickerPlayer>().setPrecursor = true;
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