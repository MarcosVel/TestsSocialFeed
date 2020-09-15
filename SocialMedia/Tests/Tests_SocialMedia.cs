using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using System.Net.Mime;

namespace Tests {
    /*
     USE CASES:
    User Creates Posts (content)
    User Follows User (social graph)
    User Views Feed (Posts by Followed Users)
    User Likes Post (social graph)
    User Shares Post (social graph)
     */


    [TestClass]
    public class Tests_SocialMedia {
        [TestMethod]
        public void Test_001() {
            var user = new User(1);
            var post1 = new Post("One");
            var post2 = new Post("Two");

            var content = new Content();
            content.Publish(user, post1);
            content.Publish(user, post2);

            Check.That(content.PublishPosts.Keys).CountIs(1);
            Check.That(content.PublishPosts.Keys).Contains(user);
            Check.That(content.PublishPosts[user]).Contains(post1, post2);
        }

        [TestMethod]
        public void Test_002() {
            var u1 = new User(1);
            var u2 = new User(2);
            var u3 = new User(3);
            var u4 = new User(4);
            var u5 = new User(5);

            var graph = new SocialGraph();
            graph.Follows(u1, u2);
            graph.Follows(u1, u3);
            graph.Follows(u1, u4);
            graph.Follows(u1, u5);

            Check.That(graph.Follow.Keys).CountIs(1);
            Check.That(graph.Follow.Keys).Contains(u1);
            Check.That(graph.Follow[u1]).Contains(u2, u3, u4, u5);

        }

        [TestMethod]
        public void Test_003() {
            var u1 = new User(1);
            var u2 = new User(2);
            var u3 = new User(3);
            var u4 = new User(4);
            var u5 = new User(5);

            var graph = new SocialGraph();
            graph.Follows(u1, u2);
            graph.Follows(u1, u3);
            graph.Follows(u1, u4);
            graph.Follows(u1, u5);

            var post1 = new Post("One");
            var post2 = new Post("Two");

            var content = new Content();
            content.Publish(u2, post1);
            content.Publish(u3, post2);

            var follows = graph.GetFollows(u1);
            var feed = content.GetFeed(follows);

            Check.That(feed).CountIs(2);
        }

        [TestMethod]
        public void Test_004() {
            var u1 = new User(1);
            var u2 = new User(2);
            var u3 = new User(3);
            var u4 = new User(4);
            var u5 = new User(5);

            var graph = new SocialGraph();
            graph.Follows(u1, u2);
            graph.Follows(u1, u3);
            graph.Follows(u1, u4);
            graph.Follows(u1, u5);

            var post1 = new Post("One");
            var post2 = new Post("Two");

            var content = new Content();
            content.Publish(u2, post1);
            content.Publish(u3, post2);

            content.Like(u1, post1);
            content.Share(u1, post1);

            Check.That(content.LikedPosts.Keys).CountIs(1);
            Check.That(content.LikedPosts.Keys).Contains(u1);
            Check.That(content.LikedPosts[u1]).Contains(post1);

            Check.That(content.SharedPosts.Keys).CountIs(1);
            Check.That(content.SharedPosts.Keys).Contains(u1);
            Check.That(content.SharedPosts[u1]).Contains(post1);
        }

        [TestMethod]
        public void Test_005() {
            var u1 = new User(1);
            var u2 = new User(2);
            var u3 = new User(3);
            var u4 = new User(4);
            var u5 = new User(5);

            var graph = new SocialGraph();
            graph.Follows(u1, u2);
            graph.Follows(u1, u3);
            graph.Follows(u1, u4);
            graph.Follows(u1, u5);

            var post1 = new Post("One");
            var post2 = new Post("Two");

            var content = new Content();
            content.Publish(u2, post1);
            content.Publish(u3, post2);

            content.Like(u4, post1);
            content.Share(u5, post2);

            var follows = graph.GetFollows(u1);
            var feed = content.GetFeed(follows);

            Check.That(feed).CountIs(2);
        }

    }
}
