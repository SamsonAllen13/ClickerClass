using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MouseClicker : ClickerWeapon
	{
		public static readonly int MouseTrapCount = 3;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Trap = ClickerSystem.RegisterClickEffect(Mod, "Trap", 10, new Color(120, 120, 120), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				//"Can't properly offload this into projectiles"
				SoundEngine.PlaySound(SoundID.Item152, position);
				
				for (int k = 0; k < MouseTrapCount; k++)
				{
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -2f), ModContent.ProjectileType<MouseClickerPro>(), damage / 2, 0f, player.whoAmI);
				}
			},
			descriptionArgs: new object[] { MouseTrapCount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5.5f);
			SetColor(Item, new Color(80, 80, 80));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.Trap);

			Item.damage = 54;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
