using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class RGBGreaves : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow"),
					Color = () => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0) * 0.75f
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 45000;
			Item.rare = 4;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.RainbowBrick, 35).AddIngredient(ItemID.CrystalShard, 20).AddTile(TileID.Anvils).Register();
		}
	}
}
