using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    public class FootballPlayer
    {

        private int _ID;
        private string _name;
        private double _price;
        private int _shirtnumber;

        public FootballPlayer()
        {

        }

        public FootballPlayer(int id, string name, double price, int shirtNumber)
        {
            ID = id;
            Name = name;
            Price = price;
            ShirtNumber = shirtNumber;
        }

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {

                if (value.Trim().Length < 4)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _name = value;
                }
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _price = value;
                }
            }
        }
        public int ShirtNumber
        {
            get { return _shirtnumber; }
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _shirtnumber = value;
                }
            }
        }

        public override string ToString()
        {
            return $"id is : {ID}, name is : {Name}, price is {Price} and number is {ShirtNumber}";
        }
    }
}
