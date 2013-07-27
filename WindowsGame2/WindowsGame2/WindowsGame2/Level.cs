using System;
using System.Collections.Generic;
using System.IO;

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
    class Level
    {

        public List<Block> LevelBlocks = new List<Block>();
        public List<StyleBlock> StyleBlocks = new List<StyleBlock>();
        public Rectangle GameSpace;
        public int GameSpaceWidth;
        public int GameSpaceHeight;
        public String LevelString;
        public Texture2D DefaultTex;
        public float PlayerStartX = 0;
        public float PlayerStartY = 0;
        public Level()
        {
            GameSpaceWidth = 200;
            GameSpaceHeight = 200;
           
        }
        public Level(int width, int height)
        {
            GameSpaceWidth = width;
            GameSpaceHeight = height;
            

        }

        public void AddBlock(Vector2 pos, Texture2D tex)
        {
            LevelBlocks.Add(new Block(pos, tex));
        }

        public void AddBlock(Block a)
        {
            LevelBlocks.Add(a);
        }

        public void AddSytleBlock(StyleBlock a)
        {

            StyleBlocks.Add(a);
        }
        public void ParseFromTextFile(String file)
        {
            StreamReader tr = new StreamReader(file);
            String temp = " ";
           
            while(temp != null)
            {
                 temp = tr.ReadLine();
                if (temp.Contains("endLevel"))
                {
                   
                    break;
                }
                else if (temp.Contains("add"))
                {
                    if (temp.Contains("block.."))
                    {
                        String StringX = temp.Substring(10, 3);
                        String StringY = temp.Substring(14, 3);
                        String StringWidth = temp.Substring(18, 3);
                        String StringHeight = temp.Substring(22, 3);
                        if (temp.Substring(15).Contains("none"))
                        {
                            AddBlock(new Block(new Vector2(int.Parse(StringX), int.Parse(StringY)), DefaultTex, int.Parse(StringWidth), int.Parse(StringHeight)));
                        }
                    }else if(temp.Contains("Player.")){
                            String StringX = temp.Substring(10,3);
                            String StringY = temp.Substring(14, 3);

                            PlayerStartX = (float)int.Parse(StringX);
                            PlayerStartY = (float)int.Parse(StringY);
                    
                          }


                }


            }
            tr.Close();
        }



        public void draw(SpriteBatch a)
        {
GameSpace = new Rectangle(-GameSpaceWidth, -GameSpaceHeight, 2*GameSpaceWidth, 2*GameSpaceHeight);
           
            for (int x = 0; x < LevelBlocks.Count; x++)
            {
                LevelBlocks[x].draw(a);
            }
            for (int x = 0; x < StyleBlocks.Count; x++)
            {

                StyleBlocks[x].draw(a);
            }
        }


        
    }
}
