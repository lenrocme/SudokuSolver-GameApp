using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_v._1._0
{
    class AlghLoesung
    {
        private List<List<sbyte>> list2D = new List<List<sbyte>>();
        public bool falshInput = false;
        public sbyte possibilities = 0;

        public AlghLoesung() { }
        public AlghLoesung(List<List<sbyte>> list2D)
        {
            this.list2D = list2D;
            Loesung(); // prepar new table
        }
        public List<List<sbyte>> getList2D()
        {
            return list2D;
        }
        private void Loesung()
        {          
           if (writeAllBoxes(list2D) == null)
                justDoIt(list2D);
        }
        //take data from list and make one box
        private List<sbyte> FromListToBox(sbyte BoxNr, List<List<sbyte>> list2D)
        {
            List<sbyte> oneBoxNumbersList = new List<sbyte>();

            sbyte horizontal = new Box(BoxNr).getHorizontal();
            sbyte vertical = new Box(BoxNr).getVertical();

            for (sbyte v = 0; v < 3; v++)
            {
                for (sbyte h = 0; h < 3; h++)
                {
                    oneBoxNumbersList.Add(list2D[vertical + v][horizontal + h]);
                }
            }
            return oneBoxNumbersList;
        }
        // search which number aalready inputed in the one box, afterthat write a list with number we have to find(1..9)
        public List<sbyte> ListNotInputedNumbersPerBox(sbyte nrBox, List<List<sbyte>> list2D)
        {
            List<sbyte> listNotInputedNumbers = new List<sbyte>();
            for (sbyte i = 1; i < 10; i++)
            {
                if (!FromListToBox(nrBox, list2D).Contains(i))
                {
                    listNotInputedNumbers.Add(i);
                }
            }          
            return listNotInputedNumbers;
        }
        public List<sbyte> ListNotInputedNumbersPerBox(sbyte index, List<sbyte> list2D)
        {
            index = (SByte)(index - index % 9);
            List<sbyte> listNotInputedNumbers = new List<sbyte>();
            for (sbyte i = 0; i < 9; i++)
            {
                listNotInputedNumbers.Add(list2D[index + i]);
            }
            return listNotInputedNumbers;
        }
        private Boolean checkHorizontal(sbyte checkNumberH, sbyte stelle, List<List<sbyte>> list2D)
        {
            for (sbyte i = 0; i < 9; i++)
            {
                if (checkNumberH.Equals(list2D[stelle][i]))
                {
                    return true; // this number exist in the row
                }
            }
            return false; // number is NOT in the column
        }
        private Boolean checkVertical(sbyte checkNumberV, sbyte stelle, List<List<sbyte>> list2D)
        {
            for (sbyte i = 0; i < 9; i++)
            {
                if (checkNumberV == list2D[i][stelle])
                {
                    return true; // this number exist in the column
                }
            }
            return false; //number is NOT in the column
        }
        private void writeOneBox(sbyte nrBox, List<List<sbyte>> list2D)
        {
            bool check = true;
            while (check)
            {
                check = false; // if not changed = only one loop
                List<sbyte> checkLocalList = new List<sbyte>(FromListToBox(nrBox, list2D));
                if (!checkLocalList.Contains(0))
                {
                    break;
                }
                foreach (var element in ListNotInputedNumbersPerBox(nrBox, list2D))
                {
                    List<sbyte> localList = new List<sbyte>(FromListToBox(nrBox, list2D));
                    Box elementIndex = new Box(nrBox);
                    sbyte count = 0;
                    sbyte columnIndex = 0;
                    sbyte rowIndex = 0;
                    sbyte index = -1;
                    for (sbyte i = 0; i < 9; i++)
                    {
                        if (localList[i] == 0)
                        {
                            if (!checkHorizontal(element, elementIndex.getRow(i), list2D))
                            {
                                if (!checkVertical(element, elementIndex.getColumn(i), list2D))
                                {
                                    count++;
                                    columnIndex = elementIndex.getColumn(i);
                                    rowIndex = elementIndex.getRow(i);
                                    index = i;
                                }
                            }
                        }
                    }
                    if (count == 1)
                    {
                        list2D[rowIndex][columnIndex] = element;
                        localList[index] = element;
                        // we changed an element in liste, so we can make one more loop to see if we can make another change,
                        // if not, then false, and while lopp its stop 
                        check = true;
                    }
                }
                if (!checkLocalList.Contains(0))
                {
                    check = false;
                }
            }
        }
        public List<List<sbyte>> writeAllBoxes(List<List<sbyte>> list2D)
        {
            if (!CheckOnEmpty(list2D))
                return list2D;
            bool check = true;
            while (check)
            {
                sbyte count = 0;
                sbyte secondCount = 0;
                for (sbyte r = 0; r < 9; r++)
                {
                    for (sbyte c = 0; c < 9; c++)
                    {
                        if (list2D[r][c] == 0)
                        {
                            count++; //make first controle point
                        }
                    }
                }
                for (sbyte i = 1; i < 10; i++)
                {
                    writeOneBox(i, list2D);
                }
                for (sbyte r = 0; r < 9; r++)
                {
                    for (sbyte c = 0; c < 9; c++)
                    {
                        if (list2D[r][c] == 0)
                        {
                            secondCount++;          //second controle point
                        }
                    }
                }
                if (count == secondCount)        // if they a equals, then no changes posible, this mean no inaf data in table, or Sudoku had more then dificulty easy*
                {
                    check = false;
                    return null;        // not posible with this alhg to loesen...
                }
                if (secondCount == 0)     // no 0, it's mean  the table is full
                {
                    check = false;
                }
            }
            return list2D;    // easy Sudoku is fulfilled
        }
                                                                                /*___________algh with guess________________*/
        public void justDoIt(List<List<sbyte>> list2D)
        {
            possibilities = 0;
            sbyte index = 0;
            sbyte temp = 0;       
            List<sbyte> originalList = new List<sbyte>(createOneLineList(list2D));
            List<sbyte> copyList = new List<sbyte>(originalList);
            while (true)
            {
                /*Console.WriteLine("\n\n##################### anfang  ############################");
                Test.checkListConsole(create2DListFromLineList(copyList));
                Console.WriteLine("\n\nindex :   " + index+ "\n\n");
                Console.WriteLine("\n\nnumber :   " + temp + "\n\n");
                Test.checkListConsole(originalList);
                Test.checkListConsole(copyList);
                Console.WriteLine("\n\n");*/
                if(index < 0)
                {
                    falshInput = true;
                    return;
                }
                if (originalList[index] == 0)
                {                    
                    if (Convert.ToInt32(copyList[index]) == 9)
                    {
                        copyList[index] = 0;
                        index--;
                        continue;
                    }
                    temp = (SByte)(copyList[index] + 1);
                    if (temp == 10)
                    {
                        index--;
                        continue;
                    }
                    /*Console.WriteLine("\n\nnumber :   " + temp + "\n\n");
                    Console.WriteLine("\n''''''''''''\n");
                    Test.checkListConsole(ListNotInputedNumbersPerBox(index, copyList));
                    Console.WriteLine("\n''''''''''''\n");*/
                    while (true)
                    {
                        if (!new List<sbyte>(ListNotInputedNumbersPerBox(index, copyList)).Contains(temp))      // mean this number is not yet in the table
                        {
                            if (tryHard(temp, index, copyList))
                            {
                                copyList[index] = temp;
                                //checkListConsole(create2DListFromLineList(copyList));
                                if (!CheckOnEmpty(create2DListFromLineList(copyList))) 
                                {
                                    possibilities++;
                                    if (possibilities > 1)
                                       return;
                                    this.list2D = new List<List<sbyte>>(create2DListFromLineList(copyList));
                                    temp++;
                                    if (temp > 9)
                                    {
                                        copyList[index] = 0;
                                        index--;
                                        break;
                                    }
                                    break;
                                }
                                index++;
                                break;
                            }
                            else
                            {
                                temp++;
                                if (temp > 9)
                                {
                                    copyList[index] = 0;
                                    index--;
                                    break;
                                }
                            }
                        }
                        else
                            temp++;
                        if (temp > 9)
                        {
                            copyList[index] = 0;
                            index--;
                            break;
                        }
                    }
                }
                else if (temp > 9)                
                    index--;                
                else
                    index++;
            }
        }
        public bool tryHard(sbyte element, sbyte index, List<sbyte> copyList)
        {
            Box getnr = new Box();
            Box elementIndex = new Box(getnr.getFromIndexBooxNr(index));
            List<List<sbyte>> copyList2D = new List<List<sbyte>>(create2DListFromLineList(copyList));
            index %= 9;
            if (checkHorizontal(element, elementIndex.getRow(index), copyList2D))
                return false;
            if (checkVertical(element, elementIndex.getColumn(index), copyList2D))
                return false;            // true for passt
            return true;       // 
        }
        public List<List<sbyte>> create2DListFromLineList(List<sbyte> list1D)  // 2dlist from format online boxe one bx one
        {
            List<List<sbyte>> list2D = new List<List<sbyte>>();
            for (sbyte z = 0; z < 79; z += 27) 
            { 
                for (sbyte i = 0; i < 9; i += 3)                        //check point
                {
                    List<sbyte> temp = new List<sbyte>();
                    for (sbyte nr = 0; nr < 19; nr += 9)
                    {
                        for (sbyte c = 0; c < 3; c++)
                            temp.Add(list1D[c + nr + i + z]);
                    }
                    list2D.Add(temp);
                } 
            }
            return list2D;
        }
        public List<sbyte> createOneLineList(List<List<sbyte>> liste2D)      //one box in line + other box in line....
        {
            List<sbyte> newList = new List<sbyte>();
            for (sbyte nrBox = 1; nrBox < 10; nrBox++)
            {
                foreach (var element in FromListToBox(nrBox, liste2D))
                {
                    newList.Add(element);
                }
            }
            return newList;
        }
        public bool CheckOnEmpty(List<List<sbyte>> list)
        {
            foreach( var element in list)
            {
                if (element.Contains(0))
                    return true;
            }
            return false;
        }
    }
}
