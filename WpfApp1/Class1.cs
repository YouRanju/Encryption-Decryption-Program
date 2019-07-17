using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aaa
{
    class Class1
    {
        public static char[][] alphabetBoard = new char[5][];
        public static bool oddFlag = false;
        public static String zCheck = "";

        static void Main(String[] arg)
        {
            String decryption = "";
            String encryption = "";

            String key = "";
            Console.WriteLine("암호화에 쓰일 키를 입력하세요 : ");
            key = Console.ReadLine();

            String str = "";
            Console.WriteLine("암호화할 문자열을 입력하세요 : ");
            str = Console.ReadLine();

            String blankCheck = "";
            int blankCheckCount = 0;

            setBoard(key);

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    str = str.Substring(0, i) + str.Substring(i + 1, str.Length);
                    blankCheck += 10;
                }
                else
                {
                    blankCheck += 0;
                }
                if (str[i] == 'z')
                {
                    str = str.Substring(0, i) + 'q' + str.Substring(i + 1, str.Length);
                    zCheck += 1;
                }
                else
                {
                    zCheck += 0;
                }
            }

            encryption = strEncryption(key, str);

            Console.WriteLine("암호화된 문자열 : " + encryption);

            for (int i = 0; i < encryption.Length; i++)
            {
                if (encryption[i] == ' ') //공백제거
                    encryption = encryption.Substring(0, i) + encryption.Substring(i + 1, encryption.Length);
            }

            decryption = strDecryption(key, encryption, zCheck);

            for (int i = 0; i < decryption.Length; i++)
            {
                if (blankCheck[i] == '1')
                {
                    decryption = decryption.Substring(0, i) + " " + decryption.Substring(i, decryption.Length);
                }
            }

            Console.WriteLine("복호화된 문자열 : " + decryption);
        }
/// /////////////////////////////////////////////////////////////////////////////////////////////////////
        private static String strDecryption(String key, String str, String zCheck)
        {
            ArrayList playFair = new ArrayList();
            ArrayList decPlayFair = new ArrayList();
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            String decStr = "";

            int lengthOddFlag = 1;

            for (int i = 0; i < str.Length; i += 2)
            {
                char[] tmpArr = new char[2];
                tmpArr[0] = str[i];
                tmpArr[1] = str[i + 1];
                playFair.Add(tmpArr);
            }

            for (int i = 0; i < playFair.Count; i++)
            {

                char[] tmpArr = new char[2];
                for (int j = 0; j < alphabetBoard.Length; j++)
                {
                    for (int k = 0; k < alphabetBoard[j].Length; k++)
                    {
                        if (alphabetBoard[j][k] == playFair[i].ToString()[0])
                        {
                            x1 = j;
                            y1 = k;
                        }
                        if (alphabetBoard[j][k] == playFair[i].ToString()[1])
                        {
                            x2 = j;
                            y2 = k;
                        }
                    }
                }

                if (x1 == x2) //행이 같은 경우 각각 바로 아래열 대입
                {
                    tmpArr[0] = alphabetBoard[x1][(y1 + 4) % 5];
                    tmpArr[1] = alphabetBoard[x2][(y2 + 4) % 5];
                }
                else if (y1 == y2) //열이 같은 경우 각각 바로 옆 열 대입
                {
                    tmpArr[0] = alphabetBoard[(x1 + 4) % 5][y1];
                    tmpArr[1] = alphabetBoard[(x2 + 4) % 5][y2];
                }
                else //행, 열 다른경우 각자 대각선에 있는 곳.
                {
                    tmpArr[0] = alphabetBoard[x2][y1];
                    tmpArr[1] = alphabetBoard[x1][y2];
                }

                decPlayFair.Add(tmpArr);

            }

            for (int i = 0; i < decPlayFair.Count; i++) //중복 문자열 돌려놓음
            {
                if (i != decPlayFair.Count - 1 && decPlayFair[i].ToString()[1] == 'x'
                        && decPlayFair[i].ToString()[0] == decPlayFair[i + 1].ToString()[0])
                {
                    decStr += decPlayFair[i].ToString()[0];
                }
                else
                {
                    decStr += decPlayFair[i].ToString()[0] + "" + decPlayFair[i].ToString()[1];
                }
            }



            for (int i = 0; i < zCheck.Length; i++)//z위치 찾아서 q로 돌려놓음
            {
                if (zCheck[i] == '1')
                    decStr = decStr.Substring(0, i) + 'z' + decStr.Substring(i + 1, decStr.Length);

            }



            if (oddFlag) decStr = decStr.Substring(0, decStr.Length - 1);


            //띄어쓰기
            for (int i = 0; i < decStr.Length; i++)
            {
                if (i % 2 == lengthOddFlag)
                {
                    decStr = decStr.Substring(0, i + 1) + " " + decStr.Substring(i + 1, decStr.Length);
                    i++;
                    lengthOddFlag = ++lengthOddFlag % 2;
                }
            }

            return decStr;
        }
///////////////////////////////////////////////////////////////////////////////////////////////////
        private static String strEncryption(String key, String str)
        {
            ArrayList playFair = new ArrayList();
            ArrayList encPlayFair = new ArrayList();
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            String encStr = "";

            for (int i = 0; i < str.Length; i += 2) // arraylist 세팅
            {
                char[] tmpArr = new char[2];
                tmpArr[0] = str[i];
                try
                {
                    if (str[i] == str[i + 1]) //글이 반복되면 x추가
                    {
                        tmpArr[1] = 'x';
                        i--;
                    }
                    else
                    {
                        tmpArr[1] = str[i + 1];
                    }
                }
                catch (Exception e)
                {
                    tmpArr[1] = 'x';
                    oddFlag = true;
                }
                playFair.Add(tmpArr);
            }

            for (int i = 0; i < playFair.Count; i++)
            {
                Console.WriteLine(playFair[i].ToString()[0] + "" + playFair[i].ToString()[1] + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < playFair.Count; i++)
            {

                char[] tmpArr = new char[2];
                for (int j = 0; j < alphabetBoard.Length; j++) //쌍자암호의 각각 위치체크
                {
                    for (int k = 0; k < alphabetBoard[j].Length; k++)
                    {
                        if (alphabetBoard[j][k] == playFair[i].ToString()[0])
                        {
                            x1 = j;
                            y1 = k;
                        }
                        if (alphabetBoard[j][k] == playFair[i].ToString()[1])
                        {
                            x2 = j;
                            y2 = k;
                        }
                    }
                }


                if (x1 == x2) //행이 같은경우
                {
                    tmpArr[0] = alphabetBoard[x1][(y1 + 1) % 5];
                    tmpArr[1] = alphabetBoard[x2][(y2 + 1) % 5];
                }
                else if (y1 == y2) //열이 같은 경우
                {
                    tmpArr[0] = alphabetBoard[(x1 + 1) % 5][y1];
                    tmpArr[1] = alphabetBoard[(x2 + 1) % 5][y2];
                }
                else //행, 열 모두 다른경우
                {
                    tmpArr[0] = alphabetBoard[x2][y1];
                    tmpArr[1] = alphabetBoard[x1][y2];
                }

                encPlayFair.Add(tmpArr);

            }

            for (int i = 0; i < encPlayFair.Count; i++)
            {
                encStr += encPlayFair[i].ToString()[0] + "" + encPlayFair[i].ToString()[1] + " ";
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
                    if (key[i] == keyForSet[j])
                    {
                        duplicationFlag = true;
                        break;
                    }
                }
                if (!(duplicationFlag)) keyForSet += key[i];
                duplicationFlag = false;
            }
            //배열에 대입
            for (int i = 0; i < alphabetBoard.Length; i++)
            {
                for (int j = 0; j < alphabetBoard[i].Length; j++)
                {
                    alphabetBoard[i][j] = keyForSet[keyLengthCount++];
                }
            }
            //배열에 대입
            for (int i = 0; i < alphabetBoard.Length; i++)
            {
                for (int j = 0; j < alphabetBoard[i].Length; j++)
                {
                    Console.WriteLine(alphabetBoard[i][j] + "-");
                }
                Console.WriteLine();
            }


        }

    }
}