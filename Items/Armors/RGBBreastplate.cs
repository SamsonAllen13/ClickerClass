using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class RGBBreastplate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50) * 0.8f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = 4;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.RainbowBrick, 40).AddIngredient(ItemID.CrystalShard, 25).AddTile(TileID.Anvils).Register();
		}
	}
}
