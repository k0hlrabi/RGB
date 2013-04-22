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
    class projectile: Block
    {
        
        public Color projColor;
        public Vector2 angle;
        public Boolean active = false;
        public int speed;
        public Player player;
        GraphicsDeviceManager graphics;
        int viewhight;
        int viewWidth;
        projectile(int velocity, Color color, Vector2 pos, Texture2D a, Player play, GraphicsDeviceManager graphicz, int viewportWidth,int viewportHeight)
            : base(pos, a)

        {
            player = play;
            speed = velocity;
            projColor = color;
            graphics = graphicz;


        }

        public void activate(Vector2 direction)
        {
            active = true;
            direction = angle;
        }

        public void update()
        {
            
            if (active)
            {
                Position += angle * speed;
            }
            else { }

            BBox = new Rectangle((int)Position.X, (int)Position.Y, BlockTexture.Width, BlockTexture.Height);
        }

        public void deActivate()
        {

            active = false;
        }

        


        public bool checkMoveValid(Level a, int x, int y)
        {
            Rectangle GBox = new Rectangle(x, y,  BlockTexture.Width, BlockTexture.Height);
          
            int temp = 0;
            for (int z = 0; z <= a.LevelBlocks.Count - 1; z++)
            {
                Rectangle rectangleA = a.LevelBlocks[z].BBox;
                if (GBox.Intersects(rectangleA))
                {

                    temp = z;
                    break;

                }

            }

            Rectangle rectangleB = a.LevelBlocks[temp].BBox;

            if (GBox.Intersects(rectangleB))
            {

                return false;
            }
            else
            {
                return true;
            }



        }

    }
}
