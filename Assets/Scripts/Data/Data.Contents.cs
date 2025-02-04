using System.Collections.Generic;
using System;
using UnityEngine;

namespace Data
{
    #region Stat
    // 메모리상에서 들고 있는 것을 파일로 변환할 수 있다..?
    [Serializable]
    public class Stat
    {
        // [SerializeField] // public을 붙이기 싫으면 이거 사용

        // 이름을 json파일과 통일해야함
        // public으로 하지 않으면 파싱해주지 못함
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        // 해당 리스트의 이름은 json의 루트 이름과 같아야한다.
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}