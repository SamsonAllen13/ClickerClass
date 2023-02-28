using ClickerClass.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PrecursorBreastplate : ClickerItem
	{
		public static readonly float DamageIncrease = 0.15f;
		public static readonly float RadiusDecrease = 0.5f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease * 100f, RadiusDecrease * 100f);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(255, 255, 255, 0) * 0.8f * 0.5f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 1, 60, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += DamageIncrease;
			clickerPlayer.clickerRadius -= 2 * RadiusDecrease;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.LunarTabletFragment, 18).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
