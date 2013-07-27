using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{

    
 

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player one;
        Camera2D cam;
        Level awesome = new Level();
        Texture2D LevelTex;
        Texture2D PlayerTex;
        SpriteFont Font1;
        String GameText;
        Boolean GameOver;
       public int windowWidth;
       public int windowHeight;
       Vector2 MousePos = Vector2.Zero;
       Boolean isGameActive;
       Boolean invOpenable;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

       
        protected override void Initialize()
        {
            //GlobalVariables= GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;
            awesome.PlayerStartX = 0;
            awesome.PlayerStartY = -100;
            awesome.GameSpaceHeight = 2000;
            awesome.GameSpaceWidth = 10000;
            one = new Player(awesome.PlayerStartX,awesome.PlayerStartY, Content.Load<Texture2D>("spritesheet"),3);
            one.ReadyWeapons(spriteBatch, cam, awesome);
             cam = new Camera2D(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
             cam.Zoom = .5f;
            // TODO: Add your initialization logic here
             isGameActive = true;
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LevelTex = Content.Load<Texture2D>("sprite");
            PlayerTex = Content.Load<Texture2D>("spritesheet");

            Font1 = Content.Load<SpriteFont>("Times New Roman");

            
            
            
            //levelSetup

            awesome.DefaultTex = LevelTex;
            //awesome.ParseFromTextFile(@"C:\Users\will\Documents\Visual Studio 2010\Projects\WindowsGame2\WindowsGame2\WindowsGame2\Levels\Awesome.txt");
           

            ///THIS SHIT BE FOR THE DEV LEVELLLLLL
            awesome.AddBlock(Vector2.Zero, awesome.DefaultTex);
            for (int x = 0; x < awesome.DefaultTex.Width*100; x += awesome.DefaultTex.Width)
            {
                awesome.AddBlock(new Vector2(x, 00), awesome.DefaultTex);
            }
            
            GameText = "Game Over";
            GameOver = false;

            //END OF DEV LEVELLLLL
            



            //Set the mouse to the game window
            Mouse.SetPosition(0, 0);

            //Projectiles
            one.ReadyWeapons(spriteBatch, cam, awesome);

            
        }

      
        protected override void UnloadContent()
        {
           
        }

        
      
        protected override void Update(GameTime gameTime)
        {
            //This is so we can pause the game

            KeyboardState HighLevelKeyState = Keyboard.GetState();

            if (HighLevelKeyState.IsKeyDown(Keys.Escape) && invOpenable)
            {
                if (isGameActive)
                {
                    isGameActive = false; invOpenable = false;
                }
                else
                {
                    isGameActive = true; invOpenable = false;
                }

            }

            if (HighLevelKeyState.IsKeyUp(Keys.Escape) && !invOpenable)
            {
                invOpenable = true;
            }

            if (isGameActive)
            {
                MouseState Mousez = Mouse.GetState();
                //MousePos = new Vector2(MathHelper.Clamp(Mousez.X,0,GraphicsDevice.Viewport.Width), MathHelper.Clamp(Mousez.Y,0,GraphicsDevice.Viewport.Height));
                MousePos = new Vector2(Mousez.X, Mousez.Y);
                float slope;
                /*  if (Mousez.X - one.posX == 0)
                  {
                      one.lookingAngle = new Vector2(0, 1);
                  }
                  else
                  {
                      slope = ( Mousez.Y-one.posY ) / ( Mousez.X- one.posX);
                      one.lookingAngle = new Vector2(1, slope );
                  }
            
                  */

                //Dev Level Game rules
                if (one.checkIfInGameBounds(awesome))
                {

                    float CameraPosY = MathHelper.Clamp((float)one.posY, -80, 80);
                    float CameraPosX = cam.Pos.X;

                    if (one.posX >= CameraPosX + 650)
                    {
                        CameraPosX = CameraPosX + one.currentMovmentModifier.X;
                    }
                    if (one.posX <= CameraPosX - 650)
                    {
                        CameraPosX = CameraPosX +one.currentMovmentModifier.X;
                    }
                    cam.Pos = new Vector2(CameraPosX, CameraPosY);
                    KeyboardState a = Keyboard.GetState();
                    one.update(gameTime, a, awesome);
                    GameOver = false;

                }
                else
                {

                    GameOver = true;
                }

                //End dev Level game Rules






            }
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Rectangle mouserect = new Rectangle((int)(MousePos.X),(int)(MousePos.Y), (int)LevelTex.Width, (int)LevelTex.Height);
            //cam.Pos = new Vector2(500.0f, 200.0f);
            // cam.Zoom = 2.0f // Example of Zoom in
            // cam.Zoom = 0.5f // Example of Zoom out

            
            //// if using XNA 4.0
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                    BlendState.AlphaBlend,
                                    null,
                                    null,
                                    null,
                                    null,
                                    cam.get_transformation(GraphicsDevice ));

            // Draw Everything
            // You can draw everything in their positions since the cam matrix has already done the maths for you 
             

               if (GameOver)
               {
                   Vector2 BuferZone = new Vector2(cam.Pos.X - 200, cam.Pos.Y - 115);
                   spriteBatch.DrawString(Font1, GameText, BuferZone, Color.Red, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
               }
               

                one.Draw(spriteBatch);
               awesome.draw(spriteBatch);
              //spriteBatch.Draw(LevelTex, mouserect, Color.Gray);
               
            
              
                
            
            spriteBatch.End();
            // TODO: Add your drawing code here
             
            base.Draw(gameTime);
        }
    }
}
