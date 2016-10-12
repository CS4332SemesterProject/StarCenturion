using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SemesterProject
{
    class ScreenManager
    {
        public static ScreenManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen;

        public static ScreenManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new IntroScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("Load/IntroScreen.xml");
        }

        //Load contents of the current Screen.
        public void LoadStuff(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadStuff();
        }

        //Unload and free any unused memory and objects.
        public void UnloadStuff()
        {
            currentScreen.UnloadStuff();
        }

        //Update the game
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        //Draw sprites to the screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
