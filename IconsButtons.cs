using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_v._1._0
{
    class IconsButtons
    {

        public void deleteSelectedTxtBox(TextBox selexctedTextBox)
        {
            if (!selexctedTextBox.ReadOnly)
                selexctedTextBox.Text = "";
        }
    }
}
