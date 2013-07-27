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
        
      
        public Vector2 angle;
        public Boolean active = false;
        public float speed;
        public Player player;
        
        GraphicsDeviceManager graphics;
        int viewhight;
        int viewWidth;
       
        public projectile(float velocity, Color color, Vector2 pos, Texture2D a, int viewportWidth,int viewportHeight)
          :  base(pos, a){

        
           // player = play;
            speed = velocity;
            BlockColor = color;
            angle = new Vector2(-1, 0);
            width = a.Width / 2;
            height = a.Height / 2;
           // graphics = graphicz;


        }

        public projectile(projectile a) :base(a.Position, a.BlockTexture)
        {
            speed = a.speed;
            BlockColor = a.BlockColor;
            width = a.width / 2;
            height = a.height / 2;

        }

        public void activate(Vector2 direction)
        {
            active = true;
            angle = direction;
        }



        public void activate(Vector2 direction, Color outputColor)
        {
            active = true;
            angle = direction;
            BlockColor = outputColor;
        }
        public void update(Vector2 newPos)
        {
            
            if (active)
            {
                Position.X += angle.X*speed;
                Position.Y += angle.Y*speed;
            }
            else {

                Position = newPos;
            }
            

            

            BBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            
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
