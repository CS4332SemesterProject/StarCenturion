using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SemesterProject
{
    public class ImageEffect
    {
        protected Image image;
        public bool IsActive;

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadStuff(ref Image Image)
        {
            this.image = Image;
        }

        public virtual void UnloadStuff()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

    }
}
