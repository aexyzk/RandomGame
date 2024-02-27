using System;
using System.IO;
using System.Numerics;

namespace YACS2DGE.YACS2DGE{
    public class Level{
        static int tilesize = 16;
        public static string line;
        public static int lineNum = 0;
        public static int scaleMod = 2;
        public static void InitLevel(Vector2 offset, string filename, Vector2 scale){
            try{
                StreamReader file = new StreamReader("Assets/Levels/level.txt");

                line = file.ReadLine();
                lineNum++;

                while(line != null){
                    //Log.Info(line);
                    for (int i = 0; i < line.Length; i++){
                        if (line[i].ToString() == "#"){
                            // Calculate scaled position and size
                            int x = ((i + 1) * tilesize * scaleMod) - ((tilesize * scaleMod) / 2); // Adjusted position calculation
                            int y = (lineNum * tilesize * scaleMod) - ((tilesize * scaleMod) / 2); // Adjusted position calculation
                            //int width = tilesize * scaleMod; // Adjusted size calculation
                            //int height = tilesize * scaleMod; // Adjusted size calculation

                            //new Rect2D(new Vector2(x, y), new Vector2(width, height), $"Block X: {i + 1}, Y: {lineNum}", Color.White());
                            new Sprite2D(new Vector2(x + offset.x, y + offset.y), scale, $"Level Piece, Row: {lineNum}, Columm: {i + 1}", filename);
                        }
                    }
                    line = file.ReadLine();
                    lineNum++;
                }

                file.Close(); 
            }catch(Exception _ex){
                Log.Error(_ex.Message);
            }finally{
                Log.Info("Loaded Level!");
            }
        }
    }
}