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
	public class OverclockHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow"),
					Color = Color.White * 0.8f * 0.75f
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 30000;
			Item.rare = 6;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<OverclockSuit>() && legs.type == ModContent.ItemType<OverclockBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = LangHelper.GetText("SetBonus.Overclock");
			player.GetModPlayer<ClickerPlayer>().setOverclock = true;
		}

		public override void UpdateVanitySet(Player player)
		{
			Lighting.AddLight(player.Center, 0.3f, 0.075f, 0.075f);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.HallowedBar, 14).AddIngredient(ItemID.SoulofSight, 6).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
