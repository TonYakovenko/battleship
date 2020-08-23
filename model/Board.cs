using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.model
{
    public class Board
    {
        public const int Row = 10;
        public const int Col = Row;
        public List<int> Map;
        public List<Ship> Fleet = new List<Ship>();
        public List<List<Spot>> board = new List<List<Spot>>();
        public string [,] playBoard;
        //public List<Ship> Fleet;

        private List<int> CreateEmptyMap()
        {
            List<int> map = new List<int>();
            for(int i=0; i < 100; i++)
            {
                map.Add(0);
            }
            return map;
        }
        public void createMap(List<Ship> fleet)
        {
            this.Fleet = fleet;
            this.Map = CreateEmptyMap();

            foreach(Ship ship in fleet)
            {
                for(int i=0; i < ship.Coordinates.Count; i++)
                {
                    this.Map[ship.Coordinates[i]] = 1; 
                }
            }

            CreateBoard();
        }
        private void CreateBoard()
        {
            for(int i=0; i<Row; i++)
            {
                List<Spot> boardCol = new List<Spot>();

                for(int j=0; j<Col; j++)
                {
                    int id = i*Col + j;
                    bool isFilled = Convert.ToBoolean(this.Map[id]);
                    Spot sp = new Spot(isFilled);
                    sp.setId(id);
                    boardCol.Add(sp);
                }
                
                board.Add(boardCol);
            }
        }

        public void showBoard()
        {
           Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("--------------------");
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < Row; i++)
            {
                for(int j = 0; j < Col; j++)
                {
                    sb.Append(" " + board[i][j].showFace());
                }
                sb.AppendLine();
            }
            
            System.Console.WriteLine(sb);
            System.Console.WriteLine("--------------------");
            
        }

        private int _currShipId;
        //TODO Nice to have Chars for row and Int for column

        // returns Hit, Missed or Destroyed
        public string MakeShot(int intRow, int intCol)
        {
            bool hit;
            int spotId = intRow * Col + intCol;

            board[intRow][intCol].setStatus(false); // reveal the spot
            hit = board[intRow][intCol].getValue(); // hit or not
            if(hit)
            {
                _currShipId = GetShipId(spotId);
                if(IsDestroyed(_currShipId, spotId))
                {
                    System.Console.WriteLine(Fleet[_currShipId].Name + " is DESTROYED!");
                    return "Destroyed";
                }
                else
                    return "Hit";
            }
            else
            {
                return "Missed";
            }
        }

        private int GetShipId(int spotId)
        {
            int shipIndex = -1;

            for(int i = 0; i < Fleet.Count; i ++)
            {
                for(int j = 0; j < Fleet[i].Coordinates.Count; j ++)
                {
                    if(Fleet[i].Coordinates[j] == spotId)
                    {
                        shipIndex = i;
                    } 
                }
            }

            return shipIndex;
        }

        private bool IsDestroyed(int shipId, int spotId)
        {
            Fleet[shipId].Coordinates.Remove(spotId);
            
            if(Fleet[shipId].Coordinates.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RevealMultipleSpots(List<int> list)
        {
            for(int i=0; i < list.Count; i++)
            {
                int intRow = (int)Math.Floor((decimal)list[i] / Col);
                int intCol = list[i] - intRow * Col;
                board[intRow][intCol].setStatus(false);
            }
        }
    }
}