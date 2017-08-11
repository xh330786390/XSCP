using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using   XSCP.Common.Model;
using System.IO;

namespace   XSCP.Data.Controllers
{
    public class CoreMethod
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="lottery"></param>
        /// <param name="fileName"></param>
        public void ReadFile(DateTime dt, Lottery lottery, string fileName)
        {
            lottery.ClearLotterys();// 清空开奖记录

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    lottery.AddLottery(dt, line);
                }
                sr.Close();
                sr.Dispose();
            }
        }

        /// <summary>
        /// 开奖趋势分析
        /// </summary>
        public void AnalyzeTendency(Lottery lottery, Tendency<Tendency2Model> tendency, int index1, int index2)
        {
            tendency.ClearTendencys();//清空记录
            Tendency2Model tm;

            AnalyzeTendency at = new AnalyzeTendency();
            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new Tendency2Model();
                tm.ID = i + 1;
                tm.Lottery = lottery.Lt_Lotterys[i].Lottery;
                tm.Big = at.BigNum(lottery.Lt_Lotterys, i, index1, index2);//大大
                tm.BigSmall = at.BigSmallNum(lottery.Lt_Lotterys, i, index1, index2);//大小
                tm.SmallBig = at.SmallBigNum(lottery.Lt_Lotterys, i, index1, index2);//小大
                tm.Small = at.SmallNum(lottery.Lt_Lotterys, i, index1, index2);//小小

                tm.Odd = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 1);//奇奇
                tm.OddPair = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 0);//奇偶
                tm.PairOdd = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 1);//偶奇
                tm.Pair = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 0);//偶偶
                tm.Dbl = at.DblNum(lottery.Lt_Lotterys, i, index1, index2);//重数

                tm.Sno = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;

                tendency.AddTendency(tm);//添加趋势记录
            }
        }
    }
}
