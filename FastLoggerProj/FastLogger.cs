using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace KWClubb.FastLogger
{
   /// <summary>
   /// 
   /// </summary>
   public static class FastLogger
   {
      private static ConcurrentQueue<string> Log = new ConcurrentQueue<string>();
      /// <summary>
      /// 
      /// </summary>
      /// <param name="client"></param>
      /// <param name="file"></param>
      /// <param name="linenu"></param>
      /// <param name="msg"></param>
      /// <param name="function"></param>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
      public static void Add(string client, string msg, [CallerFilePath] string file = null, [CallerLineNumber] int linenu = 0, [CallerMemberName] string function = null)
      {
         string time = DateTime.Now.ToString("HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
         int t1, t2, t3, t4;
         System.Threading.ThreadPool.GetAvailableThreads(out t1, out t2);
         System.Threading.ThreadPool.GetMaxThreads(out t3, out t4);
         int t0 = System.Threading.Thread.CurrentThread.ManagedThreadId;
         string fullMsg = String.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}|{4}({5})|{6}|{7}/{8},{9}/{10}", time, client, function, msg, System.IO.Path.GetFileName(file), linenu, t0, t1, t3, t2, t4);
         Log.Enqueue(fullMsg);
      }
      /// <summary>
      /// 
      /// </summary>
      public static void Init()
      {
         Log = new ConcurrentQueue<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="filename"></param>
      public static void Dump(string filename)
      {
         var f = System.IO.File.CreateText(filename);
         f.AutoFlush = true;
         string s;

         while (Log.TryDequeue(out s))
         {
            f.WriteLine(s);
         }
         f.Close();
      }
   }
}
