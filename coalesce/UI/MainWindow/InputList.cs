﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceInputPlugin;
using CoalesceOutputPlugin;

namespace coalesce.UI.MainWindow
{
    public class InputList : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public Guid Id { get; set; }
        public ICoalesceInputPlugin Plugin { get; set; }
        public Guid InstanceId { get; set; }
    }

    public class OutputList : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public Guid Id { get; set; }
        public ICoalesceOutputPlugin Plugin { get; set; }
        public Guid InstanceId { get; set; }
    }
}
