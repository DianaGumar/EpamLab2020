using System;
////using System.Collections.Generic;
////using System.Linq;
////using FluentAssertions;
////using NUnit.Framework;
using TicketManagement.DataAccess;
////using TicketManagement.DataAccess.DAL;
////using TicketManagement.DataAccess.Entities;

namespace TicketManagement.IntegrationTests
{
    public class TMEventServiseTests : IDisposable
    {
        private readonly TMContext _context = new TMContext();
        private bool _isDisposed;

        public void ByeTicketTest()
        {
            throw new NotImplementedException();
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // free managed resources
                _context.Dispose();
            }

            _isDisposed = true;
        }
    }
}
