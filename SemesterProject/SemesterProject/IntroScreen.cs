using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SemesterProject
{
    public class IntroScreen : GameScreen
    {
        public Image Image;
        public Vector2 Position;

        public override void LoadStuff()
        {
            base.LoadStuff();
            Image.LoadStuff();
        }

        public override void UnloadStuff()
        {
            base.UnloadStuff();
            Image.UnloadStuff();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if(InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
            {
                ScreenManager.Instance.ChangeScreens("TitleScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
