using System;
using Microsoft.Xna.Framework;

namespace StarCenturion.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private readonly Game _game;

        public MainMenuScreen(IServiceProvider serviceProvider, Game game)
            : base(serviceProvider)
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