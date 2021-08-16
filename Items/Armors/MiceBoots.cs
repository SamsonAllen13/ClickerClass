using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MiceBoots : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 105000;
			Item.rare = 10;
			Item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += 0.06f;
			player.moveSpeed += 0.20f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 15).AddIngredient(ItemID.LunarBar, 12).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
