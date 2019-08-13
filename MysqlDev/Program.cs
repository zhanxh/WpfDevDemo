using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlDev
{
    class Program
    {
        static string conStr = "server=47.99.211.21;port=3306;database=teliapp;user=teli;password=Tljk@1314;";
        static string qryStr = "select id,pid,name,tpid from area_infos;";
        static List<AreaData> datas = new List<AreaData>();
        static void Main(string[] args)
        {
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();

            using (MySqlCommand mys = new MySqlCommand(qryStr, con))
            {
                var reader = mys.ExecuteReader();
                while (reader.Read())
                {
                    AreaData ad = new AreaData();
                    ad.id = (int)reader[0];
                    ad.pid = (int)reader[1];
                    ad.name = reader[2].ToString();
                    ad.tpid = (int)reader[3];
                    Console.WriteLine(ad.ToString());
                    datas.Add(ad);
                }
            }

            List<object> objs = new List<object>();
            var tps = datas.GroupBy(p => new { p.tpid }).Select(p=> p.Key.tpid).ToList();
            //foreach(var tp in tps)
            //{
            //    var dtx = datas.Where(p => p.tpid == tp).ToList();
            //    objs.Add(dtx);
            //}

            //省
            
            var pros = datas.Where(p => p.tpid == 1).OrderBy(p => p.id).ToList();

            #region iii
            int idx = 0;
            int idxcity = 0;
            int idxtown = 0;
            List<AreaData> nPros = new List<AreaData>();
            foreach (var pro in pros)
            {
                idx++;
                var oldIdx = pro.id;

                //市 
                //using (MySqlTransaction txcity = con.BeginTransaction())
                {
                    int idxcityseq = 0;
                    string sqlcity = "replace into area_city(id, pid, name, seq) values(@id, @pid, @name, @seq);";
                    var citys = datas.Where(p => p.pid == pro.id && p.tpid == 2).OrderBy(p => p.id).ToList();
                    foreach (var city in citys)
                    {
                        idxcity++;
                        idxcityseq++;
                        //MySqlCommand cmdcity = new MySqlCommand(sqlcity, con, txcity);
                        //cmdcity.Parameters.AddWithValue("@id", idxcity);
                        //cmdcity.Parameters.AddWithValue("@pid", idx);
                        //cmdcity.Parameters.AddWithValue("@name", city.name);
                        //cmdcity.Parameters.AddWithValue("@seq", idxcityseq);
                        //cmdcity.ExecuteNonQuery();

                        //区、县
                        using (MySqlTransaction txtown = con.BeginTransaction())
                        {
                            int idxtownseq = 0;
                            string sqltown = "replace into area_town(id, pid, name, seq) values(@id, @pid, @name, @seq);";
                            var towns = datas.Where(p => p.pid == city.id && p.tpid == 3).OrderBy(p => p.id).ToList();
                            foreach (var town in towns)
                            {
                                idxtown++;
                                idxtownseq++;
                                MySqlCommand cmd = new MySqlCommand(sqltown, con, txtown);
                                cmd.Parameters.AddWithValue("@id", idxtown);
                                cmd.Parameters.AddWithValue("@pid", idxcity);
                                cmd.Parameters.AddWithValue("@name", town.name);
                                cmd.Parameters.AddWithValue("@seq", idxtownseq);
                                cmd.ExecuteNonQuery();
                            }
                            txtown.Commit();
                        }

                    }
                    //txcity.Commit();
                }
            }
            Console.WriteLine(string.Format("province:{0},city:{1},town:{2}", idx, idxcity, idxtown));

            #endregion

            #region 省入库
            //try
            //{
            //    using (MySqlTransaction tx = con.BeginTransaction())
            //    {
            //        foreach (var pro in pros)
            //        {
            //            idx++;
            //            string proStr = "replace into area_province(id, name, seq) values(@id, @name, @seq);";
            //            MySqlCommand cmd = new MySqlCommand(proStr, con, tx);
            //            cmd.Parameters.AddWithValue("@id", idx);
            //            cmd.Parameters.AddWithValue("@name", pro.name);
            //            cmd.Parameters.AddWithValue("@seq", idx);
            //            cmd.ExecuteNonQuery();
            //        }
            //        tx.Commit();
            //    }
            //}catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion

            con.Close();
            Console.ReadKey();
        }

        static void Connect()
        {
            
        }
    }

    public class AreaData
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string name { get; set; }
        public int tpid { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", id, pid, name, tpid);
        }
    }
}
