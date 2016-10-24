using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarCenturion.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private readonly Game _game;

        public MainMenuScreen(IServiceProvider serviceProvider, Game game, GraphicsDevice graphicsDevice,
            GameWindow window) : base(serviceProvider, graphicsDevice, window)
        {
            _game = game;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("New Game", Show<GameScreen>);
            AddMenuItem("Exit", _game.Exit);
        }
    }
}