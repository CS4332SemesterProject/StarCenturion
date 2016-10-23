using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using StarCenturion.Screens;

namespace StarCenturion
{
    public class StarCenturionGame : Game
    {
        public StarCenturionGame()
        {
            // ReSharper disable once ObjectCreationAsStatement
            // Graphics device manager, by its design, must be created even if not used.
            new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            var screens = new Screen[]
            {
                new IntroScreen(Services),
                new MainMenuScreen(Services, this),
                new GameScreen(Services, GraphicsDevice, Window)
            };

            Components.Add(new ScreenComponent(this, screens));

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
    }
}