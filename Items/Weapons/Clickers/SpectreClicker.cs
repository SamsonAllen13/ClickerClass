using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpectreClicker : ClickerWeapon
	{
		public override LocalizedText Tooltip => this.GetLocalization("Tooltip");

		public override void SetStaticDefaults()
		{
			//Special tooltip set before this normally, but we use lang keys so it's handled automatically
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Arcane, projsInheritItemElements: true);

			ClickEffect.PhaseReach = ClickerSystem.RegisterClickEffect(Mod, "PhaseReach", 1, new Color(100, 255, 255), null);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 8.5f); //Radius doesn't matter with the Phase Reach effect, except for aimbot module.
								   //This is set to cover pretty much the entire screen with no buffs
			SetColor(Item, new Color(100, 255, 255));
			SetDust(Item, 88);
			AddEffect(Item, ClickEffect.PhaseReach);

			Item.damage = 50;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 9, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.SpectreBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
