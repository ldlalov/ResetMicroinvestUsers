﻿using System;
using System.Collections.Generic;

namespace ADTTest.Micro;

public partial class System
{
    public string Version { get; set; } = null!;

    public short? ProductId { get; set; }

    public DateTime? LastBackup { get; set; }
}
