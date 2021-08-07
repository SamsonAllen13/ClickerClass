using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace ClickerClass.DrawLayers
{
	public class DrawLayerData
	{
		public Asset<Texture2D> Texture { get; init; }

		public Color Color { get; init; } = new Color(255, 255, 255, 0) * 0.8f;
	}
}
