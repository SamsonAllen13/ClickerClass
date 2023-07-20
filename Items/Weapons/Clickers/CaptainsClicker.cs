using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CaptainsClicker : ClickerWeapon
	{
		public static readonly int CannonballAmount = 4;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Bombard = ClickerSystem.RegisterClickEffect(Mod, "Bombard", 12, new Color(255, 225, 50), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item14, position);

				for (int k = 0; k < CannonballAmount; k++)
				{
					Vector2 startSpot = new Vector2(position.X + Main.rand.Next(-150, 151), position.Y - 500 + Main.rand.Next(-25, 26));
					Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-25, 26), position.Y + Main.rand.Next(-25, 26));
					Vector2 vector = endSpot - startSpot;
					float speed = 8f + Main.rand.NextFloat(-1f, 1f);
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}
					Projectile.NewProjectile(source, startSpot, vector, ModContent.ProjectileType<CaptainsClickerPro>(), damage, knockBack, player.whoAmI, endSpot.X, endSpot.Y);
				}
			},
			descriptionArgs: new object[] { CannonballAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.15f);
			SetColor(Item, new Color(255, 225, 50));
			SetDust(Item, 10);
			AddEffect(Item, ClickEffect.Bombard);

			Item.damage = 26;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 3, 60, 0);
			Item.rare = ItemRarityID.LightRed;
		}
	}
}
