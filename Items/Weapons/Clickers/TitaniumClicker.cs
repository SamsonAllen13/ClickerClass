using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TitaniumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.RazorsEdge = ClickerSystem.RegisterClickEffect(Mod, "RazorsEdge", 12, new Color(150, 150, 150), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int index = 0; index < 5; index++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TitaniumClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI, index, hasSpawnEffects);
					spawnEffects = false;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.25f);
			SetColor(Item, new Color(150, 150, 150));
			SetDust(Item, 146);
			AddEffect(Item, ClickEffect.RazorsEdge);

			Item.damage = 32;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 3, 22, 0);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.TitaniumBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
