using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MindfulClicker : ClickerWeapon
	{
		public static readonly int CharmReduction = 30;
		
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Psychic, projsInheritItemElements: true);

			ClickEffect.Charm = ClickerSystem.RegisterClickEffect(Mod, "Charm", 1, new Color(255, 190, 235), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<MindfulClickerPro>(), 0, 0f, player.whoAmI);
			},
			preHardMode: true,
			descriptionArgs: new object[] { CharmReduction });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(255, 165, 255));
			SetDust(Item, 120);
			AddEffect(Item, ClickEffect.Charm);

			Item.damage = 7;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 30, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
