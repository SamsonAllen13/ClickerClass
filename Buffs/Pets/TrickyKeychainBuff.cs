using Terraria;
using Terraria.ModLoader;
using ClickerClass.Utilities;

namespace ClickerClass.Buffs.Pets
{
	public class TrickyKeychainBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref player.GetClickerPlayer().petTrickyKeychain, ModContent.ProjectileType<Projectiles.Pets.TrickyKeychainPro>());
		}
	}
}