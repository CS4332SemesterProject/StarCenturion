using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Screens;

namespace StarCenturion.Screens
{
    public class IntroScreen : Screen
    {
        private readonly KeyboardListener _keyboardListener;
        private readonly MouseListener _mouseListener;
        private readonly IServiceProvider _serviceProvider;
        private float _alpha = 1.0f;
        private GraphicsDevice _graphicsDevice;
        private Texture2D _introLogo;
        private Vector2 _logoCentered;
        private SpriteBatch _spriteBatch;

        public IntroScreen(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _keyboardListener = new KeyboardListener();
            _mouseListener = new MouseListener();
        }

        protected ContentManager Content { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            Content = new ContentManager(_serviceProvider, "Content");

            // User hitting enter will skip the intro screen.
            _keyboardListener.KeyReleased += (sender, args) =>
            {
                if (args.Key == Keys.Enter)
                    Show<MainMenuScreen>();
            };

            // User clicking the mouse will skip the intro screen.
            _mouseListener.MouseClicked += (sender, args) =>
            {
                var clickPoint = new Point(args.CurrentState.X, args.CurrentState.Y);
                if (_graphicsDevice.Viewport.Bounds.Contains(clickPoint))
                    Show<MainMenuScreen>();
            };
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _introLogo = Content.Load<Texture2D>("intro-logo");

            var graphicsDeviceService =
                (IGraphicsDeviceService) _serviceProvider.GetService(typeof(IGraphicsDeviceService));
            _graphicsDevice = graphicsDeviceService.GraphicsDevice;

            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public override void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();
            _spriteBatch.Dispose();

            base.UnloadContent();
        }

        public override void Dispose()
        {
            base.Dispose();

            _spriteBatch.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _keyboardListener.Update(gameTime);
            _mouseListener.Update(gameTime);

            float logoX = (_graphicsDevice.Viewport.Width - _introLogo.Width) / 2f;
            float logoY = (_graphicsDevice.Viewport.Height - _introLogo.Height) / 2f;
            _logoCentered = new Vector2(logoX, logoY);

            _alpha -= 0.008f;

            // intro fades to black and then we show the next screen.
            if (_alpha < 0.0f)
                Show<MainMenuScreen>();
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_introLogo, _logoCentered, Color.White * _alpha);
            _spriteBatch.End();
        }
    }
}