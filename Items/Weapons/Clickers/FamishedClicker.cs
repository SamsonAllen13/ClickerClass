using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FamishedClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Devour = ClickerSystem.RegisterClickEffect(Mod, "Devour", null, null, 15, new Color(200, 125, 125), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				//Add effect here when I have the time
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(200, 125, 125));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Devour);

			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 50000;
			Item.rare = -1;
		}
	}
}
