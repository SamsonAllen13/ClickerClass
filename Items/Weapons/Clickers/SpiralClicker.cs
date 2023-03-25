using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpiralClicker : ClickerWeapon
	{
		public static readonly int SpiralAmount = 2;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.BloodSucker = ClickerSystem.RegisterClickEffect(Mod, "BloodSucker", 8, new Color(160, 40, 35), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.NPCHit13, position);

				for (int index = 0; index < SpiralAmount; index++)
				{
					Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<SpiralClickerPro>(), (int)(damage * 0.5f), 1f, player.whoAmI, index);
				}
			},
			descriptionArgs: new object[] { SpiralAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3f);
			SetColor(Item, new Color(160, 40, 35));
			SetDust(Item, 5);
			AddEffect(Item, ClickEffect.BloodSucker);

			Item.damage = 22;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 3, 60, 0);
			Item.rare = ItemRarityID.LightRed;
		}
	}
}
