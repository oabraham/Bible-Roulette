using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bible_Roulette
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void CeroBoxes()
        {
            foreach (RichTextBox Richtxb in NumberPanel.Controls.OfType<RichTextBox>())
            {
                Richtxb.Text = ResetTextBoxes;
            }

            InitVerseDataTable();

        }

        public int RandomUpdateInterval = 50;
        public const int BookTimeLength = 5000;
        public const int ChapterTimeLength = 10000;
        public const int VerseTimeLength = 15000;
        public volatile int ElapsedTime = 0;
        public const int ResetTime = 0;
        public const string ResetTextBoxes = "0";
        public const int MaxBook = 7;
        public const int MinBook = 0;
        public const int MinChapter = 0;
        public const int MaxChapterOne = 2;
        public const int MaxChapterTwo = 10;
        public const int MaxChapterThree = 10;
        public const int MinVerse = 0;
        public const int MaxVerseOne = 2;
        public const int MaxVerseTwo = 10;
        public const int MaxVerseThree = 10;
        public DataTable VerseCountPerChapters;

        public void RandomizeBox()
        {

            spinBtn.BackColor = SystemColors.ActiveCaption;
            spinBtn.Refresh();

            Random RandomForBookOne;
            Random RandomForBookTwo ;

            Random RandomForChapterOne;
            Random RandomForChapterTwo ;
            Random RandomForChapterThree ;

            Random RandomForVerseOne ;
            Random RandomForVerseTwo ;
            Random RandomForVerseThree ;

            ElapsedTime = ResetTime;
            
            while (ElapsedTime <= VerseTimeLength)
            {

                if (ElapsedTime <= BookTimeLength)
                {

                    RandomForBookOne = new Random();
                    RandomForBookTwo = new Random();

                    Book1.Text = RandomForBookOne.Next(MinBook, MaxBook).ToString();
                    Book2.Text = RandomForBookTwo.Next(MinBook, MaxBook).ToString();


                    RandomForBookOne = null;
                    RandomForBookTwo = null;
            
                    Book1.Refresh();
                    Book2.Refresh();

                }

                if(ElapsedTime <= ChapterTimeLength)
                {
                    RandomForChapterOne = new Random();
                    RandomForChapterTwo = new Random();
                    RandomForChapterThree = new Random();

                    Chapter1.Text = RandomForChapterOne.Next(MinChapter, MaxChapterOne).ToString();
                    Chapter2.Text = RandomForChapterTwo.Next(MinChapter, MaxChapterTwo ).ToString();
                    Chapter3.Text = RandomForChapterThree.Next(MinChapter, MaxChapterThree).ToString();

                    RandomForChapterOne = null;
                    RandomForChapterTwo = null;
                    RandomForChapterThree = null;

                    Chapter1.Refresh();
                    Chapter2.Refresh();
                    Chapter3.Refresh();

                }

                RandomForVerseOne = new Random();
                RandomForVerseTwo = new Random();
                RandomForVerseThree = new Random();

                Verse1.Text = RandomForVerseOne.Next(MinVerse , MaxVerseOne).ToString();
                Verse2.Text = RandomForVerseTwo.Next(MinVerse, MaxVerseTwo).ToString();
                Verse3.Text = RandomForVerseThree.Next(MinVerse, MaxVerseThree).ToString();

                RandomForVerseOne = null;
                RandomForVerseTwo = null;
                RandomForVerseThree = null;

                Verse1.Refresh();
                Verse2.Refresh();
                Verse3.Refresh();

                ElapsedTime += RandomUpdateInterval;

            }

            CheckResult();
          
        }

        public void CheckResult()
        {

            int ChapterNumber = int.Parse(Chapter1.Text + Chapter2.Text + Chapter3.Text) ;
            int BookNumber = int.Parse(Book1.Text + Book2.Text);
            int VerseNumber = int.Parse(Verse1.Text + Verse2.Text + Verse3.Text) ;
            bool isReal = false;
            String BookName = "";
            int MaxChapters;
            DataRow[] versecount;

            if ((BookNumber != 0) && (VerseNumber != 0) && (ChapterNumber != 0))
            {
                
                if (BookList.TryGetValue(BookNumber, out BookName))
                {

                    MaxChapters = NumberOfChaptersInBook[BookName];

                    if (ChapterNumber <= MaxChapters)
                    {

                        versecount = VerseCountPerChapters.Select("BookName = '" + BookName + "' and [" + ChapterNumber.ToString() + "] >= " + VerseNumber.ToString());

                        if (versecount.Count() > 0)
                        {
                            isReal = true;
                        }

                    }

                }
            }

            if (isReal)
            {

                spinBtn.BackColor = Color.Green;
                
                String Result = BookName + " " + ChapterNumber.ToString() + ":" + VerseNumber.ToString();
                
                DialogResult OpenBrowser = MessageBox.Show(
                    Result + " Show in browser?",                 
                    "Landed",                   
                    MessageBoxButtons.YesNo ,  
                    MessageBoxIcon.Question); 

                if(OpenBrowser == DialogResult.Yes)
                {
                    OpenVerse(Result);
                }

            }
            else
            {
                spinBtn.BackColor = Color.Red;
            }

            versecount = null;

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            CeroBoxes();
        }

        private void spinBtn_Click(object sender, EventArgs e)
        {
            RandomizeBox();
        }

        public void OpenVerse(String verse)
        {
            verse = verse.Replace(":", "%3A");
            verse = verse.Replace(" ", "%20");

            string target = "http://www.biblegateway.com/passage/?search=" + verse + "^&version=KJV";

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = target, UseShellExecute = true });
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }

        public void InitVerseDataTable()
        {
            
            VerseCountPerChapters  = new DataTable();

            VerseCountPerChapters.Columns.Add("BookName", typeof(string));
            
            for(int chapter = 1; chapter <= 150; chapter++)
            {
                VerseCountPerChapters.Columns.Add(chapter.ToString(), typeof(int));
            }

            //Genesis
            DataRow BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Genesis";
            BibleBook["1"] = 31;
            BibleBook["2"] = 25;
            BibleBook["3"] = 24;
            BibleBook["4"] = 26;
            BibleBook["5"] = 32;
            BibleBook["6"] = 22;
            BibleBook["7"] = 24;
            BibleBook["8"] = 22;
            BibleBook["9"] = 29;
            BibleBook["10"] = 32;
            BibleBook["11"] = 32;
            BibleBook["12"] = 20;
            BibleBook["13"] = 18;
            BibleBook["14"] = 28;
            BibleBook["15"] = 21;
            BibleBook["16"] = 16;
            BibleBook["17"] = 27;
            BibleBook["18"] = 33;
            BibleBook["19"] = 38;
            BibleBook["20"] = 18;
            BibleBook["21"] = 34;
            BibleBook["22"] = 24;
            BibleBook["23"] = 20;
            BibleBook["24"] = 67;
            BibleBook["25"] = 34;
            BibleBook["26"] = 35;
            BibleBook["27"] = 46;
            BibleBook["28"] = 22;
            BibleBook["29"] = 35;
            BibleBook["30"] = 43;
            BibleBook["31"] = 55;
            BibleBook["32"] = 32;
            BibleBook["33"] = 20;
            BibleBook["34"] = 31;
            BibleBook["35"] = 29;
            BibleBook["36"] = 43;
            BibleBook["37"] = 36;
            BibleBook["38"] = 30;
            BibleBook["39"] = 23;
            BibleBook["40"] = 23;
            BibleBook["41"] = 57;
            BibleBook["42"] = 38;
            BibleBook["43"] = 34;
            BibleBook["44"] = 34;
            BibleBook["45"] = 28;
            BibleBook["46"] = 34;
            BibleBook["47"] = 31;
            BibleBook["48"] = 22;
            BibleBook["49"] = 33;
            BibleBook["50"] = 26;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Exodus
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Exodus";
            BibleBook["1"] = 22;
            BibleBook["2"] = 25;
            BibleBook["3"] = 22;
            BibleBook["4"] = 31;
            BibleBook["5"] = 23;
            BibleBook["6"] = 30;
            BibleBook["7"] = 25;
            BibleBook["8"] = 32;
            BibleBook["9"] = 35;
            BibleBook["10"] = 29;
            BibleBook["11"] = 10;
            BibleBook["12"] = 51;
            BibleBook["13"] = 22;
            BibleBook["14"] = 31;
            BibleBook["15"] = 27;
            BibleBook["16"] = 36;
            BibleBook["17"] = 16;
            BibleBook["18"] = 27;
            BibleBook["19"] = 25;
            BibleBook["20"] = 26;
            BibleBook["21"] = 36;
            BibleBook["22"] = 31;
            BibleBook["23"] = 33;
            BibleBook["24"] = 18;
            BibleBook["25"] = 40;
            BibleBook["26"] = 37;
            BibleBook["27"] = 21;
            BibleBook["28"] = 43;
            BibleBook["29"] = 46;
            BibleBook["30"] = 38;
            BibleBook["31"] = 18;
            BibleBook["32"] = 35;
            BibleBook["33"] = 23;
            BibleBook["34"] = 35;
            BibleBook["35"] = 35;
            BibleBook["36"] = 38;
            BibleBook["37"] = 29;
            BibleBook["38"] = 31;
            BibleBook["39"] = 43;
            BibleBook["40"] = 38;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Leviticus
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Leviticus";
            BibleBook["1"] = 17;
            BibleBook["2"] = 16;
            BibleBook["3"] = 17;
            BibleBook["4"] = 35;
            BibleBook["5"] = 19;
            BibleBook["6"] = 30;
            BibleBook["7"] = 38;
            BibleBook["8"] = 36;
            BibleBook["9"] = 24;
            BibleBook["10"] = 20;
            BibleBook["11"] = 47;
            BibleBook["12"] = 8;
            BibleBook["13"] = 59;
            BibleBook["14"] = 57;
            BibleBook["15"] = 33;
            BibleBook["16"] = 34;
            BibleBook["17"] = 16;
            BibleBook["18"] = 30;
            BibleBook["19"] = 37;
            BibleBook["20"] = 27;
            BibleBook["21"] = 24;
            BibleBook["22"] = 33;
            BibleBook["23"] = 44;
            BibleBook["24"] = 23;
            BibleBook["25"] = 55;
            BibleBook["26"] = 46;
            BibleBook["27"] = 34;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Numbers
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Numbers";
            BibleBook["1"] = 54;
            BibleBook["2"] = 34;
            BibleBook["3"] = 51;
            BibleBook["4"] = 49;
            BibleBook["5"] = 31;
            BibleBook["6"] = 27;
            BibleBook["7"] = 89;
            BibleBook["8"] = 26;
            BibleBook["9"] = 23;
            BibleBook["10"] = 36;
            BibleBook["11"] = 35;
            BibleBook["12"] = 16;
            BibleBook["13"] = 33;
            BibleBook["14"] = 45;
            BibleBook["15"] = 41;
            BibleBook["16"] = 50;
            BibleBook["17"] = 13;
            BibleBook["18"] = 32;
            BibleBook["19"] = 22;
            BibleBook["20"] = 29;
            BibleBook["21"] = 35;
            BibleBook["22"] = 41;
            BibleBook["23"] = 30;
            BibleBook["24"] = 25;
            BibleBook["25"] = 18;
            BibleBook["26"] = 65;
            BibleBook["27"] = 23;
            BibleBook["28"] = 31;
            BibleBook["29"] = 40;
            BibleBook["30"] = 16;
            BibleBook["31"] = 54;
            BibleBook["32"] = 42;
            BibleBook["33"] = 56;
            BibleBook["34"] = 29;
            BibleBook["35"] = 34;
            BibleBook["36"] = 13;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Deuteronomy
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Deuteronomy";
            BibleBook["1"] = 46;
            BibleBook["2"] = 37;
            BibleBook["3"] = 29;
            BibleBook["4"] = 49;
            BibleBook["5"] = 33;
            BibleBook["6"] = 25;
            BibleBook["7"] = 26;
            BibleBook["8"] = 20;
            BibleBook["9"] = 29;
            BibleBook["10"] = 22;
            BibleBook["11"] = 32;
            BibleBook["12"] = 32;
            BibleBook["13"] = 18;
            BibleBook["14"] = 29;
            BibleBook["15"] = 23;
            BibleBook["16"] = 22;
            BibleBook["17"] = 20;
            BibleBook["18"] = 22;
            BibleBook["19"] = 21;
            BibleBook["20"] = 20;
            BibleBook["21"] = 23;
            BibleBook["22"] = 30;
            BibleBook["23"] = 25;
            BibleBook["24"] = 22;
            BibleBook["25"] = 19;
            BibleBook["26"] = 19;
            BibleBook["27"] = 26;
            BibleBook["28"] = 68;
            BibleBook["29"] = 29;
            BibleBook["30"] = 20;
            BibleBook["31"] = 30;
            BibleBook["32"] = 52;
            BibleBook["33"] = 29;
            BibleBook["34"] = 12;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Joshua
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Joshua";
            BibleBook["1"] = 18;
            BibleBook["2"] = 24;
            BibleBook["3"] = 17;
            BibleBook["4"] = 24;
            BibleBook["5"] = 15;
            BibleBook["6"] = 27;
            BibleBook["7"] = 26;
            BibleBook["8"] = 35;
            BibleBook["9"] = 27;
            BibleBook["10"] = 43;
            BibleBook["11"] = 23;
            BibleBook["12"] = 24;
            BibleBook["13"] = 33;
            BibleBook["14"] = 15;
            BibleBook["15"] = 63;
            BibleBook["16"] = 10;
            BibleBook["17"] = 18;
            BibleBook["18"] = 28;
            BibleBook["19"] = 51;
            BibleBook["20"] = 9;
            BibleBook["21"] = 45;
            BibleBook["22"] = 34;
            BibleBook["23"] = 16;
            BibleBook["24"] = 33;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Judges
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Judges";
            BibleBook["1"] = 36;
            BibleBook["2"] = 23;
            BibleBook["3"] = 31;
            BibleBook["4"] = 24;
            BibleBook["5"] = 31;
            BibleBook["6"] = 40;
            BibleBook["7"] = 25;
            BibleBook["8"] = 35;
            BibleBook["9"] = 57;
            BibleBook["10"] = 18;
            BibleBook["11"] = 40;
            BibleBook["12"] = 15;
            BibleBook["13"] = 25;
            BibleBook["14"] = 20;
            BibleBook["15"] = 20;
            BibleBook["16"] = 31;
            BibleBook["17"] = 13;
            BibleBook["18"] = 31;
            BibleBook["19"] = 30;
            BibleBook["20"] = 48;
            BibleBook["21"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Ruth
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Ruth";
            BibleBook["1"] = 22;
            BibleBook["2"] = 23;
            BibleBook["3"] = 18;
            BibleBook["4"] = 22;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Samuel
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Samuel";
            BibleBook["1"] = 28;
            BibleBook["2"] = 36;
            BibleBook["3"] = 21;
            BibleBook["4"] = 22;
            BibleBook["5"] = 12;
            BibleBook["6"] = 21;
            BibleBook["7"] = 17;
            BibleBook["8"] = 22;
            BibleBook["9"] = 27;
            BibleBook["10"] = 27;
            BibleBook["11"] = 15;
            BibleBook["12"] = 25;
            BibleBook["13"] = 23;
            BibleBook["14"] = 52;
            BibleBook["15"] = 35;
            BibleBook["16"] = 23;
            BibleBook["17"] = 58;
            BibleBook["18"] = 30;
            BibleBook["19"] = 24;
            BibleBook["20"] = 42;
            BibleBook["21"] = 15;
            BibleBook["22"] = 23;
            BibleBook["23"] = 29;
            BibleBook["24"] = 22;
            BibleBook["25"] = 44;
            BibleBook["26"] = 25;
            BibleBook["27"] = 12;
            BibleBook["28"] = 25;
            BibleBook["29"] = 11;
            BibleBook["30"] = 31;
            BibleBook["31"] = 13;
            VerseCountPerChapters.Rows.Add(BibleBook);
            
            //2 Samuel
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Samuel";
            BibleBook["1"] = 27;
            BibleBook["2"] = 32;
            BibleBook["3"] = 39;
            BibleBook["4"] = 12;
            BibleBook["5"] = 25;
            BibleBook["6"] = 23;
            BibleBook["7"] = 29;
            BibleBook["8"] = 18;
            BibleBook["9"] = 13;
            BibleBook["10"] = 19;
            BibleBook["11"] = 27;
            BibleBook["12"] = 31;
            BibleBook["13"] = 39;
            BibleBook["14"] = 33;
            BibleBook["15"] = 37;
            BibleBook["16"] = 23;
            BibleBook["17"] = 29;
            BibleBook["18"] = 33;
            BibleBook["19"] = 43;
            BibleBook["20"] = 26;
            BibleBook["21"] = 22;
            BibleBook["22"] = 51;
            BibleBook["23"] = 39;
            BibleBook["24"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Kings
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Kings";
            BibleBook["1"] = 53;
            BibleBook["2"] = 46;
            BibleBook["3"] = 28;
            BibleBook["4"] = 34;
            BibleBook["5"] = 18;
            BibleBook["6"] = 38;
            BibleBook["7"] = 51;
            BibleBook["8"] = 66;
            BibleBook["9"] = 28;
            BibleBook["10"] = 29;
            BibleBook["11"] = 43;
            BibleBook["12"] = 33;
            BibleBook["13"] = 34;
            BibleBook["14"] = 31;
            BibleBook["15"] = 34;
            BibleBook["16"] = 34;
            BibleBook["17"] = 24;
            BibleBook["18"] = 46;
            BibleBook["19"] = 21;
            BibleBook["20"] = 43;
            BibleBook["21"] = 29;
            BibleBook["22"] = 53;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Kings
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Kings";
            BibleBook["1"] = 18;
            BibleBook["2"] = 25;
            BibleBook["3"] = 27;
            BibleBook["4"] = 44;
            BibleBook["5"] = 27;
            BibleBook["6"] = 33;
            BibleBook["7"] = 20;
            BibleBook["8"] = 29;
            BibleBook["9"] = 37;
            BibleBook["10"] = 36;
            BibleBook["11"] = 21;
            BibleBook["12"] = 21;
            BibleBook["13"] = 25;
            BibleBook["14"] = 29;
            BibleBook["15"] = 38;
            BibleBook["16"] = 20;
            BibleBook["17"] = 41;
            BibleBook["18"] = 37;
            BibleBook["19"] = 37;
            BibleBook["20"] = 21;
            BibleBook["21"] = 26;
            BibleBook["22"] = 20;
            BibleBook["23"] = 37;
            BibleBook["24"] = 20;
            BibleBook["25"] = 30;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Chronicles
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Chronicles";
            BibleBook["1"] = 54;
            BibleBook["2"] = 55;
            BibleBook["3"] = 24;
            BibleBook["4"] = 43;
            BibleBook["5"] = 26;
            BibleBook["6"] = 81;
            BibleBook["7"] = 40;
            BibleBook["8"] = 40;
            BibleBook["9"] = 44;
            BibleBook["10"] = 14;
            BibleBook["11"] = 47;
            BibleBook["12"] = 40;
            BibleBook["13"] = 14;
            BibleBook["14"] = 17;
            BibleBook["15"] = 29;
            BibleBook["16"] = 43;
            BibleBook["17"] = 27;
            BibleBook["18"] = 18;
            BibleBook["19"] = 18;
            BibleBook["20"] = 8;
            BibleBook["21"] = 30;
            BibleBook["22"] = 19;
            BibleBook["23"] = 32;
            BibleBook["24"] = 31;
            BibleBook["25"] = 31;
            BibleBook["26"] = 32;
            BibleBook["27"] = 34;
            BibleBook["28"] = 21;
            BibleBook["29"] = 30;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Chronicles
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Chronicles";
            BibleBook["1"] = 17;
            BibleBook["2"] = 18;
            BibleBook["3"] = 17;
            BibleBook["4"] = 22;
            BibleBook["5"] = 14;
            BibleBook["6"] = 42;
            BibleBook["7"] = 22;
            BibleBook["8"] = 18;
            BibleBook["9"] = 31;
            BibleBook["10"] = 19;
            BibleBook["11"] = 23;
            BibleBook["12"] = 16;
            BibleBook["13"] = 22;
            BibleBook["14"] = 15;
            BibleBook["15"] = 19;
            BibleBook["16"] = 14;
            BibleBook["17"] = 19;
            BibleBook["18"] = 34;
            BibleBook["19"] = 11;
            BibleBook["20"] = 37;
            BibleBook["21"] = 20;
            BibleBook["22"] = 12;
            BibleBook["23"] = 21;
            BibleBook["24"] = 27;
            BibleBook["25"] = 28;
            BibleBook["26"] = 23;
            BibleBook["27"] = 9;
            BibleBook["28"] = 27;
            BibleBook["29"] = 36;
            BibleBook["30"] = 27;
            BibleBook["31"] = 21;
            BibleBook["32"] = 33;
            BibleBook["33"] = 25;
            BibleBook["34"] = 33;
            BibleBook["35"] = 27;
            BibleBook["36"] = 23;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Ezra
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Ezra";
            BibleBook["1"] = 11;
            BibleBook["2"] = 70;
            BibleBook["3"] = 13;
            BibleBook["4"] = 24;
            BibleBook["5"] = 17;
            BibleBook["6"] = 22;
            BibleBook["7"] = 28;
            BibleBook["8"] = 36;
            BibleBook["9"] = 15;
            BibleBook["10"] = 44;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Nehemiah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Nehemiah";
            BibleBook["1"] = 11;
            BibleBook["2"] = 20;
            BibleBook["3"] = 32;
            BibleBook["4"] = 23;
            BibleBook["5"] = 19;
            BibleBook["6"] = 19;
            BibleBook["7"] = 73;
            BibleBook["8"] = 18;
            BibleBook["9"] = 38;
            BibleBook["10"] = 39;
            BibleBook["11"] = 36;
            BibleBook["12"] = 47;
            BibleBook["13"] = 31;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Esther
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Esther";
            BibleBook["1"] = 22;
            BibleBook["2"] = 23;
            BibleBook["3"] = 15;
            BibleBook["4"] = 17;
            BibleBook["5"] = 14;
            BibleBook["6"] = 14;
            BibleBook["7"] = 10;
            BibleBook["8"] = 17;
            BibleBook["9"] = 32;
            BibleBook["10"] = 3;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Job
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Job";
            BibleBook["1"] = 22;
            BibleBook["2"] = 13;
            BibleBook["3"] = 26;
            BibleBook["4"] = 21;
            BibleBook["5"] = 27;
            BibleBook["6"] = 30;
            BibleBook["7"] = 21;
            BibleBook["8"] = 22;
            BibleBook["9"] = 35;
            BibleBook["10"] = 22;
            BibleBook["11"] = 20;
            BibleBook["12"] = 25;
            BibleBook["13"] = 28;
            BibleBook["14"] = 22;
            BibleBook["15"] = 35;
            BibleBook["16"] = 22;
            BibleBook["17"] = 16;
            BibleBook["18"] = 21;
            BibleBook["19"] = 29;
            BibleBook["20"] = 29;
            BibleBook["21"] = 34;
            BibleBook["22"] = 30;
            BibleBook["23"] = 17;
            BibleBook["24"] = 25;
            BibleBook["25"] = 6;
            BibleBook["26"] = 14;
            BibleBook["27"] = 23;
            BibleBook["28"] = 28;
            BibleBook["29"] = 25;
            BibleBook["30"] = 31;
            BibleBook["31"] = 40;
            BibleBook["32"] = 22;
            BibleBook["33"] = 33;
            BibleBook["34"] = 37;
            BibleBook["35"] = 16;
            BibleBook["36"] = 33;
            BibleBook["37"] = 24;
            BibleBook["38"] = 41;
            BibleBook["39"] = 30;
            BibleBook["40"] = 24;
            BibleBook["41"] = 34;
            BibleBook["42"] = 17;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Psalms
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Psalms";
            BibleBook["1"] = 6;
            BibleBook["2"] = 12;
            BibleBook["3"] = 8;
            BibleBook["4"] = 8;
            BibleBook["5"] = 12;
            BibleBook["6"] = 10;
            BibleBook["7"] = 17;
            BibleBook["8"] = 9;
            BibleBook["9"] = 20;
            BibleBook["10"] = 18;
            BibleBook["11"] = 7;
            BibleBook["12"] = 8;
            BibleBook["13"] = 6;
            BibleBook["14"] = 7;
            BibleBook["15"] = 5;
            BibleBook["16"] = 11;
            BibleBook["17"] = 15;
            BibleBook["18"] = 50;
            BibleBook["19"] = 14;
            BibleBook["20"] = 9;
            BibleBook["21"] = 13;
            BibleBook["22"] = 31;
            BibleBook["23"] = 6;
            BibleBook["24"] = 10;
            BibleBook["25"] = 22;
            BibleBook["26"] = 12;
            BibleBook["27"] = 14;
            BibleBook["28"] = 9;
            BibleBook["29"] = 11;
            BibleBook["30"] = 12;
            BibleBook["31"] = 24;
            BibleBook["32"] = 11;
            BibleBook["33"] = 22;
            BibleBook["34"] = 22;
            BibleBook["35"] = 28;
            BibleBook["36"] = 12;
            BibleBook["37"] = 40;
            BibleBook["38"] = 22;
            BibleBook["39"] = 13;
            BibleBook["40"] = 17;
            BibleBook["41"] = 13;
            BibleBook["42"] = 11;
            BibleBook["43"] = 5;
            BibleBook["44"] = 26;
            BibleBook["45"] = 17;
            BibleBook["46"] = 11;
            BibleBook["47"] = 9;
            BibleBook["48"] = 14;
            BibleBook["49"] = 20;
            BibleBook["50"] = 23;
            BibleBook["51"] = 19;
            BibleBook["52"] = 9;
            BibleBook["53"] = 6;
            BibleBook["54"] = 7;
            BibleBook["55"] = 23;
            BibleBook["56"] = 13;
            BibleBook["57"] = 11;
            BibleBook["58"] = 11;
            BibleBook["59"] = 17;
            BibleBook["60"] = 12;
            BibleBook["61"] = 8;
            BibleBook["62"] = 12;
            BibleBook["63"] = 11;
            BibleBook["64"] = 10;
            BibleBook["65"] = 13;
            BibleBook["66"] = 20;
            BibleBook["67"] = 7;
            BibleBook["68"] = 35;
            BibleBook["69"] = 36;
            BibleBook["70"] = 5;
            BibleBook["71"] = 24;
            BibleBook["72"] = 20;
            BibleBook["73"] = 28;
            BibleBook["74"] = 23;
            BibleBook["75"] = 10;
            BibleBook["76"] = 12;
            BibleBook["77"] = 20;
            BibleBook["78"] = 72;
            BibleBook["79"] = 13;
            BibleBook["80"] = 19;
            BibleBook["81"] = 16;
            BibleBook["82"] = 8;
            BibleBook["83"] = 18;
            BibleBook["84"] = 12;
            BibleBook["85"] = 13;
            BibleBook["86"] = 17;
            BibleBook["87"] = 7;
            BibleBook["88"] = 18;
            BibleBook["89"] = 52;
            BibleBook["90"] = 17;
            BibleBook["91"] = 16;
            BibleBook["92"] = 15;
            BibleBook["93"] = 5;
            BibleBook["94"] = 23;
            BibleBook["95"] = 11;
            BibleBook["96"] = 13;
            BibleBook["97"] = 12;
            BibleBook["98"] = 9;
            BibleBook["99"] = 9;
            BibleBook["100"] = 5;
            BibleBook["101"] = 8;
            BibleBook["102"] = 28;
            BibleBook["103"] = 22;
            BibleBook["104"] = 35;
            BibleBook["105"] = 45;
            BibleBook["106"] = 48;
            BibleBook["107"] = 43;
            BibleBook["108"] = 13;
            BibleBook["109"] = 31;
            BibleBook["110"] = 7;
            BibleBook["111"] = 10;
            BibleBook["112"] = 10;
            BibleBook["113"] = 9;
            BibleBook["114"] = 8;
            BibleBook["115"] = 18;
            BibleBook["116"] = 19;
            BibleBook["117"] = 2;
            BibleBook["118"] = 29;
            BibleBook["119"] = 176;
            BibleBook["120"] = 7;
            BibleBook["121"] = 8;
            BibleBook["122"] = 9;
            BibleBook["123"] = 4;
            BibleBook["124"] = 8;
            BibleBook["125"] = 5;
            BibleBook["126"] = 6;
            BibleBook["127"] = 5;
            BibleBook["128"] = 6;
            BibleBook["129"] = 8;
            BibleBook["130"] = 8;
            BibleBook["131"] = 3;
            BibleBook["132"] = 18;
            BibleBook["133"] = 3;
            BibleBook["134"] = 3;
            BibleBook["135"] = 21;
            BibleBook["136"] = 26;
            BibleBook["137"] = 9;
            BibleBook["138"] = 8;
            BibleBook["139"] = 24;
            BibleBook["140"] = 13;
            BibleBook["141"] = 10;
            BibleBook["142"] = 7;
            BibleBook["143"] = 12;
            BibleBook["144"] = 15;
            BibleBook["145"] = 21;
            BibleBook["146"] = 10;
            BibleBook["147"] = 20;
            BibleBook["148"] = 14;
            BibleBook["149"] = 9;
            BibleBook["150"] = 6;       
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Proverbs
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Proverbs";
            BibleBook["1"] = 33;
            BibleBook["2"] = 22;
            BibleBook["3"] = 35;
            BibleBook["4"] = 27;
            BibleBook["5"] = 23;
            BibleBook["6"] = 35;
            BibleBook["7"] = 27;
            BibleBook["8"] = 36;
            BibleBook["9"] = 18;
            BibleBook["10"] = 32;
            BibleBook["11"] = 31;
            BibleBook["12"] = 28;
            BibleBook["13"] = 25;
            BibleBook["14"] = 35;
            BibleBook["15"] = 33;
            BibleBook["16"] = 33;
            BibleBook["17"] = 28;
            BibleBook["18"] = 24;
            BibleBook["19"] = 29;
            BibleBook["20"] = 30;
            BibleBook["21"] = 31;
            BibleBook["22"] = 29;
            BibleBook["23"] = 35;
            BibleBook["24"] = 34;
            BibleBook["25"] = 28;
            BibleBook["26"] = 28;
            BibleBook["27"] = 27;
            BibleBook["28"] = 28;
            BibleBook["29"] = 27;
            BibleBook["30"] = 33;
            BibleBook["31"] = 31;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Ecclesiastes
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Ecclesiastes";
            BibleBook["1"] = 18;
            BibleBook["2"] = 26;
            BibleBook["3"] = 22;
            BibleBook["4"] = 16;
            BibleBook["5"] = 20;
            BibleBook["6"] = 12;
            BibleBook["7"] = 29;
            BibleBook["8"] = 17;
            BibleBook["9"] = 18;
            BibleBook["10"] = 20;
            BibleBook["11"] = 10;
            BibleBook["12"] = 14;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Song of Solomon
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Song of Solomon";
            BibleBook["1"] = 17;
            BibleBook["2"] = 17;
            BibleBook["3"] = 11;
            BibleBook["4"] = 16;
            BibleBook["5"] = 16;
            BibleBook["6"] = 13;
            BibleBook["7"] = 13;
            BibleBook["8"] = 14;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Isaiah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Isaiah";
            BibleBook["1"] = 31;
            BibleBook["2"] = 22;
            BibleBook["3"] = 26;
            BibleBook["4"] = 6;
            BibleBook["5"] = 30;
            BibleBook["6"] = 13;
            BibleBook["7"] = 25;
            BibleBook["8"] = 22;
            BibleBook["9"] = 21;
            BibleBook["10"] = 34;
            BibleBook["11"] = 16;
            BibleBook["12"] = 6;
            BibleBook["13"] = 22;
            BibleBook["14"] = 32;
            BibleBook["15"] = 9;
            BibleBook["16"] = 14;
            BibleBook["17"] = 14;
            BibleBook["18"] = 7;
            BibleBook["19"] = 25;
            BibleBook["20"] = 6;
            BibleBook["21"] = 17;
            BibleBook["22"] = 25;
            BibleBook["23"] = 18;
            BibleBook["24"] = 23;
            BibleBook["25"] = 12;
            BibleBook["26"] = 21;
            BibleBook["27"] = 13;
            BibleBook["28"] = 29;
            BibleBook["29"] = 24;
            BibleBook["30"] = 33;
            BibleBook["31"] = 9;
            BibleBook["32"] = 20;
            BibleBook["33"] = 24;
            BibleBook["34"] = 17;
            BibleBook["35"] = 10;
            BibleBook["36"] = 22;
            BibleBook["37"] = 38;
            BibleBook["38"] = 22;
            BibleBook["39"] = 8;
            BibleBook["40"] = 31;
            BibleBook["41"] = 29;
            BibleBook["42"] = 25;
            BibleBook["43"] = 28;
            BibleBook["44"] = 28;
            BibleBook["45"] = 25;
            BibleBook["46"] = 13;
            BibleBook["47"] = 15;
            BibleBook["48"] = 22;
            BibleBook["49"] = 26;
            BibleBook["50"] = 11;
            BibleBook["51"] = 23;
            BibleBook["52"] = 15;
            BibleBook["53"] = 12;
            BibleBook["54"] = 17;
            BibleBook["55"] = 13;
            BibleBook["56"] = 12;
            BibleBook["57"] = 21;
            BibleBook["58"] = 14;
            BibleBook["59"] = 21;
            BibleBook["60"] = 22;
            BibleBook["61"] = 11;
            BibleBook["62"] = 12;
            BibleBook["63"] = 19;
            BibleBook["64"] = 12;
            BibleBook["65"] = 25;
            BibleBook["66"] = 24;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Jeremiah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Jeremiah";
            BibleBook["1"] = 19;
            BibleBook["2"] = 37;
            BibleBook["3"] = 25;
            BibleBook["4"] = 31;
            BibleBook["5"] = 31;
            BibleBook["6"] = 30;
            BibleBook["7"] = 34;
            BibleBook["8"] = 22;
            BibleBook["9"] = 26;
            BibleBook["10"] = 25;
            BibleBook["11"] = 23;
            BibleBook["12"] = 17;
            BibleBook["13"] = 27;
            BibleBook["14"] = 22;
            BibleBook["15"] = 21;
            BibleBook["16"] = 21;
            BibleBook["17"] = 27;
            BibleBook["18"] = 23;
            BibleBook["19"] = 15;
            BibleBook["20"] = 18;
            BibleBook["21"] = 14;
            BibleBook["22"] = 30;
            BibleBook["23"] = 40;
            BibleBook["24"] = 10;
            BibleBook["25"] = 38;
            BibleBook["26"] = 24;
            BibleBook["27"] = 22;
            BibleBook["28"] = 17;
            BibleBook["29"] = 32;
            BibleBook["30"] = 24;
            BibleBook["31"] = 40;
            BibleBook["32"] = 44;
            BibleBook["33"] = 26;
            BibleBook["34"] = 22;
            BibleBook["35"] = 19;
            BibleBook["36"] = 32;
            BibleBook["37"] = 21;
            BibleBook["38"] = 28;
            BibleBook["39"] = 18;
            BibleBook["40"] = 16;
            BibleBook["41"] = 18;
            BibleBook["42"] = 22;
            BibleBook["43"] = 13;
            BibleBook["44"] = 30;
            BibleBook["45"] = 5;
            BibleBook["46"] = 28;
            BibleBook["47"] = 7;
            BibleBook["48"] = 47;
            BibleBook["49"] = 39;
            BibleBook["50"] = 46;
            BibleBook["51"] = 64;
            BibleBook["52"] = 34;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Lamentations
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Lamentations";
            BibleBook["1"] = 22;
            BibleBook["2"] = 22;
            BibleBook["3"] = 66;
            BibleBook["4"] = 22;
            BibleBook["5"] = 22;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Ezekiel
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Ezekiel";
            BibleBook["1"] = 28;
            BibleBook["2"] = 10;
            BibleBook["3"] = 27;
            BibleBook["4"] = 17;
            BibleBook["5"] = 17;
            BibleBook["6"] = 14;
            BibleBook["7"] = 27;
            BibleBook["8"] = 18;
            BibleBook["9"] = 11;
            BibleBook["10"] = 22;
            BibleBook["11"] = 25;
            BibleBook["12"] = 28;
            BibleBook["13"] = 23;
            BibleBook["14"] = 23;
            BibleBook["15"] = 8;
            BibleBook["16"] = 63;
            BibleBook["17"] = 24;
            BibleBook["18"] = 32;
            BibleBook["19"] = 14;
            BibleBook["20"] = 49;
            BibleBook["21"] = 32;
            BibleBook["22"] = 31;
            BibleBook["23"] = 49;
            BibleBook["24"] = 27;
            BibleBook["25"] = 17;
            BibleBook["26"] = 21;
            BibleBook["27"] = 36;
            BibleBook["28"] = 26;
            BibleBook["29"] = 21;
            BibleBook["30"] = 26;
            BibleBook["31"] = 18;
            BibleBook["32"] = 32;
            BibleBook["33"] = 33;
            BibleBook["34"] = 31;
            BibleBook["35"] = 15;
            BibleBook["36"] = 38;
            BibleBook["37"] = 28;
            BibleBook["38"] = 23;
            BibleBook["39"] = 29;
            BibleBook["40"] = 49;
            BibleBook["41"] = 26;
            BibleBook["42"] = 20;
            BibleBook["43"] = 27;
            BibleBook["44"] = 31;
            BibleBook["45"] = 25;
            BibleBook["46"] = 24;
            BibleBook["47"] = 23;
            BibleBook["48"] = 35;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Daniel
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Daniel";
            BibleBook["1"] = 21;
            BibleBook["2"] = 49;
            BibleBook["3"] = 30;
            BibleBook["4"] = 37;
            BibleBook["5"] = 31;
            BibleBook["6"] = 28;
            BibleBook["7"] = 28;
            BibleBook["8"] = 27;
            BibleBook["9"] = 27;
            BibleBook["10"] = 21;
            BibleBook["11"] = 45;
            BibleBook["12"] = 13;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Hosea
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Hosea";
            BibleBook["1"] = 11;
            BibleBook["2"] = 23;
            BibleBook["3"] = 5;
            BibleBook["4"] = 19;
            BibleBook["5"] = 15;
            BibleBook["6"] = 11;
            BibleBook["7"] = 16;
            BibleBook["8"] = 14;
            BibleBook["9"] = 17;
            BibleBook["10"] = 15;
            BibleBook["11"] = 12;
            BibleBook["12"] = 14;
            BibleBook["13"] = 16;
            BibleBook["14"] = 9;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Joel
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Joel";
            BibleBook["1"] = 20;
            BibleBook["2"] = 32;
            BibleBook["3"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Amos
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Amos";
            BibleBook["1"] = 15;
            BibleBook["2"] = 16;
            BibleBook["3"] = 15;
            BibleBook["4"] = 13;
            BibleBook["5"] = 27;
            BibleBook["6"] = 14;
            BibleBook["7"] = 17;
            BibleBook["8"] = 14;
            BibleBook["9"] = 15;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Obadiah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Obadiah";
            BibleBook["1"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Jonah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Jonah";
            BibleBook["1"] = 17;
            BibleBook["2"] = 10;
            BibleBook["3"] = 10;
            BibleBook["4"] = 11;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Micah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Micah";
            BibleBook["1"] = 16;
            BibleBook["2"] = 13;
            BibleBook["3"] = 12;
            BibleBook["4"] = 13;
            BibleBook["5"] = 15;
            BibleBook["6"] = 16;
            BibleBook["7"] = 20;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Nahum
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Nahum";
            BibleBook["1"] = 15;
            BibleBook["2"] = 13;
            BibleBook["3"] = 19;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Habakkuk
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Habakkuk";
            BibleBook["1"] = 17;
            BibleBook["2"] = 20;
            BibleBook["3"] = 19;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Zephaniah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Zephaniah";
            BibleBook["1"] = 18;
            BibleBook["2"] = 15;
            BibleBook["3"] = 20;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Haggai
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Haggai";
            BibleBook["1"] = 15;
            BibleBook["2"] = 23;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Zechariah
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Zechariah";
            BibleBook["1"] = 21;
            BibleBook["2"] = 13;
            BibleBook["3"] = 10;
            BibleBook["4"] = 14;
            BibleBook["5"] = 11;
            BibleBook["6"] = 15;
            BibleBook["7"] = 14;
            BibleBook["8"] = 23;
            BibleBook["9"] = 17;
            BibleBook["10"] = 12;
            BibleBook["11"] = 17;
            BibleBook["12"] = 14;
            BibleBook["13"] = 9;
            BibleBook["14"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Malachi
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Malachi";
            BibleBook["1"] = 14;
            BibleBook["2"] = 17;
            BibleBook["3"] = 18;
            BibleBook["4"] = 6;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Matthew
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Matthew";
            BibleBook["1"] = 25;
            BibleBook["2"] = 23;
            BibleBook["3"] = 17;
            BibleBook["4"] = 25;
            BibleBook["5"] = 48;
            BibleBook["6"] = 34;
            BibleBook["7"] = 29;
            BibleBook["8"] = 34;
            BibleBook["9"] = 38;
            BibleBook["10"] = 42;
            BibleBook["11"] = 30;
            BibleBook["12"] = 50;
            BibleBook["13"] = 58;
            BibleBook["14"] = 36;
            BibleBook["15"] = 39;
            BibleBook["16"] = 28;
            BibleBook["17"] = 27;
            BibleBook["18"] = 35;
            BibleBook["19"] = 30;
            BibleBook["20"] = 34;
            BibleBook["21"] = 46;
            BibleBook["22"] = 46;
            BibleBook["23"] = 39;
            BibleBook["24"] = 51;
            BibleBook["25"] = 46;
            BibleBook["26"] = 75;
            BibleBook["27"] = 66;
            BibleBook["28"] = 20;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Mark
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Mark";
            BibleBook["1"] = 45;
            BibleBook["2"] = 28;
            BibleBook["3"] = 35;
            BibleBook["4"] = 41;
            BibleBook["5"] = 43;
            BibleBook["6"] = 56;
            BibleBook["7"] = 37;
            BibleBook["8"] = 38;
            BibleBook["9"] = 50;
            BibleBook["10"] = 52;
            BibleBook["11"] = 33;
            BibleBook["12"] = 44;
            BibleBook["13"] = 37;
            BibleBook["14"] = 72;
            BibleBook["15"] = 47;
            BibleBook["16"] = 20;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Luke
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Luke";
            BibleBook["1"] = 80;
            BibleBook["2"] = 52;
            BibleBook["3"] = 38;
            BibleBook["4"] = 44;
            BibleBook["5"] = 39;
            BibleBook["6"] = 49;
            BibleBook["7"] = 50;
            BibleBook["8"] = 56;
            BibleBook["9"] = 62;
            BibleBook["10"] = 42;
            BibleBook["11"] = 54;
            BibleBook["12"] = 59;
            BibleBook["13"] = 35;
            BibleBook["14"] = 35;
            BibleBook["15"] = 32;
            BibleBook["16"] = 31;
            BibleBook["17"] = 37;
            BibleBook["18"] = 43;
            BibleBook["19"] = 48;
            BibleBook["20"] = 47;
            BibleBook["21"] = 38;
            BibleBook["22"] = 71;
            BibleBook["23"] = 56;
            BibleBook["24"] = 53;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //John
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "John";
            BibleBook["1"] = 51;
            BibleBook["2"] = 25;
            BibleBook["3"] = 36;
            BibleBook["4"] = 54;
            BibleBook["5"] = 47;
            BibleBook["6"] = 71;
            BibleBook["7"] = 53;
            BibleBook["8"] = 59;
            BibleBook["9"] = 41;
            BibleBook["10"] = 42;
            BibleBook["11"] = 57;
            BibleBook["12"] = 50;
            BibleBook["13"] = 38;
            BibleBook["14"] = 31;
            BibleBook["15"] = 27;
            BibleBook["16"] = 33;
            BibleBook["17"] = 26;
            BibleBook["18"] = 40;
            BibleBook["19"] = 42;
            BibleBook["20"] = 31;
            BibleBook["21"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //The Acts
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "The Acts";
            BibleBook["1"] = 26;
            BibleBook["2"] = 47;
            BibleBook["3"] = 26;
            BibleBook["4"] = 37;
            BibleBook["5"] = 42;
            BibleBook["6"] = 15;
            BibleBook["7"] = 60;
            BibleBook["8"] = 40;
            BibleBook["9"] = 43;
            BibleBook["10"] = 48;
            BibleBook["11"] = 30;
            BibleBook["12"] = 25;
            BibleBook["13"] = 52;
            BibleBook["14"] = 28;
            BibleBook["15"] = 41;
            BibleBook["16"] = 40;
            BibleBook["17"] = 34;
            BibleBook["18"] = 28;
            BibleBook["19"] = 41;
            BibleBook["20"] = 38;
            BibleBook["21"] = 40;
            BibleBook["22"] = 30;
            BibleBook["23"] = 35;
            BibleBook["24"] = 27;
            BibleBook["25"] = 27;
            BibleBook["26"] = 32;
            BibleBook["27"] = 44;
            BibleBook["28"] = 31;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Romans
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Romans";
            BibleBook["1"] = 32;
            BibleBook["2"] = 29;
            BibleBook["3"] = 31;
            BibleBook["4"] = 25;
            BibleBook["5"] = 21;
            BibleBook["6"] = 23;
            BibleBook["7"] = 25;
            BibleBook["8"] = 39;
            BibleBook["9"] = 33;
            BibleBook["10"] = 21;
            BibleBook["11"] = 36;
            BibleBook["12"] = 21;
            BibleBook["13"] = 14;
            BibleBook["14"] = 23;
            BibleBook["15"] = 33;
            BibleBook["16"] = 27;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Corinthians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Corinthians";
            BibleBook["1"] = 31;
            BibleBook["2"] = 16;
            BibleBook["3"] = 23;
            BibleBook["4"] = 21;
            BibleBook["5"] = 13;
            BibleBook["6"] = 20;
            BibleBook["7"] = 40;
            BibleBook["8"] = 13;
            BibleBook["9"] = 27;
            BibleBook["10"] = 33;
            BibleBook["11"] = 34;
            BibleBook["12"] = 31;
            BibleBook["13"] = 13;
            BibleBook["14"] = 40;
            BibleBook["15"] = 58;
            BibleBook["16"] = 24;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Corinthians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Corinthians";
            BibleBook["1"] = 24;
            BibleBook["2"] = 17;
            BibleBook["3"] = 18;
            BibleBook["4"] = 18;
            BibleBook["5"] = 21;
            BibleBook["6"] = 18;
            BibleBook["7"] = 16;
            BibleBook["8"] = 24;
            BibleBook["9"] = 15;
            BibleBook["10"] = 18;
            BibleBook["11"] = 33;
            BibleBook["12"] = 21;
            BibleBook["13"] = 14;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Galatians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Galatians";
            BibleBook["1"] = 24;
            BibleBook["2"] = 21;
            BibleBook["3"] = 29;
            BibleBook["4"] = 31;
            BibleBook["5"] = 26;
            BibleBook["6"] = 18;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Ephesians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Ephesians";
            BibleBook["1"] = 23;
            BibleBook["2"] = 22;
            BibleBook["3"] = 21;
            BibleBook["4"] = 32;
            BibleBook["5"] = 33;
            BibleBook["6"] = 24;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Philippians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Philippians";
            BibleBook["1"] = 30;
            BibleBook["2"] = 30;
            BibleBook["3"] = 21;
            BibleBook["4"] = 23;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Colossians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Colossians";
            BibleBook["1"] = 29;
            BibleBook["2"] = 23;
            BibleBook["3"] = 25;
            BibleBook["4"] = 18;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Thessalonians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Thessalonians";
            BibleBook["1"] = 10;
            BibleBook["2"] = 20;
            BibleBook["3"] = 13;
            BibleBook["4"] = 18;
            BibleBook["5"] = 28;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Thessalonians
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Thessalonians";
            BibleBook["1"] = 12;
            BibleBook["2"] = 17;
            BibleBook["3"] = 18;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Timothy
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Timothy";
            BibleBook["1"] = 20;
            BibleBook["2"] = 15;
            BibleBook["3"] = 16;
            BibleBook["4"] = 16;
            BibleBook["5"] = 25;
            BibleBook["6"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Timothy
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Timothy";
            BibleBook["1"] = 18;
            BibleBook["2"] = 26;
            BibleBook["3"] = 17;
            BibleBook["4"] = 22;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Titus
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Titus";
            BibleBook["1"] = 16;
            BibleBook["2"] = 15;
            BibleBook["3"] = 15;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Philemon
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Philemon";
            BibleBook["1"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Hebrews
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Hebrews";
            BibleBook["1"] = 14;
            BibleBook["2"] = 18;
            BibleBook["3"] = 19;
            BibleBook["4"] = 16;
            BibleBook["5"] = 14;
            BibleBook["6"] = 20;
            BibleBook["7"] = 28;
            BibleBook["8"] = 13;
            BibleBook["9"] = 28;
            BibleBook["10"] = 39;
            BibleBook["11"] = 40;
            BibleBook["12"] = 29;
            BibleBook["13"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //James
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "James";
            BibleBook["1"] = 27;
            BibleBook["2"] = 26;
            BibleBook["3"] = 18;
            BibleBook["4"] = 17;
            BibleBook["5"] = 20;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 Peter
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 Peter";
            BibleBook["1"] = 25;
            BibleBook["2"] = 25;
            BibleBook["3"] = 22;
            BibleBook["4"] = 19;
            BibleBook["5"] = 14;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 Peter
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 Peter";
            BibleBook["1"] = 21;
            BibleBook["2"] = 22;
            BibleBook["3"] = 18;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //1 John
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "1 John";
            BibleBook["1"] = 10;
            BibleBook["2"] = 29;
            BibleBook["3"] = 24;
            BibleBook["4"] = 21;
            BibleBook["5"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //2 John
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "2 John";
            BibleBook["1"] = 13;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //3 John
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "3 John";
            BibleBook["1"] = 14;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Jude
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Jude";
            BibleBook["1"] = 25;
            VerseCountPerChapters.Rows.Add(BibleBook);

            //Revelation
            BibleBook = VerseCountPerChapters.NewRow();
            BibleBook["BookName"] = "Revelation";
            BibleBook["1"] = 20;
            BibleBook["2"] = 29;
            BibleBook["3"] = 22;
            BibleBook["4"] = 11;
            BibleBook["5"] = 14;
            BibleBook["6"] = 17;
            BibleBook["7"] = 17;
            BibleBook["8"] = 13;
            BibleBook["9"] = 21;
            BibleBook["10"] = 11;
            BibleBook["11"] = 19;
            BibleBook["12"] = 17;
            BibleBook["13"] = 18;
            BibleBook["14"] = 20;
            BibleBook["15"] = 8;
            BibleBook["16"] = 21;
            BibleBook["17"] = 18;
            BibleBook["18"] = 24;
            BibleBook["19"] = 21;
            BibleBook["20"] = 15;
            BibleBook["21"] = 27;
            BibleBook["22"] = 21;
            VerseCountPerChapters.Rows.Add(BibleBook);

        }

        public Dictionary<String, int> NumberOfChaptersInBook = new Dictionary<String, int>()
        {
            {"Genesis", 50        },
            {"Exodus", 40         },
            {"Leviticus", 27      },
            {"Numbers", 36        },
            {"Deuteronomy", 34    },
            {"Joshua", 24         },
            {"Judges", 21         },
            {"Ruth",    4         },
            {"1 Samuel",    31    },
            {"2 Samuel",  24      },
            {"1 Kings", 22        },
            {"2 Kings", 25        },
            {"1 Chronicles", 29   },
            {"2 Chronicles",  36  },
            {"Ezra",    10        },
            {"Nehemiah",    13    },
            {"Esther",  10        },
            {"Job", 42            },
            {"Psalms",  150       },
            {"Proverbs",  31      },
            {"Ecclesiastes",  12  },
            {"Song of Solomon", 8 },
            {"Isaiah",  66        },
            {"Jeremiah",    52    },
            {"Lamentations",    5 },
            {"Ezekiel", 48        },
            {"Daniel",  12        },
            {"Hosea",   14        },
            {"Joel",    3         },
            {"Amos",    9         },
            {"Obadiah", 1         },
            {"Jonah",   4         },
            {"Micah",   7         },
            {"Nahum",   3         },
            {"Habakkuk",  3       },
            {"Zephaniah",   3     },
            {"Haggai",  2         },
            {"Zechariah",   14    },
            {"Malachi", 4         },
            {"Matthew", 28        },
            {"Mark",    16        },
            {"Luke",    24        },
            {"John",    21        },
            {"The Acts", 28       },
            {"Romans",  16        },
            {"1 Corinthians", 16  },
            {"2 Corinthians", 13  },
            {"Galatians",   6     },
            {"Ephesians",   6     },
            {"Philippians", 4     },
            {"Colossians",  4     },
            {"1 Thessalonians", 5 },
            {"2 Thessalonians", 3 },
            {"1 Timothy",   6     },
            {"2 Timothy",   4     },
            {"Titus",   3         },
            {"Philemon",    1     },
            {"Hebrews", 13        },
            {"James",   5         },
            {"1 Peter",  5        },
            {"2 Peter",  3        },
            {"1 John",  5         },
            {"2 John",  1         },
            {"3 John",  1         },
            {"Jude",    1         },
            {"Revelation",  22    }
        };
        
        public Dictionary<int, String> BookList = new Dictionary<int, String>()
        {
            {1, "Genesis"          },
            {2, "Exodus"           },
            {3, "Leviticus"        },
            {4, "Numbers"          },
            {5, "Deuteronomy"      },
            {6, "Joshua"           },
            {7, "Judges"           },
            {8, "Ruth"             },
            {9, "1 Samuel"         },
            {10, "2 Samuel"        },
            {11, "1 Kings"         },
            {12, "2 Kings"         },
            {13, "1 Chronicles"    },
            {14, "2 Chronicles"    },
            {15, "Ezra"            },
            {16, "Nehemiah"        },
            {17, "Esther"          },
            {18, "Job"             },
            {19, "Psalms"          },
            {20, "Proverbs"        },
            {21, "Ecclesiastes"    },
            {22, "Song of Solomon" },
            {23, "Isaiah"          },
            {24, "Jeremiah"        },
            {25, "Lamentations"    },
            {26, "Ezekiel"         },
            {27, "Daniel"          },
            {28, "Hosea"           },
            {29, "Joel"            },
            {30, "Amos"            },
            {31, "Obadiah"         },
            {32, "Jonah"           },
            {33, "Micah"           },
            {34, "Nahum"           },
            {35, "Habakkuk"        },
            {36, "Zephaniah"       },
            {37, "Haggai"          },
            {38, "Zechariah"       },
            {39, "Malachi"         },
            {40, "Matthew"         },
            {41, "Mark"            },
            {42, "Luke"            },
            {43, "John"            },
            {44, "The Acts"        },
            {45, "Romans"          },
            {46, "1 Corinthians"   },
            {47, "2 Corinthians"   },
            {48, "Galatians"       },
            {49, "Ephesians"       },
            {50, "Philippians"     },
            {51, "Colossians"      },
            {52, "1 Thessalonians" },
            {53, "2 Thessalonians" },
            {54, "1 Timothy"       },
            {55, "2 Timothy"       },
            {56, "Titus"           },
            {57, "Philemon"        },
            {58, "Hebrews"         },
            {59, "James"           },
            {60, "1 Peter"         },
            {61, "2 Peter"         },
            {62, "1 John"          },
            {63, "2 John"          },
            {64, "3 John"          },
            {65, "Jude"            },
            {66, "Revelation"      }
        };

    }
}
