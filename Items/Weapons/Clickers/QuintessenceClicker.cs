using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class QuintessenceClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Transcend = ClickerSystem.RegisterClickEffect(Mod, "Transcend", 25, new Color(220, 180, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item130, player.Center);
				player.AddBuff(ModContent.BuffType<TranscendBuff>(), 1800, false);
				for (int k = 0; k < 8; k++)
				{
					ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon, new ParticleOrchestraSettings
					{
						PositionInWorld = player.Center,
						MovementVector = Main.rand.NextVector2Circular(3f, 3f)
					}, player.whoAmI);
				}
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(220, 180, 255));
			SetDust(Item, 272);
			AddEffect(Item, ClickEffect.Transcend);

			Item.damage = 160;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Purple;
		}
	}
}
