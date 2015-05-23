using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess
{
    public class Repository : IDisposable
    {
        // Init Context only when it is accessed for the 1st time.
        private DatabaseContext _context = null;
        internal DatabaseContext Context
        {
            get
            {
                if (_context == null) _context = new DatabaseContext();
                return _context;
            }
        }

        private string _currentPersonId = null;
        public string CurrentPersonId
        {
            get
            {
                if (_currentPersonId == null)
                {
                    _currentPersonId = (string)CallContext.GetData("PersonId") ?? string.Empty;

                }
                return _currentPersonId;
            }
        }

        private string _currentProfileId = null;
        public string CurrentProfileId
        {
            get
            {
                if (_currentProfileId == null)
                {
                    _currentProfileId = (string)CallContext.GetData("ProfileId") ?? string.Empty;
                }
                return _currentProfileId;
            }
        }

        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_context != null) _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion
    }
}
