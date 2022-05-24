using ClickerClass.Dusts;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using ClickerClass.Utilities;
using ClickerClass.DrawLayers;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TorchClicker : ClickerWeapon
	{
		public static Asset<Texture2D> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Smite = ClickerSystem.RegisterClickEffect(Mod, "Smite", null, null, 10, new Color(255, 245, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item42, position);
				
				//Temporary effect//
				//Consider idea where up to 10 nearby torches fire their respective flame color towards cursor and each one does something different on hit
				for (int k = 0; k < 4 + Main.rand.Next(5); k++)
				{
					float xChoice = Main.rand.Next(-100, 101);
					float yChoice = Main.rand.Next(-100, 101);
					xChoice += xChoice > 0 ? 200 : -200;
					yChoice += yChoice > 0 ? 200 : -200;
					Vector2 startSpot = new Vector2(position.X + xChoice, position.Y + yChoice);
					Vector2 endSpot = new Vector2(position.X + Main.rand.Next(-10, 11), position.Y + Main.rand.Next(-10, 11));
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
			
			if (!Main.dedServ)
			{
				glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

				HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
				{
					Texture = glowmask,
					Color = (PlayerDrawSet drawInfo) => new Color(255, 255, 255, 50) * 0.75f
				});
			}
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
			Item.rare = 3;
		}
		
		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.2f, 0.15f, 0.15f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, new Color(255, 255, 255, 50) * 0.75f, rotation, scale);
		}
		
		//TODO maybe look into reworking this into OnSpawn with GlobalItem
		//The following 2 go into any class (assuming it's a one-time thing, in the item class for organization, otherwise needs to generalize this into a system)
		public override void Load()
		{
			On.Terraria.Item.NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool += Item_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool;
		}

		private int Item_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool(On.Terraria.Item.orig_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool orig, Terraria.DataStructures.IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
		{
			/*
				* Try dropping when these conditions are true
				* int number = Item.NewItem(new EntitySource_ByItemSourceId(this, 6), (int)position.X, (int)position.Y, width, height, 5043);
					if (Main.netMode == 1)
						NetMessage.SendData(21, -1, -1, null, number, 1f);
				*/
			//If this causes a recursion somehow, im screaming
			Player player = Main.LocalPlayer;
			if (source is EntitySource_TorchGod torchGodSource &&
				torchGodSource.Context == "TorchGod_FavorLoot" &&
				torchGodSource.TargetedEntity == player &&
				Type == ItemID.TorchGodsFavor && Stack == 1)
			{
				int itemToDrop = ModContent.ItemType<TorchClicker>();
				int number = Item.NewItem(source, player.getRect(), itemToDrop);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f);
				}
			}

			int ret = orig(source, X, Y, Width, Height, Type, Stack, noBroadcast, pfix, noGrabDelay, reverseLookup);
			return ret;
		}
	}
}
