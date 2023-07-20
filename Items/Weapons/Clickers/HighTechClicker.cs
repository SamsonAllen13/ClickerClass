using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HighTechClicker : ClickerWeapon
	{
		public static readonly int LaserAmount = 4;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				thoriumMod.Call("AddMartianItemID", Item.type);
			}

			ClickEffect.Discharge = ClickerSystem.RegisterClickEffect(Mod, "Discharge", 10, new Color(75, 255, 200), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < LaserAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<HighTechClickerPro>(), damage, 0f, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			descriptionArgs: new object[] { LaserAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(75, 255, 200));
			SetDust(Item, 226);
			AddEffect(Item, ClickEffect.Discharge);

			Item.damage = 86;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
