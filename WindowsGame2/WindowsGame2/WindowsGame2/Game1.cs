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
        SpriteFont Font1;
        String GameText;
        Boolean GameOver;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            awesome.PlayerStartX = 0;
            awesome.PlayerStartY = -100;
            awesome.GameSpaceHeight = 2000;
            awesome.GameSpaceWidth = 10000;
            one = new Player(awesome.PlayerStartX,awesome.PlayerStartY, Content.Load<Texture2D>("sprite"));
             cam = new Camera2D(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
             cam.Zoom = .5f;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LevelTex = Content.Load<Texture2D>("sprite");
            Font1 = Content.Load<SpriteFont>("Times New Roman");

            //levelSetup

            awesome.DefaultTex = LevelTex;
            //awesome.ParseFromTextFile(@"C:\Users\will\Documents\Visual Studio 2010\Projects\WindowsGame2\WindowsGame2\WindowsGame2\Levels\Awesome.txt");


            awesome.AddBlock(Vector2.Zero, awesome.DefaultTex);
            for (int x = 0; x < awesome.DefaultTex.Width*30; x += awesome.DefaultTex.Width)
            {
                awesome.AddBlock(new Vector2(x, 00), awesome.DefaultTex);
            }
            
            GameText = "Game Over";
            GameOver = false;
            
        }

      
        protected override void UnloadContent()
        {
           
        }

        
      
        protected override void Update(GameTime gameTime)
        {


            if (one.checkIfInGameBounds(awesome))
            {
                
                float CameraPosY = MathHelper.Clamp((float)one.posY, -80, 80);
                cam.Pos = new Vector2(one.posX + 30, CameraPosY);
                KeyboardState a = Keyboard.GetState();
                one.update(gameTime, a, awesome);
                GameOver = false;
                
            }
            else
            {
                
                GameOver = true;
            }
            
            





           

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
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

               
            
              
                
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
