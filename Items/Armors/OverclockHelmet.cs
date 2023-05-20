using ClickerClass.DrawLayers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class OverclockHelmet : ClickerItem
	{
		public static readonly int ClickAmount = 100;
		public static readonly int SetBonusDamageDecrease = 60;
		public static readonly int ClickAmountDecreaseFlat = 1;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ClickAmountDecreaseFlat);

		public static LocalizedText SetBonusText { get; private set; }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow"),
					Color = (PlayerDrawSet drawInfo) => Color.White * 0.8f * 0.75f
				});
			}

			SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(ClickAmount, SetBonusDamageDecrease);
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 0, 60, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += ClickAmountDecreaseFlat;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<OverclockSuit>() && legs.type == ModContent.ItemType<OverclockBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = SetBonusText.ToString();
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
