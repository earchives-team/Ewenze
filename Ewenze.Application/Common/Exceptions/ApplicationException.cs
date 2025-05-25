﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Common.Exceptions
{

    public interface IAppExceptionWithReason
    {
        Enum GetReason();
    }

    public class ApplicationException<TReason> : Exception, IAppExceptionWithReason where TReason : Enum
    {
        private const string ReasonKey = "AppExceptionReason";

        public TReason Reason
        {
            get
            {
                if (Data.Contains(ReasonKey) && Data[ReasonKey] is TReason reason)
                {
                    return reason;
                }
                return default;
            }

            set => Data[ReasonKey] = value;
        }

        public string? InvalidProperty { get; set; }
        public ApplicationException() { }

        public ApplicationException(string? message) : base(message) { }

        public ApplicationException(string message, Exception innerException) : base(message, innerException) { }

        public Enum GetReason()
        {
           return Reason;
        }
    }
}
