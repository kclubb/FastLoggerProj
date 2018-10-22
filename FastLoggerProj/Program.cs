using System;
using KWClubb.FastLogger;

namespace FastLoggerProj
{
   class Program
   {
      static void Main(string[] args)
      {
         FastLogger.Init();

         FastLogger.Add("1T0001", "Hi there");

         FastLogger.Dump("test.log");
      }
   }
}
