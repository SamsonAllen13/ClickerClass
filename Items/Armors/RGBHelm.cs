using ClickerClass.DrawLayers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class RGBHelm : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow"),
					Color = () => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0) * 0.75f
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 40000;
			Item.rare = 4;
			Item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<RGBBreastplate>() && legs.type == ModContent.ItemType<RGBGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = LangHelper.GetText("SetBonus.RGB");
			player.GetModPlayer<ClickerPlayer>().setRGB = true;
		}

		public override void UpdateVanitySet(Player player)
		{
			Lighting.AddLight(player.Center, Main.DiscoColor.ToVector3() * 0.75f);
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.RainbowBrick, 25).AddIngredient(ItemID.CrystalShard, 15).AddTile(TileID.Anvils).Register();
		}
	}
}
