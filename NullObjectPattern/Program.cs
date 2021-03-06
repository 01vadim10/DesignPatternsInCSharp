﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NullObjectPattern
{
    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    public class NullLog : ILog
    {
        private int i;
        public int RecordLimit
        {
            get
            {
                return i + 1;
            }
        }

        public int RecordCount
        {
            get
            {
                return i++;
            }
            set
            {

            }
        }

        public void LogInfo(string message)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingleCallTest()
        {
            var a = new Account(new NullLog());
            a.SomeOperation();
        }

        [Test]
        public void ManyCallsTest()
        {
            var a = new Account(new NullLog());
            for (int i = 0; i < 100; ++i)
                a.SomeOperation();
        }
    }
}
