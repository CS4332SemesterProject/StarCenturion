using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SemesterProject
{
    public class ScreenManager
    {
        public static ScreenManager instance;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, nextScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if(instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
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

        public void ChangeScreens(string screen)
        {
            nextScreen = (GameScreen)Activator.CreateInstance(Type.GetType("SemesterProject." + screen));
            System.Diagnostics.Debug.Write("\n\n\n" + nextScreen.ToString() + "\n\n\n");
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        private void Transition(GameTime gameTime)
        {
            if(IsTransitioning)
            {
                Image.Update(gameTime);
                if(Image.Alpha == 1.0f) //Start Transition
                {
                    currentScreen.UnloadStuff();
                    currentScreen = nextScreen;
                    xmlGameScreenManager.Type = currentScreen.Type;
                    if(File.Exists(currentScreen.XmlPath))
                    {
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    }
                    currentScreen.LoadStuff();
                }
                else if(Image.Alpha == 0.0f) //Done Transition. Set values to false;
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
                    
            }
        }

        //Load contents of the current Screen.
        public void LoadStuff(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadStuff();
            Image.LoadStuff();
        }

        //Unload and free any unused memory and objects.
        public void UnloadStuff()
        {
            currentScreen.UnloadStuff();
            Image.UnloadStuff();
        }

        //Update the game
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        //Draw sprites to the screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if(IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }
        }
    }
}
