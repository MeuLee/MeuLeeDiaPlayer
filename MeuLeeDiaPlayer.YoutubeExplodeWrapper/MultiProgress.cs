﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    public class MultiProgress
    {
        private readonly object _lockObject = new object();
        private readonly Dictionary<string, double> progressPair = new();
        private readonly IProgress<double> _parentProgress;
        private readonly int _tasksNumber;
        private double _totalProgress => progressPair.Values.Sum() / _tasksNumber;

        public MultiProgress(IProgress<double> parentProgress, int tasksNumber)
            => (_parentProgress, _tasksNumber) = (parentProgress, tasksNumber);

        public void Add(string key)
        {
            lock (_lockObject)
            {
                progressPair.Add(key, 0);
            }            
        }

        public void Update(string key, double value)
        {
            if (progressPair[key] >= value) return;
            lock (_lockObject)
            {
                progressPair[key] = value;
            }
            _parentProgress?.Report(_totalProgress);
        }
    }
}
