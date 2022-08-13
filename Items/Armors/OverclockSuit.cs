using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class OverclockSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(255, 255, 255, 0) * 0.8f * 0.75f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 1, 10, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HallowedBar, 22).AddIngredient(ItemID.SoulofMight, 6).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
