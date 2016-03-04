using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CivilizationEntity.CivilizationProperty
{
    public class Culture
    {
        public double Value { set; get; }

        public Culture(double val)
        {
            Value = val;
        }
    }
}
