using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items
{
	[AutoloadEquip(EquipType.Wings)]
	public class TheScroller : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Scroller");
			Tooltip.SetDefault("Allows flight and slow fall"
							+ "\nHold DOWN to fall faster");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 100000;
			item.rare = 10;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 180;
			if (player.controlUp)
			{
				player.maxFallSpeed /= 2f;
			}
			if (player.controlDown)
			{
				player.maxFallSpeed *= player.wet ? 2.25f : 2.5f;
			}
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.5f;
			ascentWhenRising = 0.35f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.35f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 3.5f;
			acceleration *= 1.15f;
		}

		public override bool WingUpdate(Player player, bool inUse)
		{
			if (inUse)
			{
				player.flapSound = true;
				int rate = 6;
				if (player.wings == player.wingsLogic)
				{
					if (player.controlUp)
					{
						rate = 3;
					}
					else if (player.controlDown)
					{
						rate = 10;
					}
				}
				if (player.miscCounter % (rate * 2) == 0)
				{
					Main.PlaySound(SoundID.Item24, player.position);
				}

				if (player.miscCounter % rate == 0)
				{
					int numDusts = 20;
					Vector2 playerVelocity = player.velocity * 0.4f + Vector2.UnitY * player.gravDir * 6;
					Vector2 playerOffset = player.Center + new Vector2(16 * -player.direction, 12 * player.gravDir);
					for (int i = 0; i < numDusts; i++)
					{
						Vector2 position = -Vector2.UnitY.RotatedBy(i * MathHelper.TwoPi / numDusts) * new Vector2(1f, 0.25f);
						Vector2 velocity = playerVelocity + position * 1.25f;
						position = position * 8 + playerOffset;
						Dust dust = Dust.NewDustPerfect(position, mod.DustType("MiceDust"), velocity);
						dust.noGravity = true;
						dust.scale = 0.8f + rate * 0.04f;
						dust.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
					}
				}
			}
			else if (!inUse && player.controlJump && !player.controlDown && player.velocity.Y * player.gravDir > 0f)
			{
				if (player.miscCounter % 20 == 0)
				{
					int numDusts = 25;
					Vector2 playerVelocity = player.velocity * 0.4f + Vector2.UnitY * player.gravDir * 6;
					Vector2 playerOffset = player.Center + new Vector2(15.25f * -player.direction, 14 * player.gravDir);
					for (int i = 0; i < numDusts; i++)
					{
						Vector2 position = -Vector2.UnitY.RotatedBy(i * MathHelper.TwoPi / numDusts) * new Vector2(1f, 0.25f);
						Vector2 velocity = playerVelocity + position * 2;
						position = position * 8 + playerOffset;
						Dust dust = Dust.NewDustPerfect(position, mod.DustType("MiceDust"), velocity);
						dust.noGravity = true;
						dust.fadeIn = 1f;
						dust.scale = 0.8f;
						dust.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
					}
				}
			}
			return base.WingUpdate(player, inUse);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MiceFragment", 14);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}