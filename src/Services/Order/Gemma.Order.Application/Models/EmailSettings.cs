﻿namespace Gemma.Order.Application.Models
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string CompanyAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}