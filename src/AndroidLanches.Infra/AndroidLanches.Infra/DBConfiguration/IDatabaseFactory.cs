﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AndroidLanches.Infra.DBConfiguration
{
    public interface IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
