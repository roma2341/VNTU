using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicLab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //data
            int Vzv = 39*1000000;//Обсяг виробництва валової продукцiї у звiтному роцi,  грн.
            int Vpl = 4819 * 10000;//Обсяг виробництва валової продукцiї у плановому роцi, грн.
            int Ch_vsih_zvr = 1256; //Середньоспискова чисельнiсть всiх робiтникiв у звiтному роцi, чол.
            double Dpercent = 85.6; // Вiдсоток основних робiтникiв у звiтному роцi, %
            int Chp = 1423; //Очiкувана спискова чисельнiсть  робiтникiв на кiнець звiтного (початок планового) року, чол
            int DТ = 75850; //Зниження нормативної трудомiсткостi основних процесiв виробничої програми у плановому роцi,нормо-год.
            int Fn = 258; // Номiнальний рiчний фонд часу одного робiтника, днi.
            int Fdd = 259;//Реальний рiчний фонд часу одного робiтника,днi.
            int Fdh = 1870;//Реальний рiчний фонд часу одного робiтника,години.
            double DKn = 10; //Плановий коефiцiєнт перевиконання норм виробiтку, %.
            int Chzv_zag = 197; //Звiльнено з пiдприємства за звiтний рiк, чол.
            int Chprin = 55;//Прийнято на пiдприємство за звiтний рiк, чол.
            int Chzv = 180;//Чисельнiсть звiльнених за власним бажанням, за порушення трудової дисциплiни або з iнших причин, не пов’язаних з виробництвом, чол
            //process data
            double Ch_or_zv_r = getCh_or_zvr(Ch_vsih_zvr, Dpercent);
            System.Console.WriteLine("Середньоспискова чисельнiсть основних робiтникiв у звiтному роцi:{0:0.###}",Ch_or_zv_r);

            double ChDop_zvr = getCh_dop_zvr(Ch_vsih_zvr, Ch_or_zv_r);//середньоспискова чисельнiсть допомiжних робiтникiв у звiтному роцi, чол
            System.Console.WriteLine("середньоспискова чисельнiсть допомiжних робiтникiв у звiтному роцi, чол:{0:0.###}", ChDop_zvr);
      


           double Kn = getKn(DKn);//Плановий коефiцiєнт виконання норм виробiтку
            System.Console.WriteLine("Плановий коефiцiєнт виконання норм виробiтку:{0:0.###}", Kn);
            double DN_or = getDNor(DТ, Fdh, Kn);// умовна економiя чисельностi робiтникiв за рахунок зниження нормативної трудомiсткостi основних процесiв виробничої програми, чол.
            System.Console.WriteLine("умовна економiя чисельностi робiтникiв за рахунок зниження нормативної трудомiсткостi основних процесiв виробничої програми:{0:0.###}",DN_or);
            double Ch_or_pl_r = getCh_or_plr(Ch_or_zv_r, DN_or); // серед­ньоспискова кiлькiсть основних i допомiжних робiтникiв на пiдприємствi у плановому роцi
            System.Console.WriteLine("серед­ньоспискова кiлькiсть основних робiтникiв на пiдприємствi у плановому роцi:{0:0.###}",Ch_or_pl_r);
            double Ch_dop_pl_r = getCh_dop_plr(ChDop_zvr, DN_or);
            System.Console.WriteLine("серед­ньоспискова кiлькiсть додаткових робiтникiв на пiдприємствi у плановому роцi:{0:0.###}",Ch_dop_pl_r);
          double Ksp = getKsp(Fn, Fdd);//коефiцiєнт списковостi, який показує вiдсоток працюючих, якi не зявилися на роботу
            System.Console.WriteLine("коефiцiєнт списковостi:" + Ksp);
            double ChYav_or_plr = getChYav_or_plr(Ch_or_pl_r, Ksp);
            System.Console.WriteLine("Середньявочна чисельнiсть основних робiтникiв у плановому роцi:{0:0.###}", ChYav_or_plr);
            double Ch_vsih_plr = getChVsih_plr(Ch_or_pl_r, Ch_dop_pl_r);
            System.Console.WriteLine("Середньоспискова чисельнiсть всiх робiтникiв у плановому роцi:{0:0.###}",Ch_vsih_plr);
            double Chk = getChK(Ch_vsih_plr, Chp);
            System.Console.WriteLine("очiкувана спискова чисельнiсть  робiтникiв на кiнець планового року, чол.:{0:0.###}",Chk);
            double DCh_naim = getChNaim(Chk, Chp);
            System.Console.WriteLine(" Чисельнiсть додаткового найму (чи звiльнення) робiтникiв у плановому роцi:{0:0.###}",DCh_naim);


            double pp_sr_zv_r = getPPsr_zvr(Vzv, Ch_or_zv_r);
            System.Console.WriteLine("Середня продуктивнiсть працi основних робiтникiв у звiтному роцi у вартiсному вираженнi розраховується за такою формулою, грн:{0:0.###}",pp_sr_zv_r);

            double pp_vsih_zv_r = getPPvsih_zvr(Vzv, Ch_vsih_zvr);
            System.Console.WriteLine("Середня продуктивнiсть працi всiх робiтникiв у звiтному роцi, у вартiсному вираженнi розраховується за такою формулою, грн/ чол:{0:0.###}",pp_vsih_zv_r);
            //end
            double pp_sr_pl_r = getPPsr_plr(Vpl, Ch_vsih_plr);
            System.Console.WriteLine("середня продуктивнiсть  працi основних робiтникiв у плановому роцi, у вартiсному вираженнi грн\\чол.:{0:0.###} ",pp_sr_pl_r);
            double pp_vsih_plr = getPPvsih_plr(Vpl, Ch_vsih_plr);
            System.Console.WriteLine("Середня продуктивнiсть працi всiх робiтникiв у плановому роцi, у вартiсному вираженнi розраховується за такою формулою, грн/ чол:{0:0.###}",pp_vsih_plr);
            double DPPor = getDPPor(pp_sr_pl_r, pp_sr_zv_r);
            System.Console.WriteLine(" Змiна продуктивностi працi основних робiтникiв у плановому роцi порiвняно зi звiтним розраховується за формулою, грн/ чол:{0:0.###}", DPPor);
            double DPP_vsih = getDPPvsih(pp_vsih_plr, pp_vsih_zv_r);
            System.Console.WriteLine(" Змiна продуктивностi працi всiх робiтникiв у плановому роцi порiвняно зi звiтним грн/ чол:{0:0.###}",DPP_vsih);
            double DVchis = getDVchis(pp_vsih_zv_r, Ch_vsih_plr, Ch_vsih_zvr);
            System.Console.WriteLine("Додатковий прирiст чи зниження обсягу виробництва за рахунок змiни чисельностi:{0:0.###}", DVchis);
            double DVpp = getDVpp(Ch_vsih_plr, pp_vsih_plr, pp_vsih_zv_r);
            System.Console.WriteLine("одатковий прирiст чи зниження обсягу виробництва за рахунок змiни продуктивностi працi:{0:0.###}",DVpp);
            double DVchis_and_pp = getDVpp_and_chis(DVchis, DVpp);
            System.Console.WriteLine("Додатковий прирiст обсягу виробництва за рахунок змiни чисельностi i продуктивностi працi :{0:0.###}",DVchis_and_pp);
            double ChOr_ppzvr = getChOr_ppzvr(Vpl, pp_sr_zv_r);

            double Eor_pp = getEor_pp(ChOr_ppzvr, Ch_or_pl_r);
            Console.WriteLine("умовна економiя чисельностi основних робiтникiв за рахунок змiни продуктивностi працi:{0:0.###}", Eor_pp);
            double Eor_nn = getEor_пп(Vpl, pp_sr_zv_r, Ch_or_zv_r);
            Console.WriteLine("Умовна економiя чисельностi основних робiтникiв за рахунок змiни обсягу виробництва:{0:0.###}", Eor_nn);
            double E_dop_pp = getEdop_pp(ChDop_zvr, Vpl, Vzv, Ch_dop_pl_r);
            Console.WriteLine("Умовна економiя чисельностi допомiжних робiтникiв за рахунок росту обсягу виробництва:{0:0.###}", E_dop_pp);
            double Kop = getKop(Chp, Ch_vsih_zvr);
            Console.WriteLine("Оборот робочої сили за прийомом:{0:0.###}" ,Kop);
            double Koz = getKoz(Chzv_zag, Ch_vsih_zvr);
            Console.WriteLine("Оборот робочої сили за звiльненням:{0:0.###}", Koz);
            double Kpl = getKpl(Chzv, Ch_vsih_zvr);
            Console.WriteLine("Коефiцiєнт плинностi:{0:0.###}", Kpl);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }
        /// <summary>
        /// середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол.
        /// </summary>
        /// <param name="ch_vsih_zvr">середньоспискова чисельнiсть всiх робiтникiв у звiтному роцi, чол.</param>
        /// <param name="Dpercent">вiдсоток основних робiтникiв у звiтному роцi, %.</param>
        /// <returns></returns>
        static double getCh_or_zvr(double ch_vsih_zvr,double Dpercent)
        {
            double result = ch_vsih_zvr * Dpercent/100;
            Console.WriteLine(string.Format("Чо.р_зв.р=Чвсiх_зв.р*^%/100%={0:0.###}*{1:0.###}/100%={2:0.###} чоловiк",
                ch_vsih_zvr, Dpercent, result));
            return result;
        }
        /// <summary>
        /// середньоспискова чисельнiсть допомiжних робiтникiв у звiтному роцi, чол.
        /// </summary>
        /// <param name="ch_vsih_zvr">середньоспискова чисельнiсть всiх робiтникiв у звiтному роцi, чол.</param>
        /// <param name="ch_or_zvr">середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол.</param>
        /// <returns></returns>
        static double getCh_dop_zvr(double ch_vsih_zvr, double ch_or_zvr)
        {
            double result = ch_vsih_zvr - ch_or_zvr;
            Console.WriteLine(string.Format("Чдоп_зв.р=Чвсiх_зв.р-Чо.р_зв.р={0:0.###}-{1:0.###}={2:0.###} чоловiк", ch_vsih_zvr, ch_or_zvr, result));
            return result;
        }
    
        /// <summary>
        /// серед­ньоспискова кiлькiсть основних i допомiжних робiтникiв на пiдприємствi у плановому роцi
        /// </summary>
        /// <param name="ch_or_zvr">середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол.</param>
        /// <param name="deltaNo_or">умовна економiя чисельностi робiтникiв за рахунок зниження нормативної трудомiсткостi основних процесiв виробничої програми, чол</param>
        /// <returns></returns>
        static double getCh_or_plr(double ch_or_zvr,double deltaNo_or)
        {
            double result = ch_or_zvr - deltaNo_or;
            Console.WriteLine(string.Format("Чо.р_пл.р=Чо.р_зв.р-DNо.р={0:0.###}-{1:0.###}={2:0.###} чоловiк", ch_or_zvr, deltaNo_or, result));
            return result;
        }
        static double getCh_dop_plr(double ch_dop_zvr, double deltaNo_or)
        {
            double result = ch_dop_zvr - deltaNo_or;
            Console.WriteLine(string.Format("Чдоп.р_пл.р=Чдоп.р_зв.р-DNо.р={0:0.###}-{1:0.###}={2:0.###} чоловiк", ch_dop_zvr, deltaNo_or, result));
            return result;
        }
        /// <summary>
        /// умовна економiя чисельностi робiтникiв за рахунок зниження нормативної трудомiсткостi основних процесiв виробничої програми, чол.
        /// </summary>
        /// <param name="DT">зниження нормативної трудомiсткостi основних процесiв виробничої програми у плановому роцi, нормо-год.</param>
        /// <param name="Fd">реальний рiчний фонд часу одного робiтника, год.</param>
        /// <param name="Kn">плановий коефiцiєнт виконання норм виробiтку</param>
        /// <returns></returns>
        static double getDNor(int DT,double Fd, double Kn)
        {
            double result = DT / (Fd * Kn);
            Console.WriteLine(string.Format("DNo.r=DT/(Фд*Кн)={0:0.###}/({1:0.###}*{2:0.###})={3:0.##} чоловiк", DT, Fd, Kn, result));
            return result;
        }
        /// <summary>
        /// плановий коефiцiєнт виконання норм виробiтку
        /// </summary>
        /// <param name="DKn">коефiцiєнт перевиконання норм виробiтку, %</param>
        /// <returns></returns>
        static double getKn(double DKn)
        {
            double result = 1 + DKn / 100;
            Console.WriteLine(string.Format("Kн=1+DKн/100%=1+{0:0.###}/100%={1:0.##}", DKn, result));
            return result;
        }
        /// <summary>
        /// Середньявочна чисельнiсть основних робiтникiв у плановому роцi
        /// </summary>
        /// <param name="ch_or_plr">середньоспискова чисельнiсть основних робiтникiв у плановому роцi, чол</param>
        /// <param name="Ksp"> коефiцiєнт списковостi, який показує вiдсоток працюючих, якi не зявилися на роботу</param>
        /// <returns></returns>
        static double getChYav_or_plr(double ch_or_plr,double Ksp)
        {
            double result = ch_or_plr / Ksp;
            Console.WriteLine(string.Format("Чяв.с.р_пл.р=Чо.р_пл.р/Ксп={0:0.###}/{1:0.###}={2:0.##}", ch_or_plr, Ksp, result));
            return result;
        }
        /// <summary>
        /// коефiцiєнт списковостi, який показує вiдсоток працюючих, якi не зявилися на роботу
        /// </summary>
        /// <param name="Fn">номiнальний рiчний фонд часу одного робiтника, днi.</param>
        /// <param name="Fd">реальний рiчний фонд часу одного робiтника, днi</param>
        /// <returns></returns>
        static double getKsp(double Fn,double Fd)
        {
            double result = Fn / Fd;
            Console.WriteLine(string.Format("Ксп=Фн/Фд={0:0.###}/{1:0.###}={2:0.##}", Fn, Fd, result));
            return result;
        }
        /// <summary>
        /// Середньявочна чисельнiсть основних робiтникiв у звiтному роцi
        /// </summary>
        /// <param name="ch_or_zvr">середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол.</param>
        /// <param name="Ksp">коефiцiєнт списковостi, який показує вiдсоток працюючих, якi не зявилися на роботу</param>
        /// <returns></returns>
        static double getChYav_or_zvr(int ch_or_zvr, double Ksp)
        {
            double result = ch_or_zvr / Ksp;
            Console.WriteLine(string.Format("Чяв.о.р_зв.р=Чо.р_зв.р/Kсп={0:0.###}/{1:0.###}={2:0.##}", ch_or_zvr, Ksp, result));
            return result;
        }
        /// <summary>
        /// Середньоспискова чисельнiсть всiх робiтникiв у плановому роцi
        /// </summary>
        /// <param name="chOr_plr">еред­ньоспискова кiлькiсть основних i допомiжних робiтникiв на пiдприємствi у плановому роцi</param>
        /// <param name="chDop_plr"> середньоспискова чисельнiсть всiх робiтникiв у плановому роцi, чол.</param>
        /// <returns></returns>
        static double getChVsih_plr(double chOr_plr, double chDop_plr)
        {
            double result = chOr_plr + chDop_plr;
            Console.WriteLine(string.Format("Чвсiх_пл.р=Чо.р_пл.р+Чдоп_пл.р={0:0.###}+{1:0.###}={2:0.###}", chOr_plr, chDop_plr, result));
            return result;
        }
        /// <summary>
        /// Чисельнiсть додаткового найму (чи звiльнення) робiтникiв у плановому роцi
        /// </summary>
        /// <param name="Chk">очiкувана спискова чисельнiсть  робiтникiв на кiнець планового року, чол.</param>
        /// <param name="Chp">очiкувана спискова чисельнiсть  робiтникiв на початок планового року, чол.</param>
        /// <returns></returns>
        static double getChNaim(double Chk, double Chp)
        {
            double result = Chk - Chp;
            Console.WriteLine(string.Format("DЧнайм=Чк-Чп={0:0.###}-{1:0.###}={2:0.###}", Chk, Chp, result));
            return result;
        }
        /// <summary>
        /// очiкувана спискова чисельнiсть  робiтникiв на кiнець планового року, чол.
        /// </summary>
        /// <param name="ch_vsih_plr">Середньоспискова чисельнiсть всiх робiтникiв у плановому роцi</param>
        /// <param name="chP">очiкувана спискова чисельнiсть  робiтникiв на початок планового року, чол</param>
        /// <returns></returns>
        static double getChK(double ch_vsih_plr,double chP)
        {
            double result = 2 * ch_vsih_plr - chP;
            Console.WriteLine(string.Format("Чк=2*Чвсiх_плр-Чп=2*{0:0.###}-{1:0.###}={2:0.###}", ch_vsih_plr, chP, result));
            return result;
        }
        /// <summary>
        /// Середня продуктивнiсть працi основних робiтникiв у звiтному роцi у вартiсному вираженнi розраховується за такою формулою, грн/ чол.
        /// </summary>
        /// <param name="Vzv">обсяг виробництва валової продукцiї у звiтному роцi, грн</param>
        /// <param name="Ch_sr_zvr">середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол. </param>
        /// <returns></returns>
        static double getPPsr_zvr(double Vzv,double Ch_sr_zvr)
        {
            double result = Vzv / Ch_sr_zvr;
            Console.WriteLine(string.Format("ППс.р_зв.р=Vзв/Чс.р._вз.р={0:0.###}/{1:0.###}={2:0.###} грн/чол.", Vzv, Ch_sr_zvr, result));
            return result;
        }
        /// <summary>
        /// Середня продуктивнiсть працi всiх робiтникiв у звiтному роцi, у вартiсному вираженнi розраховується за такою формулою, грн/ чол
        /// </summary>
        /// <param name="Vzv">обсяг виробництва валової продукцiї у звiтному роцi, грн</param>
        /// <param name="Vvsih_zvr">середньоспискова чисельнiсть всiх робiтникiв у звiтному роцi, чол</param>
        /// <returns></returns>
        static double getPPvsih_zvr(double Vzv, double Vvsih_zvr)
        {
            double result = Vzv / Vvsih_zvr;
            Console.WriteLine(string.Format("ППвсiх.зв.р.=Vзв/Чвсiх.зв.р={0:0.###}/{1:0.###}={2:0.###} грн/чол.", Vzv, Vvsih_zvr, result));
            return result;
        }
        /// <summary>
        /// середня продуктивнiсть  працi основних робiтникiв у плановому роцi, у вартiсному вираженнi 
        /// </summary>
        /// <param name="Vpl">обсяг виробництва валової продукцiї у плановому роцi,грн</param>
        /// <param name="Ch_sr_plr">ередньоспискова чисельнiсть основних робiтникiв у плановому роцi, чол.</param>
        /// <returns></returns>
        static double getPPsr_plr(double Vpl, double Ch_sr_plr)
        {
            double result = Vpl / Ch_sr_plr;
            Console.WriteLine(string.Format("ППс.р_пл.р=Vзв/Чс.р._пл.р={0:0.###}/{1:0.###}={2:0.###} грн/чол.", Vpl, Ch_sr_plr, result));
            return result;
        }
        /// <summary>
        /// Середня продуктивнiсть працi всiх робiтникiв у плановому роцi, у вартiсному вираженнi розраховується за такою формулою, грн/ чол.:
        /// </summary>
        /// <param name="Vpl">обсяг виробництва валової продукцiї у плановому роцi,грн</param>
        /// <param name="Vvsih_plr">середньоспискова чисельнiсть всiх робiтникiв у плановому роцi, чол</param>
        /// <returns></returns>
        static double getPPvsih_plr(double Vpl, double Vvsih_plr)
        {
            double result = Vpl / Vvsih_plr;
            Console.WriteLine(string.Format("ППвсiх.пл.р.=Vзв/Чвсiх.пл.р={0:0.###}/{1:0.###}={2:0.###} грн/чол.", Vpl, Vvsih_plr, result));
            return result;
        }
        /// <summary>
        /// Змiна продуктивностi працi основних робiтникiв у плановому роцi порiвняно зi звiтним розраховується за формулою, грн/ чол.
        /// </summary>
        /// <param name="DDor_plr">середня продуктивнiсть  працi основних робiтникiв у плановому роцi, грн/ чол.</param>
        /// <param name="DDor_zvr">середня продуктивнiсть працi основних робiтникiв у звiтному роцi  грн/ чол. </param>
        /// <returns></returns>
        static double getDPPor(double DDor_plr,double DDor_zvr)
        {
            double result = (DDor_plr - DDor_zvr) / DDor_zvr * 100;
            Console.WriteLine(string.Format("DППо.р=(ППс.р_пл.р-ПП.с.р_зв.р)/ППо.р_зв.р=({0:0.###}-{1:0.###})/{1:0.###}={2:0.###} грн/чол.", DDor_plr, DDor_zvr, result));
            return result;
        }
        /// <summary>
        /// Змiна продуктивностi працi всiх робiтникiв у плановому роцi порiвняно зi звiтним розраховується за формулою,  грн/ чол.
        /// </summary>
        /// <param name="DDvsih_plr">середня продуктивнiсть працi всiх робiтникiв у плановому роцi,грн/ чол.</param>
        /// <param name="DDvsih_zvr">середня продуктивнiсть працi всiх робiтникiв у звiтному роцi, грн/ чол.</param>
        /// <returns></returns>
        static double getDPPvsih(double DDvsih_plr, double DDvsih_zvr)
        {
            double result = (DDvsih_plr - DDvsih_zvr) / DDvsih_zvr * 100;
            Console.WriteLine(string.Format("DППвсiх=(ППвсiх_пл.р-ППвсiх_зв.р)/ППвсiх_зв.р=({0:0.###}-{1:0.###})/{1:0.###}={2:0.###} грн/чол.", DDvsih_plr, DDvsih_zvr, result));
            return result;
        }
        /// <summary>
        /// Додатковий прирiст чи зниження обсягу виробництва за рахунок змiни чисельностi
        /// </summary>
        /// <param name="PPvsih_zvr"> середня продуктивнiсть працi всiх робiтникiв у звiтному роцi,грн/ чол.</param>
        /// <param name="Ch_vsih_plr">ередньоспискова чисельнiсть всiх робiтникiв у плановому роцi, чол</param>
        /// <param name="Ch_vsih_zvr">середньоспискова чисельнiсть всiх робiтникiв у звiтному роцi, чол</param>
        /// <returns></returns>
        static double getDVchis (double PPvsih_zvr, double Ch_vsih_plr,double Ch_vsih_zvr)
        {
            double result = PPvsih_zvr * (Ch_vsih_plr - Ch_vsih_zvr);
            Console.WriteLine(string.Format("DVчис.=ППвсiх_зв.р*(Чвсiх_пл.р-Чвсiх_зв.р)={0:0.###}*({1:0.###}-{2:0.###})={3:0.###}", PPvsih_zvr, Ch_vsih_plr, Ch_vsih_zvr, result));
            return result;
        }
        /// <summary>
        /// Додатковий прирiст чи зниження обсягу виробництва за рахунок змiни продуктивностi працi 
        /// </summary>
        /// <param name="Chvsih_plr">середньоспискова чисельнiсть всiх робiтникiв у плановому роцi, чол. </param>
        /// <param name="PP_vsih_plr">середня продуктивнiсть працi всiх робiтникiв у плановому роцi,  грн/ чол.</param>
        /// <param name="PP_vsih_zvr">середня продуктивнiсть працi всiх робiтникiв у звiтному роцi, грн/ чол.</param>
        /// <returns></returns>
        static double getDVpp(double Chvsih_plr, double PP_vsih_plr, double PP_vsih_zvr)
        {
            double result = Chvsih_plr * (PP_vsih_plr - PP_vsih_zvr);
            Console.WriteLine(string.Format("DVпп=Чвсiх_пл.р*(ППвсiх_пл.р-ППвсiх_зв.р)={0:0.###}*({1:0.###}-{2:0.###})={3:0.###}", Chvsih_plr, PP_vsih_plr, PP_vsih_zvr, result));
            return result;
        }
        /// <summary>
        /// Додатковий прирiст обсягу виробництва за рахунок змiни чисельностi i продуктивностi
        /// </summary>
        /// <param name="DVchis">Додатковий прирiст чи зниження обсягу виробництва за рахунок змiни чисельностi </param>
        /// <param name="DVpp">Додатковий прирiст чи зниження обсягу виробництва за рахунок змiни продуктивностi працi </param>
        /// <returns></returns>
        static double getDVpp_and_chis(double DVchis,double DVpp)
        {
            double result = DVchis + DVpp;
            Console.WriteLine(string.Format("DVпп+чис=DVчис+DVпп={0:0.###}+{1:0.###}={2:0.###}", DVchis, DVpp, result));
            return result;
        }
        /// <summary>
        /// умовна економiя чисельностi основних робiтникiв за рахунок змiни продуктивностi працi
        /// </summary>
        /// <param name="Ch_or_pp_zvr"></param>
        /// <param name="Ch_zv_plr">серед­ньоспискова кiлькiсть основних i допомiжних робiтникiв на пiдприємствi у плановому роцi</param>
        /// <returns></returns>
        static double getEor_pp(double Ch_or_pp_zvr,double Ch_zv_plr)
        {
            double result = Ch_or_pp_zvr - Ch_zv_plr;
            Console.WriteLine(string.Format("Eо.р_пп=Чо.р_пп.зв.р-Чо.р_пл.р={0:0.###}-{1:0.###}={2:0.###}", Ch_or_pp_zvr, Ch_zv_plr, result));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vpl">обсяг виробництва валової продукцiї у плановому роцi,грн</param>
        /// <param name="PPor_zvr">середня продуктивнiсть працi основних робiтникiв у звiтному роцi грн/ чол.</param>
        /// <returns></returns>
        static double getChOr_ppzvr(double Vpl,double PPor_zvr)
        {
            double result = Vpl / PPor_zvr;
            Console.WriteLine(string.Format("Чс.р_пп.вз.р=Vпл/ППо.р_зв.р={0:0.###}/{1:0.###}={2:0.###}", Vpl, PPor_zvr, result));
            return result;
        }
        /// <summary>
        /// Умовна економiя чисельностi основних робiтникiв за рахунок змiни обсягу виробництва
        /// </summary>
        /// <param name="Vpl">обсяг виробництва валової продукцiї у плановому роцi,грн</param>
        /// <param name="PPor_zvr">середня продуктивнiсть працi основних робiтникiв у звiтному роцi грн/ чол.</param>
        /// <param name="Chor_zvr">середньоспискова чисельнiсть основних робiтникiв у звiтному роцi, чол.</param>
        /// <returns></returns>
        static double getEor_пп(double Vpl,double PPsr_zvr,double Chor_zvr)
        {
            double result = Vpl / PPsr_zvr - Chor_zvr;
            Console.WriteLine(string.Format("Ео.р_пп=Vпл/ППор_зв.р-Чор_зв.р={0:0.###}/{1:0.###}-{2:0.###}={3:0.###}", Vpl, PPsr_zvr, Chor_zvr, result));
            return result;
        }
        /// <summary>
        /// Умовна економiя чисельностi допомiжних робiтникiв за рахунок росту обсягу виробництва
        /// </summary>
        /// <param name="ChDop_zvr">середньоспискова чисельнiсть допомiжних робiтникiв у звiтному роцi, чол.</param>
        /// <param name="Vpl">обсяг виробництва валової продукцiї у плановому роцi,грн</param>
        /// <param name="Vzv">обсяг виробництва валової продукцiї у звiтному роцi,грн</param>
        /// <param name="ChDop_plr">середньоспискова чисельнiсть допомiжних робiтникiв у плановому роцi</param>
        /// <returns></returns>
        static double getEdop_pp(double ChDop_zvr,double Vpl, double Vzv, double ChDop_plr)
        {
            double result = ChDop_zvr * Vpl / Vzv - ChDop_plr;
            Console.WriteLine(string.Format("Едоп_пп=Чдоп_зв.р*Vпл/Vзв-Чдоп_пл.р={0:0.###}*{1:0.###}/{2:0.###}-{3:0.###}={4:0.###}", ChDop_zvr, Vpl, Vzv, ChDop_plr, result));
            return result;
        }
        /// <summary>
        /// Оборот робочої сили за прийомом
        /// </summary>
        /// <param name="Chp">исельнiсть прийнятих на роботу за вiдповiдний перiод, чол</param>
        /// <param name="Chvsih_zvr">середньоспискова чисельнiсть працiвникiв у цьо­му ж перiодi, чол.</param>
        /// <returns></returns>
        static double getKop(double Chp,double Chvsih_zvr)
        {
            double result = Chp / Chvsih_zvr;
            Console.WriteLine(string.Format("Коп=Чп/Чвсiх_зв.р={0:0.###}/{1:0.###}={2:0.###}", Chp, Chvsih_zvr, result));
            return result;
        }
        /// <summary>
        /// Оборот робочої сили за звiльненням
        /// </summary>
        /// <param name="Chzv_zag">загальна чисельнiсть звiльнених за вiдповiдний перiод з будь-яких причин, чол</param>
        /// <param name="Chvsih_zvr">середньоспискова чисельнiсть працiвникiв у цьо­му ж перiодi, чол.</param>
        /// <returns></returns>
        static double getKoz(double Chzv_zag,double Chvsih_zvr)
        {
            double result = Chzv_zag / Chvsih_zvr;
            Console.WriteLine(string.Format("Коз=Чзв.заг/Чвсiх_зв.р={0:0.###}/{1:0.###}={2:0.###}", Chzv_zag, Chvsih_zvr, result));
            return result;
        }
        /// <summary>
        /// Коефiцiєнт плинностi 
        /// </summary>
        /// <param name="Chzv">чисельнiсть звiльнених за власним бажанням, за порушення трудової дисциплiни або з iнших причин, не пов’язаних з виробництвом, чол</param>
        /// <param name="Chvsih_zvr">середньоспискова чисельнiсть працiвникiв у цьо­му ж перiодi, чол.</param>
        /// <returns></returns>
        static double getKpl (double Chzv,double Chvsih_zvr)
        {
            double result = Chzv / Chvsih_zvr;
            Console.WriteLine(string.Format("Кпл = Чзв/Чвсiх_зв.р={0:0.###}/{1:0.###}={2:0.###}", Chzv, Chvsih_zvr, result));
            return result;
        }
    }
}
