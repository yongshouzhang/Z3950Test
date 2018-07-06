using System.Collections;
using System.Collections.Generic;

namespace Z3950
{
    public class ResultSetEnumerator :IEnumerator<Record>,IEnumerator
    {
        internal ResultSetEnumerator(ResultSet resultSet)
        {
            _resultSet = resultSet;
        }

        private readonly ResultSet _resultSet;

        #region IEnumerator Members

        public void Reset()
        {
            _position = 0;
        }
        
       object IEnumerator.Current
        {
            get
            {
                return ((ResultSet)_resultSet)[_position];
            }
        }

        public Record Current
        {
            get
            {
                return ((ResultSet)_resultSet)[_position];
            }
        }

        public bool MoveNext()
        {
            _position++;

            if (_position < ((ResultSet)_resultSet).Size)
            {
                return true;
            }
            else
            {
                _position--;
                return false;
            }
        }

        private uint _position = uint.MaxValue;
     
        public void Dispose()
        {
            //_resultSet.Clear();
        }
        #endregion
    }
}