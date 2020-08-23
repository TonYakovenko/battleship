using System.Collections.Generic;

namespace Battleship.model
{
    public class Maps
    {
       /* public static int[] Map1 = new int[100]
        {
            0,0,0,0,1,1,0,0,0,0,
            0,1,0,0,0,0,0,0,0,1,
            0,1,0,1,1,1,0,0,0,1,
            0,0,0,0,0,0,0,0,0,1,
            0,0,1,1,1,1,1,1,0,1,
            0,0,0,0,0,0,0,0,0,0,
            0,1,0,0,0,0,0,1,0,0,
            0,1,0,1,0,1,0,1,0,0,
            0,1,0,1,0,1,0,1,0,1,
            0,0,0,0,0,1,0,1,0,1
        };
        */
        
        public static List<Ship> Fleet = new List<Ship>
        {
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{4,5}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{11,21}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{73,83}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{89,99}},

            new Ship(){Name = "Submarine", Coordinates = new List<int>{23,24,25}},
            new Ship(){Name = "Submarine", Coordinates = new List<int>{61,71,81}},
            new Ship(){Name = "Submarine", Coordinates = new List<int>{75,85,95}},
            
            new Ship(){Name = "Battleship", Coordinates = new List<int>{19, 29, 39, 49}},
            new Ship(){Name = "Battleship", Coordinates = new List<int>{67, 77, 87, 97}},

            new Ship(){Name = "Carrier", Coordinates = new List<int>{42, 43, 44, 45, 46, 47}}
        };


        /*
        public static int[] Map2 = new int[100]
        {
            0,0,0,0,0,0,1,1,0,0,
            0,1,0,1,1,0,0,0,0,0,
            0,1,0,0,0,0,1,1,1,0,
            0,1,0,1,1,0,0,0,0,0,
            0,1,0,0,0,0,0,1,0,0,
            0,1,0,1,1,1,0,1,0,0,
            0,1,0,0,0,0,0,1,0,1,
            0,0,0,1,0,0,0,1,0,1,
            0,0,0,1,0,0,0,0,0,0,
            0,0,0,1,0,0,1,1,1,1
        };
        */

        public static List<Ship> Fleet2 = new List<Ship>
        {
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{6, 7}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{13, 14}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{33, 34}},
            new Ship(){Name = "Patrol Boat", Coordinates = new List<int>{69, 79}},

            new Ship(){Name = "Submarine", Coordinates = new List<int>{26, 27, 28}},
            new Ship(){Name = "Submarine", Coordinates = new List<int>{53, 54, 55}},
            new Ship(){Name = "Submarine", Coordinates = new List<int>{73, 83, 93}},
            
            new Ship(){Name = "Battleship", Coordinates = new List<int>{47, 57, 67, 77}},
            new Ship(){Name = "Battleship", Coordinates = new List<int>{96, 97, 98, 99}},

            new Ship(){Name = "Carrier", Coordinates = new List<int>{11, 21, 31, 41, 51, 61}}
        };

        /*public static int[] emptyMap = new int[100]
        {
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0
        };
        */
    }
}