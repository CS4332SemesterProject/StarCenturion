using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SemesterProject
{
    public class IntroScreen : GameScreen
    {
        Texture2D image;
        public string Path;
        public Vector2 Position;

        public override void LoadStuff()
        {
            base.LoadStuff();
            image = content.Load<Texture2D>(Path);
        }

        public override void UnloadStuff()
        {
            base.UnloadStuff();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, Color.White);
        }
    }
}
