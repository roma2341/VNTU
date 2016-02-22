using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            /////INPUT DATA PROCESSING
            char[] characters = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
            double Tpost = 31;//Перiод поставки
            int Tzp = 9; //Тривалiсть зриву поставка
            double[] kM_pl = {0.9,0.83,0.72,0.87}; //Коефiцiєнт використання матерiалу запланований
            double[] kM_fact = { 0.84, 0.86, 0.76, 0.85 };//Коефiцiєнт використання матерiалу фактичний
            double[] qClear = {16,32,22,10};//Чиста маса виробiв (кг)
            double Npl_all = 310000;//Запланований рiчний випуск усiх виробiв за звiтний рiк грн
            double Nfact_all = 373000;//Фактичний рiчний випуск усiх виробiв за звiтний рiк грн
            double Snoz_pl = 54000;//Запланований середньорiчний залишок нормованих оборотних засобiв у звiтному роцi грн
            double Snoz_fact = 45800;//Фактичний середньорiчний залишок нормованих оборотних засобiв у звiтному роцi грн
            double[] Tc = { 10, 22, 16, 13 }; //Тривалiсть циклу виготовлення виробiв, днi
            double[] Co = { 56000, 29000, 26000, 21000 };//Сума одноразових витрат для виготовлення партiї виробiв, грн
            double Bm = 392;
            int[] Nfact_percents = { 29, 16, 24, 31 };
            int[] Npl_percents = { 30, 15, 23, 32 };
            double[] Nfact = new double[Nfact_percents.Length];//Фактичний рiчний випуск виробiв
            double[] Npl = new double[Npl_percents.Length]; //Запланований рiчний випуск виробiв
            int[] Ci_percent = {78,80,73,75};
            double[] costs = { 25, 30, 18, 45 };
            double[] Ci = new double[Ci_percent.Length];//Собiвартiсть
            for (int i = 0; i < Ci_percent.Length; i++)
            {
                Ci[i] = costs[i] * Ci_percent[i] / 100;
            }

            for (int i = 0; i < Nfact.Length; i++)
            {
                Nfact[i] = Nfact_percents[i] * Nfact_all / 100;
            }
            for (int i = 0; i < Nfact.Length; i++)
            {
                Npl[i] = Npl_percents[i] * Npl_all / 100;
            }

            ///////////////RESULT PROCESSING
            //Суарнi потреби пiдприємства в певному матерiалi
          
            double Mpl = getMTotal(Npl, qClear);
            double Mfact = getMTotal(Nfact, qClear);
            Console.WriteLine(String.Format("Сумарнi потреби пiдприємства в певному матерiалi:\nПланова:{0}\nФактична:{1}",Mpl,Mfact));

            //Денна потреба
            double Dplan = getD(Mpl);
            double Dfact = getD(Mfact);
            Console.WriteLine(String.Format("Денна потреба:\nПланова:{0}\nФактична:{1}", Dplan, Dfact));
            //Страховий запас
            double Zstr_fact = getZstr(Dfact, Tzp);
            double Zstr_plan = getZstr(Dplan, Tzp);
            Console.WriteLine(String.Format("Страховий запас:\nПланова:{0}\nФактична:{1}", Zstr_fact, Zstr_plan));
            //Поточний запас
            double Zpot_plan = getZpot(Dplan, Tpost);
            double Zpot_fact = getZpot(Dfact, Tpost);
            Console.WriteLine(String.Format("Поточний запас:\nПланова:{0}\nФактична:{1}", Zpot_plan, Zpot_fact));
            //Середнiй запас
            double Zser_plan = getZser(Zpot_plan, Zstr_plan);
            double Zser_fact = getZser(Zpot_fact, Zstr_fact);
            Console.WriteLine(String.Format("Середнiй запас:\nПланова:{0}\nФактична:{1}", Zser_plan, Zser_fact));
            //Максимальний запас
            double Zmax_plan = getZMax(Zpot_plan, Zstr_plan);
            double Zmax_fact = getZMax(Zpot_fact, Zstr_fact);
            Console.WriteLine(String.Format("Максимальний запас:\nПланова:{0}\nФактична:{1}", Zmax_plan, Zmax_fact));


            //Обсяг реалiзованої продукцiї
            double Qp_fact = getQp(costs, Nfact);
            double Qp_plan = getQp(costs, Npl);
            Console.WriteLine(String.Format("Обсяг реалiзованої продукцiї:\nПланова:{0}\nФактична:{1}", Qp_plan, Qp_fact));

            //Коефiцiєнт оборотностi
            double kOb_fact = getKob(Qp_fact, Snoz_fact);
            double kOb_pl = getKob(Qp_plan, Snoz_pl);
            Console.WriteLine(String.Format("Коефiцiєнт оборотностi:\nПланова:{0}\nФактична:{1}", kOb_pl, kOb_fact));

            //тривалiсть одного обороту
            double tOb_fact = getTob(kOb_fact);
            double tOb_plan = getTob(kOb_pl);
            Console.WriteLine(String.Format("тривалiсть одного обороту:\nПланова:{0}\nФактична:{1}", tOb_plan, tOb_fact));

            //кiлькiсть днiв скорочення перiоду обороту оборотних засобiв, днiв
            double deltaScor = getDeltaScor(tOb_fact, tOb_plan);
            Console.WriteLine(String.Format("кiлькiсть днiв скорочення перiоду обороту оборотних засобiв:{0}", deltaScor));

            // Абсолютне вивiльнення (у грошових одиницях) оборотних засобiв
            double voza = getVOZA(Qp_fact, deltaScor);


            // Вiдносне вивiльнення (у грошових одиницях) оборотних засобiв
            double vozv = getVOZV(Qp_plan, Qp_fact);

            Console.WriteLine(String.Format("вивiльнення оборотних засобiв:\nАбсолютне:{0}\nВiдносне:{1}", voza, vozv));

            // Повна собiвартiсть всiєї партiї продукцiї
            Console.WriteLine("Повна собiвартiсть всiєї партiї продукцiї:");
            double[] Cvp = new double[Ci.Length];
            for (int i = 0; i < Cvp.Length; i++)
            {
                Cvp[i] = getCvp(Co[i], Ci[i]);
                Console.WriteLine(characters[i]+":" + Cvp[i]);
            }


            // сума поточних витрат у собiвартостi всiєї партiї виробiв
            Console.WriteLine("сума поточних витрат у собiвартостi всiєї партiї виробiв:");
            double[] Cp = new double[Co.Length];
            for (int i = 0; i < Cvp.Length; i++)
            {
                Cp[i] = getCp(Cvp[i], Co[i]);
                Console.WriteLine(characters[i] + ":" + Cp[i]);
            }
            //Коефiцiєнт наростання витрат
            Console.WriteLine("сума поточних витрат у собiвартостi всiєї партiї виробiв:");
            double[] knv = new double[Co.Length];
            for (int i = 0; i < knv.Length; i++)
            {
                knv[i] = getKnv(Co[i], Cp[i]);
                Console.WriteLine(characters[i] + ":" + knv[i]);
            }
            // Норма запасу оборотних фондiв у незавершеному виробництвi
            double[] Hnv_pl = new double[Cvp.Length];
            double[] Hnv_fact = new double[Cvp.Length];
            Console.WriteLine("Норма запасу оборотних фондiв у незавершеному виробництвi:");
            for (int i = 0; i < Cvp.Length; i++)
            {
                Hnv_pl[i] = getHnv(Cvp[i], Npl_all, Tc[i], knv[i]);
                Hnv_fact[i] = getHnv(Cvp[i], Nfact_all, Tc[i], knv[i]);
                Console.WriteLine(String.Format("{0}:Планова:{1}\tФактична:{2}",characters[i],
                    Hnv_pl[i], Hnv_fact[i]));
            }
            //коефiцiєнт завантаження
            double kZav_fact = getKzav(Snoz_fact, Qp_fact);
            double kZav_plan = getKzav(Snoz_pl, Qp_plan);
            Console.WriteLine(String.Format("коефiцiєнт завантаження:\nПлановий:{0}\nФактичний:{1}", kZav_plan, kZav_fact));
            //загальна сума матерiальних затрат
            Console.WriteLine("загальна сума матерiальних затрат:");
            double[] Mz = new double[costs.Length];
            for (int i = 0; i < Mz.Length; i++)
            {
                Mz[i] = getMz(Mpl, costs[i]);
                Console.WriteLine(characters[i] + ":" + Mz[i]);
            }
            //матерiалоємнiсть 
            Console.WriteLine("Матерiалоємнiсть:");
            double[] Me_fact = new double[Mz.Length];
            double[] Me_plan = new double[Mz.Length];
            for (int i = 0; i < Mz.Length; i++)
            {
                Me_fact[i] = getMe(Mz[i], Qp_fact);
                Me_plan[i] = getMe(Mz[i], Qp_plan);
                Console.WriteLine(String.Format("{0}:Планова:{1}\tФактична:{2}",characters[i],Me_plan[i], Me_fact[i]));
            }

            //Матерiаловiддача
            Console.WriteLine("Матерiаловiддача:");
            double[] Mv_fact = new double[Mz.Length];
            double[] Mv_plan = new double[Mz.Length];

            for (int i = 0; i < Mz.Length; i++)
            {
                Mv_fact[i] = getMv(Qp_fact, Mz[i]);
                Mv_plan[i] = getMv(Qp_plan, Mz[i]);
                Console.WriteLine(String.Format("{0}:Планова:{1}\tФактична:{2}", characters[i], Mv_plan[i], Mv_fact[i]));
            }

            //Коефiцiєнт використання матерiалiв (не може перевищувати 1)
            Console.WriteLine("Коефiцiєнт використання матерiалiв (не може перевищувати 1):");
            double[] kvik_pl = new double[Npl.Length];
            double[] kvik_fact = new double[Npl.Length];
            for (int i = 0; i < Npl.Length; i++)
            {
                kvik_pl[i] = getKvikM(Npl, qClear[i],Mz[i]);
                kvik_fact[i] = getKvikM(Nfact, qClear[i],Mz[i]);
                Console.WriteLine(String.Format("{0}:Планова:{1}\tФактична:{2}", characters[i], kvik_pl[i], kvik_fact[i]));
            }
            Console.ReadKey(false);

            //double D_pl = getD(1);

        }
        /// <summary>
        /// Страховий запас
        /// </summary>
        /// <param name="D">Фактична\планова денна потреба пiдприємства у матерiалi</param>
        /// <param name="T">Тривалiсть зриву поставки</param>
        /// <returns></returns>
        static double getZstr (double D, int T)
        {
            double result = D * T;
            Console.WriteLine("3стр=Д*Тз.п="+D+"*"+T+"="+result);
            return result;
        }
        /// <summary>
        /// фактична/планова денна потреба пiдприємства у певному матерiалi
        /// </summary>
        /// <param name="mTotal">Фактична\планова сумарна потреба пiдприємства в певному матерiалi у натуральних одиницях</param>
        /// <returns></returns>
        static double getD(double mTotal)
        {
            double result = mTotal / 360;
            Console.WriteLine("Д=Мс.ф/360=" + mTotal + "/360" + "=" + result);
            return result;
        }
        /// <summary>
        /// Сумарна потреба пiдприємства в певному матерiалi у натуральних одиницях
        /// </summary>
        /// <param name="N">обсяги випуску виробiв у натуральному вираженнi, вiдповiдно фактичний i запланований, шт</param>
        /// <param name="q">маси (площi) видiв заготовок, кг.</param>
        /// <returns></returns>
        static double getMTotal(double[] N,double[] q)
        {
            double nqSum = 0;
            for (int i = 0; i< N.Length; i++)
            {
                nqSum += N[i]*q[i];
            }
            Console.WriteLine(String.Format("Мс.ф.=Сумма(1:{0})Ni*qi={1}", N.Length,nqSum));
            return nqSum;
        }
        /// <summary>
        /// маса (площа) заготовки
        /// </summary>
        /// <param name="qClear">чиста маса (площа) виробу у натуральному вираженнi, кг (м2);</param>
        /// <param name="kM">коефiцiєнт використання матерiалу</param>
        /// <returns></returns>
        static double getQz(double qClear,double kM)
        {
            double result = qClear / kM;
            Console.WriteLine(String.Format("qзi=qч/Kм={0}/{1}={2}",qClear,kM,result));
            return result;
        }
        /// <summary>
        /// поточний запас матерiалу − для забезпечення безпере­бiйного процесу
        /// виробництва матерiальними ресурсами мiж двома черговими поставками
        /// </summary>
        /// <param name="Df">фактична денна потреба пiдприємства у певному матерiалi, кг</param>
        /// <param name="Tpost"> перiод поставки певного матерiалу</param>
        /// <returns></returns>
        static double getZpot(double Df, double Tpost)
        {
            double result = Df * Tpost;
            Console.WriteLine(String.Format("Зпот=Дф*Тпост={0}*{1}={2}", Df, Tpost, result));
            return result;
        }
        /// <summary>
        /// середнiй запас матерiалу
        /// </summary>
        /// <param name="Zpot">поточний запас</param>
        /// <param name="Zstr">страховий запас</param>
        /// <returns></returns>
        static double getZser( double Zpot, double Zstr)
        {
            double result = Zstr + 0.5 * Zpot;
            Console.WriteLine(String.Format("Зсер=Зстр+0.5*Зпот={0}+0.5*{1}={2}", Zstr, Zpot, result));
            return result;
        }
        /// <summary>
        /// максимальний запас матерiалу
        /// </summary>
        /// <param name="Zpot">поточний запас</param>
        /// <param name="Zstr">страховий запас</param>
        /// <returns></returns>
        static double getZMax(double Zpot, double Zstr)
        {
            double result = Zpot + Zstr;
            Console.WriteLine(String.Format("Змакс=Зпот+Зстр.ф={0}+{1}={2}", Zpot, Zstr, result));
            return result;
        }
        /// <summary>
        /// Загальна матерiалоємнiсть
        /// </summary>
        /// <param name="Mz">загальна сума матерiальних затрат</param>
        /// <param name="Qp">обсяг реалiзованої продукцiї пiдприємства</param>
        /// <returns></returns>
        static double getMe(double Mz, double Qp)
        {
            double result = Mz / Qp;
            Console.WriteLine(String.Format("Мє=Мз/Qр={0}/{1}={2}", Mz, Qp, result));
            return result;
        }
        /// <summary>
        /// загальна сума матерiальних затрат
        /// </summary>
        /// <param name="Mc">Сумарна потреба пiдприємства в певному матерiалi у натуральних одиницях</param>
        /// <param name="Bm">вартiсть одиницi матерiалу, грн</param>
        /// <returns></returns>
        static double getMz(double Mc, double Bm)
        {
            double result = Mc * Bm;
            Console.WriteLine(String.Format("Мз=Мс*Вм={0}/{1}={2}", Mc, Bm, result));
            return result;
        }
        /// <summary>
        /// матерiаловiддача
        /// </summary>
        /// <param name="Qp">обсяг реалiзованої продукцiї пiдприємств, грн</param>
        /// <param name="Mz">агальна сума матерiальних затрат</param>
        /// <returns></returns>
        static double getMv(double Qp, double Mz)
        {
            double result = Qp / Mz;
            Console.WriteLine(String.Format("Мв=Qр/Мз={0}/{1}={2}", Qp, Mz, result));
            return result;
        }
        /// <summary>
        /// Коефiцiєнт використання матерiалiв (не може перевищувати 1)
        /// </summary>
        /// <param name="N">обсяг випуску виробу у натуральному вираженнi</param>
        /// <param name="q_ch">чиста маса (площа) виробу у натуральному вираженнi, кг (м2)</param>
        /// <returns></returns>
        static double getKvikM(double[] N,double q_ch,double Mz)
        {
            double sum = 0;
            for (int i = 0; i < N.Length; i++)
            {
                sum += N[i] * q_ch;
            }
            double result = sum / Mz;
            Console.WriteLine(String.Format("Квик.матер=Сумма(1:{0})(Ni*qч/Мз)/Мс={1}/{2}={3}", N.Length, sum, Mz, result));
            return result;
        }
        /// <summary>
        /// коефiцiєнт оборотностi
        /// </summary>
        /// <param name="Qp">Обсяг реалiзованої продукцiї пiдприємства</param>
        /// <param name="Snoz">середньорiчний залишок нормованих оборотних за­собiв</param>
        /// <returns></returns>
        static double getKob(double Qp, double Snoz)
        {
            double result = Qp / Snoz;
            Console.WriteLine(String.Format("Коб=Qр/Sноз={0}/{1}={2}", Qp, Snoz, result));
            return result;
        }
        /// <summary>
        /// Обсяг реалiзованої продукцiї пiдприємства
        /// </summary>
        /// <param name="Cost">цiна виробiв</param>
        /// <param name="N">кiлькiсть виробiв</param>
        /// <returns></returns>
        static double getQp(double[] Cost, double[] N)
        {
            double sum = 0;
            for (int i = 0; i < Cost.Length; i++)
            {
                sum += Cost[i] * N[i];
            }
            Console.WriteLine(String.Format("сумма(1:{0})Цi*Ni={1}", Cost.Length, sum));
            return sum;

        }
        /// <summary>
        /// коефiцiєнт завантаження - Цей показник показує, скiльки оборотних коштiв 
        /// пiдприємства припадає на одну гривню реалiзованої продукцiї
        /// </summary>
        /// <param name="Snoz">середньорiчний залишок нормованих оборотних за­собiв</param>
        /// <param name="Qp">обсяг реалiзованої продукцiї</param>
        /// <returns></returns>
        static double getKzav(double Snoz,double Qp)
        {
            double result = Snoz / Qp;
            Console.WriteLine(String.Format("Kзав=Sноз/Qр={0}/{1}={2}", Snoz, Qp,result));
            return result;
        }
        /// <summary>
        /// тривалiсть одного обороту − показує тривалiсть одного обороту оборотних коштiв, у днях
        /// </summary>
        /// <param name="Kob">Коефiцiєнт оборотностi</param>
        /// <returns></returns>
        static double getTob (double Kob)
        {
            double result = 360 / Kob;
            Console.WriteLine(String.Format("Kзав=360/Koб=360/{0}={1}", Kob, result));
            return result;
        }
        /// <summary>
        /// Абсолютне вивiльнення (у грошових одиницях) оборотних засобiв
        /// </summary>
        /// <param name="Qpf">Обсяг реалiзованої продукцiї пiдприємства, фактичне</param>
        /// <param name="deltaScor">кiлькiсть днiв скорочення перiоду обороту оборотних засобiв</param>
        /// <returns></returns>
        static double getVOZA (double Qpf, double deltaScor)
        {
            double result = Qpf / 360 * deltaScor;
            Console.WriteLine(String.Format("ВОЗа=Qр.ф/360*дельта.скор={0}/360*{1}={2}", Qpf, deltaScor, result));
            return result;
        }
        /// <summary>
        /// кiлькiсть днiв скорочення перiоду обороту оборотних засобiв, днiв
        /// </summary>
        /// <param name="Tob_f">фактична тривалiсть обороту, днi</param>
        /// <param name="Tob_plan">запланована тривалiсть обороту</param>
        /// <returns></returns>
        static double getDeltaScor(double Tob_f, double Tob_plan)
        {
            double result = Math.Abs(Tob_f - Tob_plan);
            Console.WriteLine(String.Format("дельта.скор=Tоб.ф-Тоб.пл={0}-{1}={2}", Tob_f, Tob_plan, result));
            return result;
        }
        /// <summary>
        /// Вiдносне вивiльнення (у вiдсотках) оборотних засобiв
        /// </summary>
        /// <param name="Qp_pl">Обсяг реалiзованої продукцiї запланований</param>
        /// <param name="Qp_fact">Обсяг реалiзованої продукцiї фактичний</param>
        /// <returns></returns>
        static double getVOZV(double Qp_pl,double Qp_fact)
        {
            double result = 100 - Qp_pl / Qp_fact * 100;
            Console.WriteLine(String.Format("ВОЗа=100%-Qр.пл/Qр.ф.*100%=100%-{0}/{1}*100%={2}", Qp_pl, Qp_fact, result));
            return result;
        }
        /// <summary>
        /// Норма запасу оборотних фондiв у незавершеному виробництвi 
        /// </summary>
        /// <param name="Ci">повна собiвартiсть i-го виду виробу</param>
        /// <param name="N">рiчний обсяг випуску i-их видiв виробiв</param>
        /// <param name="Tc">тривалiсть циклу виготовлення i-их видiв виробiв</param>
        /// <param name="Knv">коефiцiєнт наростання витрат при виготовленнi i-го виду ви­робу</param>
        /// <returns></returns>
        static double getHnv(double Ci, double N, double Tc,double Knv)
        {
            double result = Ci * N * Tc * Knv / 360;
            Console.WriteLine(String.Format("Нн.в=(Ci*Ni*Tц*Kн.в)/360=({0}*{1}*{2}*{3})/360={4}", Ci, N, Tc, Knv, result));
            return result;
        }
        /// <summary>
        /// Коефiцiєнт наростання витрат
        /// </summary>
        /// <param name="Co">сума одноразових витрат у собiвартостi виробу, грн</param>
        /// <param name="Cn">сума поточних витрат у собiвартостi всiєї партiї виробiв</param>
        /// <returns></returns>
        static double getKnv(double Co, double Cn)
        {
            double result = (Co + 0.5 * Cn) / (Co + Cn);
            Console.WriteLine(String.Format("Кн.в=(Со+0.5*Cn)/(Co+Cn)=({0}+0.5*{1})/({0}+{1})={2}", Co, Cn,result));
            return result;
        }
        /// <summary>
        /// Повна собiвартiсть всiєї партiї продукцiї
        /// </summary>
        /// <param name="Co">сума одноразових витрат у собiвартостi виробу</param>
        /// <param name="Cn">сума поточних витрат у собiвартостi всiєї партiї виробiв</param>
        /// <returns></returns>
        static double getCvp(double Co,double Cn)
        {
            double result = Co + Cn;
            Console.WriteLine(String.Format("Св.п=Со+Сn={0}+{1}={2}", Co, Cn, result));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cod">Сод.i − собiвартiсть одиницi продукцiї i-го виду виробу, грн.</param>
        /// <param name="N"></param>
        /// <returns></returns>
        static double getCvp(double Cod, int N)
        {
            double result = Cod * N;
           
            return result;
        }
        /// <summary>
        /// сума поточних витрат у собiвартостi всiєї партiї виробiв
        /// </summary>
        /// <param name="Cvp"></param>
        /// <param name="Co"></param>
        /// <returns></returns>
        static double getCp(double Cvp, double Co)
        {
            double result = Cvp - Co;
            Console.WriteLine(String.Format("Сп=Св.п-Со={0}-{1}={2}", Cvp, Co, result));
            return result;
        }
    }
}
