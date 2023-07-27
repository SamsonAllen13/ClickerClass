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

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.8f, 0.7f, 0.1f);
			if (Item.timeSinceItemSpawned % 12 == 0)
			{
				Dust dust = Dust.NewDustPerfect(Item.Center + new Vector2(0f, Item.height * 0.2f) + Main.rand.NextVector2CircularEdge(Item.width, Item.height * 0.6f) * (0.3f + Main.rand.NextFloat() * 0.5f), 228, new Vector2(0f, (0f - Main.rand.NextFloat()) * 0.3f - 1.5f), 127);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
			}
		}

		public override void Load()
		{
			On_Item.NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool += Item_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool;
		}

		private static int Item_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool(On_Item.orig_NewItem_IEntitySource_int_int_int_int_int_int_bool_int_bool_bool orig, Terraria.DataStructures.IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
		{
			//Nightcrawler condition for Meteor Shower: Star.starfallBoost > 3f
			/*
				* Try dropping when these conditions are true (From Projectile.Kill(), projectile is type 12, damage > 500)
				num1018 = Item.NewItem(GetItemSource_DropAsItem(), (int)position.X, (int)position.Y, width, height, 75);
				if (Main.netMode == 1 && num1018 >= 0)
					NetMessage.SendData(21, -1, -1, null, num1018, 1f);
				*/
			if (Star.starfallBoost > 3f &&
				Type == ItemID.FallenStar && Stack == 1 &&
				source is EntitySource_DropAsItem sourceAsItem &&
				sourceAsItem.Entity is Projectile star &&
				star.damage > 500)
			{
				//Check if in screen range of any player, then random chance
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Player player = Main.player[i];

					if (player.active && !player.dead)
					{
						var rect = Utils.CenteredRectangle(player.Center, new Vector2(1920, 1080) * 1.5f);
						if (rect.Contains(X, Y) && Main.rand.NextBool(8))
						{
							int itemToDrop = ModContent.ItemType<DreamClicker>();
							int number = Item.NewItem(source, X, Y, Width, Height, itemToDrop);
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f);
							}
							break;
						}
					}
				}
			}

			int ret = orig(source, X, Y, Width, Height, Type, Stack, noBroadcast, pfix, noGrabDelay, reverseLookup);
			return ret;
		}
	}
}
