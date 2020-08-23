namespace Battleship.model
{
    public class Spot
    {
        public bool isHidden = true;
        public bool isFilled = true;

        public int spotId;

        public Spot(bool val)
        {
            isFilled = val;
        }

        public void setId(int id)
        {
            spotId = id;
        }

        public int getId()
        {
            return spotId;
        }
        public bool getValue()
        {
            return isFilled;
        }
        public bool getStatus()
        {
            return isHidden;
        }

        public void setStatus(bool iHidden)
        {
            isHidden = iHidden;
        }

        public string showFace()
        {
            if(isHidden)
            {
                return "#";
            }
            else
            if(isFilled)
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }
    }
}