using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CyclopsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Insanity = ClickerSystem.RegisterClickEffect(Mod, "Insanity", 8, new Color(75, 75, 75), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				float dist = 100f;
				float randomCircle = Main.rand.NextFloat(MathHelper.TwoPi);
				Vector2 randomCircleVector = randomCircle.ToRotationVector2();

				Vector2 spawnposition = position - randomCircleVector * (dist * 0.5f);
				Vector2 spawnvelocity = randomCircleVector * 2;
				Projectile.NewProjectile(source, spawnposition, spawnvelocity, ModContent.ProjectileType<CyclopsClickerPro>(), damage, knockBack, Main.myPlayer);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(125, 125, 125));
			SetDust(Item, 54);
			AddEffect(Item, ClickEffect.Insanity);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 1, 75, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
