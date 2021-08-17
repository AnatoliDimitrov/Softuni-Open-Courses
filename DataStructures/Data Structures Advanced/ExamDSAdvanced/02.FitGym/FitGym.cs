using System.Linq;

namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;

    public class FitGym : IGym
    {
        Dictionary<int ,Member> members = new Dictionary<int, Member>();
        Dictionary<int, Trainer> trainers = new Dictionary<int, Trainer>();
        Dictionary<Trainer, HashSet<Member>> trainersCollections = new Dictionary<Trainer, HashSet<Member>>();


        public void AddMember(Member member)
        {
            if (members.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }

            members.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainers.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            trainers.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!members.ContainsKey(member.Id))
            {
                members.Add(member.Id, member);
            }

            if (!trainers.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            if (member.Trainer != null)
            {
                throw new ArgumentException();
            }

            member.Trainer = trainer;

            if (!trainersCollections.ContainsKey(trainer))
            {
                trainersCollections[trainer] = new HashSet<Member>();
            }

            trainersCollections[trainer].Add(member);
        }

        public bool Contains(Member member)
        {
            return members.ContainsKey(member.Id);
        }

        public bool Contains(Trainer trainer)
        {
            return trainers.ContainsKey(trainer.Id);
        }

        public Trainer FireTrainer(int id)
        {
            if (!trainers.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = trainers[id];

            this.trainers.Remove(id);

            if (trainersCollections.ContainsKey(trainer))
            {
            this.trainersCollections.Remove(trainer);
            }

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!members.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = members[id];

            members.Remove(id);

            if (member.Trainer != null )
            {
                trainersCollections[member.Trainer].Remove(member);
            }

            return member;
        }

        public int MemberCount => members.Count;
        public int TrainerCount => trainers.Count;

        public IEnumerable<Member> 
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return members.Values
                .OrderBy(m => m.RegistrationDate)
                .ThenByDescending(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return trainers.Values
                .OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member> 
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            if (!trainersCollections.ContainsKey(trainer))
            {
                return new List<Member>();
            }

            return trainersCollections[trainer]
                .OrderBy(m => m.RegistrationDate)
                .ThenBy(m => m.Name);
        }

        public IEnumerable<Member> 
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {

            return members.Values
                .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
                .OrderBy(m => m.Visits)
                .ThenBy(m => m.Name);
        }

        public Dictionary<Trainer, HashSet<Member>> 
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return trainersCollections
                .OrderBy(k => k.Value.Count)
                .ThenBy(k => k.Key.Popularity)
                .ToDictionary(x => x.Key, v => v.Value);
        }
    }
}