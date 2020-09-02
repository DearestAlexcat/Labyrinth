using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
 

namespace AAA
{
    enum Position { vertical = -5, horizontal}

    class Labyrinth : Form
    {
        bool bGenerationMaze = false;
        int sizeX, sizeY, squere_side, number_vertices;
        int[,] Labirint;
        List<Kruskal> kr;
        static public int count;
        public Labyrinth()
        {
            sizeX = sizeY = squere_side = 5;
            number_vertices = 1;
        }

        public Labyrinth(int sizeX, int sizeY, int squere_side)
        {

            if (sizeX % 2 == 0) sizeX++;
            if (sizeY % 2 == 0) sizeY++;

            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.squere_side = squere_side;
            number_vertices = 1;
        }

        void GenerationRoom()
        {
            int i, j, index = 0;
            Labirint = new int[sizeX, sizeY];
            kr = new List<Kruskal>();

            Random rand = new Random();

            //формируем вершины графа
            for (i = 0; i < sizeX; i++)
            {
                for (j = 0; j < sizeY; j++)
                {
                    if ((i & 1) == 1  && 
                        (j & 1) == 1  && 
                        i < sizeX - 1 && 
                        j < sizeY - 1)
                    {               
                        Labirint[i, j] = number_vertices++;
                    }
                    else
                    {
                        if (i > 0 && 
                            j > 0 && 
                            i < sizeX - 1 && 
                            j < sizeY - 1)
                        {
                            if ((i & 1) == 0 && (j & 1) == 0) continue;
                            if ((i & 1) == 1 && (j & 1) == 0)
                                Labirint[i, j] = (int)Position.vertical;
                            else Labirint[i, j] = (int)Position.horizontal;
                            kr.Insert(index++, new Kruskal(i, j, rand.Next(101)));
                        }    
                    }
                    
                }
            }
        }

        void GenerationMST()
        {
            int x, y;
            kr.Sort(new MComparer());
            Kruskal.FormLieder(number_vertices);
            
            for (int i = 0; i < kr.Count(); i++)
            {
                if (Labirint[kr[i].v1, kr[i].v2] == (int)Position.vertical)
                {
                    x = Labirint[kr[i].v1, kr[i].v2 - 1];
                    y = Labirint[kr[i].v1, kr[i].v2 + 1];   
                }
                else
                {
                    x = Labirint[kr[i].v1 - 1, kr[i].v2];
                    y = Labirint[kr[i].v1 + 1, kr[i].v2];
                }

                if (Kruskal.union(x - 1, y - 1))
                {
                    Labirint[kr[i].v1, kr[i].v2] = (int)RoomState.Free;
                }
            }          
            kr = new List<Kruskal>();  
        }

        public void GenerationMaze()
        {
            GenerationRoom();
            GenerationMST();
            bGenerationMaze = true;
        }

        public void DrawLabyrint(Graphics gr, Point pt, int MenuStripHeight, bool GenerationLife = false)
        {

            if (bGenerationMaze == false)
            {            
                return;
            }

            Rectangle rect = new Rectangle(pt.X, pt.Y + MenuStripHeight, squere_side, squere_side);
            int choice;
            
            if (GenerationLife)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        choice = Labirint[i, j];
                        switch (choice)
                        {
                            case (int)RoomState.Brick: gr.FillRectangle(Brushes.Gray, rect); break;         //непроходимая клетка
                            case (int)RoomState.Free: gr.FillRectangle(Brushes.GreenYellow, rect); break;   //уничтоженная клетка
                            case (int)RoomState.Found: gr.FillRectangle(Brushes.Olive, rect); break;        //пройденная клетка
                            case (int)RoomState.Processed: gr.FillRectangle(Brushes.Orange, rect); break;   //в процессе
                            default: gr.FillRectangle(Brushes.GreenYellow, rect); break;                    //свободная клетка
                        }
                        rect.Offset(squere_side, 0);
                    }
                    rect.Offset(-squere_side * sizeY, squere_side);
                }
            }
            else
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {                  
                        if (Labirint[i, j] > 0 || Labirint[i, j] == -1)
                        {
                            gr.FillRectangle(Brushes.White, rect);  // свободная стенка
                        }
                        else
                        {
                            gr.FillRectangle(Brushes.Black, rect);
                        }
                        rect.Offset(squere_side, 0);
                        count++;
                      
                    }
                    rect.Offset(-squere_side * sizeY, squere_side);
                }
            }
        }
    }
}
