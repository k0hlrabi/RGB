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
    class ProjHandler
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera2D cam;
        Player player;
        Level level;
        List<projectile> activeProjectiles;
        List<projectile> loadedProjectiles;
        public int currentSeleciton = 0;
        public ProjHandler(GraphicsDeviceManager mainGraphics, SpriteBatch mainSpriteBatch, Camera2D mainCamera, Player mainPlayer, Level currentLevel)
        {
            graphics = mainGraphics;
            spriteBatch = mainSpriteBatch;
            cam = mainCamera;
            player = mainPlayer;
            level = currentLevel;

        }
        public void addProjectile(projectile a)
        {
            loadedProjectiles.Add(a);
        }

        public void fire()
        {
            activeProjectiles.Add(loadedProjectiles[currentSeleciton]);
            activeProjectiles[currentSeleciton].Position = player.pos;
            activeProjectiles[currentSeleciton].activate(player.lookingAngle);
        }
        public void update()
        {
            

        }


        public void changeSelection(int selection)
        {
            if (selection < 0)
            {
                currentSeleciton = 0;
            }
            if (selection > loadedProjectiles.Count)
            {
                currentSeleciton = loadedProjectiles.Count;
            }


        }
            
            
            public bool checkMoveValid(Level a, int x, int y)
        {
            
                if (activeProjectiles.Count == 0)
            {
                return false;
            }

            Rectangle GBox = new Rectangle();
            Rectangle rectangleB = new Rectangle();
            int temp;

          foreach(projectile currentProj in activeProjectiles){
            GBox = new Rectangle(x, y, currentProj.BlockTexture.Width, currentProj.BlockTexture.Height);

             temp = 0;
            for (int z = 0; z <= a.LevelBlocks.Count - 1; z++){
                Rectangle rectangleA = a.LevelBlocks[z].BBox;
                if (GBox.Intersects(rectangleA))
                {
                    
                    temp = z;
                    break;
                    
                }
   
            }

             rectangleB = a.LevelBlocks[temp].BBox;

        
        
            }
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

