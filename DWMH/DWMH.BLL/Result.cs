using System;
using System.Collections.Generic;

namespace DWMH.BLL
{
    public class Result<T>
    {
        public bool Success => _messages.Count == 0;
        public List<string> Messages => new List<string>(_messages);
        public T Value { get; set; }
        private List<string> _messages = new List<string>();

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
