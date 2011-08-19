﻿using System;
using System.Collections.Generic;

namespace Cassette
{
    public interface IModuleCache<T>
        where T : Module
    {
        bool IsUpToDate(DateTime dateTime, string version, IFileSystem sourceFileSystem);
        IEnumerable<T> LoadModules();
        void SaveModuleContainer(IModuleContainer<T> moduleContainer, string version);
    }
}