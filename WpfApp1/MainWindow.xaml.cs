using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static char[ , ] alphabetBoard = new char[5, 5];
        public static String keywords = "";

        String decryption = "";
        String encryption = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void encryptionb_Click(object sender, RoutedEventArgs e)
        {
            keywords = "";
            outkeyword.Text = "";
            k1.Content = "";
            outputtext.Text = "";


            setBoard(inputkeyword.Text.ToLower());

            for (int i = 0; i < alphabetBoard.GetLength(0); i++)
            {
                for (int j = 0; j < alphabetBoard.GetLength(1); j++)
                {
                    if(j == alphabetBoard.GetLength(1) -1)
                    {
                        if(alphabetBoard[i,j] == 'q' || alphabetBoard[i, j] == 'z')
                        {
                            k1.Content += "q/z";
                        }
                        else
                        {
                            k1.Content += alphabetBoard[i, j] + "";
                        }
                    } else
                    {
                        if (alphabetBoard[i, j] == 'q' || alphabetBoard[i, j] == 'z')
                        {
                            k1.Content += "q/z - ";
                        }
                        else
                        {
                            k1.Content += alphabetBoard[i, j] + " - ";
                        }
                    }
                   
                }
                k1.Content += "\n";
            }

            outkeyword.Text = keywords;

            String key = inputkeyword.Text;
            String str = inputtext.Text;

            str = str.ToLower();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    str = str.Substring(0, i) + str.Substring(i + 1);
                    i -= 1;
                }

                if (!(str[i] >= 'a' && str[i] <= 'z'))
                {
                    str = str.Substring(0, i) + str.Substring(i + 1);
                    i -= 1;
                }
            }

            str = str.Replace('z', 'q');

            encryption = strEncryption(key, str.ToLower());

            outputtext.Text = encryption;
        }
        /// //////////////////////////////////////////////////////////////////////////
        private void decryptionb_Click(object sender, RoutedEventArgs e)
        {
            keywords = "";
            outkeyword.Text = "";
            k1.Content = "";
            outputtext.Text = "";

            setBoard(inputkeyword.Text.ToLower());

            String key = inputkeyword.Text;
            String str = inputtext.Text;

            outkeyword.Text = keywords;

            str = str.ToLower();

            for (int i = 0; i < alphabetBoard.GetLength(0); i++)
            {
                for (int j = 0; j < alphabetBoard.GetLength(1); j++)
                {
                    if (j == alphabetBoard.GetLength(1) - 1)
                    {
                        if (alphabetBoard[i, j] == 'q' || alphabetBoard[i, j] == 'z')
                        {
                            k1.Content += "q/z";
                        }
                        else
                        {
                            k1.Content += alphabetBoard[i, j] + "";
                        }
                    }
                    else
                    {
                        if (alphabetBoard[i, j] == 'q' || alphabetBoard[i, j] == 'z')
                        {
                            k1.Content += "q/z - ";
                        }
                        else
                        {
                            k1.Content += alphabetBoard[i, j] + " - ";
                        }
                    }

                }
                k1.Content += "\n";
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    str = str.Substring(0, i) + str.Substring(i + 1);
                    i -= 1;
                }

                if (!(str[i] >= 'a' && str[i] < 'z'))
                {
                    str = str.Substring(0, i) + str.Substring(i + 1);
                    i -= 1;
                }
            }

            str = str.Replace('z', 'q');

            decryption = strDecryption(key, str.ToLower());

            outputtext.Text = decryption;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < alphabetBoard.GetLength(0); i++) {
                for (int j = 0; j < alphabetBoard.GetLength(1); j++)
                {
                    alphabetBoard[i,j] = ' ';
                }
            }

            keywords = "";
            k1.Content = "";
            inputkeyword.Text = "";
            outkeyword.Text = "";
            inputtext.Text = "";
            outputtext.Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        private static String strDecryption(String key, String str)
        {
            List<List<char>> playFair = new List<List<char>>();
            List<List<char>> decPlayFair = new List<List<char>>();
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            String decStr = "";

            if(str.Length % 2 == 0)
            {
                for (int i = 0; i < str.Length; i += 2)
                {
                    List<char> tmpArr = new List<char>();
                    tmpArr.Add(str[i]);
                    tmpArr.Add(str[i + 1]);
                    playFair.Add(tmpArr);
                }
            } else
            {
                for (int i = 0; i < str.Length-1; i += 2)
                {
                    List<char> tmpArr = new List<char>();
                    tmpArr.Add(str[i]);
                    tmpArr.Add(str[i + 1]);
                    playFair.Add(tmpArr);
                }
            }
            

            for (int i = 0; i < playFair.Count; i++)
            {
                List<char> tmpArr = new List<char>();
                for (int j = 0; j < alphabetBoard.GetLength(0); j++)
                {
                    for (int k = 0; k < alphabetBoard.GetLength(1); k++)
                    {
                        if (alphabetBoard[j, k] == playFair[i][0])
                        {
                            x1 = j;
                            y1 = k;
                        }
                        if (alphabetBoard[j, k] == playFair[i][1])
                        {
                            x2 = j;
                            y2 = k;
                        }
                    }
                }

                if (x1 == x2) //행이 같은 경우 각각 바로 아래열 대입
                {
                    tmpArr.Add(alphabetBoard[x1, (y1 + 4) % 5]);
                    tmpArr.Add(alphabetBoard[x2, (y2 + 4) % 5]);
                }
                else if (y1 == y2) //열이 같은 경우 각각 바로 옆 열 대입
                {
                    tmpArr.Add(alphabetBoard[(x1 + 4) % 5, y1]);
                    tmpArr.Add(alphabetBoard[(x2 + 4) % 5, y2]);
                }
                else //행, 열 다른경우 각자 대각선에 있는 곳.
                {
                    tmpArr.Add(alphabetBoard[x2, y1]);
                    tmpArr.Add(alphabetBoard[x1, y2]);
                }

                decPlayFair.Add(tmpArr);
            }

            for (int i = 0; i < decPlayFair.Count; i++) //중복 문자열 돌려놓음
            {
                if (i != decPlayFair.Count - 1 && decPlayFair[i][1] == 'x'
                        && decPlayFair[i][0] == decPlayFair[i + 1][0])
                {
                    if(decPlayFair[i][0] == 'q')
                    {
                        decStr += "q(z)";
                    } else
                    {
                        decStr += decPlayFair[i][0];
                    }
                }
                else
                {
                    if (decPlayFair[i][0] == 'q')
                    {
                        decStr += "q(z)" + "" + decPlayFair[i][1];
                    }
                    else if (decPlayFair[i][1] == 'q')
                    {
                        decStr += decPlayFair[i][0] + "" + "q(z)";
                    }
                    else
                    {
                        decStr += decPlayFair[i][0] + "" + decPlayFair[i][1];
                    }
                }
            }

            if (decStr[decStr.Length - 1] == 'x')   //끝 x 제거
            {
                decStr = decStr.Substring(0, decStr.Length - 1);
            }

            return decStr;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        private static String strEncryption(String key, String str)
        {
            List<List<char>> playFair = new List<List<char>>();
            List<List<char>> encPlayFair = new List<List<char>>();
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            String encStr = "";

            for (int i = 0; i < str.Length; i += 2) // arraylist 세팅
            {
                List<char> tmpArr = new List<char>();
                tmpArr.Add(str[i]);
                try
                {
                    if (str[i] == str[i + 1]) //글이 반복되면 x추가
                    {
                        tmpArr.Add('x');
                        i--;
                    }
                    else
                    {
                        tmpArr.Add(str[i + 1]);
                    }
                }
                catch (Exception e)
                {
                    tmpArr.Add('x');
                }
                playFair.Add(tmpArr);
            }

            for (int i = 0; i < playFair.Count; i++)
            {

                List<char> tmpArr = new List<char>();

                for (int j = 0; j < alphabetBoard.GetLength(0); j++) //쌍자암호의 각각 위치체크
                {
                    for (int k = 0; k < alphabetBoard.GetLength(1); k++)
                    {
                        if (alphabetBoard[j,k] == playFair[i][0])
                        {
                            x1 = j;
                            y1 = k;
                        }
                        if (alphabetBoard[j, k] == playFair[i][1])
                        {
                            x2 = j;
                            y2 = k;
                        }
                    }
                }

                if (x1 == x2) //행이 같은경우
                {
                    tmpArr.Add(alphabetBoard[x1, (y1 + 1) % 5]);
                    tmpArr.Add(alphabetBoard[x2, (y2 + 1) % 5]);
                }
                else if (y1 == y2) //열이 같은 경우
                {
                    tmpArr.Add(alphabetBoard[(x1 + 1) % 5, y1]);
                    tmpArr.Add(alphabetBoard[(x2 + 1) % 5, y2]);
                }
                else //행, 열 모두 다른경우
                {
                    tmpArr.Add(alphabetBoard[x2, y1]);
                    tmpArr.Add(alphabetBoard[x1, y2]);
                }

                encPlayFair.Add(tmpArr);

            }

            for (int i = 0; i < encPlayFair.Count; i++)
            {

                encStr += encPlayFair[i][0] + "" + encPlayFair[i][1] + " ";
            }

            if (encStr[encStr.Length-1] == ' ')
            {
                encStr = encStr.Remove(encStr.Length - 1);
            }

            return encStr;
        }
        ///////////////////////////////////////////////////////////////////////
        private static void setBoard(String key)
        {
            String keyForSet = "";                  // 중복된 문자가 제거된 문자열을 저장할 문자열.
            bool duplicationFlag = false;        // 문자 중복을 체크하기 위한 flag 변수.
            int keyLengthCount = 0;                 // alphabetBoard에 keyForSet을 넣기 위한 count변수.

            key += "abcdefghijklmnopqrstuvwxyz";    // 키에 모든 알파벳을 추가.


            // 중복처리
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < keyForSet.Length; j++)
                {
                    if (key[i] == keyForSet[j] || (key[i] == 'q' && keyForSet.IndexOf('z') != -1))
                    {
                        duplicationFlag = true;
                        break;
                    }
                }

                if (!(duplicationFlag))
                {
                    if (key.Length - 26 > i)
                    {
                        keywords += key[i];
                    }
                    
                    keyForSet += key[i];
                }
                duplicationFlag = false;
            }

            keyForSet = keyForSet.Replace('z', 'q');

            //배열에 대입
            for (int i = 0; i < alphabetBoard.GetLength(0); i++)
            {
                for (int j = 0; j < alphabetBoard.GetLength(1); j++)
                {
                    alphabetBoard[i, j] = keyForSet[keyLengthCount++];

                }   
            }
        }
    }
}
