using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BoneClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Bone Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 1.1f);
			SetColor(item, new Color(225, 225, 200, 0));
			SetDust(item, 216);
			SetAmount(item, 12);
			SetEffect(item, "Lacerate");


			item.damage = 13;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.noMelee = true;
			item.value = 15000;
			item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilOre, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
