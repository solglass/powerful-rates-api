﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventContracts
{
    public class EmailMessage
    {
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailMessage()
        {
            ToName = "";
        }

    }
}
