using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_v._1._0
{   
    class Box
    {
        private sbyte vertical;
        private sbyte horizontal;
        private sbyte nrBox;

        public Box(sbyte nrBox)
        {
            this.nrBox = nrBox;
            setVerticHoriz();
        }
        public Box() { }

        public sbyte getFromIndexBooxNr(sbyte index)
        {
            sbyte temp = 9;
            for(sbyte boxNr = 1; boxNr < 10; boxNr++)
            {
                if (index < temp)
                    return boxNr;
                else
                    temp += 9;
            }
            return 0;
        }

        private void setVerticHoriz()
        {
            switch (nrBox)
            {
                case 1:
                    this.vertical = 0; this.horizontal = 0;
                    break;
                case 2:
                    this.vertical = 0; this.horizontal = 3;
                    break;
                case 3:
                    this.vertical = 0; this.horizontal = 6;
                    break;
                case 4:
                    this.vertical = 3; this.horizontal = 0;
                    break;
                case 5:
                    this.vertical = 3; this.horizontal = 3;
                    break;
                case 6:
                    this.vertical = 3; this.horizontal = 6;
                    break;
                case 7:
                    this.vertical = 6; this.horizontal = 0;
                    break;
                case 8:
                    this.vertical = 6; this.horizontal = 3;
                    break;
                case 9:
                    this.vertical = 6; this.horizontal = 6;
                    break;
            }
        }

        public sbyte getRow(sbyte elementIndex)
        {
            if (elementIndex < 3)
            {
                return (SByte)(0 + vertical);
            }
            else if (elementIndex < 6)
            {
                return (SByte)(1 + vertical);
            }
            else
            {
                return (SByte)(2 + vertical);
            }
        }

        public sbyte getColumn(sbyte elementindex)
        {
            if(elementindex % 3 == 0)
            {
                return (SByte)(0 + horizontal);
            }
            else if(elementindex % 3 == 1)
            {
                return (SByte)(1 + horizontal);
            }
            else
            {
                return (SByte)(2 + horizontal);
            }
        }

        public sbyte getVertical()
        {
            return vertical;
        }

        public sbyte getHorizontal()
        {
            return horizontal;
        }

    }
}
