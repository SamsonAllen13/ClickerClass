using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class MiceMask : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Mask");
			Tooltip.SetDefault("Increases click damage by 4%"
							+ "\nIncreases click critical strike chance by 6%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 70000;
			item.rare = 10;
			item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.04f;
			clickerPlayer.clickerCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MiceSuit") && legs.type == mod.ItemType("MiceBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Right clicking a position within your clicker radius will teleport you to it";
			player.GetModPlayer<ClickerPlayer>().clickerMiceSet = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 10);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}