﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class SyncEntity
    {
        public Guid Id { get; set; }

        public DateTime LastChangedAt { get; set; }

        public string JsonData { get; set; }

        public string SyncType { get; set; }

        public string ObjectType { get; set; }

        public string Origin { get; set; }
    }
}
