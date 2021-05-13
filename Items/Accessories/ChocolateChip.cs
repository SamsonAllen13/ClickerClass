using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class ChocolateChip : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ChocolateChip = ClickerSystem.RegisterClickEffect(mod, "ChocolateChip", null, null, 15, new Color(165, 110, 60, 0), delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				int chocolate = ModContent.ProjectileType<ChocolateChipPro>();
				for (int k = 0; k < 6; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f), chocolate, (int)(damage * 0.2), 0f, player.whoAmI, Main.rand.Next(Main.projFrames[chocolate]), hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 50000;
			item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.ChocolateChip);
		}
	}
}
