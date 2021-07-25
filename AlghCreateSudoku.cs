using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_v._1._0
{
    class AlghCreateSudoku
    {
        private List<List<sbyte>> createdTable = new List<List<sbyte>>();
        public bool negativIndex = false;
        private AlghLoesung listWork = new AlghLoesung();
        private List<List<sbyte>> niveauList = new List<List<sbyte>>();

        public AlghCreateSudoku(){}

        public async Task setNiveauAndCreate(sbyte niveauDifficulty, sbyte nrElements)
        {
            await Task.Run(() =>
            {
                if (niveauDifficulty <= 2)
                {
                    do
                    {
                        getTableGame(niveauDifficulty, nrElements);
                        checkIfOnlyOnePosible();
                    } while (listWork.possibilities != 1);
                    Console.WriteLine("first");
                }
                else
                {
                    do
                    {
                        do
                        {
                            getTableGame(niveauDifficulty, 32);
                            checkIfOnlyOnePosible();
                        } while (listWork.possibilities != 1);
                        Console.WriteLine("second");
                        Test.checkListConsole(niveauList);
                    } while (!searchUniqTable(createdTable, niveauDifficulty, nrElements));// not uniq ??
                                                                                           //setGameLists(niveauDifficulty, nrElements);
                }
            });

        }

        public void setGameLists(sbyte niveauDifficulty, sbyte nrElements)
        {
            do
            {
                connectAlghCreateResolve();               
                Test.checkListConsole(createdTable);
                Console.WriteLine("second");
            } while (!searchUniqTable(createdTable, niveauDifficulty, nrElements));// not uniq ??
            Console.WriteLine("second");

        }

        public List<List<sbyte>> getList2D()
        {
            return createdTable;
        }

        public List<List<sbyte>> getNiveauList()
        {
            return niveauList;
        }

        private List<List<sbyte>> setListOneLine2D()
        {
            List<List<sbyte>> list2DOneLine = new List<List<sbyte>>();
            Random rm = new Random();
            for (sbyte b = 0; b < 81; b++)  // 2D list: 80 list with 9 randoms numbers
            {
                HashSet<sbyte> newRandomList = new HashSet<sbyte>();
                List<sbyte> outputRandomList = new List<sbyte>();
                for (sbyte sb = 0; sb < 9; sb++)
                    while (!newRandomList.Add((SByte)(rm.Next(1, 10)))) ;  // not alow duplicated numbers
                foreach (var element in newRandomList)
                    outputRandomList.Add(element);
                list2DOneLine.Add(outputRandomList);
            }
            return list2DOneLine;
        }

        private List<List<sbyte>> createOriginalList() // only for test
        {
            List<List<sbyte>> outputList = new List<List<sbyte>>();
            for (sbyte sb = 0; sb < 9; sb++)
            {
                List<sbyte> temp = new List<sbyte>();
                for (sbyte b = 0; b < 9; b++)
                {
                    temp.Add(0);
                }
                outputList.Add(temp);
            }
            return outputList;
        }

        private List<List<sbyte>> CreateSudoku()
        {
            int count = 0;
            sbyte index = 0;
            sbyte temp = 0;
            sbyte iRlist = 0;
            List<sbyte> originalList = new List<sbyte>(listWork.createOneLineList(createOriginalList()));
            List<sbyte> copyList = new List<sbyte>(originalList);
            List<List<sbyte>> randomsNumbs = new List<List<sbyte>>(setListOneLine2D());
            while (count < 300)
            {
                count++;
                if (index < 0)
                {
                    negativIndex = true;
                    return null;
                }                      // in random list are same index with coppy list, but in random we have an list with randoms numbers 1 to 9
                if (iRlist == 8)      // check if is last element in list, for true we go back
                {
                    copyList[index] = 0;
                    index--;
                    iRlist = (SByte)randomsNumbs[index].IndexOf(copyList[index]);
                    continue;
                }
                while (true)
                {
                    if (!new List<sbyte>(listWork.ListNotInputedNumbersPerBox(index, copyList)).Contains(temp))      // mean this number is not yet in the table
                    {
                        if (listWork.tryHard(temp, index, copyList))        // check duplicate on Horizontal and vertical
                        {
                            copyList[index] = temp;
                            if (!listWork.CheckOnEmpty(listWork.create2DListFromLineList(copyList)))
                            {
                                //niveauList = new List<List<sbyte>>(listWork.create2DListFromLineList(copyList));
                                return new List<List<sbyte>>(listWork.create2DListFromLineList(copyList));
                            }
                            index++; //next step
                            iRlist = 0; // on next step we take all the time first element from next list in 2dlist
                                        // also with irlist = 0, we go trough the first if*
                            temp = randomsNumbs[index][iRlist]; // new element
                            break;
                        }
                        else
                        {
                            iRlist++;
                            if (iRlist > 8)
                            {
                                copyList[index] = 0;
                                index--;
                                iRlist = (SByte)randomsNumbs[index].IndexOf(copyList[index]);
                                break;
                            }
                            else
                                temp = randomsNumbs[index][iRlist]; // take next element from same list in 2dlist
                        }
                    }
                    else
                    {
                        iRlist++;
                        if (iRlist > 8)
                        {
                            copyList[index] = 0;
                            index--;
                            iRlist = (SByte)randomsNumbs[index].IndexOf(copyList[index]);
                            break;
                        }
                        else
                            temp = randomsNumbs[index][iRlist];      // take next element from same list in 2dlist
                    }
                }              
            }
            return new List<List<sbyte>>(listWork.create2DListFromLineList(copyList));
        }

        private void connectAlghCreateResolve()
        {
           //GC.Collect();
           // count++;
           // Console.WriteLine(count);

            createdTable = new List<List<sbyte>>(CreateSudoku());
            if (listWork.writeAllBoxes(createdTable) == null)     // if false, try call createsudoku and make another list
            {
                connectAlghCreateResolve();   
            }
            createdTable = new List<List<sbyte>>(listWork.writeAllBoxes(createdTable));
        }

        private List<List<sbyte>> randomTableIndex()
        {
            Random rm = new Random();
            HashSet<sbyte> newRandomList = new HashSet<sbyte>();
            List<sbyte> intOutputRandomList = new List<sbyte>();
            List<List<sbyte>> int2DList = new List<List<sbyte>>();
            for (sbyte sb = 0; sb < 38; sb++)
                while (!newRandomList.Add((SByte)(rm.Next(11, 100)))) ;  // not alow duplicated numbers
            foreach (var element in newRandomList)
            {
                intOutputRandomList.Add((sbyte)(element / 10));
                intOutputRandomList.Add((sbyte)(element - (element / 10) * 10));               
            }
            for( sbyte sb = 0; sb < 76; sb+=2)
            {
                List<sbyte> sbList = new List<sbyte>();
                for (sbyte i = 0; i < 2; i++)
                    sbList.Add(intOutputRandomList[sb + i]);
                int2DList.Add(sbList);
            }
            return int2DList;
        }

        public List<List<sbyte>> getTableGame(sbyte niveauDifficulty, sbyte nrOfElements)
        {
            connectAlghCreateResolve();
            List<List<sbyte>> newList = new List<List<sbyte>>(createOriginalList());
            List<List<sbyte>> coordList = new List<List<sbyte>>(randomTableIndex());
            for (sbyte i = 0 ; i < nrOfElements; i++)
            {
                sbyte coord1 = coordList[i][0];
                sbyte coord2 = coordList[i][1];
                if (coord2 > 0)
                    coord2--;
                coord1--;
                newList[coord1][coord2] = createdTable[coord1][coord2];
            }
            return niveauList = newList;
        }

        public void checkIfOnlyOnePosible() // if more posibility to resolve sudoku, thats not allowed, this make another table
        {
            //Test.checkListConsole(niveauList);
            listWork.justDoIt(niveauList);
            //Console.WriteLine("posibility:  " + listWork.possibilities);
           // Console.WriteLine(listWork.falshInput);

        }

        private bool searchUniqTable(List<List<sbyte>> list2D, sbyte niveauDifficulty, sbyte nrOfElements) // take created table, try every posibility to find uniq table
        {
           //List<List<sbyte>> preparedList2D = new List<List<sbyte>>(list2D);
            List<sbyte> oneLineOriginalList = new List<sbyte>(listWork.createOneLineList(list2D));
            List<sbyte> preparedoneLineList = new List<sbyte>(oneLineOriginalList);
            sbyte index = 0;
            sbyte step = 1;
            sbyte nrPosibilities = 0;
            sbyte nrOfZeros = 0; //how many Zero are in the table (max)
            do
            {

                    preparedoneLineList[index] = 0;
                    listWork.justDoIt(listWork.create2DListFromLineList(preparedoneLineList));
                    nrPosibilities = listWork.possibilities;
                
                if ( nrPosibilities == 1 && index < 80)
                {
                    step = 1;
                    index++;
                    nrOfZeros++;
                }
                else
                {
                    nrOfZeros--;
                    preparedoneLineList[index - step] = oneLineOriginalList[index - step];  // with step we can goo back to first index
                    step++;
                    if (step > index)
                        return false;       // imposible with this table ein uniq sudoku to find
                }
                if ( index == 80)
                {
                    preparedoneLineList[index - step] = oneLineOriginalList[index - step]; 


                }
            } while (nrOfZeros < 81 - nrOfElements);
            niveauList = listWork.create2DListFromLineList(preparedoneLineList);
            return true;
        }
    }
}
