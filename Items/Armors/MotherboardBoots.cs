using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MotherboardBoots : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 25000;
			Item.rare = 3;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.06f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddRecipeGroup("ClickerClass:SilverBar", 20).AddIngredient(ItemID.Wire, 50).AddTile(TileID.Anvils).Register();
		}
	}
}