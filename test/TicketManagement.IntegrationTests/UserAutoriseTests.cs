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
    public class UserAutoriseTests : IDisposable
    {
        private readonly TMContext _context = new TMContext("DefaultConnection");
        private bool _isDisposed;

        public void AutorizeTMEventManagerPositive()
        {
            throw new NotImplementedException();
        }

        public void AutorizeTMEventManagerNegative()
        {
            throw new NotImplementedException();
        }

        public void AutorizeAutorizedUserNegative()
        {
            throw new NotImplementedException();
        }

        public void AutorizeAutorizedUserPositive()
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
