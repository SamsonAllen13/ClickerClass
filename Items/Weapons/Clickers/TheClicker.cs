using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TheClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("The Clicker");

			//TODO orphaned?
			ClickEffect.TheClick = ClickerSystem.RegisterClickEffect(mod, "TheClick", "The Click", "Causes the clicker's attacks to additionally deal damage equal to 1% of the enemy's maximum life", 1, new Color(255, 255, 255, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{

			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 6f);
			SetColor(item, new Color(255, 255, 255, 0));
			SetDust(item, 91);
			AddEffect(item, ClickEffect.TheClick);

			item.damage = 150;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
		}
	}
}
