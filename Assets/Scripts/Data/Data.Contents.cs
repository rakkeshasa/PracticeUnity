using System.Collections.Generic;
using System;
using UnityEngine;

namespace Data
{
    #region Stat
    // �޸𸮻󿡼� ��� �ִ� ���� ���Ϸ� ��ȯ�� �� �ִ�..?
    [Serializable]
    public class Stat
    {
        // [SerializeField] // public�� ���̱� ������ �̰� ���

        // �̸��� json���ϰ� �����ؾ���
        // public���� ���� ������ �Ľ������� ����
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        // �ش� ����Ʈ�� �̸��� json�� ��Ʈ �̸��� ���ƾ��Ѵ�.
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