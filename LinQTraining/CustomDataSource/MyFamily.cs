using System.Collections;

namespace LinQTraining.CustomDataSource
{
    public class MyFamily : IEnumerable<FamilyMember>
    {
        private readonly FamilyMember _mom;
        private readonly FamilyMember _dad;
        private readonly FamilyMember _son;
        private readonly FamilyMember _daughter;

        public MyFamily()
        {
            _mom = new FamilyMember { Age = 40, Name = "Mom" };
            _dad = new FamilyMember { Age = 43, Name = "Dad" };
            _son = new FamilyMember { Age = 10, Name = "Son" };
            _daughter = new FamilyMember { Age = 15, Name = "Daughter" };
        }

        public IEnumerator<FamilyMember> GetEnumerator()
        {
            return new MyFamilyEnumerator(_mom, _dad, _son, _daughter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    class MyFamilyEnumerator : IEnumerator<FamilyMember>
    {
        private int _currentIdx;
        private readonly FamilyMember[] _members;

        public FamilyMember Current => _members[_currentIdx];

        object IEnumerator.Current => _members[_currentIdx];

        public MyFamilyEnumerator(params FamilyMember[] members)
        {
            _members = members;
            _currentIdx = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_currentIdx + 1 < _members.Length)
            {
                _currentIdx += 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _currentIdx = -1;
        }
    }
}
