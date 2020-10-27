using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	[AutoloadEquip(EquipType.Head)]
	public class MotherboardHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Motherboard Helmet");
			Tooltip.SetDefault("Increases your base click radius by 20%");
		}

		public override void SetDefaults()
		{
			isClicker = true;
			item.width = 18;
			item.height = 18;
			item.value = 20000;
			item.rare = 3;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.4f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MotherboardSuit") && legs.type == mod.ItemType("MotherboardBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Right click to place a radius extending sensor";
			player.GetModPlayer<ClickerPlayer>().clickerMotherboardSet = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("ClickerClass:SilverBar", 15);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}