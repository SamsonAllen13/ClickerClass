using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TheClicker : ClickerWeapon
	{
		public static readonly int AdditionalDamageLifeRatio = 1;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.TheClick = ClickerSystem.RegisterClickEffect(Mod, "TheClick", 1, new Color(255, 255, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				//Spawned with damage 1, custom code in ModifyHitNPC
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TheClickerPro>(), 1, 0f, player.whoAmI);
			},
			descriptionArgs: new object[] { AdditionalDamageLifeRatio });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(255, 255, 255));
			SetDust(Item, 91);
			AddEffect(Item, ClickEffect.TheClick);

			Item.damage = 130;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Red;
		}
	}
}
