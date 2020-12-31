using System.Collections;
using System.Collections.Generic;

using TypeValuePair = System.Collections.Generic.KeyValuePair<TournamentsEnhanced.TournamentType, uint>;

namespace TournamentsEnhanced
{
  public class TournamentTypeComparer : IComparer<TournamentType>
  {
    public int Compare(TournamentType x, TournamentType y)
    {
      throw new System.NotImplementedException();
    }

    private class TypeValueLookupDictionary : IDictionary<TournamentType, uint>
    {
      private
      public uint this[TournamentType key]
      { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

      public ICollection<TournamentType> Keys => throw new System.NotImplementedException();

      public ICollection<uint> Values => throw new System.NotImplementedException();

      public int Count => throw new System.NotImplementedException();

      public bool IsReadOnly => throw new System.NotImplementedException();

      public void Add(TournamentType key, uint value)
      {
        throw new System.NotImplementedException();
      }

      public void Add(TypeValuePair item)
      {
        throw new System.NotImplementedException();
      }

      public void Clear()
      {
        throw new System.NotImplementedException();
      }

      public bool Contains(TypeValuePair item)
      {
        throw new System.NotImplementedException();
      }

      public bool ContainsKey(TournamentType key)
      {
        throw new System.NotImplementedException();
      }

      public void CopyTo(TypeValuePair[] array, int arrayIndex)
      {
        throw new System.NotImplementedException();
      }

      public IEnumerator<TypeValuePair> GetEnumerator()
      {
        throw new System.NotImplementedException();
      }

      public bool Remove(TournamentType key)
      {
        throw new System.NotImplementedException();
      }

      public bool Remove(TypeValuePair item)
      {
        throw new System.NotImplementedException();
      }

      public bool TryGetValue(TournamentType key, out uint value)
      {
        throw new System.NotImplementedException();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        throw new System.NotImplementedException();
      }
    }
  }
}