using System;
using System.Linq;
using System.Collections.Generic;

namespace Tests {
    internal class SocialGraph {

        public Dictionary<User, List<User>> Follow { get; set; } = new Dictionary<User, List<User>>();
        internal void Follows(User u1, User u2) {
            if(Follow.ContainsKey(u1)) {
                Follow[u1].Add(u2);
            }
            else {
                Follow.Add(u1, new List<User>() { u2 });
            }
        }

        internal List<User> GetFollows(User u1) {
            if (Follow.ContainsKey(u1)) {
                return Follow[u1];
            }
            else {
                return new List<User>();
            }
        }
    }
}