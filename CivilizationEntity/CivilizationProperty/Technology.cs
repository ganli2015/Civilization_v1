using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CivilizationEntity.CivilizationProperty
{
    public class Technology
    {
        public double Value { set; get; }

        public Technology(double val)
        {
            Value = val;
        }
    }
}
