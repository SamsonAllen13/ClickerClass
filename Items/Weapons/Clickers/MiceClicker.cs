using ClickerClass.DrawLayers;
using ClickerClass.Dusts;
using ClickerClass.Projectiles;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MiceClicker : ClickerWeapon
	{
		public static readonly int BoltAmount = 8;

		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Celestial, projsInheritItemElements: true);

			ClickEffect.Collision = ClickerSystem.RegisterClickEffect(Mod, "Collision", 10, new Color(150, 150, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				bool spawnEffects = true;
				for (int k = 0; k < BoltAmount; k++)
				{
					float hasSpawnEffects = spawnEffects ? 1f : 0f;
					Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), ModContent.ProjectileType<MiceClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, (int)DateTime.Now.Ticks, hasSpawnEffects);
					spawnEffects = false;
				}
			},
			descriptionArgs: new object[] { BoltAmount });

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));

				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = glowmask.Value,
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 50) * 0.7f
				});
			}
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(150, 150, 225));
			SetDust(Item, ModContent.DustType<MiceDust>());
			AddEffect(Item, ClickEffect.Collision);

			Item.damage = 94;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Red;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value.Value, new Color(255, 255, 255, 50) * 0.7f, rotation, scale);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 18).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
