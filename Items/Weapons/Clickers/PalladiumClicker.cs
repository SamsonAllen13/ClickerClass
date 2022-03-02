using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PalladiumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Regenerate = ClickerSystem.RegisterClickEffect(Mod, "Regenerate", null, null, 8, new Color(250, 150, 100), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
				player.AddBuff(BuffID.RapidHealing, 120, false);
				for (int i = 0; i < 15; i++)
				{
					int index = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<LoveDust>(), 0f, 0f, 0, Color.White, 1f);
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
			SetRadius(Item, 2.85f);
			SetColor(Item, new Color(250, 150, 100));
			SetDust(Item, 144);
			AddEffect(Item, ClickEffect.Regenerate);

			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 92000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.PalladiumBar, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
