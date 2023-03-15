using System;
using ClickerClass.DrawLayers;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class MiceMask : ClickerItem
	{
		public static readonly int DamageIncrease = 6;
		public static readonly int CritIncrease = 6;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease, CritIncrease);

		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));

				HeadLayer.RegisterData(Item.headSlot, new DrawLayerData()
				{
					Texture = ModContent.Request<Texture2D>(Texture + "_Head_Glow")
				});
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 7, 0, 0);
			Item.rare = ItemRarityID.Red;
			Item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage<ClickerDamage>() += DamageIncrease / 100f;
			player.GetCritChance<ClickerDamage>() += CritIncrease;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MiceSuit>() && legs.type == ModContent.ItemType<MiceBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = LangHelper.GetText("SetBonus.Mice");
			player.GetModPlayer<ClickerPlayer>().setMice = true;
		}

		public override void UpdateVanitySet(Player player)
		{
			Lighting.AddLight(player.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value.Value, new Color(255, 255, 255, 0) * 0.8f, rotation, scale);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 10).AddIngredient(ItemID.LunarBar, 8).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
