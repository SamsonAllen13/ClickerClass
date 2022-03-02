using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShroomiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.AutoClick = ClickerSystem.RegisterClickEffect(Mod, "AutoClick", null, null, 20, new Color(150, 150, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(ModContent.BuffType<AutoClick>(), 300, false);
				for (int i = 0; i < 15; i++)
				{
					int index = Dust.NewDust(player.position, 20, 20, 15, 0f, 0f, 255, default(Color), 1.5f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.velocity *= 0.75f;
					int x = Main.rand.Next(-50, 51);
					int y = Main.rand.Next(-50, 51);
					dust.position.X += x;
					dust.position.Y += y;
					dust.velocity.X = -x * 0.075f;
					dust.velocity.Y = -y * 0.075f;
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(150, 150, 255));
			SetDust(Item, 113);
			AddEffect(Item, ClickEffect.AutoClick);

			Item.damage = 64;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 450000;
			Item.rare = 8;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.ShroomiteBar, 8).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}
