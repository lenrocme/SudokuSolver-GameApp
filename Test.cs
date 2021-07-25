using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_v._1._0
{
    class Test
    {

        public static void checkListConsole(List<List<sbyte>> liste)
        {
            string temp2 = "";
            foreach (var list in liste)
            {
                string temp = "";
                foreach (var element in list)
                {
                    temp += element + ",";
                }
                temp2 = temp2 + "\n" + temp;
            }
            Console.WriteLine("******************************");
            Console.WriteLine(temp2);
        }
        public static void checkListConsole(List<sbyte> liste)
        {
            string temp = "=";
            foreach (var element in liste)
            {
                temp += element + ",";
            }
            Console.WriteLine("******************************");
            Console.WriteLine(temp);
        }
    }
}
