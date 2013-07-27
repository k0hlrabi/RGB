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
    class Block
    {
        public Vector2 Position;
        public Texture2D BlockTexture;
        public Rectangle BBox;
        public int width;
        public int height;
        public Color BlockColor;
        
        



        public Block(Vector2 pos, Texture2D a)
        {
            Position = pos;
            BlockTexture = a;
            width = BlockTexture.Width;
            height = BlockTexture.Height;
            BlockColor = Color.Red;
            BBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }
        public Block(Vector2 pos, Texture2D a, int width, int height)
        {
            Position = pos;
            BlockTexture = a;
            BBox = new Rectangle((int)Position.X, (int)Position.Y, (int)width, (int)height);
        }
       


        public void draw(SpriteBatch a)
        {

            BBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
           

           

         
         a.Draw(BlockTexture, BBox, BlockColor);
        }




    }
}
