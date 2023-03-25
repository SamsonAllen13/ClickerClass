using ClickerClass.DrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class OverclockBoots : ClickerItem
	{
		public static readonly int MoveSpeedIncrease = 10;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedIncrease);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				LegsLayer.RegisterData(Item.legSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Legs_Glow"),
					Color = (PlayerDrawSet drawInfo) => Color.White * 0.8f * 0.75f
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 80, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += MoveSpeedIncrease / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HallowedBar, 18).AddIngredient(ItemID.SoulofFright, 6).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
