using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MouseClicker : ClickerWeapon
	{
		public static readonly int CheeseAmount = 3;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.MouseTrap = ClickerSystem.RegisterClickEffect(Mod, "MouseTrap", 10, new Color(80, 80, 80), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < CheeseAmount; k++)
				{
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-5f, -2f), ModContent.ProjectileType<MouseClickerPro>(), damage / 2, 0f, player.whoAmI);
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { CheeseAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.5f);
			SetColor(Item, new Color(80, 80, 80));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.MouseTrap);

			Item.damage = 54;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
