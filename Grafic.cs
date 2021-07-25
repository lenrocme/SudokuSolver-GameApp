using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_v._1._0
{
    class Grafic
    {
        public void setColorTextBoxes(List<TextBox> list, bool isRedactable)
        {
            if(isRedactable)
                foreach(var element in list)
                    element.BackColor = Color.FromArgb(242, 252, 255);
            else
                foreach (var element in list)
                   // element.BackColor = Color.FromArgb(200, 245, 255);
                    element.BackColor = Color.FromArgb(217, 252, 244);
        }

        public void setTextColor(TextBox txtBox, string inputOnBox)
        {
            if (inputOnBox.Equals("right"))
                txtBox.ForeColor = Color.FromArgb(3, 153, 153);//(1, 142, 132);//(0, 111, 175);
            else if (inputOnBox.Equals("false"))
                txtBox.ForeColor = Color.FromArgb(252, 92, 68);
            else
                txtBox.ForeColor = Color.FromArgb(115, 125, 125);
        }
    }
}
