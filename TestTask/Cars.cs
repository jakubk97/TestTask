using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTask
{
    class Cars
    {
        private string manufacturer, model, capacity;
        public Cars(string Manufacturer, string Model, string Capacity)
        {
            manufacturer = Manufacturer;
            model = Model;
            capacity = Capacity;
        }

        public string Capacity
        {
            get
            {
                return capacity;
            }
        }

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
        }
    }
}
