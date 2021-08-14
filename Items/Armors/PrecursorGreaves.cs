using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class PrecursorGreaves : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow"),
					Color = Color.White * 0.8f * 0.5f
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 65000;
			Item.rare = 8;
			Item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.15f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LunarTabletFragment, 15).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
