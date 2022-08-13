using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ClickerClass.Items.Accessories
{
	public class ChocolateChip : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.ChocolateChip = ClickerSystem.RegisterClickEffect(Mod, "ChocolateChip", null, null, 15, new Color(165, 110, 60, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				int chocolate = ModContent.ProjectileType<ChocolateChipPro>();
				for (int k = 0; k < 6; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f), chocolate, Math.Max(1, (int)(damage * 0.2f)), 0f, player.whoAmI, Main.rand.Next(Main.projFrames[chocolate]), hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().EnableClickEffect(ClickEffect.ChocolateChip);
		}
	}
}
