using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ChlorophyteClicker : ClickerWeapon
	{
		public static readonly int CloudAmount = 10;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Nature, projsInheritItemElements: true);

			ClickEffect.ToxicRelease = ClickerSystem.RegisterClickEffect(Mod, "ToxicRelease", 10, new Color(175, 255, 100), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < CloudAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<ChlorophyteClickerPro>(), (int)(damage * 0.25f), 0f, player.whoAmI, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			descriptionArgs: new object[] { CloudAmount });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.75f);
			SetColor(Item, new Color(175, 255, 100));
			SetDust(Item, 89);
			AddEffect(Item, ClickEffect.ToxicRelease);

			Item.damage = 38;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Lime;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.ChlorophyteBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
