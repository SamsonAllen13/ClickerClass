using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class MiceSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, new Color(255, 255, 255, 0) * 0.8f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 140000;
			Item.rare = 10;
			Item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.10f;
			clickerPlayer.clickerRadius += 0.5f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 20).AddIngredient(ItemID.LunarBar, 16).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
