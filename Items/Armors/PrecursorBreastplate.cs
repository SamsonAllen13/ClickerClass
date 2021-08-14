using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PrecursorBreastplate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, new Color(255, 255, 255, 0) * 0.8f * 0.5f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 80000;
			Item.rare = 8;
			Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.15f;
			clickerPlayer.clickerRadius -= 1f;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LunarTabletFragment, 18).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}