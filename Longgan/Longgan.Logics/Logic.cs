using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.Logics
{

    public class Logic : IDisposable
    {
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
        private string _userAgent = null;
        public string UserAgent
        {
            get
            {

                if (_userAgent == null)
                {
                    _userAgent = (string)CallContext.GetData("UserAgent") ?? string.Empty;
                }
                return _userAgent;
            }

        }
        #region IDisposable Implementation

        private List<IDisposable> _disposables = null;
        public List<IDisposable> Disposables
        {
            get
            {
                if (_disposables == null) _disposables = new List<IDisposable>();
                return _disposables;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_disposables != null)
                    {
                        foreach (IDisposable disposable in _disposables)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Logic()
        {
            Dispose(false);
        }

        #endregion
    }
}
