using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_v._1._0
{
    public partial class Sudoku : Form
    {
        private List<List<sbyte>> list2D = new List<List<sbyte>>();
        private List<List<sbyte>> inputedTableToCompareRightAnswers = new List<List<sbyte>>();
        private List<List<sbyte>> niveauTempList = new List<List<sbyte>>();
        private List<List<string>> cashList = new List<List<string>>();
        private List<TextBox> txtBoxList = new List<TextBox>();
        private bool hamburgMenu = false;
        private sbyte niveau = 1;
        private sbyte itemMenu = 1;
        private bool undoButton = false;
        private TimeSpan countTime;
        private bool showRightAnswers = false;
        private Grafic grafic = new Grafic();
        private IconsButtons iconButton = new IconsButtons();
        private bool windowVertical = false; // false for minimal
        private TextBox lastSelectedTxtBox = new TextBox();
        private bool gameStatus = false; // false for playing, true for ended game
        private bool solverStatus = false; // false for = is not solved yet, true for solved

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Sudoku()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.ClientSize = new System.Drawing.Size(420, 543);
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            hardToolStripMenuItem.Visible = false;  // not devloped, in progress
            expertToolStripMenuItem.Visible = false;
            AddTxtBox();
            lastSelectedTxtBox.Text = "A"; // check point, if is "A", mean not selected, iconButton for Tipp is not gonna work
            selectedPlay();
        }

        private bool compareLists()
        {
            Set2DArray(inputedTableToCompareRightAnswers);
            for (sbyte row = 0; row < 9; row++)
                for (sbyte col = 0; col < 9; col++)
                    if (list2D[row][col] != inputedTableToCompareRightAnswers[row][col])
                        return false;
            return true;
        }
        private async void selectedPlay()
        {
            grafic.setColorTextBoxes(txtBoxList, true);
            countTime = new TimeSpan(0, 0, 0);
            cleanBoxes();
            foreach (var element in txtBoxList)     // enable input
                element.ReadOnly = false;
            lbAnotation.Text = countTime.ToString(@"hh\:mm\:ss");
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            buttonLeft.Text = "Neues Sp";
            buttonRight.Text = "Anzeigen";
            AlghCreateSudoku createSudoku = new AlghCreateSudoku();
            gameStatus = false;
            cashList.Clear();
            if (niveau == 1)
            {                                       //(niveauDificulty , nrElements)
                lbDifficulty.Text = "Niveau: Easy";
                await createSudoku.setNiveauAndCreate(1, 36);    //36 = how many number gonna inputed in table                   
            }
            else if (niveau == 2)
            {
                lbDifficulty.Text = "Niveau: Medium";
                await createSudoku.setNiveauAndCreate(2, 32);
            }
            else if (niveau == 3)
            {
                lbDifficulty.Text = "Niveau: Hard";
                await createSudoku.setNiveauAndCreate(3, 29);
            }     
            else if (niveau == 4)
            {
                lbDifficulty.Text = "Niveau: Expert";
                await createSudoku.setNiveauAndCreate(4, 23);
            }
            list2D = createSudoku.getList2D();
            niveauTempList = createSudoku.getNiveauList();
            PutDataInBoxes(niveauTempList);
            //setNoEmptyTextBoxNonRedactable();
            timer.Start();
            //lbAnotation.Text = "Erstellt in: " + watch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void selectedLoeser()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lbAnotation.Text = "Sudoku v.1.01 Alpha";
            lbDifficulty.Text = "Niveau";
            list2D.Clear();
            if (!IsNumberNotZero())
            {
                lbAnotation.Text = "Falsche Eingabe";
            }
            else
            {
                Set2DArray(list2D);
                if (checkHorizontalVerticalOnDuplicates())
                    lbAnotation.Text = "Falsche Eingabe! Duplikate Zahl in einer Spalte oder in einer Reihe";
                else if (checkDupilicatesInBoxSudoku())
                    lbAnotation.Text = "Falsche Eingabe! Duplikate Zahl in einem Box";
                else
                {
                    lbDifficulty.Text = "Niveau:  " + CheckDificutllty();
                    AlghLoesung alghEasyNiv = new AlghLoesung(list2D);
                    if (alghEasyNiv.falshInput)
                        lbAnotation.Text = "Falsche Eingabe";
                    else
                    {   // here we go, if inputed data was right
                        PutDataInBoxes(alghEasyNiv.getList2D());
                        watch.Stop();
                        lbAnotation.Text = "Ausführungszeit: " + watch.ElapsedMilliseconds.ToString() + " ms";
                        solverStatus = true;
                    }
                }
            }
        }

       

        //list with all txtbox
        private void AddTxtBox()
        {
            txtBoxList.Add(textBox11);
            txtBoxList.Add(textBox12);
            txtBoxList.Add(textBox13);
            txtBoxList.Add(textBox14);
            txtBoxList.Add(textBox15);
            txtBoxList.Add(textBox16);
            txtBoxList.Add(textBox17);
            txtBoxList.Add(textBox18);
            txtBoxList.Add(textBox19);
            txtBoxList.Add(textBox21);
            txtBoxList.Add(textBox22);
            txtBoxList.Add(textBox23);
            txtBoxList.Add(textBox24);
            txtBoxList.Add(textBox25);
            txtBoxList.Add(textBox26);
            txtBoxList.Add(textBox27);
            txtBoxList.Add(textBox28);
            txtBoxList.Add(textBox29);
            txtBoxList.Add(textBox31);
            txtBoxList.Add(textBox32);
            txtBoxList.Add(textBox33);
            txtBoxList.Add(textBox34);
            txtBoxList.Add(textBox35);
            txtBoxList.Add(textBox36);
            txtBoxList.Add(textBox37);
            txtBoxList.Add(textBox38);
            txtBoxList.Add(textBox39);
            txtBoxList.Add(textBox41);
            txtBoxList.Add(textBox42);
            txtBoxList.Add(textBox43);
            txtBoxList.Add(textBox44);
            txtBoxList.Add(textBox45);
            txtBoxList.Add(textBox46);
            txtBoxList.Add(textBox47);
            txtBoxList.Add(textBox48);
            txtBoxList.Add(textBox49);
            txtBoxList.Add(textBox51);
            txtBoxList.Add(textBox52);
            txtBoxList.Add(textBox53);
            txtBoxList.Add(textBox54);
            txtBoxList.Add(textBox55);
            txtBoxList.Add(textBox56);
            txtBoxList.Add(textBox57);
            txtBoxList.Add(textBox58);
            txtBoxList.Add(textBox59);
            txtBoxList.Add(textBox61);
            txtBoxList.Add(textBox62);
            txtBoxList.Add(textBox63);
            txtBoxList.Add(textBox64);
            txtBoxList.Add(textBox65);
            txtBoxList.Add(textBox66);
            txtBoxList.Add(textBox67);
            txtBoxList.Add(textBox68);
            txtBoxList.Add(textBox69);
            txtBoxList.Add(textBox71);
            txtBoxList.Add(textBox72);
            txtBoxList.Add(textBox73);
            txtBoxList.Add(textBox74);
            txtBoxList.Add(textBox75);
            txtBoxList.Add(textBox76);
            txtBoxList.Add(textBox77);
            txtBoxList.Add(textBox78);
            txtBoxList.Add(textBox79);
            txtBoxList.Add(textBox81);
            txtBoxList.Add(textBox82);
            txtBoxList.Add(textBox83);
            txtBoxList.Add(textBox84);
            txtBoxList.Add(textBox85);
            txtBoxList.Add(textBox86);
            txtBoxList.Add(textBox87);
            txtBoxList.Add(textBox88);
            txtBoxList.Add(textBox89);
            txtBoxList.Add(textBox91);
            txtBoxList.Add(textBox92);
            txtBoxList.Add(textBox93);
            txtBoxList.Add(textBox94);
            txtBoxList.Add(textBox95);
            txtBoxList.Add(textBox96);
            txtBoxList.Add(textBox97);
            txtBoxList.Add(textBox98);
            txtBoxList.Add(textBox99);
        }

        private void setNoEmptyTextBoxNonRedactable()
        {
            foreach (var element in txtBoxList)
                if (!element.Text.Equals(""))
                    element.ReadOnly = true;
        }

        private bool isEmptyTextBoxes()
        {
            foreach (var element in txtBoxList)
                if (element.Text.Equals(""))
                    return true;
            return false;
        }

        // create list for one row form index n to n+9
        private List<sbyte> FuncCreateList(int n)
        {
            List<sbyte> listnumbersRow = new List<sbyte>();
            for (int i = n; i < 9 + n; i++)
            {
                if (txtBoxList[i].Text.Equals(""))
                {
                    listnumbersRow.Add(0);
                }
                else
                {
                    listnumbersRow.Add(SByte.Parse(txtBoxList[i].Text));
                }
            }
            return listnumbersRow;
        }

        //each row one list, all writed in 2d list
        private void Set2DArray(List<List<sbyte>> listeFromTextBoxes)
        {
            listeFromTextBoxes.Clear();
            for( int i = 0; i < 73; i = i + 9)
            {
                listeFromTextBoxes.Add(FuncCreateList(i));
            }

        }

        private List<string> setCashList()
        {
            List<string> cashList = new List<string>();
            foreach (var element in txtBoxList)
                cashList.Add(element.Text);
            return cashList;
        }
        //we need in every box number from 1..9, not allowed 0 or not nubmer and also is not allowed every box without input
        //check if input is number and not zero
        private Boolean IsNumber(TextBox txtBox)
        {
            int n;
            bool b = int.TryParse(txtBox.Text, out n); // true if input is a number
            return b;
        }
        //check if input in all boxex are right
        private Boolean IsNumberNotZero()
        {
            int count = 0;
            for(int i = 0; i < txtBoxList.Count; i++)
            {
                if (txtBoxList[i].Text != "")
                {
                    if (!IsNumber(txtBoxList[i]) || txtBoxList[i].Text.Equals("0"))
                    {
                        return false;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            if (count > 0)   // minimum one boxe have to be not empty
            {
                return true;
            }
            return false;
            
        }

        public void PutDataInBoxes(List<List<sbyte>> list2D)
        {
            for(sbyte row = 0; row < 9; row++)
            {
                for(sbyte col = 0; col < 9; col++)
                {
                    if (!(list2D[row][col] == 0))
                    {
                        txtBoxList[row * 9 + col].Text = list2D[row][col].ToString();
                    }
                    else
                    {
                       // txtBoxList[row * 9 + col].Text = "";
                    }
                }
            }
        }

        private void PutDataInBoxesFrom1Line(List<string> liste)
        {
            sbyte index = 0;
            foreach (var txtBox in txtBoxList)
                txtBox.Text = liste[index++];
        }

        private void buttonLosen(object sender, EventArgs e) //left
        {
            if(itemMenu == 1)
            {
                cashList.Clear();
                //niveauTempList.Clear();
                selectedPlay();
            }
            else if(itemMenu == 2)
            {
                selectedLoeser();
            }
        }

        private void buttonEntfernen(object sender, EventArgs e) //right
        {
            if (itemMenu == 1)
            {
                if (niveauTempList.Count > 0)
                {
                    showRightAnswers = true;
                    PutDataInBoxes(list2D);
                }
            }
            else if (itemMenu == 2)
            {
                cleanBoxes();
            }
        }

        private void cleanBoxes()
        {
            foreach (var element in txtBoxList)
            {
                element.Text = "";
            }
            lbAnotation.Text = "Sudoku v.1.5 Beta";
           // lbDifficulty.Text = "Niveau";
        }

        private string CheckDificutllty()
        {
            int count = 0;
            for (int i = 0; i < txtBoxList.Count; i++)
            {
                if (txtBoxList[i].Text != "")
                    count++;      
            }
            if (count < 25)
                return "Expert";
            if (count > 24 && count < 30)
                return "Hard";
            if (count > 29 && count < 33)
                return "Medium";
            return "Easy";
        }

        private bool checkDupilicatesInBoxSudoku()
        {
            List<sbyte> temp = new List<sbyte>(new AlghLoesung().createOneLineList(list2D));
            for (int i = 0; i < 9; i++)
            {
                List<sbyte> temporar = new List<sbyte>();
                for (sbyte n = 0; n < 9; n++)
                    temporar.Add(temp[n]);                
                for (sbyte j = 1; j < 10; j++)
                {
                    temporar.Remove(j);
                    if (temporar.Contains(j))
                        return true;
                }
            }
            return false;
        }

        private bool checkHorizontalVerticalOnDuplicates()
        {
            for (sbyte r = 0; r < 9; r++)
            {
                for (sbyte c = 0; c < 9; c++)
                {
                    if (list2D[r][c] != 0)
                    {
                        for(sbyte index = 0; index < 9; index++)
                        {                                       // check all columns and all rows, if  find duplicates return true
                            if (index != c)   // if index = c, mean same coordinat, not need to compare
                                if (list2D[r][c] == list2D[r][index])
                                    return true; 
                            if (index != r)
                                if (list2D[r][c] == list2D[index][c])
                                    return true;
                        }
                    }
                }
            }
            return false;
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btn_Colse_App_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Minimizieren_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_Colse_App_MouseLeave(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.ForeColor = Color.FromArgb(211, 211, 211);
            //btn_Colse_App.ForeColor = Color.FromArgb(211, 211, 211);
        }

        private void btn_Colse_App_MouseEnter(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.ForeColor = Color.White;
            //btn_Colse_App.ForeColor = Color.White;
        }

        private void MenuBurger(object sender, MouseEventArgs e)
        {
            /*if (!hamburgMenu)
             {
                 contextMenuStrip1.Show(Cursor.Position);
                 hamburgMenu = true;
             }
             else
                 hamburgMenu = false;     */
             contextMenuStrip1.Show(Cursor.Position);

        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDifficulty.Text = "Niveau: Easy";
            niveau = 1;
            itemMenu = 1;
            selectedPlay();
        }
        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDifficulty.Text = "Niveau: Medium";
            niveau = 2;
            itemMenu = 1;

            selectedPlay();
        }
        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDifficulty.Text = "Niveau: Hard";
            niveau = 3;
            itemMenu = 1;

            selectedPlay();
        }
        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbDifficulty.Text = "Niveau: Expert";
            niveau = 4;
            itemMenu = 1;

            selectedPlay();
        }

        private void loeserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            solverStatus = false;
            cleanBoxes();
            itemMenu = 2;
            foreach (var element in txtBoxList)
            {
                element.ReadOnly = false;
                element.BackColor = Color.FromArgb(242, 252, 255);
                element.ForeColor = Color.FromArgb(115, 125, 125);
            }
            buttonLeft.Text = "Neues Sp";
            buttonLeft.Text = "Lösen";
            buttonRight.Text = "Löschen";
            lbDifficulty.Text = "Niveau";
            //selectedLoeser();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (itemMenu == 1)
            {
                countTime = countTime.Add(TimeSpan.FromSeconds(1));
                lbAnotation.Text = "Spieldauer: " + countTime.ToString(@"hh\:mm\:ss");
            }
        }

        private void textBoxTextChange(object sender, EventArgs e)
        {
            if ( itemMenu == 1 && gameStatus == false)
            {
                TextBox txtBox = (TextBox)sender;
                sbyte row = (sbyte)(txtBoxList.IndexOf(txtBox) / 9);
                sbyte column = (sbyte)(txtBoxList.IndexOf(txtBox) % 9);

                if (txtBox.Text.Equals(niveauTempList[row][column] + "") && !(niveauTempList[row][column] + "").Equals("0"))
                {
                    txtBox.ReadOnly = true;
                    grafic.setTextColor(txtBox, "nonRedactable");
                }
                if (!isEmptyTextBoxes())
                    if (IsNumberNotZero())
                        if (compareLists())
                        {
                            grafic.setColorTextBoxes(txtBoxList, false);
                            foreach (var element in txtBoxList)
                            {
                                element.ReadOnly = true;
                            }
                            timer.Stop();
                            gameStatus = true;
                            if (!showRightAnswers)
                                lbAnotation.Text = "Ihre Zeit: " + countTime;
                            else
                            {
                                lbAnotation.Text = "";
                                showRightAnswers = false;
                            }
                        }
                if (txtBox.Text.Equals(list2D[row][column] + "") && (niveauTempList[row][column] + "").Equals("0"))
                {
                    grafic.setTextColor(txtBox, "right");
                    if (!undoButton)//!txtBox.Text.Equals(""))
                        cashList.Add(setCashList());

                }
                else if (!txtBox.Text.Equals(list2D[row][column] + "") && (niveauTempList[row][column] + "").Equals("0"))
                {
                    grafic.setTextColor(txtBox, "false");
                    if (!undoButton)//!txtBox.Text.Equals(""))
                        cashList.Add(setCashList());
                }
            }           
        }

        private void resizeVertical(object sender, EventArgs e)
        {/*
            if (!windowVertical)
            {
                this.ClientSize = new System.Drawing.Size(430, 700);
                windowVertical = true;
                flowLayoutPanel1.Visible = true;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(430, 543);
                windowVertical = false;
                flowLayoutPanel1.Visible = false;
            }
       */ }

        private void onSelectedTextBox(object sender, EventArgs e)
        {
            if (!gameStatus)
            {
                lastSelectedTxtBox.BackColor = Color.FromArgb(242, 252, 255);
                lastSelectedTxtBox = (TextBox)sender;
                lastSelectedTxtBox.BackColor = Color.FromArgb(217, 252, 244);
            }
        }

        private void ButtonDeleteSelectedTxtBox(object sender, EventArgs e)
        {
            if(!solverStatus || !gameStatus)
                iconButton.deleteSelectedTxtBox(lastSelectedTxtBox);
        }

        private void showTippOnSelectedTextBox(object sender, EventArgs e)
        {
            
            if (itemMenu == 1 && gameStatus == false)
            {
                undoButton = true;
                if (!lastSelectedTxtBox.Text.Equals("A"))
                {
                    sbyte row = (sbyte)(txtBoxList.IndexOf(lastSelectedTxtBox) / 9);
                    sbyte column = (sbyte)(txtBoxList.IndexOf(lastSelectedTxtBox) % 9);
                    lastSelectedTxtBox.Text = list2D[row][column].ToString();
                    niveauTempList[row][column] = list2D[row][column];
                    grafic.setTextColor(lastSelectedTxtBox, "tipp");
                    lastSelectedTxtBox.ReadOnly = true;
                }
                undoButton = false;
            }             
        }

        private void undoOnTextBoxFromCash(object sender, EventArgs e)
        {
            foreach (var el in cashList)
                foreach(var elem in el)
                    Console.Write(elem);
            Console.WriteLine();
            if(itemMenu == 1 && gameStatus == false)
            {
                undoButton = true;
                if (cashList.Count > 0)
                {
                    PutDataInBoxesFrom1Line(cashList[cashList.Count - 1]);
                    cashList.RemoveAt(cashList.Count - 1);
                    PutDataInBoxes(niveauTempList);
                }
                else
                {
                    cleanBoxes();
                    PutDataInBoxes(niveauTempList);
                }
                undoButton = false;

            }
        }
    }
}
