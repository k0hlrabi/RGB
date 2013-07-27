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
        //Good Chance we'll ditch the player for a constantly updating origin location....Fuck it! I'll do it now!
        Player player;
        Vector2 Origin;
        //THERE! I FUCKING DID IT.
        Level level;
        List<projectile> activeProjectiles = new List<projectile>();
        List<projectile> QueueList = new List<projectile>();
        List<projectile> loadedProjectiles = new List<projectile>();
        public int currentSeleciton = 0;
        public int currentClip = 0;
        public int MaxClip = 5;
        public int numActiveProj = 0;
        public ProjHandler(SpriteBatch mainSpriteBatch, Camera2D mainCamera, Player mainPlayer, Level currentLevel)
        {
            
            spriteBatch = mainSpriteBatch;
            cam = mainCamera;
            player = mainPlayer;
            level = currentLevel;

        }
        public void addProjectile(projectile a)
        {
            loadedProjectiles.Add(a);
        }

        public void fire(Vector2 lookin)
        {
            //currentClip++;
            if (currentClip < MaxClip)
            {
               activeProjectiles.Add( new projectile(loadedProjectiles[currentSeleciton]));
               activeProjectiles[numActiveProj].activate(player.lookingAngle * 20);
               numActiveProj++;
               currentClip++;
            }




                /*
                //loadedProjectiles[currentSeleciton].Position = player.pos;
                //loadedProjectiles[currentSeleciton].angle = lookin;
                //loadedProjectiles[currentSeleciton].activate(lookin);
                //activeProjectiles.Add(new projectile(loadedProjectiles[currentSeleciton]));
                QueueList.Add(new projectile(loadedProjectiles[currentSeleciton]));
                foreach (projectile i in QueueList)
                {
                    //i.angle = lookin;
                    activeProjectiles.Add(i);
                    
                }

                

                foreach (projectile i in activeProjectiles)
                {
                    i.activate(lookin);
                }

                
                
                   
                
                
            }
            else
            {
               

                
            }*/


            
        }

        public void killActive()
        {
           List<projectile> blankList = new List<projectile>();
            activeProjectiles = blankList;

        }
        public void Reload()
        {
            currentClip = 0;


        }
        public void update()
        {
            foreach (projectile i in loadedProjectiles)
            {

                i.update(player.pos);
            }

           
            
            for(int x = 0; x < activeProjectiles.Count; x++){
                projectile a = activeProjectiles[x];
                a.update(player.pos);
                if (player.pos.X + 1000 < a.BBox.X || player.pos.X - 1000 > a.BBox.X)
                {

                    a.deActivate();
                    activeProjectiles.Remove(a);
                    numActiveProj--;
                    

                    
                }

            }
              
             

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

        public void draw()
        {
            foreach (projectile i in activeProjectiles)
            {

                i.draw(spriteBatch);

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

