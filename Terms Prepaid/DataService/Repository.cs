using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace DataService
{
    public class Repository
    {
        public readonly SimpleIoc IoC;
        private static Repository _instance;

        private Repository()
        {
            IoC = new SimpleIoc();
        }

        public static Repository Instance()
        {
            return _instance ?? (_instance = new Repository());
        }

        public static void Register<TClass>(Func<TClass> func) where TClass : class 
        {
            Instance().IoC.Register<TClass>(func);
        }

        public static void Register<TClass>(Func<TClass> func, string key) where TClass : class
        {
            Instance().IoC.Register<TClass>(func, key);
        }

        public static TClass GetInstance<TClass>() where TClass : class
        {
            return Instance().IoC.GetInstance<TClass>();
        }

        public static TClass GetInstance<TClass>(string key) where TClass : class
        {
            return Instance().IoC.GetInstance<TClass>(key);
        }
    }
}
