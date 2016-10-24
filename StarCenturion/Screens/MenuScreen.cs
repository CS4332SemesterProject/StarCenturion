using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

namespace StarCenturion.Screens
{
    public class MenuScreen : Screen
    {
        private readonly BoxingViewportAdapter _boxingViewportAdapter;
        private readonly KeyboardListener _keyboardListener;
        private readonly MouseListener _mouseListener;
        private readonly IServiceProvider _serviceProvider;
        private GraphicsDevice _graphicsDevice;
        private int _selectedItemIndex;
        private SpriteBatch _spriteBatch;

        protected MenuScreen(IServiceProvider serviceProvider, GraphicsDevice graphicsDevice, GameWindow window)
        {
            _serviceProvider = serviceProvider;
            _graphicsDevice = graphicsDevice;
            MenuItems = new List<MenuItem>();
            _mouseListener = new MouseListener();
            _keyboardListener = new KeyboardListener();

            int w = ScreenConst.Width;
            int h = ScreenConst.Height;
            int vb = ScreenConst.VerticalBleed;
            int hb = ScreenConst.HorizontalBleed;

            _boxingViewportAdapter = new BoxingViewportAdapter(window, _graphicsDevice, w, h, hb, vb);
            _mouseListener = new MouseListener(_boxingViewportAdapter);
        }

        public List<MenuItem> MenuItems { get; }
        protected BitmapFont Font { get; private set; }
        protected ContentManager Content { get; private set; }

        protected void AddMenuItem(string text, Action action)
        {
            var menuItem = new MenuItem(Font, text)
            {
                Position = new Vector2(300, 200 + 32 * MenuItems.Count),
                Action = action,
                Color = Color.LightGray
            };

            MenuItems.Add(menuItem);
        }

        public override void Initialize()
        {
            base.Initialize();

            _mouseListener.MouseClicked += OnMouseClick;
            _keyboardListener.KeyReleased += OnKeyRelease;

            Content = new ContentManager(_serviceProvider, "Content");
        }

        private void OnMouseClick(object sender, MouseEventArgs args)
        {
            bool isHovered = MenuItems[_selectedItemIndex].BoundingRectangle.Contains(args.Position);

            if (isHovered)
                MenuItems[_selectedItemIndex].Action.Invoke();
        }

        private void OnKeyRelease(object sender, KeyboardEventArgs args)
        {
            switch (args.Key)
            {
                case Keys.Enter:
                    MenuItems[_selectedItemIndex].Action.Invoke();
                    break;
                case Keys.Up:
                case Keys.W:
                    if (_selectedItemIndex > 0)
                        _selectedItemIndex--;
                    break;
                case Keys.S:
                case Keys.Down:
                    if (_selectedItemIndex < MenuItems.Count - 1)
                        _selectedItemIndex++;
                    break;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _spriteBatch.Dispose();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var graphicsDeviceService =
                (IGraphicsDeviceService) _serviceProvider.GetService(typeof(IGraphicsDeviceService));
            _graphicsDevice = graphicsDeviceService.GraphicsDevice;

            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            Font = Content.Load<BitmapFont>("Fonts/Orbitron");
        }

        public override void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _mouseListener.Update(gameTime);
            _keyboardListener.Update(gameTime);

            UpdateItemColor();
        }

        private void UpdateItemColor()
        {
            for (var i = 0; i < MenuItems.Count; ++i)
            {
                var mousePosition = _boxingViewportAdapter.PointToScreen(Mouse.GetState().Position);
                bool isHovered = MenuItems[i].BoundingRectangle.Contains(mousePosition);

                if (isHovered)
                    _selectedItemIndex = i;
                else
                    MenuItems[i].Color = Color.LightGray;
            }
            MenuItems[_selectedItemIndex].Color = Color.White;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _graphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _boxingViewportAdapter.GetScaleMatrix());

            foreach (var menuItem in MenuItems)
                menuItem.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}