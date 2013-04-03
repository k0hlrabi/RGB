using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    class line
    {

        List<Vector2> pointsList = new List<Vector2>();
        Texture2D tex;

        public line(List<Vector2> inputPoints)
        {
            pointsList = inputPoints;
        }

        public void draw(SpriteBatch a)
        {
            foreach(Vector2 x in pointsList){
                a.Draw(tex, new Rectangle(x.X,x.Y,)
            

            }

        }


    }
}
