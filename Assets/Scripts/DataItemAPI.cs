using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataItemAPI
{
    public List<EntryAPI> entries;
    [Serializable]
    public class EntryAPI : IComparable
    {
        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
        
        public int CompareTo(object obj)
        {
            var otherEntry = (EntryAPI) obj; 
            int result = String.Compare(domain, otherEntry.domain, StringComparison.Ordinal);
            if (result == 0)
                result = String.Compare(cluster, otherEntry.cluster, StringComparison.Ordinal);
            if (result == 0)
                result = String.Compare(standarddescription, otherEntry.standarddescription, StringComparison.Ordinal);
            return result;
        }
    }
}
