using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Dusts
{
	public class FrozenDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.frame = new Rectangle(0, Main.rand.Next(3) * 18, 18, 18);
		}

		public override bool Update(Dust dust)
		{
			dust.rotation += dust.velocity.X * 0.05f;
			dust.velocity *= 0.9f;
			dust.scale *= 0.95f;
			dust.alpha -= 5;

			Lighting.AddLight(dust.position, 0.025f, 0.1f, 0.3f);
			if (dust.scale < 0.175f)
			{
				dust.active = false;
			}

			return false;
		}
		
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color(255, 255, 255, 0) * 0.35f;
		}
	}
}