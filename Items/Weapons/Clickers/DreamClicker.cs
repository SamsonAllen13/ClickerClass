using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class DreamClicker : ClickerWeapon
	{
		public static readonly int StarStrikesAmount = 4;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.StarSlice = ClickerSystem.RegisterClickEffect(Mod, "StarSlice", 8, new Color(255, 235, 180), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < StarStrikesAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<DreamClickerPro>(), damage, knockBack, Main.myPlayer, hasSpawnEffects, k);
					spawnEffects = false;
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { StarStrikesAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.15f);
			SetColor(Item, new Color(255, 235, 180));
			SetDust(Item, 57);
			AddEffect(Item, ClickEffect.StarSlice);

			Item.damage = 9;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.rare = ItemRarityID.Green;
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().itemDreamClicker = true;
		}

		//TODO - Make it rarely fall out of the sky like fallen stars but only during a shooting star night
	}
}
