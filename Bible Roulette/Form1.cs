using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int MaxChapters, MaxVerses;

            if (BookList.TryGetValue( BookNumber, out BookName ) )
            {

                MaxChapters = NumberOfChaptersInBook[BookName];

                if (ChapterNumber <= MaxChapters)
                {
                    //TODO check the amount of verses in a given chapter
                    isReal = true;
                }

            }

            if ((BookNumber == 0) || (VerseNumber == 0)) { isReal = false; }

            if (isReal)
            {
                spinBtn.BackColor = Color.Green;
                MessageBox.Show(BookName + " " + ChapterNumber.ToString() + ":" + VerseNumber.ToString() + "?");
            }
            else
            {
                spinBtn.BackColor = Color.Red;
            }

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            CeroBoxes();
        }

        private void spinBtn_Click(object sender, EventArgs e)
        {
            RandomizeBox();
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
