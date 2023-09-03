using ClickerClass.Buffs;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AshenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			
			ClickEffect.Bold = ClickerSystem.RegisterClickEffect(Mod, "Bold", 1, new Color(255, 150, 150), null, preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.15f);
			SetColor(Item, new Color(255, 150, 150));
			SetDust(Item, 286);
			AddEffect(Item, ClickEffect.Bold);

			Item.damage = 3;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 0, 15);
			Item.rare = ItemRarityID.Blue;
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			int y = (int)(player.position.Y / 16);
			float fac = y / (Main.maxTilesY * 0.166f) + 0.55f;
			int increase = 6 - (int)fac;
			
			damage.Flat = increase;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.AshWood, 10).AddTile(TileID.WorkBenches).Register();
		}
	}
}
