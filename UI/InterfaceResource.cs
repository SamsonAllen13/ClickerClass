using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;

namespace ClickerClass.UI
{
	public abstract class InterfaceResource : GameInterfaceLayer
	{
		public InterfaceResource(string name, InterfaceScaleType scaleType) : base(name, scaleType) { }
		public abstract int GetInsertIndex(List<GameInterfaceLayer> layers);
		public virtual void Update(GameTime gameTime) { }
	}
}
