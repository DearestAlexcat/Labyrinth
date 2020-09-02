using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{

    public enum RoomState
    {
        Found = -3, Processed, Free, Brick
    }

    class Room
    {
        public int          x, y;            // координаты комнаты
        public RoomState    state;     // состояние комнаты 
        public              Room() { }
        public              Room(int x, int y, RoomState state = RoomState.Free)
        {
            this.x = x;
            this.y = y;
            this.state = state;
        }
       
    }

    class DFS
    {
        int man_x, man_y, sizeX, sizeY;
        public DFS() { }
        public DFS(int x) { }

        public int[] directionX = { 0, 1, 0, -1 };
        public int[] directionY = { 1, 0, -1, 0 };

        //Рекурсия. Поиск пути в глубину. Реалзация алгорится 
        public bool RealFindPath(Room current_cell)
        {
            //kraskal.Labirint[current_cell.y, current_cell.x] = (int)cell_state.Found;
            current_cell.state = RoomState.Found;

            int i;

            for (i = 0; i < 4; i++)
            {
                Room SosedCell = new Room(current_cell.x + directionX[i], current_cell.y + directionY[i]);
                if ((SosedCell.x >= 0) && (SosedCell.x < sizeX) &&
                    (SosedCell.y >= 0) && (SosedCell.y < sizeY) /*&&
                    (kraskal.Labirint[SosedCell.y, SosedCell.x] == (int)cell_state.Free)*/
                    )
                {
                    // kraskal.Labirint[SosedCell.y, SosedCell.x] = (int)cell_state.Found;
                    // current_cell.state = cell_state.Found;
                    man_x = SosedCell.x;
                    man_y = SosedCell.y;
                    return true;

                }
            }

            if (current_cell.x == 0 || current_cell.x == sizeX - 1
                || current_cell.y == 0 || current_cell.y == sizeY - 1)
            {
                man_x = current_cell.x;
                man_y = current_cell.y;
                return false; //выход найден
            }

            for (i = 0; i < 4; i++)
            {
               Room SosedCell = new Room(current_cell.x + directionX[i], current_cell.y + directionY[i]);

                if ((SosedCell.x >= 0) && (SosedCell.x < sizeX) &&
                    (SosedCell.y >= 0) && (SosedCell.y < sizeY) /*&&
                    (kraskal.Labirint[SosedCell.y, SosedCell.x] == (int)RoomState.Found)*/
                    )
                {
                   /* kraskal.Labirint[current_cell.y, current_cell.x] = (int)RoomState.Processed;*/
                    man_x = SosedCell.x;
                    man_y = SosedCell.y;
                    return true;
                }
            }

            return false;
        }
    }
}
