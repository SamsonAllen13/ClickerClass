using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpectreClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			//Special tooltip set before this normally, but we use lang keys so it's handled automatically
			base.SetStaticDefaults();

			ClickEffect.PhaseReach = ClickerSystem.RegisterClickEffect(Mod, "PhaseReach", null, null, 1, new Color(100, 255, 255), null);
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
			Item.value = 450000;
			Item.rare = 8;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.SpectreBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
