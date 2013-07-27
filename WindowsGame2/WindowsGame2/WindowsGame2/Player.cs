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
        public Vector2 pos;
        public Boolean readyToFire;

        //Adding this so there's a more mario like air control
        public Vector2 currentMovmentModifier = Vector2.Zero;
        float MovementTimer = 0;
        float MovementInterval = 100;
        public Boolean grounded = false;

        //These two are for handling projectiles. This way we wont have to declare in the main program unless we want to
        public Vector2 lookingAngle = new Vector2(-100,0);
        public ProjHandler playerWeapons;      
        //WEEEAPPPONNNZZZ 
        projectile def; 
        //End Weapons
        // This is stuff for the Animation. 
        // I probably will end up using 2 rectangles instead of one for top and bottom of the character so I can handle mouse aiming
        float Animtimer = 0;   
        float interval = 500f; 
        int curFrame = 1;
        int spriteWidth;
        int spriteHeight;
        int numFrames;
        Rectangle SpriteRect;
        //End animation

        //Animation based on movement
        Boolean moving = false;
       


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
            readyToFire = false;
            spriteWidth = tex.Width;
            spriteHeight = tex.Height;
        }


        public Player(float x, float y, Texture2D SpriteSheet, int NumFrames)
        {
            movespeed = (int)10f;
            posX = (int)x;
            posY = (int)y;
            ghostX = 0;
            ghostY = 0;
            PlayerTexture = SpriteSheet;
            movable = true;
            jumping = false;
            jumpCounter = 20;
            Falling = false;
            Physics = true;
            readyToFire = false;
            spriteHeight = SpriteSheet.Height;
            spriteWidth = SpriteSheet.Width/NumFrames;
            numFrames = NumFrames;
        }



        public void Draw(SpriteBatch a){
            pos = new Vector2(posX, posY);
            a.Draw(PlayerTexture, new Rectangle(posX, posY, spriteWidth, spriteHeight), SpriteRect, Color.White);
            //a.Draw(PlayerTexture, pos, Color.White);
            playerWeapons.draw();
        }

        public void updateAnimations(GameTime gameTime)
        {
            if (moving)
            {
                if (currentMovmentModifier.X < 5 )
                {
                    interval = 100;
                }
                else if (currentMovmentModifier.X < 7)
                {
                    interval = 75;
                }
                else if (currentMovmentModifier.X == 10)
                {
                    interval = 25;

                }


                Animtimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (curFrame < numFrames)
                {
                    if (Animtimer >= interval)
                    { Animtimer = 0; curFrame++; }
                }
                else
                {
                    Animtimer = 0; curFrame = 0;
                } 

                //SpriteRect.X = curFrame * spriteWidth;
            }



            SpriteRect = new Rectangle(curFrame * spriteWidth, 0, spriteWidth, spriteHeight);

        }
        public void update(GameTime g, KeyboardState a, Level lev)
        {
            BBox = new Rectangle(posX, posY, PlayerTexture.Width/9, PlayerTexture.Height);
            updateKeyMovement(a, lev,g);
            updatePhysicsMovement(lev);
            playerWeapons.update();
            updateAnimations(g);




        }



        public void updateKeyMovement(KeyboardState a, Level lev, GameTime g)
        {
            
            //NON MOVEMENT(I know. Misleading method name...Sorry)
            if(a.IsKeyDown(Keys.R)){
                if(readyToFire){
                    playerWeapons.Reload();
                }
            }

            
            if(a.IsKeyDown(Keys.Space)){
                if (readyToFire)
                {
                    playerWeapons.fire(lookingAngle);
                    readyToFire = false;
                }
            }
           
            if (a.IsKeyUp(Keys.Space))
            {
                readyToFire = true;
                //playerWeapons.killActive();
            }

            //END NON MOVMENT



            //Actual Movement

         
             
            //Legacy Movement

            /*
            if (a.IsKeyDown(Keys.Right))
            {
                lookingAngle = new Vector2(10, 0);

                if (checkMoveValid(lev, posX + movespeed, 0))
                {
                    if (checkMoveValid(lev, posX + movespeed, posY))
                        posX += movespeed;
                    

                    moving = true;
                }
            }

         
            if (a.IsKeyDown(Keys.Left))
            {
                lookingAngle = new Vector2(-10, 0);
                if (checkMoveValid(lev, posX - movespeed, posY))
                    posX -= movespeed;
                moving = true;
            }



            if (a.IsKeyUp(Keys.Right) && a.IsKeyUp(Keys.Left))
            {
                moving = false;
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
            
            //New Movment
      
            MovementTimer += (float)g.ElapsedGameTime.TotalMilliseconds;
            if (a.IsKeyDown(Keys.Right))
            {
                if (MovementTimer >= MovementInterval)
                { MovementTimer = 0; currentMovmentModifier.X += 1; }
            }
            else if(grounded)
            {
                if (MovementTimer >= MovementInterval && currentMovmentModifier.X >= 5)
                { MovementTimer = 0; currentMovmentModifier.X -= 5; }

                
            } 
            if (a.IsKeyDown(Keys.Left))
            {
                moving = true;
                if (MovementTimer >= MovementInterval)
                { MovementTimer = 0; currentMovmentModifier.X -= 1; }
            }
            else if(grounded)
            {
                if (MovementTimer >= MovementInterval && currentMovmentModifier.X <= -5)
                { MovementTimer = 0; currentMovmentModifier.X += 5; }
               
            }
            

            if (a.IsKeyUp(Keys.Left) && a.IsKeyUp(Keys.Right) && grounded )
            {
                moving = true;
                if (currentMovmentModifier.X < 3.5 && currentMovmentModifier.X > -3.5)
                {
                    currentMovmentModifier.X = 0;
                }
            }
            else if (a.IsKeyUp(Keys.Left) && a.IsKeyUp(Keys.Right) && !grounded)
            {



            }


            currentMovmentModifier.X = MathHelper.Clamp(currentMovmentModifier.X, -20, 20);
            posX += (int)currentMovmentModifier.X;

            if (a.IsKeyDown(Keys.Up))
            {

                jump(lev);
            }

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
                    grounded = false;


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
                    grounded = true;
                }

        }
        }

      
        // JUMPING
        public void jump(Level lev)
        {
           if(!Falling)
            jumping = true;
           grounded = false;
   
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

        // you Must Ready the ProjHandler; mostly because the ProjHandler will use a spritebatch and has its own check for intersections
        public void ReadyWeapons(SpriteBatch a,Camera2D b, Level c){
           
            readyToFire = true;
            playerWeapons = new ProjHandler(a, b, this, c);
            def = new projectile((float).1, Color.Green, pos, PlayerTexture, 0, 0);
            playerWeapons.addProjectile(def);
            
        }

       



    }
}
