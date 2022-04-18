using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TorchClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Smite = ClickerSystem.RegisterClickEffect(Mod, "Smite", null, null, 10, new Color(255, 245, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 42);
				
				//Temporary effect//
				//Consider idea where up to 10 nearby torches fire their respective flame color towards cursor and each one does something different on hit
				for (int k = 0; k < 4 + Main.rand.Next(5); k++)
				{
					float xChoice = Main.rand.Next(-100, 101);
					float yChoice = Main.rand.Next(-100, 101);
					xChoice += xChoice > 0 ? 200 : -200;
					yChoice += yChoice > 0 ? 200 : -200;
					Vector2 startSpot = new Vector2(Main.MouseWorld.X + xChoice, Main.MouseWorld.Y + yChoice);
					Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-10, 11), Main.MouseWorld.Y + Main.rand.Next(-10, 11));
					Vector2 vector = endSpot - startSpot;
					float speed = 2f;
					float mag = vector.Length();
					if (mag > speed)
					{
						mag = speed / mag;
						vector *= mag;
					}

					int torch = ModContent.ProjectileType<TorchClickerPro>();
					Projectile.NewProjectile(source, startSpot, vector, torch, (int)(damage * 0.25f), 0f, player.whoAmI, 0f, 0f);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.55f);
			SetColor(Item, new Color(255, 245, 225));
			SetDust(Item, 6);
			AddEffect(Item, ClickEffect.Smite);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 35000;
			Item.rare = 2;
		}
		
		//TODO - Add dire's figured out 'Torch God loot' feature
	}
}
