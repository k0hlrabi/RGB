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
    class Player
    {
        public int movespeed;
        public Texture2D PlayerTexture;
        public int posX;
        public int posY;
        public int ghostX;
        public int ghostY;
        public Rectangle BBox;
        public Rectangle GBox;
        public int physcounter;
        public int jumpCounter;
        public Boolean movable;
        public Boolean jumping;
        public Boolean Falling;
        public Boolean Physics;
       


        public Player(float x, float y, Texture2D tex)
        {
            movespeed = (int)10f;
            posX = (int)x;
            posY = (int)y;
            ghostX = 0;
            ghostY = 0;
            PlayerTexture = tex;
            movable = true;
            jumping = false;
            jumpCounter = 20;
            Falling = false;
            Physics = true;

        }



        public void Draw(SpriteBatch a){
            Vector2 pos = new Vector2(posX, posY);

           
            a.Draw(PlayerTexture, pos, Color.White);

        }


        public void update(GameTime g, KeyboardState a, Level lev)
        {


            BBox = new Rectangle(posX, posY, PlayerTexture.Width, PlayerTexture.Height);
           
            updateKeyMovement(a, lev);
            updatePhysicsMovement(lev);


        }


        public void updateKeyMovement(KeyboardState a, Level lev)
        {
            if (a.IsKeyDown(Keys.Right))
            {
                if (checkMoveValid(lev, posX + movespeed, posY))
                    posX += movespeed;

            }
            if (a.IsKeyDown(Keys.Left))
            {

                if (checkMoveValid(lev, posX - movespeed, posY))
                    posX -= movespeed;

            }

            if (a.IsKeyDown(Keys.Up))
            {

                jump(lev);
            }
            else
            {
                endJump();
            }
            
          /*  if (a.IsKeyDown(Keys.Down))
            {

                if (checkMoveValid(lev, posX, posY + movespeed))
                    posY += movespeed;
            }
          * */

        }


        public bool checkIfInGameBounds(Level a)
        {
            if (BBox.Intersects(a.GameSpace))
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public bool checkMoveValid(Level a, int x, int y)
        {
            GBox = new Rectangle(x, y, PlayerTexture.Width, PlayerTexture.Height);

            int temp = 0;
            for (int z = 0; z <= a.LevelBlocks.Count - 1; z++){
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



        public void updatePhysicsMovement(Level lev)
        {
            if (!jumping)
            {

                if (checkMoveValid(lev, posX, posY + 1 * physcounter))
                {
                    posY += 1 * physcounter;
                    physcounter += 1;
                    Falling = true;


                }
                else
                {
                    Falling = false;
                    
                }
            }else{
                if(checkMoveValid(lev, posX, posY - 1 * jumpCounter)){
                    posY -= 1 * jumpCounter;
                    jumpCounter--;

                }else{
                    jumping = false;
                }

                if (!jumping && !Falling)
                {
                    jumpCounter = 20;
                }

        }
        }

      
        // JUMPING
        public void jump(Level lev)
        {
           if(!Falling)
            jumping = true;
            
            /*
            int jumpAmount = 3 * jumpCounter;
          
            if (!Falling && jumpCounter >= 0 )
            {
                jumping = true;
                if (checkMoveValid(lev, posX, posY - jumpAmount))
                {
                    posY -= jumpAmount;
                    jumpCounter--;
                }
              
            }  else if(jumpCounter >= 0)
                {
                    jumping = false;
                    Falling = true;
                    jumpCounter = 0;
                }
            else{
                        jumping = false;
                        Falling = true;
        }
            */
        }
            

        public void performJump(Level lev)
        {

        }


        public void endJump()
        {
           // jumping = false;
            
        }


        //-------------------------------

        public void stop()
        {

            movable = true;
        }
        public void start()
        {

            movable = false;
        }




    }
}
