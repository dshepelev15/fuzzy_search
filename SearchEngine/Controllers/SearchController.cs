using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using SearchAlgorithm;
using SearchEngine.Models;

namespace SearchEngine.Controllers
{
    public class SearchController : Controller
    {
        public JsonResult Index(string query)
        {
            RecordsRepository repo = new RecordsRepository();
            var records = repo.GetAllRecords();
            
            var result = new List<Info>();
            Stopwatch stop = new Stopwatch();
            stop.Start();
            foreach (var record in records)
            {
                var str = record.Name + " " + record.Abbr;
                int[] diff = Search.DoSearch(query, str);
                result.Add(new Info() {Dif = diff, Value = str});
            }

            result.Sort();
            stop.Stop();
            var output = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                output.Add(result[i].Value + " " + result[i].Dif[0] + " " + result[i].Dif[1]);
            }
            output.Add(String.Format("Время поиска {0}", stop.ElapsedMilliseconds));
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        
    }

    class Info : IComparable
    {
        public int[] Dif;
        public string Value;
        public int CompareTo(object obj)
        {
            var o = obj as Info;
            if (o != null)
            {
                if (Dif[0] == o.Dif[0])
                {
                    return o.Dif[1] - Dif[1];
                }
                return Dif[0] - o.Dif[0];
            }
            return 0;
        }
    }
}