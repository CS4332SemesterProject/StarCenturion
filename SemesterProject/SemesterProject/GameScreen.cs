using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace SemesterProject
{
    public class GameScreen
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;
        
        public GameScreen()
        {
            Type = this.GetType();
        }

        public virtual void LoadStuff()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadStuff()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
