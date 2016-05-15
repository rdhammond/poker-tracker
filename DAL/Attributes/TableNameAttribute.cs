﻿using System;

namespace PokerTracker.DAL.Attributes
{
    public class TableNameAttribute : Attribute
    {
        private readonly string _name;

        public string Name
        {
            get { return _name; }
        }

        public TableNameAttribute(string name)
        {
            _name = name;
        }
    }
}
