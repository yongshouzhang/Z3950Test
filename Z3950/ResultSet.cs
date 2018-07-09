using System;
using System.Collections;
using System.Collections.Generic;

namespace Z3950
{
    public class ResultSet :IDisposable, IEnumerable, IEnumerable<Record>
    {
        internal ResultSet(IntPtr resultSet, Connection connection)
        {
            _resultSet = resultSet;
            _size = Yaz.ZOOM_resultset_size(_resultSet);

            if (0 == _size)
                Console.Out.WriteLine("Yaz.ZOOM_resultset_size zero");

            _records = new Record[_size];
        }

        ResultSetOptionsCollection Options { get { return new ResultSetOptionsCollection(_resultSet); } }


       public  Record this[uint index]
        {
            get
            {
                if (_records[index] == null)
                {
                    var record = Yaz.ZOOM_resultset_record(_resultSet, index);
                    _records[index] = new Record(record, this);
                }
                return _records[index];
            }
        }

        public  Record this[int index]
        {
            get
            {
                return this[(uint)index];
            }
        }

        public  uint Size { get { return _size; } }

        private IntPtr _resultSet;
        private readonly uint _size;
        private readonly Record[] _records;

        private bool _disposed = false;

        void IDisposable.Dispose()
        {
            if (!_disposed)
            {
                foreach (Record record in _records)
                    record.Dispose();

                Yaz.ZOOM_resultset_destroy(_resultSet);
                _resultSet = IntPtr.Zero;
                _disposed = true;
            }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
       
        public  IEnumerator<Record> GetEnumerator()
        {
            return new ResultSetEnumerator(this);
        }
        

        #endregion
    }
}